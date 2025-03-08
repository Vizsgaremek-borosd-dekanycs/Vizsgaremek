using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;

namespace vetcms.ClientApplication.Features.PatientClassification.GetAnimalType
{
    public class GetAnimalTypeClientQuery : IClientCommand<GetAnimalTypeClientQueryResponse>
    {
        public int Id { get; set; } = 0;
    }
}
