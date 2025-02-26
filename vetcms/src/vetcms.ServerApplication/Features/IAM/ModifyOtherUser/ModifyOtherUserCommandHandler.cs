using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Common.Abstractions.IAM;
using vetcms.ServerApplication.Common.Exceptions;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Common.IAM.Authorization;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ServerApplication.Features.IAM.ModifyOtherUser
{
    internal class ModifyOtherUserCommandHandler(IUserRepository userRepository, IMailService mailService, IAuthenticationCommon authenticationCommon) : IRequestHandler<ModifyOtherUserApiCommand, ModifyOtherUserApiCommandResponse>
    {
        string passwordChanged = "";
        public async Task<ModifyOtherUserApiCommandResponse> Handle(ModifyOtherUserApiCommand request, CancellationToken cancellationToken)
        {
            User executorUser = await authenticationCommon.GetUser(request.BearerToken);
            if (!await userRepository.ExistAsync(request.Id))
            {
                return new ModifyOtherUserApiCommandResponse()
                {
                    Success = false,
                    Message = "Nem létező felhasznló"
                };
            }
            User user = await userRepository.GetByIdAsync(request.Id);
            user = ModifyUser(request, user, executorUser);
            await userRepository.UpdateAsync(user);

            if ((executorUser.Id == user.Id))
            {
                return new ModifyOtherUserApiCommandResponse(true)
                {
                    Message = "Profil sikeresen módosítva!"
                };
            }
            else
            {
                int mailId = await mailService.SendModifyOtherUserEmailAsync(user, passwordChanged);

                return new ModifyOtherUserApiCommandResponse(true)
                {
                    Message = $"[BEMUTATÓ MÓD] Felhasználó módosítva. Az email elküldése sikeres volt. A bemutató céljából az alábbi linken nyitható meg: {mailService.GetEmailPreviewRoute(mailId)}"
                };
            }
        }

        private User ModifyUser(ModifyOtherUserApiCommand request, User targetUser, User executorUser)
        {
            UserDto userDto = request.ModifiedUser;
            Console.WriteLine(JsonSerializer.Serialize(userDto));
            Console.WriteLine(JsonSerializer.Serialize(targetUser));

            if (targetUser.PhoneNumber != userDto.PhoneNumber)
            {
                targetUser.PhoneNumber = userDto.PhoneNumber;
            }
            if (targetUser.Email != userDto.Email)
            {
                targetUser.Email = userDto.Email;
            }
            if (targetUser.VisibleName != userDto.VisibleName)
            {
                targetUser.VisibleName = userDto.VisibleName;
            }
            if (!userDto.Password.IsNullOrEmpty() && executorUser.GetPermissions().HasPermissionFlag(PermissionFlags.CAN_MODIFY_OTHER_USER_PASSWORD))
            {
                targetUser.Password = PasswordUtility.CreateUserPassword(targetUser, userDto.Password);
                passwordChanged = userDto.Password;
            }
            if (targetUser.Address != userDto.Address)
            {
                targetUser.Address = userDto.Address;
            }
            if (targetUser.DateOfBirth != userDto.DateOfBirth)
            {
                targetUser.DateOfBirth = userDto.DateOfBirth ?? DateTime.MinValue;
            }
            if (targetUser.FirstName != userDto.FirstName)
            {
                targetUser.FirstName = userDto.FirstName;
            }
            if (targetUser.LastName != userDto.LastName)
            {
                targetUser.LastName = userDto.LastName;
            }

            if(executorUser.GetPermissions().HasPermissionFlag(PermissionFlags.CAN_ASSIGN_PERMISSIONS))
            {
                targetUser.OverwritePermissions(userDto.GetPermissions());
            }

            return targetUser;
        }
    }
}
