using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vetcms.ServerApplication.Common.Abstractions.Api;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ServerApplication.Features.PatientClassification.CreateAnimalType
{
    public partial class PatientClassificationController : ApiV1ControllerBase
    {
        [HttpPost("animal-breed")]
        public async Task<CreateAnimalBreedApiCommandResponse> CreatePatientType(CreateAnimalBreedApiCommand command)
        {
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
