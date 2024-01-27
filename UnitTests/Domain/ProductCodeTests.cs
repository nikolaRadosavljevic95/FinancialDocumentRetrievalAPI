using Domain.ValueObjects;

namespace UnitTests.Domain;

public class ProductCodeTests
{
    [Fact]
    public void Constructor_AssignsCodeCorrectly()
    {
        // Arrange
        var code = "Code123";

        // Act
        var productCode = new ProductCode(code);

        // Assert
        Assert.Equal(code, productCode.Code);
    }
}
