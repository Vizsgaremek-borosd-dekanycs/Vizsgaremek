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

namespace vetcms.ServerApplication.Features.PatientManagement.GetTreatment
{
    public partial class PatientTreatmentManagementController : ApiV1ControllerBase
    {
        [HttpGet("treatments/{id}")]
        public async Task<GetPaitentTreatmentApiQueryResponse> GetPatientTreatmentById(int id)
        {
            GetPaitentTreatmentApiQuery command = new GetPaitentTreatmentApiQuery
            {
                TreatmentId = id
            };
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
