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
    internal class ListPatientTreatmentClientQueryHandler(IMediator mediator, IMapper mapper) 
        : IRequestHandler<ListPatientTreatmentClientQuery, ListPatientTreatmentClientQueryResponse>
    {
        public async Task<ListPatientTreatmentClientQueryResponse> Handle(ListPatientTreatmentClientQuery request, CancellationToken cancellationToken)
        {
            ListPatientTreatmentApiQuery query = mapper.Map<ListPatientTreatmentApiQuery>(request);
            ListPatientTreatmentApiQueryResponse response = await mediator.Send(query);
            ListPatientTreatmentClientQueryResponse result = mapper.Map<ListPatientTreatmentClientQueryResponse>(response);
            return result;
        }
    }

    internal class ListPatientTreatmentApiQueryHandler : GenericApiCommandHandler<ListPatientTreatmentApiQuery, ListPatientTreatmentApiQueryResponse>
    {
        public ListPatientTreatmentApiQueryHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}

