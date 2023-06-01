using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;
using Tranzact.OnlineStore.Domain.Models.DTOs;
using Tranzact.OnlineStore.Domain.Services.Product;
using Tranzact.OnlineStore.Infrastructure.Data;

namespace Tranzact.OnlineStore.Infrastructure.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly DBProductsContext _context;
        protected readonly DbSet<ProductBE> _entities;
        public ProductRepository(DBProductsContext context) 
        {
            _context = context;
            _entities = context.Set<ProductBE>();
        }
        public async Task<IEnumerable<ProductBE>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task Create(ProductBE product)
        {
            await _entities.AddAsync(product);
        }
        public void Remove(ProductBE product)
        {
            _entities.Remove(product);
        }
    }
}
