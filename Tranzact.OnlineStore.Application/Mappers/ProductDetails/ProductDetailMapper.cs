using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;
using Tranzact.OnlineStore.Domain.Models.DTOs;

namespace Tranzact.OnlineStore.Application.Mappers.ProductDetails
{
    public class ProductDetailMapper
    {
        // Map from DTO to BE
        public List<ProductDetail> MapProductDetails(AddEditProductDTO productDTO, int productId)
        {
            return productDTO.ProductDetails.Select(detailDTO => new ProductDetail()
            {
                ProductId = productId,
                ProductPrice = detailDTO.ProductPrice,
                Stock = detailDTO.Stock,
                WarrantyPeriod = detailDTO.WarrantyPeriod,
                ModelName = detailDTO.ModelName,
                ImageUrl = detailDTO.ImageUrl,
                ReviewRating = detailDTO.ReviewRating,
                ReviewCount = detailDTO.ReviewCount,
                ProductWeight = detailDTO.ProductWeight,
                ProductDimensions = detailDTO.ProductDimensions
            }).ToList();
        }

        // Map from BE To DTO
        public List<GetProductsDetailDTO> MapProductDetails(ProductDetail productDTO)
        {
            return new List<GetProductsDetailDTO>()
            {
                new GetProductsDetailDTO()
                {
                    DetailId = productDTO.DetailId,
                    ProductPrice = productDTO.ProductPrice,
                    Stock = productDTO.Stock,
                    WarrantyPeriod = productDTO.WarrantyPeriod,
                    ModelName = productDTO.ModelName,
                    ImageUrl = productDTO.ImageUrl,
                    ReviewRating = productDTO.ReviewRating,
                    ReviewCount = productDTO.ReviewCount,
                    ProductWeight = productDTO.ProductWeight,
                    ProductDimensions = productDTO.ProductDimensions
                }
            };
        }

        // Map from BE to DTO for response
        public List<AddEditProductDetailDTO> MapProductDetailsRegistered(List<ProductDetail> lstDetails)
        {
            return lstDetails.Select(detailDTO => new AddEditProductDetailDTO()
            {
                DetailId = detailDTO.DetailId,
                ProductPrice = detailDTO.ProductPrice,
                Stock = detailDTO.Stock,
                WarrantyPeriod = detailDTO.WarrantyPeriod,
                ModelName = detailDTO.ModelName,
                ImageUrl = detailDTO.ImageUrl,
                ReviewRating = detailDTO.ReviewRating,
                ReviewCount = detailDTO.ReviewCount,
                ProductWeight = detailDTO.ProductWeight,
                ProductDimensions = detailDTO.ProductDimensions
            }).ToList();
        }

        public List<GetProductsDetailDTO> MapProductDetails(List<ProductDetail> lstDetails)
        {
            return lstDetails.Select(detailDTO => new GetProductsDetailDTO()
            {
                DetailId = detailDTO.DetailId,
                ProductPrice = detailDTO.ProductPrice,
                Stock = detailDTO.Stock,
                WarrantyPeriod = detailDTO.WarrantyPeriod,
                ModelName = detailDTO.ModelName,
                ImageUrl = detailDTO.ImageUrl,
                ReviewRating = detailDTO.ReviewRating,
                ReviewCount = detailDTO.ReviewCount,
                ProductWeight = detailDTO.ProductWeight,
                ProductDimensions = detailDTO.ProductDimensions
            }).ToList();
        }
    }
}
