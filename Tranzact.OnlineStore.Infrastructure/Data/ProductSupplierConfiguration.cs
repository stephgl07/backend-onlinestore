using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;

namespace Tranzact.OnlineStore.Infrastructure.Data
{
    public class ProductSupplierConfiguration : IEntityTypeConfiguration<ProductSupplier>
    {
        public void Configure(EntityTypeBuilder<ProductSupplier> entity)
        {
            entity.HasKey(e => new { e.ProductId, e.SupplierId })
                    .HasName("PK__ProductS__E0B2A084A93D60EA");

            entity.ToTable("ProductSupplier");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

            entity.Property(e => e.BatchNumber)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

            entity.Property(e => e.PurchasePrice).HasColumnType("decimal(10, 2)");

            entity.Property(e => e.SupplyDate).HasColumnType("datetime");

            entity.HasOne(d => d.Product)
                .WithMany(p => p.ProductSuppliers)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductSu__Produ__6442E2C9");

            entity.HasOne(d => d.Supplier)
                .WithMany(p => p.ProductSuppliers)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductSu__Suppl__65370702");
        }
    }
}