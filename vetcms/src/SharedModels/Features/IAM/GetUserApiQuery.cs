using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using vetcms.SharedModels.Common;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Common.IAM.Authorization;

namespace vetcms.SharedModels.Features.IAM
{
    public record GetUserApiQuery : AuthenticatedApiCommandBase<GetUserApiQueryResponse>
    {
        public int UserId { get; set; }

        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, $"/api/v1/iam/users/{UserId}");
        }

        public override HttpMethodEnum GetApiMethod()
            => HttpMethodEnum.Get;

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_VIEW_OTHER_USER];
        }

        public override bool ProcessSpecialPermissionQuery(UserDto executorUser)
        {
            return UserId == executorUser.Id;
        }
    }

    public class GetUserApiQueryValidator : AbstractValidator<GetUserApiQuery>
    {
        public GetUserApiQueryValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Id nem maradhat üresen");
        }
    }

    public record GetUserApiQueryResponse : AuthenticatedCommandResult
    {
        public UserDto User { get; set; }

        public GetUserApiQueryResponse()
        {
        }

        public GetUserApiQueryResponse(bool _success, string _message = "")
        {
            Success = _success;
            Message = _message;
        }
    }
}
