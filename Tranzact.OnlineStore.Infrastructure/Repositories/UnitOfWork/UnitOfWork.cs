using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Services.Product;
using Tranzact.OnlineStore.Domain.Services.ProductDetails;
using Tranzact.OnlineStore.Domain.Services.UnitOfWork;
using Tranzact.OnlineStore.Infrastructure.Data;
using Tranzact.OnlineStore.Infrastructure.Repositories.Product;
using Tranzact.OnlineStore.Infrastructure.Repositories.ProductDetails;

namespace Tranzact.OnlineStore.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        //DbContext
        private readonly DBOnlineStoreContext _context;
        private List<Action> _rollbackActions;

        //Repositories
        private readonly IProductRepository _productRepository;
        private readonly IProductDetailRepository _productDetailRepository;

        public UnitOfWork(DBOnlineStoreContext context)
        {
            _context = context;
            _rollbackActions = new List<Action>();
            _productRepository = new ProductRepository(_context);
            _productDetailRepository = new ProductDetailRepository(_context);
        }

        public IProductRepository ProductMaster => _productRepository ?? new ProductRepository(_context);
        public IProductDetailRepository ProductDetail => _productDetailRepository ?? new ProductDetailRepository(_context);


        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void Rollback()
        {
            foreach (var rollbackAction in _rollbackActions)
            {
                rollbackAction.Invoke();
            }
            //_context.Database.RollbackTransaction();
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
            finally
            {
                _rollbackActions.Clear();
            }
        }

        public void RegisterRollbackAction(Action rollbackAction)
        {
            _rollbackActions.Add(rollbackAction);
        }
    }
}
