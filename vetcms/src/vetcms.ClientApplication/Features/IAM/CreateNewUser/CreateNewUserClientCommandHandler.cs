using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.ClientApplication.Features.IAM.DeleteUser;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ClientApplication.Features.IAM.CreateNewUser
{
    internal class CreateNewUserClientCommandHandler(IMediator mediator, IDialogService dialogService) : IRequestHandler<CreateNewUserClientCommand, bool>
    {
        public async Task<bool> Handle(CreateNewUserClientCommand request, CancellationToken cancellationToken)
        {
            CreateUserApiCommand createUserApiCommand = new CreateUserApiCommand()
            {
                NewUser = request.NewUserModel
            };

            CreateUserApiCommandResponse response = await mediator.Send(createUserApiCommand);
            if(response.Success)
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

    internal class CreateUserApiCommandHandler : GenericApiCommandHandler<CreateUserApiCommand, CreateUserApiCommandResponse>
    {
        public CreateUserApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }

}
