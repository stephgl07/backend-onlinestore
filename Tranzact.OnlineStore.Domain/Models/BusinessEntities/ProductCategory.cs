using System;
using System.Collections.Generic;

namespace Tranzact.OnlineStore.Domain.Models.BusinessEntities
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            ProductMasters = new HashSet<ProductMaster>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public int? ParentCategoryId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string? CreationUser { get; set; }
        public DateTime? LastUpdate { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<ProductMaster> ProductMasters { get; set; }
    }
}
