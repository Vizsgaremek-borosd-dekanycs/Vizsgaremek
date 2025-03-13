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
    public record CreatePatientTreatmentApiCommand : AuthenticatedApiCommandBase<CreatePatientTreatmentApiCommandResponse>
    {
        public TreatmentDto NewTreatment { get; set; }

        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, "/api/v1/animal-management/treatments");
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Post;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_ADD_PATIENT_TREATMENT];
        }
    }

    public class CreatePatientTreatmentApiCommandValidator : AbstractValidator<CreatePatientTreatmentApiCommand>
    {
        public CreatePatientTreatmentApiCommandValidator()
        {
            RuleFor(x => x.NewTreatment.PatientId)
                .NotEmpty().WithMessage("A páciens nem lehet üres");
            RuleFor(x => x.NewTreatment.Type)
                .NotEmpty().WithMessage("A típus nem lehet üres");
            RuleFor(x => x.NewTreatment.DoctorId)
                .NotEmpty().WithMessage("Az orvos nem lehet üres");
        }
    }


    public record CreatePatientTreatmentApiCommandResponse : AuthenticatedCommandResult
    {
        public CreatePatientTreatmentApiCommandResponse()
        {

        }

        public CreatePatientTreatmentApiCommandResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
