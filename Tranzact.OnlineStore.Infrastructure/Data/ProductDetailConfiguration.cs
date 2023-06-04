using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;

namespace Tranzact.OnlineStore.Infrastructure.Data
{
    public class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
    {
        public void Configure(EntityTypeBuilder<ProductDetail> entity)
        {
            entity.HasKey(e => e.DetailId)
                    .HasName("PK__ProductD__135C314D25BF93EE");

            entity.ToTable("ProductDetail");

            entity.Property(e => e.DetailId).HasColumnName("DetailID");

            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("ImageURL");

            entity.Property(e => e.ModelName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ProductDimensions)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.Property(e => e.ProductPrice).HasColumnType("decimal(10, 2)");

            entity.Property(e => e.ProductWeight).HasColumnType("decimal(10, 2)");

            entity.Property(e => e.ReviewRating).HasColumnType("decimal(3, 2)");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductDe__Produ__634EBE90");
        }
    }
}