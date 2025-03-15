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

namespace vetcms.ServerApplication.Features.PatientManagement.DeleteTreatment
{
    public partial class AnimalTreatmentManagementController : ApiV1ControllerBase
    {
        [HttpPost("treatments/batch-delete")]
        public async Task<DeletePatientTreatmentApiCommandResponse> CreatePatientType(DeletePatientTreatmentApiCommand command)
        {
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
