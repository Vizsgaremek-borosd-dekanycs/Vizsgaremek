﻿using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Common.Exceptions;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ServerApplication.Features.IAM.ModifyOtherUser
{
    internal class ModifyOtherUserCommandHandler(IUserRepository userRepository, IMailService mailService) : IRequestHandler<ModifyOtherUserApiCommand, ModifyOtherUserApiCommandResponse>
    {
        string passwordChanged = "";
        public async Task<ModifyOtherUserApiCommandResponse> Handle(ModifyOtherUserApiCommand request, CancellationToken cancellationToken)
        {
            if(!await userRepository.ExistAsync(request.Id))
            {
                return new ModifyOtherUserApiCommandResponse()
                {
                    Success = false,
                    Message = "Nem létező felhasznló"
                };
            }
            User user = await userRepository.GetByIdAsync(request.Id);
            user = ModifyUser(request, user);
            await userRepository.UpdateAsync(user);

            int mailId = await mailService.SendModifyOtherUserEmailAsync(user, passwordChanged);

            return new ModifyOtherUserApiCommandResponse(true)
            {
                Message = $"[BEMUTATÓ MÓD] Felhasználó módosítva. Az email elküldése sikeres volt. A bemutató céljából az alábbi linken nyitható meg: {mailService.GetEmailPreviewRoute(mailId)}"
            };


        }

        private User ModifyUser(ModifyOtherUserApiCommand request, User user)
        {
            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;
            user.VisibleName = request.VisibleName;
            if (!request.Password.IsNullOrEmpty())
            {
                user.Password = PasswordUtility.CreateUserPassword(user,request.Password);
                passwordChanged = request.Password;
            }
            user.OverwritePermissions(request.GetPermissions());
            //user.Address = request.Address;
            //user.DateOfBirth = request.DateOfBirth;
            //user.FirstName = request.FirstName;
            //user.LastName = request.LastName;
            return user;
        }
    }
}
