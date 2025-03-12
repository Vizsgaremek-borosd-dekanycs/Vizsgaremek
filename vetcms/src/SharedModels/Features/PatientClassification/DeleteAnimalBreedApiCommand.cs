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
    public record DeleteAnimalBreedApiCommand : AuthenticatedApiCommandBase<DeleteAnimalBreedApiCommandResponse>
    {
        public List<int> Ids { get; set; }

        public DeleteAnimalBreedApiCommand()
        {
        }

        public DeleteAnimalBreedApiCommand(int id)
        {
            Ids = new List<int> { id };
        }

        public DeleteAnimalBreedApiCommand(List<int> ids)
        {
            Ids = ids;
        }

        public DeleteAnimalBreedApiCommand(params int[] ids)
        {
            Ids = ids.ToList();
        }

        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, "/api/v1/iam/animal-breed/batch-delete");
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Post;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return new[] { PermissionFlags.CAN_DELETE_ANIMAL_BREED };
        }
    }

    public class DeleteAnimalBreedApiCommandValidator : AbstractValidator<DeleteAnimalBreedApiCommand>
    {
        public DeleteAnimalBreedApiCommandValidator()
        {
            RuleFor(x => x.Ids).NotEmpty().WithMessage("Id(k) nem maradhat(nak) üresen");
        }
    }

    public record DeleteAnimalBreedApiCommandResponse : AuthenticatedCommandResult
    {
        public DeleteAnimalBreedApiCommandResponse()
        {
        }

        public DeleteAnimalBreedApiCommandResponse(bool _success, string _message = "")
        {
            Success = _success;
            Message = _message;
        }
    }
}
