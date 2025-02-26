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
    internal class ListAnimalTypeQueryHandler(IMapper mapper, IAnimalTypeRepository animalTypeRepository) : IRequestHandler<ListAnimalTypeApiQuery, ListAnimalTypeApiQueryResponse>
    {
        public async Task<ListAnimalTypeApiQueryResponse> Handle(ListAnimalTypeApiQuery request, CancellationToken cancellationToken)
        {
            int count = await animalTypeRepository.Search(request.SearchTerm).CountAsync();

            List<AnimalType> animalTypes = await animalTypeRepository.SearchAsync(request.SearchTerm, request.Skip, request.Take);
            List<AnimalTypeDto> animalTypeDtos = mapper.Map<List<AnimalTypeDto>>(animalTypes);
            return new ListAnimalTypeApiQueryResponse(true)
            {
                AnimalTypes = animalTypeDtos,
                ResultCount = count
            };
        }
    }
}
