using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.IAM;
using vetcms.ServerApplication.Common.IAM;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.SharedModels.Common;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Common.ApiLogicExceptionHandling;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ServerApplication.Common.Behaviour
{
    public class UserValidationBehavior<TRequest, TResponse>(IAuthenticationCommon authenticationCommon, IMapper mapper) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : AuthenticatedApiCommandBase<TResponse>
        where TResponse : AuthenticatedCommandResult
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!request.BearerToken.IsNullOrEmpty() && await authenticationCommon.ValidateToken(request.BearerToken))
            {
                TResponse result = await next();
                User currentUser = await authenticationCommon.GetUser(request.BearerToken);
                result.CurrentUser = mapper.Map<UserDto>(currentUser);

                return result;
            }
            else
            {
                throw new CommonApiLogicException(ApiLogicExceptionCode.INVALID_AUTHENTICATION, "Hibás bearer token.");
            }
        }
    }
}
