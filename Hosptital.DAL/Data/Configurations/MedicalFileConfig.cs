using Hosptital.DAL.Data.Configurations.Base;
using Hosptital.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Data.Configurations
{
    public class MedicalFileConfig : BaseConfig<MedicalFile>
    {
            override public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MedicalFile> builder)
            {
                base.Configure(builder);

            builder.HasOne(m => m.Patient)
        .WithMany(u => u.MedicalFiles)
        .HasForeignKey(m => m.PatientId);
        }
    }
}
