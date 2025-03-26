using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity.PatientManagement;

namespace vetcms.ServerApplication.Infrastructure.Presistence.Repository
{
    internal class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Patient>> GetPatientsByUserIdAsync(int userId)
        {
            List<Patient> patientsByUser = Where(p => p.Owner.Id == userId).ToList();
            return Task.FromResult(patientsByUser);
        }
    }
}
