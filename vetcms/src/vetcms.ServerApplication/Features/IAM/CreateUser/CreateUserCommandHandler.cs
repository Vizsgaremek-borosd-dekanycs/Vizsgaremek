﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.SharedModels.Common.IAM.Authorization;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ServerApplication.Features.IAM.CreateUser
{
    internal class CreateUserCommandHandler(IUserRepository userRepository, IFirstTimeAuthenticationCodeRepository firstTimeAuthenticationCodeRepository, IMailService mailService) : IRequestHandler<CreateUserApiCommand, CreateUserApiCommandResponse>
    {
        public async Task<CreateUserApiCommandResponse> Handle(CreateUserApiCommand request, CancellationToken cancellationToken)
        { 
            User newUser = new User();
            newUser.PhoneNumber = request.PhoneNumber;
            newUser.Email = request.Email;
            newUser.VisibleName = request.Name;
            newUser.OverwritePermissions(new EntityPermissions(request.PermissionSet).RemoveFlag(PermissionFlags.CAN_LOGIN));

            if (userRepository.HasUserByEmail(newUser.Email))
            {
                return await Task.FromResult(new CreateUserApiCommandResponse()
                {
                    Success = false,
                    Message = "Az E-mail cím már foglalt."
                });
            }
            else
            {
                return await ProcessCreateUser(newUser);
            }
        }

        private async Task<CreateUserApiCommandResponse> ProcessCreateUser(User newUser)
        {
            await userRepository.AddAsync(newUser);
            int id = await SendEmail(newUser);
            return new CreateUserApiCommandResponse(true)
            {
                Message = $"A felhasználó sikeresen létrehozva. [BEMUTATÓ MÓD]: Az email sikeresen elküldve, a bemutató érdekében itt megtekinthető: {mailService.GetEmailPreviewRoute(id)}"
            };
        }

        private async Task<int> SendEmail(User newUser)
        {
            string token = GenerateCode();
            FirstTimeAuthenticationCode firstTimeAuthModel = new FirstTimeAuthenticationCode()
            {
                User = newUser,
                Code = token
            };
            await firstTimeAuthenticationCodeRepository.AddAsync(firstTimeAuthModel);
            return await mailService.SendFirstAuthenticationEmailAsync(firstTimeAuthModel);
        }

        private string GenerateCode()
            => Guid.NewGuid().ToString().Substring(0, 6).ToUpper();

    }
}
