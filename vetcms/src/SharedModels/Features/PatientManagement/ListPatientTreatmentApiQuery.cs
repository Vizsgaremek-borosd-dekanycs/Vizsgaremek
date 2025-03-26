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
    public record ListPatientTreatmentApiQuery : AuthenticatedApiCommandBase<ListPatientTreatmentApiQueryResponse>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
        public string SearchTerm { get; set; } = string.Empty;

        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, $"/api/v1/patient-treatment-management/treatments?skip={Skip}&take={Take}&query={SearchTerm}");
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

    public class ListPatientTreatmentApiQueryValidator : AbstractValidator<ListPatientTreatmentApiQuery>
    {
        public ListPatientTreatmentApiQueryValidator()
        {
            RuleFor(x => x.Skip)
                .GreaterThanOrEqualTo(0).WithMessage("A Skip értéke nem lehet kisebb, mint 0");

            RuleFor(x => x.Take)
                .GreaterThan(0).WithMessage("A Take értéke nagyobb kell legyen, mint 0")
                .LessThanOrEqualTo(100).WithMessage("A Take értéke nem lehet nagyobb, mint 100");

            RuleFor(x => x.SearchTerm)
                .MaximumLength(100).WithMessage("A keresési kifejezés nem haladhatja meg a 100 karaktert");
        }
    }


    public record ListPatientTreatmentApiQueryResponse : AuthenticatedCommandResult
    {
        public int ResoultCount { get; set; } = 0;
        public List<TreatmentDto> Treatments { get; set; } = new List<TreatmentDto>();
        public ListPatientTreatmentApiQueryResponse()
        {

        }

        public ListPatientTreatmentApiQueryResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }

}
