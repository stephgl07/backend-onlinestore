using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Models.ApiPromotion;

namespace Tranzact.OnlineStore.UnitTest.MockObjects
{
    public static class UserMocks
    {
        public static ApiPromResponse GetExpectedResponse()
        {
            return new ApiPromResponse
            {
                Success = true,
                Message = "Registros obtenidos exitosamente",
                Data = new List<ApiPromProductDetail>
                {
                    new ApiPromProductDetail
                    {
                        DetailId = 1,
                        Promotions = new List<ApiPromPromotion>
                        {
                            new ApiPromPromotion
                            {
                                _id = "647d1942806c9ba8f2a1f82f",
                                PromotionName = "Descuento de verano",
                                DiscountPercentage = 20
                            },
                            new ApiPromPromotion
                            {
                                _id = "647d19c9806c9ba8f2a1f830",
                                PromotionName = "Envío gratis",
                                ShippingCost = 0
                            }
                        }
                    },
                    new ApiPromProductDetail
                    {
                        DetailId = 7,
                        Promotions = new List<ApiPromPromotion>
                        {
                            new ApiPromPromotion
                            {
                                _id = "647d1942806c9ba8f2a1f82f",
                                PromotionName = "Descuento de verano",
                                DiscountPercentage = 20
                            },
                            new ApiPromPromotion
                            {
                                _id = "647d19c9806c9ba8f2a1f830",
                                PromotionName = "Envío gratis",
                                ShippingCost = 0
                            }
                        }
                    }
                }
            };
        }

        public static string ReturnPromotionApiResponseContent()
        {
            return @"
            {
                ""success"": true,
                ""message"": ""Registros obtenidos exitosamente"",
                ""data"": [
                    {
                        ""detailId"": 1,
                        ""promotions"": [
                            {
                                ""_id"": ""647d1942806c9ba8f2a1f82f"",
                                ""promotionName"": ""Descuento de verano"",
                                ""discountPercentage"": 20
                            },
                            {
                                ""_id"": ""647d19c9806c9ba8f2a1f830"",
                                ""promotionName"": ""Envío gratis"",
                                ""shippingCost"": 0
                            }
                        ]
                    },
                    {
                        ""detailId"": 7,
                        ""promotions"": [
                            {
                                ""_id"": ""647d1942806c9ba8f2a1f82f"",
                                ""promotionName"": ""Descuento de verano"",
                                ""discountPercentage"": 20
                            },
                            {
                                ""_id"": ""647d19c9806c9ba8f2a1f830"",
                                ""promotionName"": ""Envío gratis"",
                                ""shippingCost"": 0
                            }
                        ]
                    }
                ]
            }
            ";
        }
    }
}
