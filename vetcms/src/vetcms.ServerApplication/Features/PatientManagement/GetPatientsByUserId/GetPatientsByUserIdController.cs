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

namespace vetcms.ServerApplication.Features.PatientManagement.GetPatientsByUserId
{
    public partial class PatientManagementController : ApiV1ControllerBase
    {
        [HttpGet("animals/{id}")]
        public async Task<GetPatientsByUserIdApiQueryResponse> GetPatientsByUserId(int id)
        {
            GetPatientsByUserIdApiQuery command = new GetPatientsByUserIdApiQuery
            {
                UserId = id
            };
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}

