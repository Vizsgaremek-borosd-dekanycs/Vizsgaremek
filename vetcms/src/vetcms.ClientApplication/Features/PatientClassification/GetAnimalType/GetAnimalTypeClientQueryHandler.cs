using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Features.PatientClassification.AnimalBreedList;
using vetcms.ClientApplication.Features.PatientClassification.GetAnimalBreed;
using vetcms.ClientApplication.Features.PatientClassification.ListAnimalType;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientClassification.GetAnimalType
{
    internal class GetAnimalTypeClientQueryHandler : IRequestHandler<GetAnimalTypeClientQuery, GetAnimalTypeClientQueryResponse>
    {
        public async Task<GetAnimalTypeClientQueryResponse> Handle(GetAnimalTypeClientQuery request, CancellationToken cancellationToken)
        {
            AnimalTypeDto animalType = AnimalTypeListClientQueryHandler.Types.FirstOrDefault(t => t.Id == request.TypeId);

            GetAnimalTypeClientQueryResponse response = new GetAnimalTypeClientQueryResponse();
            response.Type = animalType;

            await Task.Delay(1000);
            return response;
        }
    }
}
