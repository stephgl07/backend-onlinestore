using Xunit;
using Moq;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Infrastructure.Api.ApiService;
using Newtonsoft.Json;
using Tranzact.OnlineStore.Domain.Api.Wrappers;
using Tranzact.OnlineStore.Domain.Models.ApiPromotion;
using Tranzact.OnlineStore.UnitTest.MockObjects;
using Tranzact.OnlineStore.Domain.Api;

namespace Tranzact.OnlineStore.UnitTest.ApiServiceTests
{
    public class ApiServiceShould
    {
        [Fact]
        public async Task ReturnPromotionList()
        {
            // Arrange
            string url = "https://qo53yxd1gb.execute-api.us-east-1.amazonaws.com/dev/api/lmbd-getpromotions";
            int productId = 1;
            var requestData = JsonConvert.SerializeObject(new { ProductId = productId });

            var httpClientMock = new Mock<IHttpClientWrapper>();
            httpClientMock.Setup(httpClient => httpClient.PostAsync(url, It.IsAny<HttpContent>()))
                          .ReturnsAsync(new HttpResponseMessage { Content = new StringContent(UserMocks.ReturnPromotionApiResponseContent()) });

            var apiService = new ApiService(httpClientMock.Object); // Crea una instancia de la clase ApiService y pasa el objeto Mock del HttpClient

            // Act
            var strResponseApi = await apiService.SendPostRequestAsync(url, requestData);
            var apiResponse = JsonConvert.DeserializeObject<ApiPromResponse>(strResponseApi);

            var expectedResponse = UserMocks.GetExpectedResponse();

            string response = JsonConvert.SerializeObject(apiResponse);
            string expected = JsonConvert.SerializeObject(expectedResponse);

            // Assert
            Assert.Equal(expected, response);

        }


    }
}
