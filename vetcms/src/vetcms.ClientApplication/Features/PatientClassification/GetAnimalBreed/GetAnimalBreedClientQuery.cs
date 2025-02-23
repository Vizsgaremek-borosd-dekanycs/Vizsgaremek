using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;

namespace vetcms.ClientApplication.Features.PatientClassification.GetAnimalBreed
{
    public class GetAnimalBreedClientQuery : IClientCommand<GetAnimalBreedClientQueryResponse>
    {
        public int BreedId { get; set; } = 0;
    }
}
