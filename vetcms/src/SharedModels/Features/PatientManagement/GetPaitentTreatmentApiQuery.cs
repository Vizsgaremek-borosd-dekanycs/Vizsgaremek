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
    public record GetPaitentTreatmentApiQuery : AuthenticatedApiCommandBase<GetPaitentTreatmentApiQueryResponse>
    {
        public int TreatmentId { get; set; }
        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, $"/api/v1/animal-management/treatments/{TreatmentId}");
        }

        public override HttpMethodEnum GetApiMethod()
        {
            return HttpMethodEnum.Get;
        }

        public override PermissionFlags[] GetRequiredPermissions()
        {
            return [PermissionFlags.CAN_VIEW_PATIENT_TREATMENTS];
        }
    }

    public class GetPaitentTreatmentApiQueryValidator : AbstractValidator<GetPaitentTreatmentApiQuery>
    {
        public GetPaitentTreatmentApiQueryValidator()
        {
            RuleFor(x => x.TreatmentId)
                .GreaterThan(0)
                .WithMessage("Az azonosítónak nagyobbnak kell lennie 0-nál.");
        }
    }


    public record GetPaitentTreatmentApiQueryResponse : AuthenticatedCommandResult
    {
        public TreatmentDto TreatmentModel { get; set; }
        public GetPaitentTreatmentApiQueryResponse()
        {
            
        }

        public GetPaitentTreatmentApiQueryResponse(bool _success, string _message = "")
        {
            Success = _success;
            Message = _message;
        }
    }
}
