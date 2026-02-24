using Hosptital.DAL.Data.Configurations.Base;
using Hosptital.DAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Data.Configurations
{
    public class BookingConfig:BaseConfig<Booking>
    {
        override public void Configure(EntityTypeBuilder<Booking> builder)
        {
            
            builder.HasOne(x => x.Patient)
                   .WithOne()
                   .HasForeignKey<Booking>(x => x.PatientId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
            builder.HasOne(b=>b.DoctorAvailability)
                   .WithOne()
                   .HasForeignKey<Booking>(b=>b.DoctorAvailabilityId)
                   .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);


        }
    }
}
