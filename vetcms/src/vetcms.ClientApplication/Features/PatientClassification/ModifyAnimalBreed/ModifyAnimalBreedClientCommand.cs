using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientClassification.ModifyAnimalBreed
{
    public class ModifyAnimalBreedClientCommand : IClientCommand<bool>
    {
        public int BreedId { get; set; }
        public AnimalBreedDto ModifiedBreedDto { get; set; }
    }
}
