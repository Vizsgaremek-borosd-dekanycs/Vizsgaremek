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
    internal class GetTreatmentByIdClientQueryHandler(IMediator mediator, IDialogService dialogService, IMapper mapper) : IRequestHandler<GetTreatmentByIdClientQuery, GetTreatmentByIdClientQueryResponse>
    {
        public async Task<GetTreatmentByIdClientQueryResponse> Handle(GetTreatmentByIdClientQuery request, CancellationToken cancellationToken)
        {
            GetTreatmentByIdClientQueryResponse getTreatmentResponse = new GetTreatmentByIdClientQueryResponse();
            getTreatmentResponse.TreatmentModel = ListPatientTreatmentClientQueryHandler.treatments.FirstOrDefault(x => x.Id == request.TreatmentId);
            if (getTreatmentResponse.TreatmentModel == null)
            {
                dialogService.ShowError("Treatment not found.");
                return getTreatmentResponse;
            }
            return await Task.FromResult(getTreatmentResponse);
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
