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
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ClientApplication.Features.PatientManagement.ListPatients
{
    internal class AnimalListClientQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<AnimalListClientQuery, AnimalListClientQueryResponse>
    {
        public async Task<AnimalListClientQueryResponse> Handle(AnimalListClientQuery request, CancellationToken cancellationToken)
        {
            ListPatientApiQuery query = mapper.Map<ListPatientApiQuery>(request);
            ListPatientApiQueryResponse response = await mediator.Send(query);
            AnimalListClientQueryResponse result = mapper.Map<AnimalListClientQueryResponse>(response);
            return result;
        }
    }

    internal class ListPatientsApiQueryHandler : GenericApiCommandHandler<ListPatientApiQuery, ListPatientApiQueryResponse>
    {
        public ListPatientsApiQueryHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}

