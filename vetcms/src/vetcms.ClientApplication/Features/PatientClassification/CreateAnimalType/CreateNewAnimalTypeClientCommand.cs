using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientClassification.CreateAnimalType
{
    public class CreateNewAnimalTypeClientCommand : IClientCommand<bool>
    {
        public AnimalTypeDto NewTypeModel { get; set; }
    }
}
