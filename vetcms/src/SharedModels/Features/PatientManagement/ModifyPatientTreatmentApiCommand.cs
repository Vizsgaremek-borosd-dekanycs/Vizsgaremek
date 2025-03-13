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

namespace vetcms.SharedModels.Features.PatientManagement
{
    public record ModifyPatientTreatmentApiCommand : AuthenticatedApiCommandBase<ModifyPatientTreatmentApiCommandResponse>
    {
        public int Id { get; set; }
        public TreatmentDto TreatmentModel { get; set; }
        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, $"/api/v1/animal-management/treatments/{Id}");
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Put;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_MODIFY_PATIENT_TREATMENT];
        }
    }

    public class ModifyPatientTreatmentApiCommandValidator : AbstractValidator<ModifyPatientTreatmentApiCommand>
    {
        public ModifyPatientTreatmentApiCommandValidator()
        {
            RuleFor(x => x.TreatmentModel.PatientId)
                .NotEmpty().WithMessage("A páciens nem lehet üres");
            RuleFor(x => x.TreatmentModel.Type)
                .NotEmpty().WithMessage("A típus nem lehet üres");
            RuleFor(x => x.TreatmentModel.DoctorId)
                .NotEmpty().WithMessage("Az orvos nem lehet üres");
        }
    }

    public record ModifyPatientTreatmentApiCommandResponse : AuthenticatedCommandResult
    {
        public ModifyPatientTreatmentApiCommandResponse()
        {

        }

        public ModifyPatientTreatmentApiCommandResponse(bool _success, string _message = "")
        {
            Success = _success;
            Message = _message;
        }
    }
}
