using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientClassification.AnimalBreedList
{
    public class AnimalBreedListClientQueryResponse
    {
        public List<AnimalBreedDto> AnimalBreeds { get; set; } = new();
        public int ResultCount { get; set; } = 0;
    }
}
