using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Common;
using vetcms.ServerApplication.Common.IAM;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.SharedModels.Common.ApiLogicExceptionHandling;
using vetcms.ServerApplication.Common.Abstractions.IAM;
using vetcms.SharedModels.Common.Dto;
using AutoMapper;

namespace vetcms.ServerApplication.Common.Behaviour
{
    public class PermissionRequirementBehaviour<TRequest, TResponse>(IAuthenticationCommon authenticationCommon, IMapper mapper) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : AuthenticatedApiCommandBase<TResponse>
    where TResponse : ICommandResult
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            User user = await authenticationCommon.GetUser(request.BearerToken);
            UserDto userDto = mapper.Map<UserDto>(user);
            if (user.GetPermissions().HasPermissionFlag(request.GetRequiredPermissions()) || request.ProcessSpecialPermissionQuery(userDto))
            {
                return await next();
            }
            else
            {
                throw new CommonApiLogicException(ApiLogicExceptionCode.INSUFFICIENT_PERMISSIONS, "Nem megfelelő hozzáférés.");
            }
        }
    }
}
