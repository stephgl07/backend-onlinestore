using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;
using Tranzact.OnlineStore.Domain.Services.ProductDetails;
using Tranzact.OnlineStore.Infrastructure.Data;

namespace Tranzact.OnlineStore.Infrastructure.Repositories.ProductDetails
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private readonly DBOnlineStoreContext _context;
        protected readonly DbSet<ProductDetail> _entities;
        public ProductDetailRepository(DBOnlineStoreContext context)
        {
            _context = context;
            _entities = context.Set<ProductDetail>();
        }
        public async Task<IEnumerable<ProductDetail>> GetAll()
        {
            return await _entities
                .ToListAsync();
        }
        public async Task<IEnumerable<ProductDetail>> GetByProductId(int ProductDetailId)
        {
            return await _entities.Where(detail => detail.ProductId == ProductDetailId).ToListAsync();
        }

        public async Task<ProductDetail?> GetByDetailId(int DetailId)
        {
            return await _entities
                .FindAsync(DetailId);
        }

        public async Task Create(ProductDetail productDetail)
        {
            await _entities
                .AddAsync(productDetail);
        }
        public void Update(ProductDetail productDetail)
        {
            _entities
                .Update(productDetail);
        }
        public void Remove(ProductDetail productDetail)
        {
            _entities
                .Remove(productDetail);
        }
    }
}
