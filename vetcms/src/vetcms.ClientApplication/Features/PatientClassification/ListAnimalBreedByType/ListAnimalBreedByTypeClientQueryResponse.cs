using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientClassification.ListAnimalBreedByType
{
    public class ListAnimalBreedByTypeClientQueryResponse
    {
        public List<AnimalBreedDto> BreedList { get; set; } = new();
    }
}
