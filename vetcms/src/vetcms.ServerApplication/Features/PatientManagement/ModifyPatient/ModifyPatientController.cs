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

namespace vetcms.ServerApplication.Features.PatientManagement.ModifyPatient
{
    public partial class PatientManagementController : ApiV1ControllerBase
    {
        [HttpPut("animals/{id}")]
        public async Task<ModifyPatientApiCommandResponse> UpdatePatient(int id,ModifyPatientApiCommand command)
        {
            command.Prepare(Request);
            command.Id = id;
            return await Mediator.Send(command);
        }
    }
}
