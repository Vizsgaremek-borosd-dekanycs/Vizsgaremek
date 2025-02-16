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
        [HttpPut("animal-type/{id}")]
        public async Task<ModifyAnimalTypeApiCommandResponse> UpdateAnimalType(int id, ModifyAnimalTypeApiCommand command)
        {
            command.Prepare(Request);
            command.Id = id;
            return await Mediator.Send(command);
        }
    }
}
 