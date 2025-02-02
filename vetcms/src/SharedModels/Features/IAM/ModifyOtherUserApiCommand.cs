using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Common.IAM.Authorization;

namespace vetcms.SharedModels.Features.IAM
{
    public record ModifyOtherUserApiCommand : AuthenticatedApiCommandBase<ModifyOtherUserApiCommandResponse>
    {

        public int Id { get; set; }

        /// <summary>
        /// A felhasználó e-mail címe.
        /// </summary>
        /// 
        public UserDto ModifiedUser { get; set; }

        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, "/api/v1/iam/modify-other-user");
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Put;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_MODIFY_OTHER_USER];
        }

        public EntityPermissions GetPermissions()
        {
            return new EntityPermissions(ModifiedUser.PermissionSet);
        }
    }

    public class ModifyOtherUserApiCommandValidator : AbstractValidator<ModifyOtherUserApiCommand>
    {
        public ModifyOtherUserApiCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.ModifiedUser.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.ModifiedUser.VisibleName).NotEmpty();
            RuleFor(x => x.ModifiedUser.PhoneNumber).Length(11);
            RuleFor(x => x.ModifiedUser.PermissionSet).NotEmpty().Must(x => BigInteger.TryParse(x, out BigInteger result));
        }
    }


    public record ModifyOtherUserApiCommandResponse : ICommandResult
    {
        public bool Success { get ; set ; }
        public string Message { get ; set ; }
        public ModifyOtherUserApiCommandResponse()
        {
            
        }

        public ModifyOtherUserApiCommandResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
