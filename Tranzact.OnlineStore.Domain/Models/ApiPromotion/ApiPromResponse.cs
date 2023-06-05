using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranzact.OnlineStore.Domain.Models.ApiPromotion
{
    public class ApiPromResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<ApiPromProductDetail>? Data { get; set; }
        public List<ApiPromError>? Errors { get; set; }
    }

    public class ApiPromPromotion
    {
        public string _id { get; set; }
        public string PromotionName { get; set; }
        public int? DiscountPercentage { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? ProductDiscount { get; set; }
        public int? QuantityThreshold { get; set; }
    }

    public class ApiPromProductDetail
    {
        public int DetailId { get; set; }
        public List<ApiPromPromotion> Promotions { get; set; }
    }

    public class ApiPromError
    {
        public string Value { get; set; }
        public string Msg { get; set; }
        public string Param { get; set; }
        public string Location { get; set; }

    }
}
