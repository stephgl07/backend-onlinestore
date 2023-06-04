using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;

namespace Tranzact.OnlineStore.Infrastructure.Data
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> entity)
        {
            entity.HasKey(e => e.CategoryId)
                    .HasName("PK__ProductC__19093A2BE038C187");

            entity.ToTable("ProductCategory");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

            entity.Property(e => e.CategoryDescription).IsUnicode(false);

            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.CreationDate).HasColumnType("datetime");

            entity.Property(e => e.CreationUser)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.LastUpdate).HasColumnType("datetime");

            entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");
        }
    }
}