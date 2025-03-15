using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity.PatientManagement;
using vetcms.ServerApplication.Infrastructure.Presistence.Repository;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ServerApplication.Features.PatientManagement.ListTreatment
{
    internal class ListPatientTreatmentQueryHandler(IMapper mapper, ITreatmentRepository treatmentRepository) : IRequestHandler<ListPatientTreatmentApiQuery, ListPatientTreatmentApiQueryResponse>
    {
        public async Task<ListPatientTreatmentApiQueryResponse> Handle(ListPatientTreatmentApiQuery request, CancellationToken cancellationToken)
        {
            int count = await treatmentRepository.Search(request.SearchTerm).CountAsync();


            List<Treatment> treatments = await treatmentRepository.SearchAsync(request.SearchTerm, request.Skip, request.Take);

            List<TreatmentDto> treatmentDtos = mapper.Map<List<TreatmentDto>>(treatments);
            return new ListPatientTreatmentApiQueryResponse(true)
            {
                Treatments = treatmentDtos,
                ResoultCount = count
            };
        }
    }
}
