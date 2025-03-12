using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Features.IAM.ModifyUser;

namespace vetcms.ClientApplication.Features.PatientClassification.ModifyAnimalBreed
{
    internal class ModifyAnimalBreedClientCommandHandler : IRequestHandler<ModifyAnimalBreedClientCommand, bool>
    {
        public async Task<bool> Handle(ModifyAnimalBreedClientCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(1000);
            return true;
        }
    }
}
