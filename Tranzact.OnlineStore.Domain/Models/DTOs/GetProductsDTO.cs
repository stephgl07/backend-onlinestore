using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.ApiPromotion;

namespace Tranzact.OnlineStore.Domain.Models.DTOs
{
    public class GetProductsDTO
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsActive { get; set; }
        public string? CreationDate { get; set; }
        public string? CreationUser { get; set; }
        public string? CreationTimeZone { get; set; }
        public string? LastUpdate { get; set; }
        public int? StockThreshold { get; set; }
        public virtual GetProductCategoryDTO? Category { get; set; }
        public List<GetProductsDetailDTO> ProductDetails { get; set; }
        public List<GetProductSupplierDTO> ProductSuppliers { get; set; }
    }

    public class GetProductsDetailDTO
    {
        public int DetailId { get; set; }
        public decimal? ProductPrice { get; set; }
        public int? Stock { get; set; }
        public int? WarrantyPeriod { get; set; }
        public string? ModelName { get; set; }
        public string? ImageUrl { get; set; }
        public decimal? ReviewRating { get; set; }
        public int? ReviewCount { get; set; }
        public decimal? ProductWeight { get; set; }
        public string? ProductDimensions { get; set; }
        public List<PromotionDTO>? Promotions { get; set; }
    }

    public class GetProductSupplierDTO
    {
        public int ProductId { get; set; }
        public int SupplierId { get; set; }
        public DateTime? SupplyDate { get; set; }
        public int? SupplyQuantity { get; set; }
        public decimal? PurchasePrice { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? BatchNumber { get; set; }
        public GetSupplierDTO? Supplier { get; set; }
    }

    public class GetSupplierDTO
    {
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
    }

    public class GetProductCategoryDTO
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public int? ParentCategoryId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? CreationUser { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool? IsActive { get; set; }
    }

    public class PromotionDTO
    {
        public string PromotionName { get; set; }
        public int? DiscountPercentage { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? ProductDiscount { get; set; }
        public int? QuantityThreshold { get; set; }
    }
}
