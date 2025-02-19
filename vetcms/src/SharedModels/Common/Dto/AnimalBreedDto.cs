using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vetcms.SharedModels.Common.Dto
{
    public class AnimalBreedDto
    {
        public int? Id { get; set; }
        public int TypeId { get; set; }
        public string BreedName { get; set; }
        public string Charachteristics { get; set; }
    }
}
