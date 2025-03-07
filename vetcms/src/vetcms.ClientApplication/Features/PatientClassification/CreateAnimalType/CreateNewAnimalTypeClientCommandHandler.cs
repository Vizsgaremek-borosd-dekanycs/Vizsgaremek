using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Features.PatientClassification.CreateAnimalType
{
    internal class CreateNewAnimalTypeClientCommandHandler(IMediator mediator, IDialogService dialogService) : IRequestHandler<CreateNewAnimalTypeClientCommand, bool>
    {
        public async Task<bool> Handle(CreateNewAnimalTypeClientCommand request, CancellationToken cancellationToken)
        {
            CreateAnimalTypeApiCommand createAnimalBreedApiCommand = new CreateAnimalTypeApiCommand
            {
                AnimalTypeModel = request.NewTypeModel
            };
            var response = await mediator.Send(createAnimalBreedApiCommand);

            if (response.Success)
            {
                await dialogService.ShowSuccessAsync($"{response.Message}", "Siker");
                return true;
            }
            else
            {
                dialogService.ShowError(response.Message, "Hiba");
                return false;
            }
        }
    }
}
