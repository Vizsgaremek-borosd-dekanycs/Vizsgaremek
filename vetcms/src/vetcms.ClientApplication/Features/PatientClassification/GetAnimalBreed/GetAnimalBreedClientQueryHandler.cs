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
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Features.PatientClassification.GetAnimalBreed
{
    internal class GetAnimalBreedClientQueryHandler(IMediator mediator, IDialogService dialogService, IMapper mapper) : IRequestHandler<GetAnimalBreedClientQuery, GetAnimalBreedClientQueryResponse>
    {
        public async Task<GetAnimalBreedClientQueryResponse> Handle(GetAnimalBreedClientQuery request, CancellationToken cancellationToken)
        {
            GetAnimalBreedByIdApiQuery query = mapper.Map<GetAnimalBreedByIdApiQuery>(request);
            GetAnimalBreedByIdApiQueryResponse response = await mediator.Send(query);

            if (response.Success)
            {
                return mapper.Map<GetAnimalBreedClientQueryResponse>(response);
            }
            else
            {
                dialogService.ShowError(response.Message, "Hiba");
                return new GetAnimalBreedClientQueryResponse();
            }
        }
    }

    internal class GetAnimalBreedByIdApiQueryHandler : GenericApiCommandHandler<GetAnimalBreedByIdApiQuery, GetAnimalBreedByIdApiQueryResponse>
    {
        public GetAnimalBreedByIdApiQueryHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
