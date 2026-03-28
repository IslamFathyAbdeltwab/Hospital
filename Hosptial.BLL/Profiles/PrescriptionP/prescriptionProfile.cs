using AutoMapper;

using Hosptial.BLL.ViewModels.PrescriptionViewModels;
using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Profiles.Prescription
{
    public class prescriptionProfile: Profile
    {
        
            public prescriptionProfile()
            {
                // AddBookViewModel → Booking
                CreateMap<PrescriptionViewModel, Hosptital.DAL.Entities.Prescription> ().ReverseMap();

          

        }
        }
    }

