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
        [HttpGet("users/{id}")]
        public async Task<ICommandResult> GetUsers(int id)
        {
            GetUserApiQuery command = new GetUserApiQuery()
            {
                UserId = id
            };
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
