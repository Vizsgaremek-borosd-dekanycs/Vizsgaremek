using MediatR;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Features.IAM.GetUser;
using vetcms.ClientApplication.Features.PatientClassification.AnimalBreedList;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientClassification.GetAnimalBreed
{
    internal class GetAnimalBreedClientQueryHandler : IRequestHandler<GetAnimalBreedClientQuery, GetAnimalBreedClientQueryResponse>
    {
        public async Task<GetAnimalBreedClientQueryResponse> Handle(GetAnimalBreedClientQuery request, CancellationToken cancellationToken)
        {
            AnimalBreedDto animalBreed = AnimalBreedListClientQueryHandler.Breeds.FirstOrDefault(b => b.Id == request.BreedId);

            GetAnimalBreedClientQueryResponse response = new GetAnimalBreedClientQueryResponse();
            response.Breed = animalBreed;

            await Task.Delay(1000);
            return response;
        }
    }
}
