using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.DTOs;

namespace Tranzact.OnlineStore.Domain.Services.Product
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductsDTO>> GetAll();
        Task<ProductDTO> Create(AddProductDTO productDTO);
    }
}
