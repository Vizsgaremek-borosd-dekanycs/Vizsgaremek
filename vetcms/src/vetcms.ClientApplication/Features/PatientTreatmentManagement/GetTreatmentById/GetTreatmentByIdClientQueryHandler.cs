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
    internal class GetTreatmentByIdClientQueryHandler(IMediator mediator, IDialogService dialogService, IMapper mapper) : IRequestHandler<GetTreatmentByIdClientQuery, GetTreatmentByIdClientQueryResponse>
    {
        public async Task<GetTreatmentByIdClientQueryResponse> Handle(GetTreatmentByIdClientQuery request, CancellationToken cancellationToken)
        {
            GetPaitentTreatmentApiQuery query = mapper.Map<GetPaitentTreatmentApiQuery>(request);
            GetPaitentTreatmentApiQueryResponse response = await mediator.Send(query);

            if(response.Success)
            {
                return mapper.Map<GetTreatmentByIdClientQueryResponse>(response);
            }
            else
            {
                dialogService.ShowError(response.Message, "Hiba");
                return new GetTreatmentByIdClientQueryResponse();
            }
        }
    }

    internal class GetPaitentTreatmentApiQueryHandler : GenericApiCommandHandler<GetPaitentTreatmentApiQuery, GetPaitentTreatmentApiQueryResponse>
    {
        public GetPaitentTreatmentApiQueryHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
