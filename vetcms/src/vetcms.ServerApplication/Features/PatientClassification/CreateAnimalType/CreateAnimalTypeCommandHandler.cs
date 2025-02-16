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
    internal class CreateAnimalTypeCommandHandler(IMapper mapper, IAnimalTypeRepository animalTypeRepository) : IRequestHandler<CreateAnimalTypeApiCommand, CreateAnimalTypeApiCommandResponse>
    {
        public async Task<CreateAnimalTypeApiCommandResponse> Handle(CreateAnimalTypeApiCommand request, CancellationToken cancellationToken)
        {
            AnimalType newAnimalType = mapper.Map<AnimalType>(request.AnimalTypeModel);
            newAnimalType = await animalTypeRepository.AddAsync(newAnimalType);
            return new CreateAnimalTypeApiCommandResponse { AnimalTypeData = mapper.Map<AnimalTypeDto>(newAnimalType) };
        }
    }
}
