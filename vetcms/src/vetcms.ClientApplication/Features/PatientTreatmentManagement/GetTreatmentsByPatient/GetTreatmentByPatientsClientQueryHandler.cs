using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.ClientApplication.Features.IAM.GetUser;
using vetcms.ClientApplication.Features.PatientClassification.ListAnimalBreed;
using vetcms.ClientApplication.Features.PatientManagement.ListPatients;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Features.PatientManagement.GetPatient
{
    internal class GetTreatmentByPatientsClientQueryHandler(IMediator mediator, IDialogService dialogService, IMapper mapper) 
        : IRequestHandler<GetTreatmentByPatientsClientQuery, GetTreatmentByPatientsClientQueryResponse>
    {
        public async Task<GetTreatmentByPatientsClientQueryResponse> Handle(GetTreatmentByPatientsClientQuery request, CancellationToken cancellationToken)
        {
            GetTreatmentByPatientsClientQueryResponse getTreatmentsResponse = new GetTreatmentByPatientsClientQueryResponse();
            getTreatmentsResponse.Treatments = ListPatientTreatmentClientQueryHandler.treatments.Where(x => x.PatientId == request.PatientId).ToList();
            if (getTreatmentsResponse.Treatments == null)
            {
                dialogService.ShowError("Patient not found.");
                return getTreatmentsResponse;
            }
            return await Task.FromResult(getTreatmentsResponse);
        }
    }

    //internal class GetPatientByIdApiQueryHandler : GenericApiCommandHandler<GetAnimalBreedByIdApiQuery, GetAnimalBreedByIdApiQueryResponse>
    //{
    //    public GetPatientByIdApiQueryHandler(IServiceScopeFactory serviceScopeFactory)
    //        : base(serviceScopeFactory)
    //    {
    //    }
    //}
}
