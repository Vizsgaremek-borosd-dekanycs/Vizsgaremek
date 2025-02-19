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
    public record ModifyAnimalTypeApiCommand : AuthenticatedApiCommandBase<ModifyAnimalTypeApiCommandResponse>
    {
        public int Id { get; set; }
        public AnimalTypeDto AnimalTypeModel { get; set; }
        public override string GetApiEndpoint()
        {
            return $"/api/v1/patient-classification/animal-type/{Id}";
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Put;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_MODIFY_ANIMAL_TYPES];
        }
    }

    public class ModifyAnimalTypeApiCommandValidator : AbstractValidator<ModifyAnimalTypeApiCommand>
    {
        public ModifyAnimalTypeApiCommandValidator()
        {
            RuleFor(x => x.AnimalTypeModel.TypeName)
                .NotEmpty().WithMessage("A típus neve nem lehet üres")
                .MaximumLength(100).WithMessage("A típus neve nem haladhatja meg a 100 karaktert");

            RuleFor(x => x.AnimalTypeModel.Description)
                .NotEmpty().WithMessage("A leírás nem lehet üres")
                .MaximumLength(500).WithMessage("A leírás nem haladhatja meg az 500 karaktert");
        }
    }


    public record ModifyAnimalTypeApiCommandResponse : AuthenticatedCommandResult
    {
        public AnimalTypeDto AnimalTypeData { get; set; }
        public ModifyAnimalTypeApiCommandResponse()
        {

        }

        public ModifyAnimalTypeApiCommandResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
