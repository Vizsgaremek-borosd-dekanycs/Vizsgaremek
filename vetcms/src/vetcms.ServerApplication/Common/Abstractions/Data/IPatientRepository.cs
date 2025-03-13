using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Domain.Entity.PatientManagement;

namespace vetcms.ServerApplication.Common.Abstractions.Data
{
    public interface IPatientRepository : IRepositoryBase<Patient>
    {
    }
}
