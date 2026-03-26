using AutoMapper;
using Hosptial.BLL.ViewModels.DoctorAvailabilityViewModels;
using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Profiles
{
    public class DoctorAvailabilityProfile : Profile
    {
        public DoctorAvailabilityProfile()
        {
            CreateMap<AddDoctorAvailabilityViewModel, DoctorAvailability>().ReverseMap();
            CreateMap<UpdateDoctorAvailabilityViewModel, DoctorAvailability>().ReverseMap();
            // Entity -> ViewModel
            CreateMap<DoctorAvailability, DoctorAvailabilityViewModel>()
                .ForMember(dest => dest.doctorAvailability,
                           opt => opt.MapFrom(src => src));

            // ViewModel -> Entity
            CreateMap<DoctorAvailabilityViewModel, DoctorAvailability>()
                .ForMember(dest => dest.Id,
                           opt => opt.MapFrom(src => src.doctorAvailability.Id))
                .ForMember(dest => dest.DoctorId,
                           opt => opt.MapFrom(src => src.doctorAvailability.DoctorId))
                .ForMember(dest => dest.AvailableFrom,
                           opt => opt.MapFrom(src => src.doctorAvailability.AvailableFrom))
                .ForMember(dest => dest.MaxPatients,
                           opt => opt.MapFrom(src => src.doctorAvailability.MaxPatients))
                .ForMember(dest => dest.SessionDurationMinutes,
                           opt => opt.MapFrom(src => src.doctorAvailability.SessionDurationMinutes))
                .ForMember(dest => dest.Price,
                           opt => opt.MapFrom(src => src.doctorAvailability.Price));    


        }
    }
}
