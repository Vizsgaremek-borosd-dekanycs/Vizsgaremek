using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.ServerApplication.Domain.Entity.PatientManagement;

namespace vetcms.ServerApplication.Infrastructure.Presistence.Repository
{
    internal class TreatmentRepository : RepositoryBase<Treatment>, ITreatmentRepository
    {
        public TreatmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Treatment>> GetTreatmentsByPatientIdAsync(int patientId)
        {
            List<Treatment> treatmentsByPatient = Where(p => p.Patient.Id == patientId).ToList();
            return Task.FromResult(treatmentsByPatient);
        }
    }
}
