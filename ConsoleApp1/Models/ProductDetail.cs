using System;
using System.Collections.Generic;

namespace Tranzact.OnlineStore.Scaffold.Models
{
    public partial class ProductDetail
    {
        public int DetailId { get; set; }
        public int? ProductId { get; set; }
        public decimal? ProductPrice { get; set; }
        public int? Stock { get; set; }
        public int? WarrantyPeriod { get; set; }
        public string? ModelName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? ReviewRating { get; set; }
        public int? ReviewCount { get; set; }
        public decimal? ProductWeight { get; set; }
        public string? ProductDimensions { get; set; }
        public bool? IsActive { get; set; }

        public virtual ProductMaster? Product { get; set; }
    }
}
