using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Features.PatientClassification.AnimalBreedList;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientClassification.ListAnimalType
{
    internal class AnimalTypeListClientQueryHandler(IMediator mediator) : IRequestHandler<AnimalTypeListClientQuery, AnimalTypeByBreedListClientQueryResponse>
    {
        private static readonly List<AnimalTypeDto> AnimalTypes = new()
        {
            new AnimalTypeDto { Id = 1, TypeName = "Dog", Description = "Domestic dog" },
            new AnimalTypeDto { Id = 2, TypeName = "Cat", Description = "Domestic cat" },
            new AnimalTypeDto { Id = 3, TypeName = "Bird", Description = "Various species of birds" },
            new AnimalTypeDto { Id = 4, TypeName = "Fish", Description = "Various species of fish" },
            new AnimalTypeDto { Id = 5, TypeName = "Reptile", Description = "Various species of reptiles" },
            new AnimalTypeDto { Id = 1, TypeName = "Dog", Description = "Domestic dog" },
            new AnimalTypeDto { Id = 2, TypeName = "Cat", Description = "Domestic cat" },
            new AnimalTypeDto { Id = 3, TypeName = "Bird", Description = "Various species of birds" },
            new AnimalTypeDto { Id = 4, TypeName = "Fish", Description = "Various species of fish" },
            new AnimalTypeDto { Id = 5, TypeName = "Reptile", Description = "Various species of reptiles" },
            new AnimalTypeDto { Id = 1, TypeName = "Dog", Description = "Domestic dog" },
            new AnimalTypeDto { Id = 2, TypeName = "Cat", Description = "Domestic cat" },
            new AnimalTypeDto { Id = 3, TypeName = "Bird", Description = "Various species of birds" },
            new AnimalTypeDto { Id = 4, TypeName = "Fish", Description = "Various species of fish" },
            new AnimalTypeDto { Id = 5, TypeName = "Reptile", Description = "Various species of reptiles" },
            new AnimalTypeDto { Id = 1, TypeName = "Dog", Description = "Domestic dog" },
            new AnimalTypeDto { Id = 2, TypeName = "Cat", Description = "Domestic cat" },
            new AnimalTypeDto { Id = 3, TypeName = "Bird", Description = "Various species of birds" },
            new AnimalTypeDto { Id = 4, TypeName = "Fish", Description = "Various species of fish" },
            new AnimalTypeDto { Id = 5, TypeName = "Reptile", Description = "Various species of reptiles" }
        };
        public Task<AnimalTypeByBreedListClientQueryResponse> Handle(AnimalTypeListClientQuery request, CancellationToken cancellationToken)
        {
            var filteredAnimalTypes = AnimalTypes;

            filteredAnimalTypes = AnimalTypes
                .Where(at => at.TypeName.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var paginatedAnimalTypes = filteredAnimalTypes
                .Skip(request.Skip)
                .Take(request.Take ?? filteredAnimalTypes.Count)
                .ToList();

            var response = new AnimalTypeByBreedListClientQueryResponse
            {
                AnimalType = paginatedAnimalTypes,
                ResultCount = filteredAnimalTypes.Count
            };
            Task.Delay(1000);
            return Task.FromResult(response);
        }
    }
}
