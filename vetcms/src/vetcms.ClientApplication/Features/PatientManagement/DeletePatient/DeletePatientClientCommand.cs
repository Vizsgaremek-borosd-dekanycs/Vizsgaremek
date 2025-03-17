using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;

namespace vetcms.ClientApplication.Features.PatientClassification.DeleteAnimalBreed
{
    public class DeletePatientClientCommand : IClientCommand<bool>
    {
        public List<int> AnimalIds { get; set; }
    }
}
