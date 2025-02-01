﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vetcms.ServerApplication.Infrastructure.Communication.Mail
{
    internal static class TemplateCatalog
    {
        public const string PasswordReset = "vetcms.ServerApplication.Infrastructure.Communication.Mail.MailTemplates.PasswordResetTemplate.html";
        public const string ModifyOtherUser = "vetcms.ServerApplication.Infrastructure.Communication.Mail.MailTemplates.ModifyOtherUserTemplate.html";
        public const string AdminCreateUser = "vetcms.ServerApplication.Infrastructure.Communication.Mail.MailTemplates.CreatedUserChangePasswordTemplate.html";
    }
}
