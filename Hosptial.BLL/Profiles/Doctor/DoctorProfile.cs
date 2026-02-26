using AutoMapper;
using Hosptial.BLL.ViewModels.DoctorViewModels;
using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Profiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<AddDoctorViewModel, Doctor>()
            .ForMember(dest => dest.YearsOfExperienc,
                       opt => opt.MapFrom(src => src.YearsOfExperienc));

            CreateMap<RegisterDoctorViewModel, Doctor>()
                .ForMember(dest => dest.YearsOfExperienc,
                           opt => opt.MapFrom(src => src.YearsOfExperience));

            CreateMap<Doctor, DoctorViewModel>()
                .ForMember(dest => dest.Name,
                           opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Gender,
                           opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.Phone,
                           opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Speciality,
                           opt => opt.MapFrom(src => src.Speciality.Name))
                .ForMember(dest => dest.YearsOfExperience,
                           opt => opt.MapFrom(src => src.YearsOfExperienc));

            CreateMap<Doctor, DoctorsViewModel>()
                .ForMember(dest => dest.Name,
                           opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Gender,
                           opt => opt.MapFrom(src => src.User.Gender))
                .ForMember(dest => dest.Phone,
                           opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Speciality,
                           opt => opt.MapFrom(src => src.Speciality.Name))
                .ForMember(dest => dest.YearsOfExperience,
                           opt => opt.MapFrom(src => src.YearsOfExperienc));
        }

    }
}
