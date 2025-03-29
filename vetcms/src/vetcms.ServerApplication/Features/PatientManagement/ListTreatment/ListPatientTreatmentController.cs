using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Api;
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ServerApplication.Features.PatientManagement.ListTreatment
{
    public partial class PatientTreatmentManagementController : ApiV1ControllerBase
    {
        [HttpGet("treatments")]
        public async Task<ListPatientTreatmentApiQueryResponse> GetTreatments([FromQuery(Name = "skip")] int skip = 0, [FromQuery(Name = "take")] int take = 10, [FromQuery(Name = "query")] string searchTerm = "")
        {
            ListPatientTreatmentApiQuery command = new ListPatientTreatmentApiQuery
            {
                Skip = skip,
                Take = take,
                SearchTerm = searchTerm
            };
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
