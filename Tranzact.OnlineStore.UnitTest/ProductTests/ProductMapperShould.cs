using Xunit;
using Tranzact.OnlineStore.Application.Mappers.Product;
using Tranzact.OnlineStore.Domain.Models.BusinessEntities;

namespace Tranzact.OnlineStore.UnitTest.ProductTests
{
    public class ProductMapperShould
    {
        [Fact]
        public void ReturnMappedProductFromBeToDto()
        {
            // Arrange
            var product = new ProductMaster();

            var productMapper = new ProductMapper();

            // Act
            var productDTO = productMapper.MapProductMasterGetAllById(product);

            // Assert
            Assert.NotNull(productDTO);
        }
    }
}
