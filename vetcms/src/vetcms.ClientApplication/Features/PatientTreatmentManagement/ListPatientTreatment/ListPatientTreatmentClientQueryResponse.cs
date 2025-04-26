using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientManagement.ListPatients
{
    public class ListPatientTreatmentClientQueryResponse
    {
        public List<TreatmentDto> Treatments { get; set; } = new();
        public int ResultCount { get; set; } = 0;
    }
}
