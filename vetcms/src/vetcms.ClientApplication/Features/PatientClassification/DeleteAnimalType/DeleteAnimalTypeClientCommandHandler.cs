using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Features.PatientClassification.ListAnimalType;

namespace vetcms.ClientApplication.Features.PatientClassification.DeleteAnimalType
{
    public class DeleteAnimalTypeClientCommandHandler(IMediator mediator) : IRequestHandler<DeleteAnimalTypeClientCommand, bool>
    {
        public async Task<bool> Handle(DeleteAnimalTypeClientCommand request, CancellationToken cancellationToken)
        {
            request.TypeIds.ForEach(breedId =>
            {
                var list = AnimalTypeListClientQueryHandler.Types.ToList();
                list.RemoveAll(b => b.Id == breedId);
                AnimalTypeListClientQueryHandler.Types = list.AsQueryable();
            });

            await Task.Delay(1000);
            return true;
        }
    }
}
