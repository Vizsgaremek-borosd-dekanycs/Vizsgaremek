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
    public record GetAnimalTypeByIdApiQuery : AuthenticatedApiCommandBase<GetAnimalTypeByIdApiQueryResponse>
    {
        public int Id { get; set; } = 0;
        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, $"/api/v1/patient-classification/animal-type/{Id}");
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Get;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_VIEW_ANIMAL_TYPES];
        }
    }

    public class GetAnimalTypeByIdApiQueryValidator : AbstractValidator<GetAnimalTypeByIdApiQuery>
    {
        public GetAnimalTypeByIdApiQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Az azonosítónak nagyobbnak kell lennie 0-nál.");
        }
    }


    public record GetAnimalTypeByIdApiQueryResponse : AuthenticatedCommandResult
    {
        public AnimalTypeDto AnimalTypeModel { get; set; }
        public GetAnimalTypeByIdApiQueryResponse()
        {

        }

        public GetAnimalTypeByIdApiQueryResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
