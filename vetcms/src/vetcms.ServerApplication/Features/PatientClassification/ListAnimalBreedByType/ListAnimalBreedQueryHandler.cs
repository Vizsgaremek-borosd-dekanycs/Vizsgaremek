using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using vetcms.Application.Migrations;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.ServerApplication.Infrastructure.Presistence;
using vetcms.ServerApplication.Infrastructure.Presistence.Repository;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.IAM;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ServerApplication.Features.PatientClassification.ListAnimalBreed
{
    internal class ListAnimalBreedByTypeHandler(IMapper mapper, IAnimalBreedRepository animalBreedRepository, ApplicationDbContext dbContext) : IRequestHandler<ListAnimalBreedByTypeApiQuery, ListAnimalBreedByTypeApiQueryResponse>
    {
        public async Task<ListAnimalBreedByTypeApiQueryResponse> Handle(ListAnimalBreedByTypeApiQuery request, CancellationToken cancellationToken)
        {
            int count = animalBreedRepository.Where(q => q.Type.Id == request.TypeId).Count();


            List<AnimalBreed> animalBreeds = animalBreedRepository.Where(q => q.Type.Id == request.TypeId).Skip(request.Skip).Take(request.Take).ToList();
            List<AnimalBreedDto> animalBreedDtos = mapper.Map<List<AnimalBreedDto>>(animalBreeds);
            return new ListAnimalBreedByTypeApiQueryResponse(true)
            {
                AnimalBreeds = animalBreedDtos,
                ResultCount = count
            };
        }
    }
}
