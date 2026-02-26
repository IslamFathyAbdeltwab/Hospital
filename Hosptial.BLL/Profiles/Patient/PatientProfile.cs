using AutoMapper;
using Hosptial.BLL.ViewModels.PatientViewModels;
using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Profiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            // AddPatientViewModel → Patient
            CreateMap<AddPatientViewModel, Patient>()
                .ForMember(dest => dest.DateOfBirath,
                    opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src => new Address
                    {
                        Street = src.Street,
                        City = src.City,
                        Country = src.Country
                    }));

            // RegisterPatientViewModel → Patient
            CreateMap<RegisterPatientViewModel, Patient>()
                .ForMember(dest => dest.DateOfBirath,
                    opt => opt.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src => new Address
                    {
                        Street = src.Street,
                        City = src.City,
                        Country = src.Country
                    }));

            // Patient → PatientViewModel
            CreateMap<Patient, PatientViewModel>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Phone,
                    opt => opt.MapFrom(src => src.User.PhoneNumber))
                .ForMember(dest => dest.Gender,
                    opt => opt.MapFrom(src => src.User.Gender));
        }
    }
}
