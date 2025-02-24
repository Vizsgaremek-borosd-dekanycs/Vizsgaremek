using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientClassification.ListAnimalType
{
    internal class AnimalTypeListClientQueryHandler : IRequestHandler<AnimalTypeListClientQuery, AnimalTypeListClientQueryResponse>
    {
        private static readonly List<string> SampleTypeNames = new()
        {
            "Dog", "Cat", "Bird", "Fish", "Reptile",
            "Horse", "Rabbit", "Hamster", "Guinea Pig", "Turtle",
            "Frog", "Snake", "Lizard", "Spider", "Insect",
            "Chicken", "Duck", "Goose", "Turkey", "Pig",
            "Sheep", "Goat", "Cow", "Buffalo", "Deer",
            "Elephant", "Lion", "Tiger", "Bear", "Wolf",
            "Fox", "Leopard", "Cheetah", "Giraffe", "Zebra",
            "Kangaroo", "Koala", "Panda", "Monkey", "Chimpanzee",
            "Gorilla", "Orangutan", "Baboon", "Lemur", "Otter"
        };

        private static readonly List<string> SampleDescriptions = new()
        {
            "Domestic animal", "Wild animal", "Aquatic animal", "Reptile", "Bird",
            "Farm animal", "Pet", "Exotic animal", "Endangered species", "Common animal"
        };

        static AnimalTypeListClientQueryHandler()
        {
            GenerateAnimalTypes(50);
        }

        internal static IQueryable<AnimalTypeDto> Types;

        public async Task<AnimalTypeListClientQueryResponse> Handle(AnimalTypeListClientQuery request, CancellationToken cancellationToken)
        {
            AnimalTypeListClientQueryResponse response = new();
            response.AnimalType = Types.Skip(request.Skip).Take((int)request.Take).ToList();
            response.ResultCount = Types.Count();

            await Task.Delay(1000);
            return response;
        }

        private static void GenerateAnimalTypes(int count)
        {
            var random = new Random();
            var animalTypes = new List<AnimalTypeDto>();

            for (int i = 1; i <= count; i++)
            {
                var typeName = SampleTypeNames[random.Next(SampleTypeNames.Count)];
                var description = SampleDescriptions[random.Next(SampleDescriptions.Count)];

                animalTypes.Add(new AnimalTypeDto
                {
                    Id = i,
                    TypeName = typeName,
                    Description = description
                });
            }

            Types = animalTypes.AsQueryable();
        }
    }
}

