using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Features.PatientManagement.ListPatients;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientManagement.GetPatient
{
    public class GetPatientClientQueryResponse
    {
        public PatientDto PatientModel { get; set; } = new();
    }
}
