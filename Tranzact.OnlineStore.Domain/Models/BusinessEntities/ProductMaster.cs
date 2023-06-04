using System;
using System.Collections.Generic;

namespace Tranzact.OnlineStore.Domain.Models.BusinessEntities
{
    public partial class ProductMaster
    {
        public ProductMaster()
        {
            ProductDetails = new HashSet<ProductDetail>();
            ProductSuppliers = new HashSet<ProductSupplier>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? CreationUser { get; set; }
        public string? CreationTimeZone { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int? StockThreshold { get; set; }

        public virtual ProductCategory? Category { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
        public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; }
    }
}
