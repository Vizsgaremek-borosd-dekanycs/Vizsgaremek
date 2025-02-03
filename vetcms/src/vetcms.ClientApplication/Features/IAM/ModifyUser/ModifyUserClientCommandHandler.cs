using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.ClientApplication.Features.IAM.LoginUser;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ClientApplication.Features.IAM.ModifyUser
{
    internal class ModifyUserClientCommandHandler(IMediator mediator, IDialogService dialogService) : IRequestHandler<ModifyUserClientCommand, bool>
    {
        public async Task<bool> Handle(ModifyUserClientCommand request, CancellationToken cancellationToken)
        {
            ModifyOtherUserApiCommand modifyOtherUserApiCommand = new ModifyOtherUserApiCommand()
            {
                Id = request.UserId,
                ModifiedUser = request.ModifiedUserDto
            };

            ModifyOtherUserApiCommandResponse response = await mediator.Send(modifyOtherUserApiCommand);
            if(response.Success)
            {
                await dialogService.ShowSuccessAsync("Felhasználó sikeresen módosítva", "Siker");
                return true;
            }
            else
            {
                dialogService.ShowError(response.Message, "Hiba");
                return false;
            }
        }
    }

    internal class ModifyUserApiCommandHandler : GenericApiCommandHandler<ModifyOtherUserApiCommand, ModifyOtherUserApiCommandResponse>
    {
        public ModifyUserApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
