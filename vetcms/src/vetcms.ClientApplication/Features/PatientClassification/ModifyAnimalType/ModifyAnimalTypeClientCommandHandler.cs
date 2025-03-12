using AutoMapper;
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
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ClientApplication.Features.PatientClassification.ModifyAnimalType
{
    internal class ModifyAnimalTypeClientCommandHandler(IMediator mediator, IMapper mapper, IDialogService dialogService) : IRequestHandler<ModifyAnimalTypeClientCommand, bool>
    {
        public async Task<bool> Handle(ModifyAnimalTypeClientCommand request, CancellationToken cancellationToken)
        {
            ModifyAnimalTypeApiCommand command = mapper.Map<ModifyAnimalTypeApiCommand>(request);
            ModifyAnimalTypeApiCommandResponse response = await mediator.Send(command);
            if (response.Success)
            {
                _ = await (await dialogService.ShowSuccessAsync("Sikeres módosítás", "Siker")).Result;
                return true;
            }
            else
            {
                _ = await (await dialogService.ShowErrorAsync(response.Message, "Hiba")).Result;
                return false;
            }
        }
    }

    internal class ModifyAnimalTypeApiCommandHandler : GenericApiCommandHandler<ModifyAnimalTypeApiCommand, ModifyAnimalTypeApiCommandResponse>
    {
        public ModifyAnimalTypeApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
