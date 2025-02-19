using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.SharedModels.Common.Abstract
{
    public record AuthenticatedCommandResult : ICommandResult
    {
        public UserDto? CurrentUser { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
