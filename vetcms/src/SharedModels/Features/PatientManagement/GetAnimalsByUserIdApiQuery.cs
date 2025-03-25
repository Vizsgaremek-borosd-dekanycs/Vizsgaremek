using FluentValidation;
using vetcms.SharedModels.Common;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Common.Dto;
using vetcms.SharedModels.Common.IAM.Authorization;

namespace vetcms.SharedModels.Features.PatientManagement
{
    public record GetPatientsByUserIdApiQuery : AuthenticatedApiCommandBase<GetPatientsByUserIdApiQueryResponse>
    {
        public int UserId { get; set; } = 0;
        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, $"/api/v1/patient-management/animals/by-user-id/{UserId}");
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Get;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_LOGIN];
        }
    }

    public class GetPatientsByUserIdApiQueryValidator : AbstractValidator<GetPatientsByUserIdApiQuery>
    {
        public GetPatientsByUserIdApiQueryValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("Az azonosítónak nagyobbnak kell lennie 0-nál.");
        }
    }

    public record GetPatientsByUserIdApiQueryResponse : AuthenticatedCommandResult
    {
        public List<PatientDto> Animals { get; set; }
        public GetPatientsByUserIdApiQueryResponse()
        {
        }
        public GetPatientsByUserIdApiQueryResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
