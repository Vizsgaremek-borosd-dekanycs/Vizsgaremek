using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.ServerApplication.Infrastructure.Presistence.Repository;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.IAM;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ServerApplication.Features.PatientClassification.CreatePatientType
{
    internal class ModifyAnimalBreedCommandHandler(IMapper mapper, IAnimalBreedRepository animalBreedRepository, IAnimalTypeRepository animalTypeRepository) : IRequestHandler<ModifyAnimalBreedApiCommand, ModifyAnimalBreedApiCommandResponse>
    {
        public async Task<ModifyAnimalBreedApiCommandResponse> Handle(ModifyAnimalBreedApiCommand request, CancellationToken cancellationToken)
        {
            var animalTypeExists = await animalTypeRepository.ExistAsync(request.AnimalBreedModel.TypeId);
            if (!animalTypeExists)
            {
                return new ModifyAnimalBreedApiCommandResponse(false, "Az állat típus nem létezik.");
            }

            // Check if an animal breed with the same BreedName already exists


            if (!await animalBreedRepository.ExistAsync(request.Id))
            {
                return new ModifyAnimalBreedApiCommandResponse
                {
                    Success = false,
                    Message = "Nem létezik ilyen bejegyzés az adatbázisban."
                };
            }

            AnimalBreed updatedAnimalType = mapper.Map<AnimalBreed>(request.AnimalBreedModel);
            updatedAnimalType.Type = await animalTypeRepository.GetByIdAsync(request.AnimalBreedModel.TypeId);

            updatedAnimalType.Id = request.Id;
            updatedAnimalType = await animalBreedRepository.UpdateAsync(updatedAnimalType);

            return new ModifyAnimalBreedApiCommandResponse(true) { AnimalBreedData = mapper.Map<AnimalBreedDto>(updatedAnimalType) };
        }
    }
}
