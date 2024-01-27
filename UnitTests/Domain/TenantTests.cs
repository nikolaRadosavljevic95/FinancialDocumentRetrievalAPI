using Domain.Entities;

namespace UnitTests.Domain;

public class TenantTests
{
    [Fact]
    public void Constructor_AssignsIsWhitelistedCorrectly()
    {
        // Arrange
        var isWhitelisted = true;

        // Act
        var tenant = new Tenant(isWhitelisted);

        // Assert
        Assert.Equal(isWhitelisted, tenant.IsWhitelisted);
    }
}
