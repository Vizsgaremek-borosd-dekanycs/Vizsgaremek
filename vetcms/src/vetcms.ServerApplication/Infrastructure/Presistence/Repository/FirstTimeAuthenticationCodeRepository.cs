﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Common.Abstractions.Data;
using vetcms.ServerApplication.Domain.Entity;

namespace vetcms.ServerApplication.Infrastructure.Presistence.Repository
{
    public class FirstTimeAuthenticationCodeRepository : RepositoryBase<FirstTimeAuthenticationCode>, IFirstTimeAuthenticationCodeRepository
    {
        public FirstTimeAuthenticationCodeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public string? GetCodeByUser(User user)
        {
            var result = Entities.Include(p => p.User).Where((u) => u.User == user).Select(u => u.Code).FirstOrDefault();
            return result;
        }

        public User? GetUserByCode(string code)
        {
            var result = Entities.Include(p => p.User).Where((u) => u.Code == code.ToUpper() && !u.Deleted).FirstOrDefault();
            return result.User;
        }
        
    }
}
