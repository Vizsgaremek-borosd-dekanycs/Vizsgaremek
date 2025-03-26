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
    public class AnimalTreatmentProfile : Profile
    {
        public AnimalTreatmentProfile()
        {
            CreateMap<Treatment, TreatmentDto>()
                .ForMember(dest => dest.PatientId, opt => opt.MapFrom(src => src.Patient.Id))
                .ForMember(dest => dest.DoctorId, opt => opt.MapFrom(src => src.Doctor.Id));
            CreateMap<TreatmentDto, Treatment>();
        }
    }
}
