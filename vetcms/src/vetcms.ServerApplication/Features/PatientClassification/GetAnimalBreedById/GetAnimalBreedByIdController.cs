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
        [HttpGet("animal-breed/{id}")]
        public async Task<GetAnimalBreedByIdApiQueryResponse> GetAnimalBreedById(int id)
        {
            GetAnimalBreedByIdApiQuery command = new GetAnimalBreedByIdApiQuery
            {
                Id = id
            };
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
