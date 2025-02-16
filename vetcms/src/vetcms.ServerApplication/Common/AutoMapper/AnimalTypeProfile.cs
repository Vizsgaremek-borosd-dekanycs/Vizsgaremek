﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using vetcms.ServerApplication.Domain.Entity;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ServerApplication.Common.AutoMapper
{
    public class AnimalBreedProfile : Profile
    {
        public AnimalBreedProfile()
        {
            CreateMap<AnimalBreed, AnimalBreedDto>();
            CreateMap<AnimalBreedDto, AnimalBreed>();
        }
    }
}
