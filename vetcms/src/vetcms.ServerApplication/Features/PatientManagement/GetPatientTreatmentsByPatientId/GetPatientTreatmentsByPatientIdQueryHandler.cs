using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity.PatientManagement;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.ServerApplication.Infrastructure.Presistence.Repository;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ServerApplication.Features.PatientManagement.GetPatientTreatmentsByPatientId
{
    internal class GetPatientTreatmentsByPatientIdQueryHandler(IMapper mapper, ITreatmentRepository treatmentRepository, IPatientRepository patientRepository)
        : IRequestHandler<GetPatientTreatmentsByPatientIdApiQuery, GetPatientTreatmentsByPatientIdApiQueryResponse>
    {
        public async Task<GetPatientTreatmentsByPatientIdApiQueryResponse> Handle(GetPatientTreatmentsByPatientIdApiQuery request, CancellationToken cancellationToken)
        {
            if(!await patientRepository.ExistAsync(request.PatientId))
            {
                return new GetPatientTreatmentsByPatientIdApiQueryResponse
                {
                    Success = false,
                    Message = "Páciens nem található"
                };
            }
            Patient patient = await patientRepository.GetByIdAsync(request.PatientId);
            List<Treatment> treatmentsByPatient = await treatmentRepository.GetTreatmentsByPatientIdAsync(patient.Id);

            List<TreatmentDto> treatmentDtos = mapper.Map<List<TreatmentDto>>(treatmentsByPatient);
            return new GetPatientTreatmentsByPatientIdApiQueryResponse
            {
                Success = true,
                Treatments = treatmentDtos
            };
        }
    }
}
