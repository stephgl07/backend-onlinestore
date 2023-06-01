using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Services.Product;

namespace Tranzact.OnlineStore.Domain.Services.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //Repositories
        IProductRepository Product { get; }

        //Methods
        void BeginTransaction();
        Task SaveChangesAsync();
        void Rollback();
        void RegisterRollbackAction(Action rollbackAction);
        void Commit();
    }
}
