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
using vetcms.SharedModels.Features.PatientClassification;
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ServerApplication.Features.PatientManagement.ListPatient
{
    internal class ListPatientQueryHandler(IMapper mapper, IPatientRepository patientRepository) : IRequestHandler<ListPatientApiQuery, ListPatientApiQueryResponse>
    {
        public async Task<ListPatientApiQueryResponse> Handle(ListPatientApiQuery request, CancellationToken cancellationToken)
        {
            int count = 0;
            List<Patient> patients = new();
            if (request.OwnerId.HasValue)
            {
                count = await patientRepository.Search(request.SearchTerm).Where(p => p.Owner.Id == request.OwnerId).CountAsync();
                patients = await patientRepository.Search(request.SearchTerm).Skip(request.Skip).Take(request.Take).Where(p => p.Owner.Id == request.OwnerId).ToListAsync();
            }
            else
            {
                count = await patientRepository.Search(request.SearchTerm).CountAsync();
                patients = await patientRepository.SearchAsync(request.SearchTerm, request.Skip, request.Take);
            }
            List<PatientDto> patientDtos = mapper.Map<List<PatientDto>>(patients);
            return new ListPatientApiQueryResponse(true)
            {
                Patients = patientDtos,
                ResoultCount = count
            };
        }
    }
}
