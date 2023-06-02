using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;
using Tranzact.OnlineStore.Domain.Models.DTOs;
using Tranzact.OnlineStore.Domain.Services.Product;
using Tranzact.OnlineStore.Domain.Services.UnitOfWork;

namespace Tranzact.OnlineStore.Application.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductDTO> Create(AddProductDTO productDTO)
        {
            try
            {
                //Mapping
                var product = new ProductBE()
                {
                    Name = productDTO.ProductName,
                    Description = productDTO.ProductDescription
                };

                _unitOfWork.BeginTransaction(); 
                await _unitOfWork.Product.Create(product);
                await _unitOfWork.SaveChangesAsync();

                _unitOfWork.RegisterRollbackAction(() =>
                {
                    _unitOfWork.Product.Remove(product);
                });

                _unitOfWork.Commit();

                return new ProductDTO()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                };
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<GetProductsDTO>> GetAll()
        {
            IEnumerable<ProductBE> products = await _unitOfWork.Product.GetAll();

            var productsDTO = products.Select(p => new GetProductsDTO()
            {
                Id = p.Id,
                ProductName = p.Name,
                ProductDescription = p.Description
            });

            return productsDTO;
        }
    }
}
