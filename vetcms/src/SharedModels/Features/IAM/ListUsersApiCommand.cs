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
    public record ListUsersApiCommand : AuthenticatedApiCommandBase<ListUsersApiCommandResponse>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
        public string SearchTerm { get; set; } = string.Empty;


        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, $"/api/v1/iam/users?skip={Skip}&take={Take}&query={SearchTerm}");
        }

        public override HttpMethodEnum GetApiMethod()
            => HttpMethodEnum.Get;

        public override PermissionFlags[] GetRequiredPermissions()
            => [PermissionFlags.CAN_LIST_USERS];
    }
    public class ListUsersApiCommandValidator : AbstractValidator<ListUsersApiCommand>
    {
        public ListUsersApiCommandValidator()
        {
            RuleFor(x => x.Take).NotEmpty().WithMessage("Felhasználók száma nem maradhat üresen");
        }
    }

    public record ListUsersApiCommandResponse : ICommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int ResultCount { get; set; } = 0;
        public List<UserDto> Users { get; set; } = new List<UserDto>();

        public ListUsersApiCommandResponse()
        {
        }

        public ListUsersApiCommandResponse(bool _success, string _message = "")
        {
            Success = _success;
            Message = _message;
        }

        public ListUsersApiCommandResponse(bool _success, List<UserDto> _users, string _message = "")
        {
            Success = _success;
            Message = _message;
            Users = _users;
        }
    }
}
