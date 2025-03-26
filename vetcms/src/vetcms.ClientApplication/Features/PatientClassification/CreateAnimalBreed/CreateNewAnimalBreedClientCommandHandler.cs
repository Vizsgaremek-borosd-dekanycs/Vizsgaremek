using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.SharedModels.Features.IAM;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Features.PatientClassification.CreateAnimalBreed
{
    internal class CreateNewAnimalBreedClientCommandHandler(IMediator mediator, IDialogService dialogService) : IRequestHandler<CreateNewAnimalBreedClientCommand, bool>
    {
        public async Task<bool> Handle(CreateNewAnimalBreedClientCommand request, CancellationToken cancellationToken)
        {
            CreateAnimalBreedApiCommand createAnimalBreedApiCommand = new CreateAnimalBreedApiCommand
            {
                AnimalBreedData = request.NewBreedModel
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

    internal class CreateAnimalBreedApiCommandHandler : GenericApiCommandHandler<CreateAnimalBreedApiCommand, CreateAnimalBreedApiCommandResponse>
    {
        public CreateAnimalBreedApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
