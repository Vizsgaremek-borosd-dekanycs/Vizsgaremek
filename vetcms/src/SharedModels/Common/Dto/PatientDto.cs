using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vetcms.SharedModels.Common.Dto
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public char Gender { get; set; }
        public double Weight { get; set; }
        public string MicrochipNumber { get; set; }
        public bool IsSterilised { get; set; }
        public string ChronicDiseases { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int OwnerId { get; set; }
        public int TypeId { get; set; }
        public int BreedId { get; set; }
    }
}
