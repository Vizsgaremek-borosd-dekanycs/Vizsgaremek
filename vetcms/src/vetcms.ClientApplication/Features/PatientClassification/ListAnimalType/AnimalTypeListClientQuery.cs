using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.SharedModels.Common.Abstract;

namespace vetcms.ClientApplication.Features.PatientClassification.ListAnimalType
{
    public class AnimalTypeListClientQuery : IClientCommand<AnimalTypeListClientQueryResponse>
    {
        public int BreedId { get; set; } = 0;
        public string SearchTerm { get; set; } = string.Empty;
        public int Skip { get; set; } = 0;
        public int? Take { get; set; }
    }
}
