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
    internal class CreateAnimalTypeCommandHandler(IMapper mapper, IAnimalTypeRepository animalTypeRepository) : IRequestHandler<CreateAnimalTypeApiCommand, CreateAnimalTypeApiCommandResponse>
    {
        public async Task<CreateAnimalTypeApiCommandResponse> Handle(CreateAnimalTypeApiCommand request, CancellationToken cancellationToken)
        {
            // Check if an animal type with the same TypeName already exists
            var existingAnimalType = animalTypeRepository.Where(at => at.TypeName == request.AnimalTypeModel.TypeName).FirstOrDefault();
            if (existingAnimalType != null)
            {
                return new CreateAnimalTypeApiCommandResponse(false, "Már létezik ilyen nevű bejegyzés az adatbázisban.");
            }

            AnimalType newAnimalType = mapper.Map<AnimalType>(request.AnimalTypeModel);
            newAnimalType = await animalTypeRepository.AddAsync(newAnimalType);
            return new CreateAnimalTypeApiCommandResponse(true) { AnimalTypeData = mapper.Map<AnimalTypeDto>(newAnimalType) };
        }
    }
}
