﻿using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Features.IAM.LoginUser;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ClientApplication.Common.IAM.Commands.AuthenticationStatus
{
    internal class AuthenticatedStatusQueryHandler(AuthenticationManger authenticationManger) : IRequestHandler<AuthenticatedStatusQuery, AuthenticatedStatusResponseModel>
    {
        public async Task<AuthenticatedStatusResponseModel> Handle(AuthenticatedStatusQuery request, CancellationToken cancellationToken)
        {
            UserDto? currentUser = null;
            try
            {
                currentUser = await authenticationManger.GetCurrentUser();
            }
            catch(Exception)
            {
                Console.WriteLine("Current user not found");
            }

            return new AuthenticatedStatusResponseModel(await authenticationManger.IsAuthenticated())
            {
                CurrentUser = currentUser
            };
        }
    }
}
