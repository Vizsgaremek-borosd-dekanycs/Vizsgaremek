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
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Features.PatientClassification.ListAnimalBreedByType
{
    internal class ListAnimalBreedByTypeClientQueryHandler(IMediator mediator, IDialogService dialogService, IMapper mapper) : IRequestHandler<ListAnimalBreedByTypeClientQuery, ListAnimalBreedByTypeClientQueryResponse>
    {
        public async Task<ListAnimalBreedByTypeClientQueryResponse> Handle(ListAnimalBreedByTypeClientQuery request, CancellationToken cancellationToken)
        {
            ListAnimalBreedByTypeApiQuery query = mapper.Map<ListAnimalBreedByTypeApiQuery>(request);
            ListAnimalBreedByTypeApiQueryResponse response = await mediator.Send(query);
            if (response.Success)
            {
                return mapper.Map<ListAnimalBreedByTypeClientQueryResponse>(response);
            }
            else
            {
                dialogService.ShowError(response.Message, "Hiba");
                return new ListAnimalBreedByTypeClientQueryResponse();
            }
        }
    }

    internal class ListAnimalBreedByTypeApiQueryHandler : GenericApiCommandHandler<ListAnimalBreedByTypeApiQuery, ListAnimalBreedByTypeApiQueryResponse>
    {
        public ListAnimalBreedByTypeApiQueryHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
