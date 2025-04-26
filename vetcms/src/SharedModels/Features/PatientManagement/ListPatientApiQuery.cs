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
    public record ListPatientApiQuery : AuthenticatedApiCommandBase<ListPatientApiQueryResponse>
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
        public string SearchTerm { get; set; } = string.Empty;
        public int? OwnerId { get; set; }
        public override string GetApiEndpoint()
        {
            return Path.Join(ApiBaseUrl, $"/api/v1/patient-management/animals?skip={Skip}&take={Take}&query={SearchTerm}&ownerid={OwnerId}");
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


    public class ListPatientApiQueryValidator : AbstractValidator<ListPatientApiQuery>
    {
        public ListPatientApiQueryValidator()
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

    public record ListPatientApiQueryResponse : AuthenticatedCommandResult
    {
        public int ResoultCount { get; set; } = 0;
        public List<PatientDto> Patients { get; set; } = new List<PatientDto>();
        public ListPatientApiQueryResponse()
        {

        }

        public ListPatientApiQueryResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }
}
