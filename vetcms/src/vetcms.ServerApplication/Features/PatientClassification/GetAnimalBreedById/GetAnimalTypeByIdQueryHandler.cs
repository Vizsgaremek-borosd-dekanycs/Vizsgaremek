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
    internal class GetAnimalBreedByIdQueryHandler(IMapper mapper, IAnimalBreedRepository breedRepository) : IRequestHandler<GetAnimalBreedByIdApiQuery, GetAnimalBreedByIdApiQueryResponse>
    {
        public async Task<GetAnimalBreedByIdApiQueryResponse> Handle(GetAnimalBreedByIdApiQuery request, CancellationToken cancellationToken)
        {
            if(await breedRepository.ExistAsync(request.Id))
            {
                AnimalBreed animalBreed = await breedRepository.GetByIdAsync(request.Id);
                AnimalBreedDto animalBreedDto = mapper.Map<AnimalBreedDto>(animalBreed);
                return new GetAnimalBreedByIdApiQueryResponse(true)
                {
                    AnimalBreedModel = animalBreedDto
                };
            }
            else
            {
                return new GetAnimalBreedByIdApiQueryResponse(false, "Állatfajta nem található");
            }
        }
    }
}
