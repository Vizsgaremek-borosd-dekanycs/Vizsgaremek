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

namespace vetcms.ServerApplication.Features.PatientClassification.ListAnimalBreed
{
    internal class ListAnimalBreedQueryHandler(IMapper mapper, IAnimalBreedRepository animalBreedRepository) : IRequestHandler<ListAnimalBreedApiQuery, ListAnimalBreedApiQueryResponse>
    {
        public async Task<ListAnimalBreedApiQueryResponse> Handle(ListAnimalBreedApiQuery request, CancellationToken cancellationToken)
        {
            int count = await animalBreedRepository.Search(request.SearchTerm).CountAsync();

            List<AnimalBreed> animalBreeds = await animalBreedRepository.SearchAsync(request.SearchTerm, request.Skip, request.Take);
            List<AnimalBreedDto> animalBreedDtos = mapper.Map<List<AnimalBreedDto>>(animalBreeds);
            return new ListAnimalBreedApiQueryResponse(true)
            {
                AnimalBreeds = animalBreedDtos,
                ResultCount = count
            };
        }
    }
}
