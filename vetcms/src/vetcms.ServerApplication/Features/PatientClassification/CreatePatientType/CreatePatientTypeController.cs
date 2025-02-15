using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vetcms.ServerApplication.Common.Abstractions.Api;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ServerApplication.Features.PatientClassification.CreatePatientType
{
    public partial class PatientClassificationController : ApiV1ControllerBase
    {
        [HttpPost("patient-type")]
        public async Task<CreatePatientTypeApiCommandResponse> CreatePatientType(CreatePatientTypeApiCommand command)
        {
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
 