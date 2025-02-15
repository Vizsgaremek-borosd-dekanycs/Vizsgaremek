using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.SharedModels.Common.Abstract
{
    /// <summary>
    /// Az API parancsok eredményét reprezentáló osztály.
    /// </summary>
    public record ICommandResult
    {
        /// <summary>
        /// Az API parancs sikerességét jelző tulajdonság.
        /// </summary>
        public virtual bool Success { get; set; }

        /// <summary>
        /// Az API parancs üzenetét tartalmazó tulajdonság.
        /// </summary>
        public virtual string Message { get; set; }

        public UserDto CurrentUser { get; set; }
    }
}
