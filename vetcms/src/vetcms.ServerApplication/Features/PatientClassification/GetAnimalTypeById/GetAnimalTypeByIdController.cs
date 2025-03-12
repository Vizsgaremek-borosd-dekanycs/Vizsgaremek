using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using vetcms.ServerApplication.Common.Abstractions.Api;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Features.IAM;
using vetcms.SharedModels.Features.PatientClassification;

namespace vetcms.ServerApplication.Features.PatientClassification.ListAnimalType
{
    public partial class PatientClassificationController : ApiV1ControllerBase
    {
        [HttpGet("animal-type/{id}")]
        public async Task<GetAnimalTypeByIdApiQueryResponse> GetAnimalTypeById(int id)
        {
            GetAnimalTypeByIdApiQuery command = new GetAnimalTypeByIdApiQuery
            {
                Id = id
            };
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
