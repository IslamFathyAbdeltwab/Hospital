using Hosptital.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosptital.DAL.Data.Configurations.Base
{
    public class BaseConfig<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        { 
                builder.HasKey(e => e.Id);
                builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                builder.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

        }
    }
}
