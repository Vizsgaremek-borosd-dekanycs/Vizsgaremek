using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vetcms.ServerApplication.Common.Abstractions.Data
{
    public interface IApplicationConfiguration
    {
        public string? this[string key] { get; set; }
        public string GetJwtSecret();
        public string GetJwtAudience();
        public string GetJwtIssuer();
    }
}
