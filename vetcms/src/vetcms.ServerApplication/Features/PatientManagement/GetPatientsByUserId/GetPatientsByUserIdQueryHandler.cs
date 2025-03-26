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
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ServerApplication.Features.PatientManagement.GetPatientsByUserId
{
    internal class GetPatientsByUserIdQueryHandler(IMapper mapper, IPatientRepository patientRepository, IUserRepository userRepository) : IRequestHandler<GetPatientsByUserIdApiQuery, GetPatientsByUserIdApiQueryResponse>
    {
        public async Task<GetPatientsByUserIdApiQueryResponse> Handle(GetPatientsByUserIdApiQuery request, CancellationToken cancellationToken)
        {
            if(!await userRepository.ExistAsync(request.UserId))
            {
                return new GetPatientsByUserIdApiQueryResponse
                {
                    Success = false,
                    Message = "Felhasználó nem található"
                };
            }
            User user = await userRepository.GetByIdAsync(request.UserId);
            List<Patient> patientsByUser = await patientRepository.GetPatientsByUserIdAsync(user.Id);

            List<PatientDto> patientByUserDtos = mapper.Map<List<PatientDto>>(patientsByUser);
            return new GetPatientsByUserIdApiQueryResponse
            {
                Success = true,
                Animals = patientByUserDtos
            };
        }
    }
}
