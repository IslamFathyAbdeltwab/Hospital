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
    internal class AdminConfig : BaseConfig<Admin>
    {
        public override void Configure(EntityTypeBuilder<Admin> builder)
        {
            base.Configure(builder);
            builder.HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<Admin>(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
