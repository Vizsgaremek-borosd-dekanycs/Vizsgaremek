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

namespace vetcms.ServerApplication.Features.PatientManagement.DeletePatient
{
    public partial class PatientManagementController : ApiV1ControllerBase
    {
        [HttpPost("animals/batch-delete")]
        public async Task<DeletePatientApiCommandResponse> CreatePatientType(DeletePatientApiCommand command)
        {
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
