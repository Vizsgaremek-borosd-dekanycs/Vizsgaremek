using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.IAM;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ServerApplication.Features.PatientClassification.CreateAnimalType
{
    internal class CreateAnimalBreedCommandHandler(IMapper mapper, IAnimalBreedRepository animalBreedRepository, IAnimalTypeRepository animalTypeRepository) : IRequestHandler<CreateAnimalBreedApiCommand, CreateAnimalBreedApiCommandResponse>
    {
        public async Task<CreateAnimalBreedApiCommandResponse> Handle(CreateAnimalBreedApiCommand request, CancellationToken cancellationToken)
        {
            // Check if the AnimalType exists
            var animalTypeExists = await animalTypeRepository.ExistAsync(request.AnimalBreedData.TypeId);
            if (!animalTypeExists)
            {
                return new CreateAnimalBreedApiCommandResponse(false, "Az állat típus nem létezik.");
            }

            // Check if an animal breed with the same BreedName already exists
            AnimalBreed newAnimalBreed = mapper.Map<AnimalBreed>(request.AnimalBreedData);

            newAnimalBreed.Type = await animalTypeRepository.GetByIdAsync(request.AnimalBreedData.TypeId);

            bool isExistingAnimalBreed = animalBreedRepository.Where(at => at.BreedName == request.AnimalBreedData.BreedName && at.Type == newAnimalBreed.Type).Any();
            if (isExistingAnimalBreed)
            {
                return new CreateAnimalBreedApiCommandResponse(false, "Már létezik ilyen nevű bejegyzés az adatbázisban.");
            }

            newAnimalBreed = await animalBreedRepository.AddAsync(newAnimalBreed);
            return new CreateAnimalBreedApiCommandResponse(true) { AnimalBreedData = mapper.Map<AnimalBreedDto>(newAnimalBreed) };
        }
    }
}
