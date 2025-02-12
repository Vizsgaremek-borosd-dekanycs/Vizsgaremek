using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ClientApplication.Features.IAM.GetUser;
using vetcms.ClientApplication.Features.IAM.UserList;
using vetcms.SharedModels.Features.IAM;

namespace vetcms.ClientApplication.Common.AutoMapping
{
    internal class ClientApplicationProfile : Profile
    {
        public ClientApplicationProfile()
        {
            CreateMap<GetUserApiQueryResponse, GetUserClientQueryResponse>();
        }
    }
}
