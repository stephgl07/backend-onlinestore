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
        Task<IEnumerable<GetProductsDTO>> GetAllContent();
        Task<IEnumerable<GetProductsDTO>> GetAll();
        Task<GetAllByIdProductsDTO?> GetAllById(int ProductId);
        Task<AddEditProductDTO> Create(AddEditProductDTO productDTO);
        Task<AddEditProductDTO> Update(AddEditProductDTO productDTO);
    }
}
