using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Features.PatientClassification.AnimalBreedList;

namespace vetcms.ClientApplication.Features.PatientClassification.DeleteAnimalBreed
{
    internal class DeleteAnimalBreedClientCommandHandler : IRequestHandler<DeleteAnimalBreedClientCommand, bool>
    {
        public async Task<bool> Handle(DeleteAnimalBreedClientCommand request, CancellationToken cancellationToken)
        {
            request.BreedIds.ForEach(breedId =>
            {
                var list = AnimalBreedListClientQueryHandler.Breeds.ToList();
                list.RemoveAll(b => b.Id == breedId);
                AnimalBreedListClientQueryHandler.Breeds = list.AsQueryable();
            });

            await Task.Delay(1000);
            return true;
        }
    }
}
