using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vetcms.ClientApplication.Features.PatientClassification.CreateAnimalType
{
    internal class CreateAnimalTypeClientCommandHandler : IRequestHandler<CreateNewAnimalTypeClientCommand, bool>
    {
        public async Task<bool> Handle(CreateNewAnimalTypeClientCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(1000);
            return true;
        }
    }
}
