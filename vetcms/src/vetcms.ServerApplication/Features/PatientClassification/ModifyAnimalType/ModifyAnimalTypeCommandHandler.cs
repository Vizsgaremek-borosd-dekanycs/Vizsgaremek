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

namespace vetcms.ServerApplication.Features.PatientClassification.CreatePatientType
{
    internal class ModifyAnimalTypeCommandHandler(IMapper mapper, IAnimalTypeRepository animalTypeRepository) : IRequestHandler<ModifyAnimalTypeApiCommand, ModifyAnimalTypeApiCommandResponse>
    {
        public async Task<ModifyAnimalTypeApiCommandResponse> Handle(ModifyAnimalTypeApiCommand request, CancellationToken cancellationToken)
        {
            if (!await animalTypeRepository.ExistAsync(request.Id))
            {
                return new ModifyAnimalTypeApiCommandResponse
                {
                    Success = false,
                    Message = "Nem létezik ilyen bejegyzés az adatbázisban."
                };
            }

            AnimalType updatedAnimalType = mapper.Map<AnimalType>(request.AnimalTypeModel);
            updatedAnimalType.Id = request.Id;
            updatedAnimalType = await animalTypeRepository.UpdateAsync(updatedAnimalType);

            return new ModifyAnimalTypeApiCommandResponse(true) { AnimalTypeData = mapper.Map<AnimalTypeDto>(updatedAnimalType) };
        }
    }
}
