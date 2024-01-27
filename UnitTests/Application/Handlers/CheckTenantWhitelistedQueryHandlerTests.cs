using Application.Handlers;
using Application.Interfaces;
using Application.Queries;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTests.Utilities;

namespace UnitTests.Application.Handlers;

public class CheckTenantWhitelistedQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsTrue_WhenTenantIsWhitelisted()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var mockTenantService = new Mock<ITenantService>();
        var mockLogger = new Mock<ILogger<CheckTenantWhitelistedQueryHandler>>();
        var handler = new CheckTenantWhitelistedQueryHandler(mockTenantService.Object, mockLogger.Object);
        var query = new CheckTenantWhitelistedQuery(tenantId);

        mockTenantService.Setup(service => service.CheckTenantWhitelistedAsync(tenantId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(result.Value);

        // Verify the log messages
        mockLogger.VerifyLog(LogLevel.Information, Times.Exactly(2));
        mockLogger.VerifyLog(LogLevel.Warning, Times.Never());
    }

    [Fact]
    public async Task Handle_ReturnsFalse_WhenTenantIsNotWhitelisted()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var mockTenantService = new Mock<ITenantService>();
        var mockLogger = new Mock<ILogger<CheckTenantWhitelistedQueryHandler>>();
        var handler = new CheckTenantWhitelistedQueryHandler(mockTenantService.Object, mockLogger.Object);
        var query = new CheckTenantWhitelistedQuery(tenantId);

        mockTenantService.Setup(service => service.CheckTenantWhitelistedAsync(tenantId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.Value);

        // Verify the log messages
        mockLogger.VerifyLog(LogLevel.Information, Times.Once());
        mockLogger.VerifyLog(LogLevel.Warning, Times.Once());
    }
}
