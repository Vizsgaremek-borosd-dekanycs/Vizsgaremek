using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;

namespace vetcms.ServerApplication.Domain.Entity.PatientManagement
{
    public class Treatment : AuditedEntity
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime DateOfTreatment { get; set; }
        public string Type { get; set; }
        public string? Symptoms { get; set; }
        public string? Diagnosis { get; set; }
        public string? Medications { get; set; }
        public string? Procedure { get; set; }
        public string? Description { get; set; }
        public DateTime? FollowUpDate { get; set; }
    }
}
