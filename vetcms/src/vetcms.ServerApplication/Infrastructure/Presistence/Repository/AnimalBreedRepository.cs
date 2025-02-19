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
    internal class AnimalBreedRepository : RepositoryBase<AnimalBreed>, IAnimalBreedRepository
    {

        public AnimalBreedRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override IQueryable<AnimalBreed> IncludeAll(IQueryable<AnimalBreed> dataset)
        {
            return dataset.Include(x => x.Type);
        }

    }
}
