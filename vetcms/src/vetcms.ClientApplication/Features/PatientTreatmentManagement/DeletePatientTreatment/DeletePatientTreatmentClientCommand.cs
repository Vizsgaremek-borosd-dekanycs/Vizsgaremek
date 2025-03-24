using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;

namespace vetcms.ClientApplication.Features.PatientManagement.DeletePatient
{
    public class DeletePatientTreatmentClientCommand : IClientCommand<bool>
    {
        public List<int> TreatmentIds { get; set; }
    }
}
