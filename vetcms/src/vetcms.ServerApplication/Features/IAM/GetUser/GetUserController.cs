using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vetcms.ServerApplication.Common.Abstractions.Api;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ServerApplication.Features.IAM.GetUser
{
    public partial class IamController : ApiV1ControllerBase
    {
        [HttpGet("user")]
        public async Task<ICommandResult> GetUsers([FromQuery(Name = "skip")] int skip = 0, [FromQuery(Name = "take")] int take = 10, [FromQuery(Name = "query")] string searchTerm = "")
        {
            ListUsersApiCommand command = new ListUsersApiCommand
            {
                Skip = skip,
                Take = take,
                SearchTerm = searchTerm
            };
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
