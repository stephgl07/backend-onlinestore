using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;

namespace Tranzact.OnlineStore.Infrastructure.Data
{
    public class ProductMasterConfiguration : IEntityTypeConfiguration<ProductMaster>
    {
        public void Configure(EntityTypeBuilder<ProductMaster> entity)
        {
            entity.HasKey(e => e.ProductId)
                    .HasName("PK__ProductM__B40CC6ED4715909A");

            entity.ToTable("ProductMaster");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");

            entity.Property(e => e.CreationTimeZone)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.Property(e => e.CreationUser)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.LastUpdate).HasColumnType("datetime");

            entity.Property(e => e.ProductDescription)
                .HasMaxLength(5000)
                .IsUnicode(false);

            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Category)
                .WithMany(p => p.ProductMasters)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__ProductMa__Categ__625A9A57");
        }
    }
}