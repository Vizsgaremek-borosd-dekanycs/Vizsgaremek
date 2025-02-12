using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ServerApplication.Common.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
