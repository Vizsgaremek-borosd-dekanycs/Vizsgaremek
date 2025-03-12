using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.ServerApplication.Infrastructure.Presistence.Repository;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.IAM;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ServerApplication.Features.PatientClassification.ListAnimalType
{
    internal class GetAnimalTypeByIdQueryHandler(IMapper mapper, IAnimalTypeRepository animalTypeRepository) : IRequestHandler<GetAnimalTypeByIdApiQuery, GetAnimalTypeByIdApiQueryResponse>
    {
        public async Task<GetAnimalTypeByIdApiQueryResponse> Handle(GetAnimalTypeByIdApiQuery request, CancellationToken cancellationToken)
        {
            if(await animalTypeRepository.ExistAsync(request.Id))
            {
                AnimalType animalType = await animalTypeRepository.GetByIdAsync(request.Id);
                AnimalTypeDto animalTypeDto = mapper.Map<AnimalTypeDto>(animalType);
                return new GetAnimalTypeByIdApiQueryResponse(true)
                {
                    AnimalTypeModel = animalTypeDto,
                };
            }
            else
            {
                return new GetAnimalTypeByIdApiQueryResponse(false, "Állattípus nem található");
            }
        }
    }
}
