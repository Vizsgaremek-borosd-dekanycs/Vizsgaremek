using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;

namespace vetcms.ClientApplication.Features.PatientManagement.GetPatient
{
    public class GetTreatmentByPatientsClientQuery : IClientCommand<GetTreatmentByPatientsClientQueryResponse>
    {
        public int PatientId { get; set; } = 0;
    }
}
