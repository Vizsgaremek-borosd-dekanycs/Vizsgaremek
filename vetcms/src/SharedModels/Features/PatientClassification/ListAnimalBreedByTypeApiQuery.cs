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
    public record ListAnimalBreedByTypeApiQuery : AuthenticatedApiCommandBase<ListAnimalBreedByTypeApiQueryResponse>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
        public int TypeId { get; set; }

        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, $"/api/v1/patient-classification/animal-type/{TypeId}/breeds?skip={Skip}&take={Take}");
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Get;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_VIEW_ANIMAL_BREED];
        }
    }

    public class ListAnimalBreedByTypeApiQueryValidator : AbstractValidator<ListAnimalBreedByTypeApiQuery>
    {
        public ListAnimalBreedByTypeApiQueryValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThanOrEqualTo(0).WithMessage("A Skip értéke nem lehet kisebb, mint 0");

            RuleFor(x => x.TypeId)
                .GreaterThanOrEqualTo(0).WithMessage("A típus megadása kötelező!");

            RuleFor(x => x.Take)
                .GreaterThan(0).WithMessage("A Take értéke nagyobb kell legyen, mint 0")
                .LessThanOrEqualTo(100).WithMessage("A Take értéke nem lehet nagyobb, mint 100");
        }
    }

    public record ListAnimalBreedByTypeApiQueryResponse : AuthenticatedCommandResult
    {
        public int ResultCount { get; set; } = 0;
        public List<AnimalBreedDto> AnimalBreeds { get; set; } = new List<AnimalBreedDto>();

        public ListAnimalBreedByTypeApiQueryResponse()
        {
        }

        public ListAnimalBreedByTypeApiQueryResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}