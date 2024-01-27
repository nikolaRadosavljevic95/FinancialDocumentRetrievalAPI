using Domain.Entities;

namespace UnitTests.Domain;

public class ProductTests
{
    [Fact]
    public void Constructor_AssignsProductCodeCorrectly()
    {
        // Arrange
        var productCode = "PRD123";

        // Act
        var product = new Product(productCode);

        // Assert
        Assert.Equal(productCode, product.ProductCode.Code);
    }
}
