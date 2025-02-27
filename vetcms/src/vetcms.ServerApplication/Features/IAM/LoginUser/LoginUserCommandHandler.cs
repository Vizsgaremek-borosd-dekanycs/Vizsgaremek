﻿using MediatR;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.ServerApplication.Infrastructure.Presistence;
using vetcms.ServerApplication.Infrastructure.Presistence.Repository;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Features.IAM;
using vetcms.SharedModels.Common.IAM.Authorization;
using vetcms.ServerApplication.Common.IAM;
using vetcms.SharedModels.Common.ApiLogicExceptionHandling;
using System.Runtime.CompilerServices;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Common.Abstractions.IAM;
using AutoMapper;
using vetcms.SharedModels.Common.Dto;

[assembly: InternalsVisibleTo("vetcms.ServerApplicationTests")]
namespace vetcms.ServerApplication.Features.IAM.LoginUser
{
    internal class LoginUserCommandHandler : IRequestHandler<LoginUserApiCommand, LoginUserApiCommandResponse>
    {
        private readonly IUserRepository userRepository;
        private readonly IAuthenticationCommon authenticationCommon;
        private readonly IMapper? mapper;
        public LoginUserCommandHandler(IUserRepository _userRepository, IAuthenticationCommon _authenticationCommon, IMapper _mapper)
        {
            userRepository = _userRepository;
            authenticationCommon = _authenticationCommon;
            mapper = _mapper;
        }
        public LoginUserCommandHandler(IUserRepository _userRepository, IAuthenticationCommon _authenticationCommon)
        {
            userRepository = _userRepository;
            authenticationCommon = _authenticationCommon;
        }

        public Task<LoginUserApiCommandResponse> Handle(LoginUserApiCommand request, CancellationToken cancellationToken)
        {
            // kapcsolódó logika
            User user;

            try
            {
                user = userRepository.GetByEmail(request.Email);
            }
            catch (InvalidOperationException ex) 
            {
                return Task.FromResult(new LoginUserApiCommandResponse()
                {
                    Success = false,
                    Message = "Nem létező felhasználó"
                });
            }

            if (user.GetPermissions().HasPermissionFlag(PermissionFlags.CAN_LOGIN))
            {
                if(PasswordUtility.VerifyPassword(request.Password, user.Password))
                {
                    UserDto? currentUserDto = null;
                    if(mapper != null)
                    {
                        currentUserDto = mapper.Map<UserDto>(user);
                    }
                    


                    return Task.FromResult(new LoginUserApiCommandResponse()
                    {
                        Success = true,
                        PermissionSet = user.GetPermissions().ToString(),
                        AccessToken = authenticationCommon.GenerateAccessToken(user),
                        CurrentUser = currentUserDto
                    });
                }
                return Task.FromResult(new LoginUserApiCommandResponse()
                {
                    Success = false,
                    Message = "Hibás jelszó"
                });
            }

            return Task.FromResult(new LoginUserApiCommandResponse()
            {
                Success = false,
                Message = "Hibás bejelentkezési adatok."
            });

            throw new NotImplementedException();
        }
    }
}
