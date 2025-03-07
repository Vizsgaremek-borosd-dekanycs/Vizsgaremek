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
    public record ModifyAnimalBreedApiCommand : AuthenticatedApiCommandBase<ModifyAnimalBreedApiCommandResponse>
    {
        public int Id { get; set; }
        public AnimalBreedDto AnimalBreedModel { get; set; }
        public override string GetApiEndpoint()
        {
            return $"/api/v1/patient-classification/animal-breed/{Id}";
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Put;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_MODIFY_ANIMAL_BREED];
        }
    }

    public class ModifyAnimalBreedApiCommanddValidator : AbstractValidator<ModifyAnimalBreedApiCommand>
    {

        public ModifyAnimalBreedApiCommanddValidator()
        {
            RuleFor(x => x.AnimalBreedModel.TypeId)
                .NotEmpty().WithMessage("A típus nem lehet üres");

            RuleFor(x => x.AnimalBreedModel.BreedName)
                .NotEmpty().WithMessage("A fajta neve nem lehet üres")
                .MaximumLength(100).WithMessage("A fajta neve nem haladhatja meg a 100 karaktert");

            RuleFor(x => x.AnimalBreedModel.Charachteristics)
                .NotEmpty().WithMessage("A jellemzők nem lehetnek üresek")
                .MaximumLength(1000).WithMessage("A jellemzők nem haladhatják meg az 1000 karaktert");
        }
    }


    public record ModifyAnimalBreedApiCommandResponse : AuthenticatedCommandResult
    {
        public AnimalBreedDto AnimalBreedData { get; set; }
        public ModifyAnimalBreedApiCommandResponse()
        {

        }

        public ModifyAnimalBreedApiCommandResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
