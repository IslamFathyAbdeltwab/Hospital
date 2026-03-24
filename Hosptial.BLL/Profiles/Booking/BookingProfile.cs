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
     .ForMember(dest => dest.patients,
         opt => opt.MapFrom(src => new List<Patient> { src.Patient }))

     .ForMember(dest => dest.ConsultationTime,
         opt => opt.MapFrom(src => src.ConsultionTime))

     .ForMember(dest => dest.Status,
         opt => opt.MapFrom(src => src.Status.ToString()))

     .ForMember(dest => dest.AppointmentDate,
         opt => opt.MapFrom(src => src.ConsultionTime.Date))

     .ForMember(dest => dest.End,
         opt => opt.MapFrom(src => src.ConsultionTime.AddMinutes(30))); // adjust duration if needed
        }
    }
}
