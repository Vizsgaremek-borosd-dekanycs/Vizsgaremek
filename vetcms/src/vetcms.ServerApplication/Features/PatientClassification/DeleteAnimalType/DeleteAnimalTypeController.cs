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
        [HttpPost("animal-type/batch-delete")]
        public async Task<DeleteAnimalTypeApiCommandResponse> DeleteAnimalType(DeleteAnimalTypeApiCommand command)
        {
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
 