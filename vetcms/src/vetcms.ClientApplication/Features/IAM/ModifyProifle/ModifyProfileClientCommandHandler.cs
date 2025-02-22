using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ClientApplication.Features.IAM.ModifyProifle
{
    internal class ModifyProfileClientCommandHandler(IMediator mediator, IDialogService dialogService) : IRequestHandler<ModifyProfileClientCommand, bool>
    {
        public async Task<bool> Handle(ModifyProfileClientCommand request, CancellationToken cancellationToken)
        {
            ModifyOtherUserApiCommand command = new ModifyOtherUserApiCommand()
            {
                Id = request.ModifiedProfile.Id,
                ModifiedUser = request.ModifiedProfile
            };
            ModifyOtherUserApiCommandResponse response = await mediator.Send(command);

            if(response.Success)
            {
                await dialogService.ShowSuccessAsync(response.Message, "Siker");
                return true;
            }
            else
            {
                await dialogService.ShowErrorAsync(response.Message, "Hiba");
                return false;
            }
        }
    }
}
