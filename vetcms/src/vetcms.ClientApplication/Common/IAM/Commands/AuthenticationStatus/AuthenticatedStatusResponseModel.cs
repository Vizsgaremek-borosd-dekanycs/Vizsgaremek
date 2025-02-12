﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ClientApplication.Common.IAM.Commands.AuthenticationStatus
{
    public record AuthenticatedStatusResponseModel
    {
        public bool IsAuthenticated { get; set; }
        public UserDto? CurrentUser { get; set; }

        public AuthenticatedStatusResponseModel(bool authenticated)
        {
            IsAuthenticated = authenticated;
        }
    }
}
