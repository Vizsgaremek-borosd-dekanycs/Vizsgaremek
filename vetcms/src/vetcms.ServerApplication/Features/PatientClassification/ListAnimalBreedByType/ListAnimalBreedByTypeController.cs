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

namespace vetcms.ServerApplication.Features.PatientClassification.ListAnimalBreed
{
    public partial class PatientClassificationController : ApiV1ControllerBase
    {
        [HttpGet("animal-type/{typeId}/breeds")]
        public async Task<ListAnimalBreedByTypeApiQueryResponse> GetAnimalBreedsByType(int typeId, [FromQuery(Name = "skip")] int skip = 0, [FromQuery(Name = "take")] int take = 10)
        {
            ListAnimalBreedByTypeApiQuery command = new ListAnimalBreedByTypeApiQuery
            {
                Skip = skip,
                Take = take,
                TypeId = typeId
            };
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
