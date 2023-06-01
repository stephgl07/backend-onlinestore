using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;

namespace Tranzact.OnlineStore.Infrastructure.Data
{
    public class DBProductsContext : DbContext
    {
        public DBProductsContext()
        {
        }
        public DBProductsContext(DbContextOptions<DBProductsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProductBE> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configuraciones
            modelBuilder.ApplyConfiguration(new ProductBEConfiguration());
        }
    }
}
