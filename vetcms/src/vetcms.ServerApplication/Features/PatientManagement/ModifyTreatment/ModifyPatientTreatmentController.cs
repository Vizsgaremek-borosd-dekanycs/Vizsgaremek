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

namespace vetcms.ServerApplication.Features.PatientManagement.ModifyTreatment
{
    public partial class PatientTreatmentManagementController : ApiV1ControllerBase
    {
        [HttpPost("treatments/{id}")]
        public async Task<ModifyPatientTreatmentApiCommandResponse> UpdateTreatment(int id, ModifyPatientTreatmentApiCommand command)
        {
            command.Prepare(Request);
            command.Id = id;
            return await Mediator.Send(command);
        }
    }
}
