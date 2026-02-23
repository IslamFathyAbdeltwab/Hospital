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
    public class ConsultationConfig : BaseConfig<Consultation>
    {
        override public void Configure(EntityTypeBuilder<Consultation> builder)
        {
            base.Configure(builder);
            builder.HasOne(c => c.Appointment)
           .WithOne(a => a.Consultation)
           .HasForeignKey<Consultation>(c => c.AppointmentId);


        }
    }
}
