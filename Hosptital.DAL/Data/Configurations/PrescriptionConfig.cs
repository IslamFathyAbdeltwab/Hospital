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
    public class PrescriptionConfig:BaseConfig<Prescription>
    {
        public override void Configure(EntityTypeBuilder<Prescription> builder)
        {
            base.Configure(builder);
            builder.HasOne(p => p.Doctor)
             .WithMany()
             .HasForeignKey(p => p.DoctorId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Patient)
              .WithMany()
              .HasForeignKey(p => p.PatientId)
              .OnDelete(DeleteBehavior.Restrict);
           
            builder.HasMany(p => p.Treatments)
            .WithOne()//.WithOne(x => x.Prescription) "islam" delete it becose not need navigation prop here
            //and if let it will do cycle when get prescription ;
            .HasForeignKey(t => t.PrescriptionId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
