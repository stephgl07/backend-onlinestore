using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;
using Tranzact.OnlineStore.Domain.Models.DTOs;
using Tranzact.OnlineStore.Domain.Models.Exceptions.Core.Business;
using Tranzact.OnlineStore.Domain.Services.Product;
using Tranzact.OnlineStore.Infrastructure.Data;

namespace Tranzact.OnlineStore.Infrastructure.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly DBOnlineStoreContext _context;
        protected readonly DbSet<ProductMaster> _entities;
        public ProductRepository(DBOnlineStoreContext context) 
        {
            _context = context;
            _entities = context.Set<ProductMaster>();
        }
        public async Task<IEnumerable<ProductMaster>> GetAllContent()
        {
            return await _entities
                .Include(pd => pd.ProductDetails)
                .Include(ds => ds.ProductSuppliers)
                    .ThenInclude(s => s.Supplier)
                .Include(c => c.Category)
                .ToListAsync();
        }
        public async Task<IEnumerable<ProductMaster>> GetAll()
        {
            return await _entities
                .ToListAsync();
        }
        public async Task<ProductMaster?> GetById(int ProductId)
        {
            return await _entities
                .Include(pd => pd.ProductDetails)
                .FirstOrDefaultAsync(pd => pd.ProductId == ProductId);
        }

        public async Task Create(ProductMaster product)
        {
            await _entities
                .AddAsync(product);
        }
        public void Update(ProductMaster product)
        {
            var existingProduct = _entities.FirstOrDefault(p => p.ProductId == product.ProductId);

            if (existingProduct is null)
            {
                throw new Exception("No se encontró producto a editar");
            }
            existingProduct.ProductSuppliers = product.ProductSuppliers;
            existingProduct.Category = product.Category;
            existingProduct.LastUpdate = product.LastUpdate;
            existingProduct.ProductDescription = product.ProductDescription;
            existingProduct.ProductName = product.ProductName;
            _entities.Update(existingProduct);

        }

        public void Remove(ProductMaster product)
        {
            _entities
                .Remove(product);
        }
    }
}
