using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranzact.OnlineStore.Domain.Models.DTOs
{
    public class AddEditProductDTO
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsActive { get; set; }
        public string? CreationUser { get; set; }
        public string? CreationTimeZone { get; set; }
        public int? StockThreshold { get; set; }
        public List<AddEditProductDetailDTO> ProductDetails { get; set; }
    }

    public class AddEditProductDetailDTO
    {
        public int? DetailId { get; set; }
        public decimal? ProductPrice { get; set; }
        public int? Stock { get; set; }
        public int? WarrantyPeriod { get; set; }
        public string? ModelName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? ReviewRating { get; set; }
        public int? ReviewCount { get; set; }
        public decimal? ProductWeight { get; set; }
        public string? ProductDimensions { get; set; }
    }
}
