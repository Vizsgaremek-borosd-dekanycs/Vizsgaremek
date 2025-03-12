using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;

namespace vetcms.ClientApplication.Features.PatientClassification.DeleteAnimalType
{
    public class DeleteAnimalTypeClientCommand : IClientCommand<bool>
    {
        public List<int> TypeIds { get; set; }
    }
}
