using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranzact.OnlineStore.Domain.Models.DTOs
{
    public class GetAllByIdProductsDTO
    {
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
    }
}
