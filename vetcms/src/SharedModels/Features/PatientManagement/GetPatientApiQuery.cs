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
    public record GetPatientApiQuery : AuthenticatedApiCommandBase<GetPatientApiQueryResponse>
    {
        public int PatientId { get; set; } = 0;

        public override string GetApiEndpoint()
        {
            return $"/api/v1/animal-management/animals/{PatientId}";
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Get;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_VIEW_PATIENTS];
        }
    }

    public class GetPatientApiQueryValidator : AbstractValidator<GetPatientApiQuery>
    {
        public GetPatientApiQueryValidator()
        {
            RuleFor(x => x.PatientId)
                .GreaterThan(0)
                .WithMessage("Az azonosítónak nagyobbnak kell lennie 0-nál.");
        }
    }


    public record GetPatientApiQueryResponse : AuthenticatedCommandResult
    {
        public PatientDto PatientModel { get; set; }

        public GetPatientApiQueryResponse()
        {

        }

        public GetPatientApiQueryResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }

    }
}
