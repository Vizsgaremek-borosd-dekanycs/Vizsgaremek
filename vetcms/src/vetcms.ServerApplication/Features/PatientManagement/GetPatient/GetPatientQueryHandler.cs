using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.ServerApplication.Domain.Entity.PatientManagement;
using vetcms.ServerApplication.Infrastructure.Presistence.Repository;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.PatientClassification;
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ServerApplication.Features.PatientManagement.GetPatient
{
    internal class GetPatientQueryHandler(IMapper mapper, IPatientRepository patientRepository) : IRequestHandler<GetPatientApiQuery, GetPatientApiQueryResponse>
    {
        public async Task<GetPatientApiQueryResponse> Handle(GetPatientApiQuery request, CancellationToken cancellationToken)
        {
            if (await patientRepository.ExistAsync(request.PatientId))
            {
                Patient patient = await patientRepository.GetByIdAsync(request.PatientId);
                PatientDto patientDto = mapper.Map<PatientDto>(patient);
                return new GetPatientApiQueryResponse(true)
                {
                    PatientModel = patientDto,
                };
            }
            else
            {
                return new GetPatientApiQueryResponse(false, "Páciens nem található");
            }
        }
    }
}
