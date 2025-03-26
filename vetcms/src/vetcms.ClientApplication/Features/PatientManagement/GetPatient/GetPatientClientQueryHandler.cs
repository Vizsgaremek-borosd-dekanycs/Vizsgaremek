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
    internal class GetPatientClientQueryHandler(IMediator mediator, IDialogService dialogService, IMapper mapper) : IRequestHandler<GetPatientClientQuery, GetPatientClientQueryResponse>
    {
        public async Task<GetPatientClientQueryResponse> Handle(GetPatientClientQuery request, CancellationToken cancellationToken)
        {
            GetPatientApiQuery query = mapper.Map<GetPatientApiQuery>(request);
            GetPatientApiQueryResponse response = await mediator.Send(query);

            if(response.Success)
            {
                return mapper.Map<GetPatientClientQueryResponse>(response);
            }
            else
            {
                dialogService.ShowError(response.Message, "Hiba");
                return new GetPatientClientQueryResponse();
            }
        }
    }

    internal class GetPatientByIdApiQueryHandler : GenericApiCommandHandler<GetPatientsByUserIdApiQuery, GetPatientsByUserIdApiQueryResponse>
    {
        public GetPatientByIdApiQueryHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
