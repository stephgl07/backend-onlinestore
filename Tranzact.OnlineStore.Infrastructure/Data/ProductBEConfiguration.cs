using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;

namespace Tranzact.OnlineStore.Infrastructure.Data
{
    public class ProductBEConfiguration : IEntityTypeConfiguration<ProductBE>
    {
        public void Configure(EntityTypeBuilder<ProductBE> entity)
        {
            entity.HasKey(e => new { e.Id });

            entity.ToTable("M_TB_Product");

            entity.Property(e => e.Id).HasColumnName("inIdProduct");

            entity.Property(e => e.Name)
                           .HasMaxLength(100)
                           .IsUnicode(false)
                           .HasColumnName("vcName");

            entity.Property(e => e.Description)
                           .HasMaxLength(200)
                           .IsUnicode(false)
                           .HasColumnName("vcDescription");
        }
    }
}
