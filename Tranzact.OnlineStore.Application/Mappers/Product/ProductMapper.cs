using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;
using Tranzact.OnlineStore.Domain.Models.DTOs;

namespace Tranzact.OnlineStore.Application.Mappers.Product
{
    public class ProductMapper
    {
        // Map DTO to BE for Create/Update
        public ProductMaster MapProductMaster(AddEditProductDTO productDTO)
        {
            return new ProductMaster()
            {
                ProductId = productDTO.ProductId,
                ProductName = productDTO.ProductName,
                ProductDescription = productDTO.ProductDescription,
                CategoryId = productDTO.CategoryId,
                IsActive = productDTO.IsActive,
                CreationDate = productDTO.CreationDate,
                CreationUser = productDTO.CreationUser,
                CreationTimeZone = productDTO.CreationTimeZone,
                LastUpdate = productDTO.LastUpdate,
                StockThreshold = productDTO.StockThreshold
            };
        }

        // Map BE to DTO for GetAll
        public IEnumerable<GetProductsDTO> MapProductMasterGetAll(IEnumerable<ProductMaster> products)
        {
            var productsDTO = products.Select(p => new GetProductsDTO()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                CategoryId = p.CategoryId,
                IsActive = p.IsActive,
                CreationDate = p.CreationDate,
                CreationUser = p.CreationUser,
                CreationTimeZone = p.CreationTimeZone,
                LastUpdate = p.LastUpdate,
                StockThreshold = p.StockThreshold,
                Category = (p.Category is null) ? null : new GetProductCategoryDTO()
                {
                    CategoryId = p.Category.CategoryId,
                    CategoryName = p.Category.CategoryName,
                    CategoryDescription = p.Category.CategoryDescription,
                    CreationDate = p.Category.CreationDate,
                    CreationUser = p.Category.CreationUser,
                    IsActive = p.Category.IsActive,
                    LastUpdate = p.Category.LastUpdate,
                    ParentCategoryId = p.Category.ParentCategoryId
                },
                ProductDetails = p.ProductDetails.Select(detail =>
                {
                    return new GetProductsDetailDTO()
                    {
                        DetailId = detail.DetailId,
                        ProductPrice = detail.ProductPrice,
                        Stock = detail.Stock,
                        WarrantyPeriod = detail.WarrantyPeriod,
                        ModelName = detail.ModelName,
                        ImageUrl = detail.ImageUrl,
                        ReviewRating = detail.ReviewRating,
                        ReviewCount = detail.ReviewCount,
                        ProductWeight = detail.ProductWeight,
                        ProductDimensions = detail.ProductDimensions
                    };
                }).ToList(),
                ProductSuppliers = p.ProductSuppliers.Select(productSupplier =>
                {
                    return new GetProductSupplierDTO()
                    {
                        SupplierId = productSupplier.SupplierId,
                        BatchNumber = productSupplier.BatchNumber,
                        ExpiryDate = productSupplier.ExpiryDate,
                        ProductId = productSupplier.ProductId,
                        PurchasePrice = productSupplier.PurchasePrice,
                        SupplyDate = productSupplier.SupplyDate,
                        SupplyQuantity = productSupplier.SupplyQuantity,
                        Supplier = (productSupplier.Supplier is null) ? null : new GetSupplierDTO()
                        {
                            SupplierId = productSupplier.Supplier.SupplierId,
                            Address = productSupplier.Supplier.Address,
                            CreationDate = productSupplier.Supplier.CreationDate,
                            CreationUser = productSupplier.Supplier.CreationUser,
                            Email = productSupplier.Supplier.Email,
                            IsActive = productSupplier.Supplier.IsActive,
                            LastUpdate = productSupplier.Supplier.LastUpdate,
                            PhoneNumber = productSupplier.Supplier.PhoneNumber,
                            SupplierContact = productSupplier.Supplier.SupplierContact,
                            SupplierName = productSupplier.Supplier.SupplierName,
                            WebsiteUrl = productSupplier.Supplier.WebsiteUrl
                        },
                    };
                }).ToList()
            });
            return productsDTO;
        }

        // Map BE to DTO for GetAllById
        public IEnumerable<GetAllByIdProductsDTO> MapProductMasterGetAllById(ProductMaster products)
        {
            var productsDTO = new List<GetAllByIdProductsDTO>()
            {
                new GetAllByIdProductsDTO()
                {
                    ProductId = products.ProductId,
                    ProductName = products.ProductName,
                    ProductDescription = products.ProductDescription,
                    CategoryId = products.CategoryId,
                    IsActive = products.IsActive,
                    CreationDate = products.CreationDate,
                    CreationUser = products.CreationUser,
                    CreationTimeZone = products.CreationTimeZone,
                    LastUpdate = products.LastUpdate,
                    StockThreshold = products.StockThreshold,
                }
            };
            
            return productsDTO;
        }
    }
}
