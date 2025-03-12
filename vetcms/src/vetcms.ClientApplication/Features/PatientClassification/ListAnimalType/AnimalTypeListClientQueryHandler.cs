using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Features.PatientClassification.ListAnimalType
{
    internal class AnimalTypeListClientQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<AnimalTypeListClientQuery, AnimalTypeListClientQueryResponse>
    {
        public async Task<AnimalTypeListClientQueryResponse> Handle(AnimalTypeListClientQuery request, CancellationToken cancellationToken)
        {
            ListAnimalTypeApiQuery query = mapper.Map<ListAnimalTypeApiQuery>(request);
            ListAnimalTypeApiQueryResponse response = await mediator.Send(query);
            AnimalTypeListClientQueryResponse result = mapper.Map<AnimalTypeListClientQueryResponse>(response);
            return result;
        }
    }

    internal class ListAnimalTypeApiQueryHandler : GenericApiCommandHandler<ListAnimalTypeApiQuery, ListAnimalTypeApiQueryResponse>
    {
        public ListAnimalTypeApiQueryHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}

