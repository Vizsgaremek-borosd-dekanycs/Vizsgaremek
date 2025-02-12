using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Api;
using vetcms.SharedModels.Common.Abstract;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ServerApplication.Features.IAM.ModifyOtherUser
{
    public partial class IamController : ApiV1ControllerBase
    {
        [HttpPut("users/{id}")]
        public async Task<ICommandResult> ModifyOtherUser(int id, ModifyOtherUserApiCommand command)
        {
            command.Id = id;
            command.Prepare(Request);
            return await Mediator.Send(command);
        }
    }
}
