using AutoMapper;
using Hosptial.BLL.ViewModels.BookingViewModels;
using Hosptial.BLL.ViewModels.PatientViewModels;
using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            // AddBookViewModel → Booking
            CreateMap<AddBookViewModel, Booking>();


            CreateMap<Booking, GetBookViewModel>()
             .ForMember(dest => dest.Patient,
                 opt => opt.MapFrom(src => src.Patient))
             .ForMember(dest => dest.AppointmentDate,
                 opt => opt.MapFrom(src => src.DoctorAvailability.AvailableFrom.Date))
             .ForMember(dest => dest.ConsultationTime,
                 opt => opt.MapFrom(src => src.ConsultionTime))
             .ForMember(dest => dest.End,
                 opt => opt.MapFrom(src =>
                     src.ConsultionTime.AddMinutes(
                         src.DoctorAvailability.SessionDurationMinutes)))
             .ForMember(dest => dest.Status,
                 opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
