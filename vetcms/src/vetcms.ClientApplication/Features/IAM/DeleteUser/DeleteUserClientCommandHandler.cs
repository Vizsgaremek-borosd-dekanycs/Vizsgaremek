using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.ClientApplication.Common.IAM;
using vetcms.ClientApplication.Features.IAM.RegisterUser;
using vetcms.ClientApplication.Features.IAM.UserList;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ClientApplication.Features.IAM.DeleteUser
{
    internal class DeleteUserClientCommandHandler(IMediator mediator) : IRequestHandler<DeleteUserClientCommand, bool>
    {
        public async Task<bool> Handle(DeleteUserClientCommand request, CancellationToken cancellationToken)
        {
            DeleteUserApiCommand deleteUserApiCommand = new DeleteUserApiCommand()
            {
                Ids = request.UserIds
            };

            DeleteUserApiCommandResponse response = await mediator.Send(deleteUserApiCommand);
            if(response.Success)
            {
                return true;
            }
            else
            {
                request.DialogService.ShowError(response.Message, "Hiba");
                return false;
            }

        }
    }

    internal class DeleteUserApiCommandHandler : GenericApiCommandHandler<DeleteUserApiCommand, DeleteUserApiCommandResponse>
    {
        public DeleteUserApiCommandHandler(IServiceScopeFactory serviceScopeFactory)
            : base(serviceScopeFactory)
        {
        }
    }
}
