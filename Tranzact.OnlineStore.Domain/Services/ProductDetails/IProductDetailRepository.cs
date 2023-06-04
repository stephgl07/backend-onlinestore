using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;

namespace Tranzact.OnlineStore.Domain.Services.ProductDetails
{
    public interface IProductDetailRepository
    {
        Task<IEnumerable<ProductDetail>> GetAll();
        Task<IEnumerable<ProductDetail>> GetByProductId(int productDetailId);
        Task<ProductDetail?> GetByDetailId(int DetailId);
        Task Create(ProductDetail product);
        void Update(ProductDetail product);
        void Remove(ProductDetail product);
    }
}
