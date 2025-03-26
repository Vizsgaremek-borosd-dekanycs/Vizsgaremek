using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;

namespace vetcms.ServerApplication.Domain.Entity.PatientManagement
{
    public class Patient : AuditedEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public char Gender { get; set; }
        public double Weight { get; set; }
        public string MicrochipNumber { get; set; }
        public bool IsSterilised { get; set; }
        public string ChronicDiseases { get; set; }
        public DateTime DateOfBirth { get; set; }
        public User Owner { get; set; }
        public AnimalType Type { get; set; }
        public AnimalBreed Breed { get; set; }
    }
}
