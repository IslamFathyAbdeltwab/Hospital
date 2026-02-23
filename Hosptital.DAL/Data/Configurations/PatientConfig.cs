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
    public class PatientConfig : BaseConfig<Patient>
    {
        public override void Configure(EntityTypeBuilder<Patient> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(p => p.Address, a =>
            {
                a.Property(ad => ad.Street).HasMaxLength(200);
                a.Property(ad => ad.City).HasMaxLength(100);
                a.Property(ad => ad.Country).HasMaxLength(100);
            });
             builder.HasOne(p => p.User)
                 .WithOne()
                 .HasForeignKey<Patient>(p => p.UserId)
                 .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);

        }
    }
}
