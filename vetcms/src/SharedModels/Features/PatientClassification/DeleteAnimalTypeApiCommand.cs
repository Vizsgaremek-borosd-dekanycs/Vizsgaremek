using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Common.IAM.Authorization;

namespace vetcms.SharedModels.Features.PatientClassification
{
    public record DeleteAnimalTypeApiCommand : AuthenticatedApiCommandBase<DeleteAnimalTypeApiCommandResponse>
    {
        public List<int> Ids { get; set; }

        public DeleteAnimalTypeApiCommand()
        {
        }

        public DeleteAnimalTypeApiCommand(int id)
        {
            Ids = new List<int> { id };
        }

        public DeleteAnimalTypeApiCommand(List<int> ids)
        {
            Ids = ids;
        }

        public DeleteAnimalTypeApiCommand(params int[] ids)
        {
            Ids = ids.ToList();
        }

        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, "/api/v1/iam/animal-type/batch-delete");
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Post;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return new[] { PermissionFlags.CAN_DELETE_ANIMAL_TYPES };
        }
    }

    public class DeleteAnimalTypeApiCommandValidator : AbstractValidator<DeleteAnimalTypeApiCommand>
    {
        public DeleteAnimalTypeApiCommandValidator()
        {
            RuleFor(x => x.Ids).NotEmpty().WithMessage("Id(k) nem maradhat(nak) üresen");
        }
    }

    public record DeleteAnimalTypeApiCommandResponse : AuthenticatedCommandResult
    {
        public DeleteAnimalTypeApiCommandResponse()
        {
        }

        public DeleteAnimalTypeApiCommandResponse(bool _success, string _message = "")
        {
            Success = _success;
            Message = _message;
        }
    }
}
