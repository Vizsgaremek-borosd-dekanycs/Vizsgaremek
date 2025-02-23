using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;

namespace vetcms.ClientApplication.Features.PatientClassification.AnimalBreedList
{
    public class AnimalBreedListClientQuery : IClientCommand<AnimalBreedListClientQueryResponse>
    {
        public string SearchTerm { get; set; } = string.Empty;
        public int Skip { get; set; } = 0;
        public int Take { get; set; }
    }
}
