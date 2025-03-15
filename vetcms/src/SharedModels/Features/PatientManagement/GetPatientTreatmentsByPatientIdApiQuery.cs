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

namespace vetcms.SharedModels.Features.PatientManagement
{
    public record GetPatientTreatmentsByPatientIdApiQuery : AuthenticatedApiCommandBase<GetPatientTreatmentsByPatientIdApiQueryResponse>
    {
        public int PatientId { get; set; } = 0;

        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, $"/api/v1/animal-management/treatments/{PatientId}");
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

    public class GetPatientTreatmentsByPatientIdApiQueryValidator : AbstractValidator<GetPatientTreatmentsByPatientIdApiQuery>
    {
        public GetPatientTreatmentsByPatientIdApiQueryValidator()
        {
            RuleFor(x => x.PatientId)
                .GreaterThan(0)
                .WithMessage("Az azonosítónak nagyobbnak kell lennie 0-nál.");
        }
    }

    public record GetPatientTreatmentsByPatientIdApiQueryResponse : AuthenticatedCommandResult
    {
        public List<TreatmentDto> Treatments { get; set; }
        public GetPatientTreatmentsByPatientIdApiQueryResponse()
        {
        }
        public GetPatientTreatmentsByPatientIdApiQueryResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
