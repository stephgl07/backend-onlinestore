using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.DTOs;

namespace Tranzact.OnlineStore.Domain.Services.ProductDetails
{
    public interface IProductDetailService
    {
        Task<IEnumerable<GetProductsDetailDTO>> GetAllById(int DetailId);
        Task<IEnumerable<GetProductsDetailDTO>> GetAllByProductId(int ProductId);
    }
}
