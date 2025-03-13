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
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.SharedModels.Features.PatientManagement
{
    public record CreatePatientApiCommand : AuthenticatedApiCommandBase<CreatePatientApiCommandResponse>
    {
        public PatientDto NewPatient { get; set; }

        public override string GetApiEndpoint()
        {
            return "/api/v1/animal-management/animals";
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Post;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_ADD_PATIENT];
        }
    }

    public class CreatePatientApiCommandValidator : AbstractValidator<CreatePatientApiCommand>
    {
        public CreatePatientApiCommandValidator()
        {
            RuleFor(x => x.NewPatient.Name)
                .NotEmpty().WithMessage("Az állat neve nem lehet üres")
                .MaximumLength(100).WithMessage("Az állat neve nem haladhatja meg a 100 karaktert");
            RuleFor(x => x.NewPatient.Weight)
                .GreaterThan(0).WithMessage("A súly nem lehet nulla vagy annál kisebb");
            RuleFor(x => x.NewPatient.MicrochipNumber)
                .NotEmpty().WithMessage("A mikrocsip száma nem lehet üres");
            RuleFor(x => x.NewPatient.ChronicDiseases)
                .NotEmpty().WithMessage("A krónikus betegségek nem lehetnek üresek");
            RuleFor(x => x.NewPatient.OwnerId)
                .NotEmpty().WithMessage("A tulajdonos nem lehet üres");
            RuleFor(x => x.NewPatient.TypeId)
                .NotEmpty().WithMessage("A fajta nem lehet üres");
            RuleFor(x => x.NewPatient.BreedId)
                .NotEmpty().WithMessage("A faj nem lehet üres");
        }
    }


    public record CreatePatientApiCommandResponse : AuthenticatedCommandResult
    {
        public CreatePatientApiCommandResponse()
        {

        }

        public CreatePatientApiCommandResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
