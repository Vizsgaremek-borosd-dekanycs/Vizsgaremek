using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Features.IAM.UserList;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientClassification.AnimalBreedList
{
    internal class AnimalBreedListClientQueryHandler(IMediator mediator) : IRequestHandler<AnimalBreedListClientQuery, AnimalBreedListClientQueryResponse>
    {
        private static readonly List<string> SampleBreedNames = new()
        {
            "Labrador Retriever", "German Shepherd", "Golden Retriever", "Persian Cat", "Siamese Cat",
            "Bulldog", "Poodle", "Beagle", "Rottweiler", "Yorkshire Terrier",
            "Boxer", "Dachshund", "Shih Tzu", "Doberman Pinscher", "Great Dane",
            "Sphynx Cat", "Maine Coon", "Bengal Cat", "Scottish Fold", "Russian Blue"
        };

        private static readonly List<string> SampleCharacteristics = new()
        {
            "Friendly", "Active", "Outgoing", "Confident", "Courageous", "Smart",
            "Affectionate", "Loyal", "Quiet", "Social", "Intelligent", "Playful",
            "Calm", "Gentle", "Protective", "Energetic", "Curious", "Independent"
        };

        static AnimalBreedListClientQueryHandler()
        {
            GenerateRandomAnimalBreeds(100);
        }



        internal static IQueryable<AnimalBreedDto> Breeds;
        public async Task<AnimalBreedListClientQueryResponse> Handle(AnimalBreedListClientQuery request, CancellationToken cancellationToken)
        {
            AnimalBreedListClientQueryResponse response = new();

            response.AnimalBreeds = Breeds.Skip(request.Skip).Take(request.Take).ToList();
            response.ResultCount = Breeds.Count();

            await Task.Delay(1000);
            return response;

        }

        private static void GenerateRandomAnimalBreeds(int count)
        {
            var random = new Random();
            var animalBreeds = new List<AnimalBreedDto>();

            for (int i = 1; i <= count; i++)
            {
                var breedName = SampleBreedNames[random.Next(SampleBreedNames.Count)];
                var characteristics = SampleCharacteristics[random.Next(SampleCharacteristics.Count)];

                animalBreeds.Add(new AnimalBreedDto
                {
                    Id = i,
                    TypeId = random.Next(1, 5), // Assuming there are 4 types of animals
                    BreedName = breedName,
                    Charachteristics = characteristics
                });
            }

            Breeds = animalBreeds.AsQueryable();
        }
    }
}
