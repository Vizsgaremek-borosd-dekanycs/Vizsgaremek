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
using vetcms.ClientApplication.Features.PatientClassification.ListAnimalBreed;
using vetcms.ClientApplication.Features.PatientClassification.GetAnimalBreed;
using vetcms.ClientApplication.Features.PatientClassification.ListAnimalType;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Features.PatientClassification.GetAnimalType
{
    internal class GetAnimalTypeClientQueryHandler(IMediator mediator, IMapper mapper, IDialogService dialogService) : IRequestHandler<GetAnimalTypeClientQuery, GetAnimalTypeClientQueryResponse>
    {
        public async Task<GetAnimalTypeClientQueryResponse> Handle(GetAnimalTypeClientQuery request, CancellationToken cancellationToken)
        {
            GetAnimalTypeByIdApiQuery query = mapper.Map<GetAnimalTypeByIdApiQuery>(request);
            GetAnimalTypeByIdApiQueryResponse response = await mediator.Send(query);
            if (response.Success)
            {
                return mapper.Map<GetAnimalTypeClientQueryResponse>(response);
            }
            else
            {
                dialogService.ShowError(response.Message, "Hiba");
                return new GetAnimalTypeClientQueryResponse();
            }
        }
    }

    internal class DeleteAnimalBreedApiCommandHandler : GenericApiCommandHandler<GetAnimalTypeByIdApiQuery, GetAnimalTypeByIdApiQueryResponse>
    {
        public DeleteAnimalBreedApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
