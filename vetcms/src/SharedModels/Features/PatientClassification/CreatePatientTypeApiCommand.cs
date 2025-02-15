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
using vetcms.SharedModels.Features.IAM;

namespace vetcms.SharedModels.Features.PatientClassification
{
    public record CreatePatientTypeApiCommand : AuthenticatedApiCommandBase<CreatePatientTypeApiCommandResponse>
    {
        public AnimalTypeDto PatientTypeModel { get; set; }
        public override string GetApiEndpoint()
        {
            return "/api/v1/patient-classification/patient-type";
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Post;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_ADD_PATIENT_TYPE];
        }
    }

    public class CreatePatientTypeApiCommandValidator : AbstractValidator<CreatePatientTypeApiCommand>
    {
        public CreatePatientTypeApiCommandValidator()
        {
            RuleFor(x => x.PatientTypeModel.TypeName)
                .NotEmpty().WithMessage("A típus neve nem lehet üres")
                .MaximumLength(100).WithMessage("A típus neve nem haladhatja meg a 100 karaktert");

            RuleFor(x => x.PatientTypeModel.Description)
                .NotEmpty().WithMessage("A leírás nem lehet üres")
                .MaximumLength(500).WithMessage("A leírás nem haladhatja meg az 500 karaktert");
        }
    }


    public record CreatePatientTypeApiCommandResponse : ICommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public AnimalTypeDto PatientTypeModel { get; set; }
        public CreatePatientTypeApiCommandResponse()
        {

        }

        public CreatePatientTypeApiCommandResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
