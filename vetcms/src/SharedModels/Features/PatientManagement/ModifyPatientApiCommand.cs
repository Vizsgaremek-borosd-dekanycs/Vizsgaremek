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
    public record ModifyPatientApiCommand : AuthenticatedApiCommandBase<ModifyPatientApiCommandResponse>
    {
        public int Id { get; set; }
        public PatientDto PatientModel { get; set; }
        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, $"/api/v1/patient-management/animals/{Id}");
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Put;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_MODIFY_PATIENT];
        }
    }

    public class ModifyPatientApiCommandValidator : AbstractValidator<ModifyPatientApiCommand>
    {

        public ModifyPatientApiCommandValidator()
        {
            RuleFor(x => x.PatientModel.Name)
                .NotEmpty().WithMessage("Az állat neve nem lehet üres")
                .MaximumLength(100).WithMessage("Az állat neve nem haladhatja meg a 100 karaktert");
            RuleFor(x => x.PatientModel.Weight)
                .GreaterThan(0).WithMessage("A súly nem lehet nulla vagy annál kisebb");
            RuleFor(x => x.PatientModel.MicrochipNumber)
                .NotEmpty().WithMessage("A mikrocsip száma nem lehet üres");
            RuleFor(x => x.PatientModel.ChronicDiseases)
                .NotEmpty().WithMessage("A krónikus betegségek nem lehetnek üresek");
            RuleFor(x => x.PatientModel.OwnerId)
                .NotEmpty().WithMessage("A tulajdonos nem lehet üres");
            RuleFor(x => x.PatientModel.TypeId)
                .NotEmpty().WithMessage("A fajta nem lehet üres");
            RuleFor(x => x.PatientModel.BreedId)
                .NotEmpty().WithMessage("A faj nem lehet üres");
        }
    }

    public record ModifyPatientApiCommandResponse : AuthenticatedCommandResult
    {
        public ModifyPatientApiCommandResponse()
        {

        }

        public ModifyPatientApiCommandResponse(bool _success, string _message = "")
        {
            Success = _success;
            Message = _message;
        }
    }
}
