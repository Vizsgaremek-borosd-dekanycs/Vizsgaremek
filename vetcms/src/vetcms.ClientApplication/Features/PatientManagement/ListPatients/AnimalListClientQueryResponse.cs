using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientManagement.ListPatients
{
    public class AnimalListClientQueryResponse
    {
        public List<PatientDto> Patients { get; set; } = new();
        public int ResoultCount { get; set; } = 0;
    }
}
