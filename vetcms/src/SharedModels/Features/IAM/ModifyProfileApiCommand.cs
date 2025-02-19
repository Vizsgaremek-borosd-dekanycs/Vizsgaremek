using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Common.IAM.Authorization;

namespace vetcms.SharedModels.Features.IAM
{
    public record ModifyProfileApiCommand : AuthenticatedApiCommandBase<ModifyProfileApiCommandResponse>
    {
        public UserDto ModifiedProfile { get; set; }

        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, $"/api/v1/iam/edit-profile/{ModifiedProfile.Id}");
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Put;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_LOGIN];
        }
    }

    public class ModifyProfileApiCommandValidator : AbstractValidator<ModifyProfileApiCommand>
    {
        public ModifyProfileApiCommandValidator()
        {
            RuleFor(x => x.ModifiedProfile.Email).NotEmpty().EmailAddress().WithMessage("Email mező nem maradhat üresen, és email formátumban kell lennie, pl.: kallapal@example.hu");
            RuleFor(x => x.ModifiedProfile.FirstName).NotEmpty().WithMessage("Keresznév nem maradhat üresen");
            RuleFor(x => x.ModifiedProfile.LastName).NotEmpty().WithMessage("Vezetéknév nem maradhat üresen");
            RuleFor(x => x.ModifiedProfile.VisibleName).NotEmpty().WithMessage("Megjelenített név nem maradhat üresen");
            RuleFor(x => x.ModifiedProfile.PhoneNumber).Length(11).WithMessage("Telefonszám nem megfelelő hosszúságú");
            RuleFor(x => x.ModifiedProfile.Address).NotEmpty().WithMessage("Lakcím nem maradhat üresen");
        }
    }


    public record ModifyProfileApiCommandResponse : ICommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ModifyProfileApiCommandResponse()
        {

        }

        public ModifyProfileApiCommandResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
