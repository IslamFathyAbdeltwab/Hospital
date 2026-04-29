using Hosptial.BLL.Profiles;
using Hosptial.BLL.Profiles.Prescription;
using Hosptial.BLL.Services.Classes;
using Hosptial.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptial.BLL.Common
{
    public static class AddBLLService
    {
        public static void AddBLL(this IServiceCollection services)
        {
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IDoctorAvailabilityService, DoctorAvailabilityService>();
            services.AddScoped<IPrescriptionService, PreScriptionService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IAdminService, AdminService>();





            services.AddAutoMapper(m =>
            {

                m.AddProfile(new DoctorProfile());
                m.AddProfile(new PatientProfile());
                m.AddProfile(new BookingProfile());
                m.AddProfile(new DoctorAvailabilityProfile());
                m.AddProfile(new prescriptionProfile());



            });

        }
    }
}
