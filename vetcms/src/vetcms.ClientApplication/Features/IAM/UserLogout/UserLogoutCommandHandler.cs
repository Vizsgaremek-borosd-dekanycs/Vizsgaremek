using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using vetcms.ClientApplication.Common.Abstract;
using vetcms.ClientApplication.Common.IAM;
using vetcms.ClientApplication.Features.IAM.ResetPassword;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Common.IAM.Authorization;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ClientApplication.Features.IAM.UserLogout
{
    internal class UserLogoutCommandHandler(AuthenticationManger authenticationManger) : IRequestHandler<UserLogoutCommand, bool>
    {
        public async Task<bool> Handle(UserLogoutCommand request, CancellationToken cancellationToken)
        {
            await authenticationManger.ClearAuthenticationDetails();

            return true;
        }
    }
}
