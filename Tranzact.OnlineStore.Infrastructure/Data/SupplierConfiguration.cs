using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;

namespace Tranzact.OnlineStore.Infrastructure.Data
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> entity)
        {
            entity.ToTable("Supplier");

            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.CreationDate).HasColumnType("datetime");

            entity.Property(e => e.CreationUser)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.LastUpdate).HasColumnType("datetime");

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.SupplierContact)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.WebsiteUrl)
                .IsUnicode(false)
                .HasColumnName("WebsiteURL");
        }
    }
}