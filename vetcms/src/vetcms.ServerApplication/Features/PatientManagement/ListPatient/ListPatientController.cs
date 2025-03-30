using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Api;
using vetcms.SharedModels.Features.PatientClassification;
using vetcms.SharedModels.Features.PatientManagement;

namespace vetcms.ServerApplication.Features.PatientManagement.ListPatient
{
    public partial class PatientManagementController : ApiV1ControllerBase
    {
        [HttpGet("animals")]
        public async Task<ListPatientApiQueryResponse> GetPatients([FromQuery(Name = "skip")] int skip = 0, [FromQuery(Name = "take")] int take = 10, [FromQuery(Name = "query")] string searchTerm = "", [FromQuery(Name = "ownerid")] string ownerId = "")
        {
            ListPatientApiQuery command = new ListPatientApiQuery
            {
                Skip = skip,
                Take = take,
                SearchTerm = searchTerm
            };
            if(int.TryParse(ownerId, out int ownerIdInt))
            {
                command.OwnerId = ownerIdInt;
            }
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
