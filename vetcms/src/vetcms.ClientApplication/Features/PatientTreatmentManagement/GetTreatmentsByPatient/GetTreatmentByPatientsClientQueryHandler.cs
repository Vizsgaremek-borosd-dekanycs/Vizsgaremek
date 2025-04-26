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
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ClientApplication.Features.PatientManagement.GetPatient
{
    internal class GetTreatmentByPatientsClientQueryHandler(IMediator mediator, IDialogService dialogService, IMapper mapper) 
        : IRequestHandler<GetTreatmentByPatientsClientQuery, GetTreatmentByPatientsClientQueryResponse>
    {
        public async Task<GetTreatmentByPatientsClientQueryResponse> Handle(GetTreatmentByPatientsClientQuery request, CancellationToken cancellationToken)
        {
            GetPatientTreatmentsByPatientIdApiQuery query = mapper.Map<GetPatientTreatmentsByPatientIdApiQuery>(request);
            GetPatientTreatmentsByPatientIdApiQueryResponse response = await mediator.Send(query);
            GetTreatmentByPatientsClientQueryResponse result = mapper.Map<GetTreatmentByPatientsClientQueryResponse>(response);
            if (result.Treatments == null)
            {
                dialogService.ShowError("Patient not found.");
                return result;
            }
            return await Task.FromResult(result);
        }
    }

    internal class GetPatientTreatmentsByPatientIdApiQueryHandler : GenericApiCommandHandler<GetPatientTreatmentsByPatientIdApiQuery, GetPatientTreatmentsByPatientIdApiQueryResponse>
    {
        public GetPatientTreatmentsByPatientIdApiQueryHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
