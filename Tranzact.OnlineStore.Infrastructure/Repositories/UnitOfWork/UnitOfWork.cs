using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Services.Product;
using Tranzact.OnlineStore.Domain.Services.UnitOfWork;
using Tranzact.OnlineStore.Infrastructure.Data;
using Tranzact.OnlineStore.Infrastructure.Repositories.Product;

namespace Tranzact.OnlineStore.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        //DbContext
        private readonly DBProductsContext _context;
        private List<Action> _rollbackActions;

        //Repositories
        private readonly IProductRepository _productRepository;

        public UnitOfWork(DBProductsContext context)
        {
            _context = context;
            _rollbackActions = new List<Action>();
            _productRepository = new ProductRepository(_context);
        }

        public IProductRepository Product => _productRepository ?? new ProductRepository(_context);


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
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Ocurrió un error al guardar los cambios, se necesita realizar un rollback
                Rollback();
                throw; // Relanza la excepción para que el llamador pueda manejarla
            }
            finally
            {
                // Limpia la lista de acciones de rollback para futuras transacciones
                _rollbackActions.Clear();
            }
        }

        public void RegisterRollbackAction(Action rollbackAction)
        {
            _rollbackActions.Add(rollbackAction);
        }
    }
}
