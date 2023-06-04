using System;
using System.Collections.Generic;

namespace Tranzact.OnlineStore.Scaffold.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            ProductSuppliers = new HashSet<ProductSupplier>();
        }

        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierContact { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? CreationUser { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool? IsActive { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WebsiteUrl { get; set; }

        public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; }
    }
}
