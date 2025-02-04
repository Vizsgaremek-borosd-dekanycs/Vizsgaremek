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
using vetcms.ServerApplication.Common.Exceptions;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.SharedModels.Common.Dto;
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
            UserDto userDto = request.ModifiedUser;
            Console.WriteLine(JsonSerializer.Serialize(userDto));
            Console.WriteLine(JsonSerializer.Serialize(user));

            if (user.PhoneNumber != userDto.PhoneNumber)
            {
                user.PhoneNumber = userDto.PhoneNumber;
            }
            if (user.Email != userDto.Email)
            {
                user.Email = userDto.Email;
            }
            if (user.VisibleName != userDto.VisibleName)
            {
                user.VisibleName = userDto.VisibleName;
            }
            if (!userDto.Password.IsNullOrEmpty())
            {
                user.Password = PasswordUtility.CreateUserPassword(user, userDto.Password);
                passwordChanged = userDto.Password;
            }
            if (user.Address != userDto.Address)
            {
                user.Address = userDto.Address;
            }
            if (user.DateOfBirth != userDto.DateOfBirth)
            {
                user.DateOfBirth = userDto.DateOfBirth ?? DateTime.MinValue;
            }
            if (user.FirstName != userDto.FirstName)
            {
                user.FirstName = userDto.FirstName;
            }
            if (user.LastName != userDto.LastName)
            {
                user.LastName = userDto.LastName;
            }
            user.OverwritePermissions(userDto.GetPermissions());

            return user;
        }
    }
}
