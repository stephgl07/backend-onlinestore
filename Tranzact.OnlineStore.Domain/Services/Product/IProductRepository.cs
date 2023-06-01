using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;
using Tranzact.OnlineStore.Domain.Models.DTOs;

namespace Tranzact.OnlineStore.Domain.Services.Product
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductBE>> GetAll();
        Task Create(ProductBE product);
        void Remove(ProductBE product);
    }
}
