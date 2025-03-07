using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vetcms.ClientApplication.Features.PatientClassification.CreateNewAnimalBreed
{
    internal class CreateNewAnimalBreedClientCommandHandler : IRequestHandler<CreateNewAnimalBreedClientCommand, bool>
    {
        public async Task<bool> Handle(CreateNewAnimalBreedClientCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(1000);
            return true;
        }
    }
}
