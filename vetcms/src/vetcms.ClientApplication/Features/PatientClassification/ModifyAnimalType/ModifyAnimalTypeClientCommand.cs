using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientClassification.ModifyAnimalType
{
    public class ModifyAnimalTypeClientCommand : IClientCommand<bool>
    {
        public int TypeId { get; set; }
        public AnimalTypeDto ModifiedTypeDto { get; set; }
    }
}
