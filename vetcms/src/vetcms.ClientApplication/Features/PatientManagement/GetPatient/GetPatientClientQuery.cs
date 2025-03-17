using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;

namespace vetcms.ClientApplication.Features.PatientManagement.GetPatient
{
    public class GetPatientClientQuery : IClientCommand<GetPatientClientQueryResponse>
    {
        public int PatientId { get; set; } = 0;
    }
}
