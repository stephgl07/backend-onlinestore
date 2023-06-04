using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Application.Mappers.Product;
using Tranzact.OnlineStore.Application.Mappers.ProductDetails;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;
using Tranzact.OnlineStore.Domain.Models.DTOs;
using Tranzact.OnlineStore.Domain.Services.ProductDetails;
using Tranzact.OnlineStore.Domain.Services.UnitOfWork;

namespace Tranzact.OnlineStore.Application.Services.ProductDetails
{
    public class ProductDetailService : IProductDetailService
    {
        private static ProductDetailMapper _productDetailMapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _productDetailMapper = new ProductDetailMapper();
        }

        public async Task<IEnumerable<GetProductsDetailDTO>> GetAllById(int DetailId)
        {
            ProductDetail? productDetail = await _unitOfWork.ProductDetail.GetByDetailId(DetailId);
            if (productDetail is null)
            {
                return new List<GetProductsDetailDTO>();
            }
            var productsDTO = _productDetailMapper.MapProductDetails(productDetail);
            return productsDTO;
        }

        public async Task<IEnumerable<GetProductsDetailDTO>> GetAllByProductId(int ProductId)
        {
            IEnumerable<ProductDetail> productDetail = await _unitOfWork.ProductDetail.GetByProductId(ProductId);
            var productsDTO = _productDetailMapper.MapProductDetails(productDetail.ToList());
            return productsDTO;
        }
    }
}
