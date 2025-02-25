using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;

namespace vetcms.ClientApplication.Features.PatientClassification.ListAnimalBreedByType
{
    public class ListAnimalBreedByTypeClientQuery : IClientCommand<ListAnimalBreedByTypeClientQueryResponse>
    {
        public int TypeId { get; set; } = 0;
    }
}
