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
    public class PrescriptionConfig : BaseConfig<Prescription>
    {
        override public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            base.Configure(builder);

            builder.HasOne(p => p.Consultation)
          .WithMany(c => c.Prescriptions)
          .HasForeignKey(p => p.ConsultationId);

        }
    }
}
