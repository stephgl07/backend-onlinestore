using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Application.Mappers.Product;
using Tranzact.OnlineStore.Application.Mappers.ProductDetails;
using Tranzact.OnlineStore.Domain.Api.ApiService;
using Tranzact.OnlineStore.Domain.Api.AppConfiguration;
using Tranzact.OnlineStore.Domain.Models.ApiPromotion;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;
using Tranzact.OnlineStore.Domain.Models.DTOs;
using Tranzact.OnlineStore.Domain.Services.Product;
using Tranzact.OnlineStore.Domain.Services.UnitOfWork;

namespace Tranzact.OnlineStore.Application.Services.Product
{
    public class ProductService : IProductService
    {
        private static ProductMapper _productMapper;
        private static ProductDetailMapper _productDetailMapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiService _apiService;
        private readonly IAppConfiguration _appConfiguration;
        public ProductService(IUnitOfWork unitOfWork, IApiService apiService, IAppConfiguration appConfiguration)
        {
            _unitOfWork = unitOfWork;
            _productMapper = new ProductMapper();
            _productDetailMapper = new ProductDetailMapper();
            _apiService = apiService;
            _appConfiguration = appConfiguration;
        }

        public async Task<AddEditProductDTO> Create(AddEditProductDTO productDTO)
        {
            try
            {
                var productMaster = _productMapper.MapProductMaster(productDTO);

                // Iniciar transacción
                _unitOfWork.BeginTransaction();

                int createdProductId = await CreateProduct(productMaster);
                var lstCreatedDetails = await CreateDetails(productDTO, createdProductId);

                // Confirmar la transacción
                _unitOfWork.Commit();

                productDTO.ProductId = createdProductId;
                productDTO.ProductDetails = _productDetailMapper.MapProductDetailsRegistered(lstCreatedDetails);

                return productDTO;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<AddEditProductDTO> Update(AddEditProductDTO productDTO)
        {
            try
            {
                var productMaster = _productMapper.MapProductMaster(productDTO);

                // Iniciar transacción
                _unitOfWork.BeginTransaction();

                await UpdateProduct(productMaster);
                await UpdateDetails(productDTO);

                // Confirmar la transacción
                _unitOfWork.Commit();

                return productDTO;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<GetProductsDTO>> GetAllContent()
        {
            IEnumerable<ProductMaster> products = await _unitOfWork.ProductMaster.GetAllContent();
            var productsDTO = _productMapper.MapProductMasterGetAll(products);
            return productsDTO;
        }

        public async Task<IEnumerable<GetProductsDTO>> GetAll()
        {
            IEnumerable<ProductMaster> products = await _unitOfWork.ProductMaster.GetAll();
            var productsDTO = _productMapper.MapProductMasterGetAll(products);
            return productsDTO;
        }
        public async Task<GetAllByIdProductsDTO?> GetAllById(int ProductId)
        {
            ProductMaster? product = await _unitOfWork.ProductMaster.GetById(ProductId);
            if(product is null)
            {
                return null;
            }
            var productDTO = _productMapper.MapProductMasterGetAllById(product);
            var strProduct = JsonConvert.SerializeObject(productDTO);

            string url = _appConfiguration.GetApiPromotionsUrl();
            var strResponseApi = await _apiService.SendPostRequestAsync(url, JsonConvert.SerializeObject(new { ProductId }));
            try
            {
                var apiResponse = JsonConvert.DeserializeObject<ApiPromResponse>(strResponseApi);

                if (apiResponse.Success)
                {
                    // Recorre cada detalle de producto en el DTO
                    foreach (var productDetail in productDTO.ProductDetails)
                    {
                        // Busca la promoción correspondiente al DetailId del detalle actual
                        var promotion = apiResponse.Data?.FirstOrDefault(d => d.DetailId == productDetail.DetailId)?.Promotions;

                        // Si se encontró una promoción para el detalle actual, asígnala al detalle de producto
                        if (promotion != null)
                        {
                            productDetail.Promotions = promotion.Select(p => new PromotionDTO
                            {
                                PromotionName = p.PromotionName,
                                DiscountPercentage = p.DiscountPercentage,
                                ShippingCost = p.ShippingCost,
                                ProductDiscount = p.ProductDiscount,
                                QuantityThreshold = p.QuantityThreshold
                            }).ToList();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

            return productDTO;
        }

        private async Task<int> CreateProduct(ProductMaster productMaster)
        {
            // Crear el producto maestro
            await _unitOfWork.ProductMaster.Create(productMaster);
            await _unitOfWork.SaveChangesAsync();

            // Registrar acción de rollback en caso de error
            _unitOfWork.RegisterRollbackAction(() =>
            {
                _unitOfWork.ProductMaster.Remove(productMaster);
            });

            return productMaster.ProductId;
        }

        private async Task<List<ProductDetail>> CreateDetails(AddEditProductDTO productDTO, int productId)
        {
            var details = _productDetailMapper.MapProductDetails(productDTO, productId);

            foreach (var detail in details)
            {
                await _unitOfWork.ProductDetail.Create(detail);
            }

            await _unitOfWork.SaveChangesAsync();

            return details;
        }

        private async Task UpdateProduct(ProductMaster productMaster)
        {
            // Actualizar el producto maestro
            _unitOfWork.ProductMaster.Update(productMaster);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task UpdateDetails(AddEditProductDTO productDTO)
        {
            foreach (var detailDTO in productDTO.ProductDetails)
            {
                // Obtener el detalle existente por su Id
                var existingDetail = await _unitOfWork.ProductDetail.GetByDetailId(detailDTO.DetailId ?? 0);

                if (existingDetail != null)
                {
                    // Actualizar los valores del detalle existente
                    existingDetail.ProductPrice = detailDTO.ProductPrice;
                    existingDetail.Stock = detailDTO.Stock;
                    existingDetail.WarrantyPeriod = detailDTO.WarrantyPeriod;
                    existingDetail.ModelName = detailDTO.ModelName;
                    existingDetail.ImageUrl = detailDTO.ImageUrl;
                    existingDetail.ReviewRating = detailDTO.ReviewRating;
                    existingDetail.ReviewCount = detailDTO.ReviewCount;
                    existingDetail.ProductWeight = detailDTO.ProductWeight;
                    existingDetail.ProductDimensions = detailDTO.ProductDimensions;

                    // Actualizar el detalle existente
                    _unitOfWork.ProductDetail.Update(existingDetail);
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
