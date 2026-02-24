using Hosptital.DAL.Data.Configurations.Base;
using Hosptital.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Data.Configurations
{
    public class DoctorAvailabilityConfig:BaseConfig<DoctorAvailability>
    {
        public override void Configure(EntityTypeBuilder<DoctorAvailability> builder)
        {
            base.Configure(builder);
            builder.ToTable("DoctorAvailabilities");


            builder.Property(x => x.AvailableFrom)
                   .IsRequired();

            builder.Property(x => x.MaxPatients)
                   .IsRequired();

            builder.Property(x => x.SessionDurationMinutes)
                   .IsRequired();

            // Doctor العلاقة
            builder.HasOne(x => x.Doctor)
                   .WithMany(d=>d.DoctorAvailabilities)
                   .HasForeignKey(x => x.DoctorId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
