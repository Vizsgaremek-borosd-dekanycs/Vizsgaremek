using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Common.IAM.Authorization;
using vetcms.SharedModels.Common;

namespace vetcms.SharedModels.Features.PatientManagement
{
    public record DeletePatientApiCommand : AuthenticatedApiCommandBase<DeletePatientApiCommandResponse>
    {
        public List<int> Ids { get; set; }

        public DeletePatientApiCommand()
        {
        }

        public DeletePatientApiCommand(int id)
        {
            Ids = new List<int> { id };
        }

        public DeletePatientApiCommand(List<int> ids)
        {
            Ids = ids;
        }

        public DeletePatientApiCommand(params int[] ids)
        {
            Ids = ids.ToList();
        }

        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, "/api/v1/animal-management/animals/batch-delete");
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Post;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_DELETE_PATIENT];
        }
    }

    public class DeletePatientApiCommandValidator : AbstractValidator<DeletePatientApiCommand>
    {
        public DeletePatientApiCommandValidator()
        {
            RuleFor(x => x.Ids).NotEmpty().WithMessage("Id(k) nem maradhat(nak) üresen");
        }
    }

    public record DeletePatientApiCommandResponse : AuthenticatedCommandResult
    {
        public DeletePatientApiCommandResponse()
        {
        }

        public DeletePatientApiCommandResponse(bool _success, string _message = "")
        {
            Success = _success;
            Message = _message;
        }
    }
}
