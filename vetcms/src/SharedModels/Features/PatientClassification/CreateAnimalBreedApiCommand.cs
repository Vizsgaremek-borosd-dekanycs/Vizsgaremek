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
    public record CreateAnimalBreedApiCommand : AuthenticatedApiCommandBase<CreateAnimalBreedApiCommandResponse>
    {
        public AnimalBreedDto AnimalBreedData { get; set; }
        public override string GetApiEndpoint()
        {
            return "/api/v1/patient-classification/animal-breed";
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Post;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_ADD_ANIMAL_BREED];
        }
    }

    public class CreateAnimalBreedApiCommandValidator : AbstractValidator<CreateAnimalBreedApiCommand>
    {
        public CreateAnimalBreedApiCommandValidator()
        {
            RuleFor(x => x.AnimalBreedData.BreedName)
                .NotEmpty().WithMessage("A fajta neve nem lehet üres")
                .MaximumLength(100).WithMessage("A fajta neve nem haladhatja meg a 100 karaktert");

            RuleFor(x => x.AnimalBreedData.Charachteristics)
                .NotEmpty().WithMessage("A jellemzők nem lehetnek üresek")
                .MaximumLength(500).WithMessage("A jellemzők nem haladhatják meg az 500 karaktert");
        }
    }

    public record CreateAnimalBreedApiCommandResponse : AuthenticatedCommandResult
    {
        public AnimalBreedDto AnimalBreedData { get; set; }
        public CreateAnimalBreedApiCommandResponse()
        {

        }

        public CreateAnimalBreedApiCommandResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
