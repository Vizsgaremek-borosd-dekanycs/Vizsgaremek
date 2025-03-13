using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity.PatientManagement;

namespace vetcms.ServerApplication.Infrastructure.Presistence.Repository
{
    internal class PatientRepository : RepositoryBase<Patient>, IAnimalBreedRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
