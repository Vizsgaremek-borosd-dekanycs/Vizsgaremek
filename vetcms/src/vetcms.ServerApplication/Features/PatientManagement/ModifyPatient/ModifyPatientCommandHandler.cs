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

namespace vetcms.ServerApplication.Features.PatientManagement.ModifyPatient
{
    internal class ModifyPatientCommandHandler(IMapper mapper, IPatientRepository patientRepository, IAnimalBreedRepository animalBreedRepository,
        IAnimalTypeRepository animalTypeRepository, IUserRepository userRepository)
        : IRequestHandler<ModifyPatientApiCommand, ModifyPatientApiCommandResponse>
    {
        public async Task<ModifyPatientApiCommandResponse> Handle(ModifyPatientApiCommand request, CancellationToken cancellationToken)
        {
            if (!await patientRepository.ExistAsync(request.Id))
            {
                return new ModifyPatientApiCommandResponse
                {
                    Success = false,
                    Message = "Nem létezik ilyen bejegyzés az adatbázisban."
                };
            }

            Patient updatedPatient = mapper.Map<Patient>(request.PatientModel);
            updatedPatient.Type = await animalTypeRepository.GetByIdAsync(request.PatientModel.TypeId);
            updatedPatient.Breed = await animalBreedRepository.GetByIdAsync(request.PatientModel.BreedId);
            updatedPatient.Owner = await userRepository.GetByIdAsync(request.PatientModel.OwnerId);

            updatedPatient.Id = request.Id;
            await patientRepository.UpdateAsync(updatedPatient);

            return new ModifyPatientApiCommandResponse()
            {
                Success = true,
                Message = "Módosítások elmentve!"
            };
        }
    }
}
