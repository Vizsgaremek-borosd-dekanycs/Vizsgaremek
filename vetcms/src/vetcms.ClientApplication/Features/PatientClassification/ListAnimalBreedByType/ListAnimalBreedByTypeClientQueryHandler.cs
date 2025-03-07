using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientClassification.ListAnimalBreedByType
{
    internal class ListAnimalBreedByTypeClientQueryHandler : IRequestHandler<ListAnimalBreedByTypeClientQuery, ListAnimalBreedByTypeClientQueryResponse>
    {
        public async Task<ListAnimalBreedByTypeClientQueryResponse> Handle(ListAnimalBreedByTypeClientQuery request, CancellationToken cancellationToken)
        {
            await Task.Delay(1000);
            ListAnimalBreedByTypeClientQueryResponse response = new ListAnimalBreedByTypeClientQueryResponse()
            {
                BreedList = GenerateAnimalBreeds().Where(b => b.TypeId == request.TypeId).ToList()
            };
            return response; 
        }

        private List<AnimalBreedDto> GenerateAnimalBreeds()
        {
            return new List<AnimalBreedDto>
            {
                new AnimalBreedDto { Id = 1, TypeId = 1, BreedName = "Labrador Retriever", Charachteristics = "Friendly, Active, Outgoing" },
                new AnimalBreedDto { Id = 2, TypeId = 1, BreedName = "German Shepherd", Charachteristics = "Confident, Courageous, Smart" },
                new AnimalBreedDto { Id = 3, TypeId = 2, BreedName = "Persian Cat", Charachteristics = "Affectionate, Loyal, Quiet" },
                new AnimalBreedDto { Id = 4, TypeId = 2, BreedName = "Siamese Cat", Charachteristics = "Social, Intelligent, Playful" },
                new AnimalBreedDto { Id = 5, TypeId = 3, BreedName = "Parrot", Charachteristics = "Talkative, Intelligent, Social" }
            };
        }
    }
}
