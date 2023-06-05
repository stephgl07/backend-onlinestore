using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Api;
using Tranzact.OnlineStore.Domain.Models.ApiPromotion;
using Tranzact.OnlineStore.Infrastructure.Api.ApiService;
using Tranzact.OnlineStore.UnitTest.MockObjects;
using Xunit;

namespace Tranzact.OnlineStore.UnitTest.ApiServiceTests
{
    public class DeserealizationShould
    {
        [Fact]
        public async Task DeserealizeToPromotionsObj()
        {
            // Arrange
            string url = "https://qo53yxd1gb.execute-api.us-east-1.amazonaws.com/dev/api/lmbd-getpromotions";
            int productId = 1;
            var requestData = JsonConvert.SerializeObject(new { ProductId = productId });

            var expectedResponse = UserMocks.GetExpectedResponse(); // Obtén el objeto esperado de la respuesta deserializada

            var httpClientMock = new Mock<IHttpClientWrapper>();
            httpClientMock.Setup(httpClient => httpClient.PostAsync(url, It.IsAny<HttpContent>()))
                          .ReturnsAsync(new HttpResponseMessage { Content = new StringContent(UserMocks.ReturnPromotionApiResponseContent()) });

            var apiService = new ApiService(httpClientMock.Object); // Crea una instancia de la clase ApiService y pasa el objeto Mock del HttpClient

            // Act
            var strResponseApi = await apiService.SendPostRequestAsync(url, requestData);
            var apiResponse = JsonConvert.DeserializeObject<ApiPromResponse>(strResponseApi);

            // Assert
            Assert.Equal(expectedResponse.Success, apiResponse.Success);
            Assert.Equal(expectedResponse.Message, apiResponse.Message);
            Assert.Equal(expectedResponse.Data.Count, apiResponse.Data.Count);

            for (int i = 0; i < expectedResponse.Data.Count; i++)
            {
                var expectedProductDetail = expectedResponse.Data[i];
                var actualProductDetail = apiResponse.Data[i];

                Assert.Equal(expectedProductDetail.DetailId, actualProductDetail.DetailId);

                for (int j = 0; j < expectedProductDetail.Promotions.Count; j++)
                {
                    var expectedPromotion = expectedProductDetail.Promotions[j];
                    var actualPromotion = actualProductDetail.Promotions[j];

                    Assert.Equal(expectedPromotion._id, actualPromotion._id);
                    Assert.Equal(expectedPromotion.PromotionName, actualPromotion.PromotionName);
                    Assert.Equal(expectedPromotion.DiscountPercentage, actualPromotion.DiscountPercentage);
                    Assert.Equal(expectedPromotion.ShippingCost, actualPromotion.ShippingCost);
                    Assert.Equal(expectedPromotion.ProductDiscount, actualPromotion.ProductDiscount);
                    Assert.Equal(expectedPromotion.QuantityThreshold, actualPromotion.QuantityThreshold);
                }
            }
        }
    }
}
