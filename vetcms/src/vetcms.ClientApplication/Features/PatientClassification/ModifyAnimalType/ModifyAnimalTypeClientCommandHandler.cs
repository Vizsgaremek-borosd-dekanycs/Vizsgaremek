using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vetcms.ClientApplication.Features.PatientClassification.ModifyAnimalType
{
    internal class ModifyAnimalTypeClientCommandHandler : IRequestHandler<ModifyAnimalTypeClientCommand, bool>
    {
        public async Task<bool> Handle(ModifyAnimalTypeClientCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(1000);
            return true;
        }
    }
}
