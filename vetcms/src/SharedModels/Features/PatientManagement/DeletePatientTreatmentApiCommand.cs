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
    public record DeletePatientTreatmentApiCommand : AuthenticatedApiCommandBase<DeletePatientTreatmentApiCommandResponse>
    {
        public List<int> Ids { get; set; }

        public DeletePatientTreatmentApiCommand()
        {
        }

        public DeletePatientTreatmentApiCommand(int id)
        {
            Ids = new List<int> { id };
        }

        public DeletePatientTreatmentApiCommand(List<int> ids)
        {
            Ids = ids;
        }

        public DeletePatientTreatmentApiCommand(params int[] ids)
        {
            Ids = ids.ToList();
        }

        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, "/api/v1/animal-management/treatments/batch-delete");
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Post;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_DELETE_PATIENT_TREATMENT];
        }
    }

    public class DeletePatientTreatmentApiCommandValidator : AbstractValidator<DeletePatientTreatmentApiCommand>
    {
        public DeletePatientTreatmentApiCommandValidator()
        {
            RuleFor(x => x.Ids).NotEmpty().WithMessage("Id(k) nem maradhat(nak) üresen");
        }
    }

    public record DeletePatientTreatmentApiCommandResponse : AuthenticatedCommandResult
    {
        public DeletePatientTreatmentApiCommandResponse()
        {
        }

        public DeletePatientTreatmentApiCommandResponse(bool _success, string _message = "")
        {
            Success = _success;
            Message = _message;
        }
    }
}
