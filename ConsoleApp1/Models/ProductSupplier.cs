using System;
using System.Collections.Generic;

namespace Tranzact.OnlineStore.Scaffold.Models
{
    public partial class ProductSupplier
    {
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public DateTime? SupplyDate { get; set; }
        public int? SupplyQuantity { get; set; }
        public decimal? PurchasePrice { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? BatchNumber { get; set; }

        public virtual ProductMaster Product { get; set; } = null!;
        public virtual Supplier Supplier { get; set; } = null!;
    }
}
