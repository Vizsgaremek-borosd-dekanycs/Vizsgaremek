using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientClassification.GetAnimalType
{
    public class GetAnimalTypeClientQueryResponse
    {
        public AnimalTypeDto AnimalTypeModel { get; set; } = new();
    }
}
