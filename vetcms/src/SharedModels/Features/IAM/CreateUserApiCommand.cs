using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Common.IAM.Authorization;
using static vetcms.SharedModels.Features.IAM.CreateUserApiCommandValidator;

namespace vetcms.SharedModels.Features.IAM
{
    public record CreateUserApiCommand : AuthenticatedApiCommandBase<CreateUserApiCommandResponse>
    {

        public UserDto NewUser { get; set; }

        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, "/api/v1/iam/user");
        }

        public EntityPermissions GetPermissions()
        {
            return new EntityPermissions(NewUser.PermissionSet);
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Post;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_ADD_NEW_USERS];
        }
    }

    public class CreateUserApiCommandValidator : AbstractValidator<CreateUserApiCommand>
    {
        public CreateUserApiCommandValidator()
        {
            RuleFor(x => x.NewUser.Email).NotEmpty().EmailAddress().WithMessage("Email mező nem maradhat üresen, és email formátumban kell lennie, pl.: kallapal@example.hu");
            RuleFor(x => x.NewUser.FirstName).NotEmpty().WithMessage("Keresznév nem maradhat üresen");
            RuleFor(x => x.NewUser.LastName).NotEmpty().WithMessage("Vezetéknév nem maradhat üresen");
            RuleFor(x => x.NewUser.PermissionSet).NotEmpty().Must(x => BigInteger.TryParse(x, out BigInteger result)).WithMessage("Hozzáférési engedély nem maradhat üresen");
            RuleFor(x => x.NewUser.VisibleName).NotEmpty().WithMessage("Megjelenített név nem maradhat üresen");
            RuleFor(x => x.NewUser.PhoneNumber).Length(11).WithMessage("Telefonszám nem megfelelő hosszúságú");
            RuleFor(x => x.NewUser.Address).NotEmpty().WithMessage("Lakcím nem maradhat üresen");
        }
    }


    public record CreateUserApiCommandResponse : ICommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public CreateUserApiCommandResponse()
        {    
        }

        public CreateUserApiCommandResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
