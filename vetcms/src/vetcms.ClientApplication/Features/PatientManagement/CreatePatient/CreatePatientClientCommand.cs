using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.ClientApplication.Features.PatientManagement.ListPatients;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Features.PatientManagement.CreatePatient
{
    public class CreatePatientCommand : IClientCommand<bool>
    {
        public PatientDto NewAnimalModel { get; set; }


    }
}
