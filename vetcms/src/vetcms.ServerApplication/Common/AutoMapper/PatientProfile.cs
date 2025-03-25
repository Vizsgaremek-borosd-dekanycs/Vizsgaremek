using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vetcms.ServerApplication.Domain.Entity.PatientManagement;
using vetcms.SharedModels.Common.Dto;

namespace vetcms.ServerApplication.Common.AutoMapper
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Type.Id))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.Owner.Id))
                .ForMember(dest => dest.BreedId, opt => opt.MapFrom(src => src.Breed.Id));

            CreateMap<PatientDto, Patient>()
                .ReverseMap();
        }
    }
}
