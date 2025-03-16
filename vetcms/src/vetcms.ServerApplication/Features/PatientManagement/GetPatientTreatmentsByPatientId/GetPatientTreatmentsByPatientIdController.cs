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

namespace vetcms.ServerApplication.Features.PatientManagement.GetTreatmentsByPatientId
{
    public partial class PatientTreatmentManagementController : ApiV1ControllerBase
    {
        [HttpGet("treatments/patient/{id}")]
        public async Task<GetPatientTreatmentsByPatientIdApiQueryResponse> GetPatientTreatmentByPatientId(int id)
        {
            GetPatientTreatmentsByPatientIdApiQuery command = new GetPatientTreatmentsByPatientIdApiQuery
            {
                PatientId = id
            };
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
