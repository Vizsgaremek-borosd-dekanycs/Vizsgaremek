﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Api;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ServerApplication.Features.IAM.FirstTimeAuthenticateUser
{
    public partial class IamController : ApiV1ControllerBase
    {
        [HttpPost("user-initial-auth")]
        public async Task<ICommandResult>  InitialUserAuthorisation(FirstTimeAuthenticateUserApiCommand command)
        {
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
