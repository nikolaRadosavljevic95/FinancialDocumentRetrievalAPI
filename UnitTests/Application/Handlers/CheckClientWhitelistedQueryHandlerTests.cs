using Application.Handlers;
using Application.Interfaces;
using Application.Queries;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTests.Utilities;

namespace UnitTests.Application.Handlers;

public class CheckClientWhitelistedQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsTrue_WhenClientIsWhitelisted()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var documentId = Guid.NewGuid();
        var mockClientService = new Mock<IClientService>();
        var mockLogger = new Mock<ILogger<CheckClientWhitelistedQueryHandler>>();
        var handler = new CheckClientWhitelistedQueryHandler(mockClientService.Object, mockLogger.Object);
        var query = new CheckClientWhitelistedQuery(tenantId, documentId);

        mockClientService.Setup(service => service.CheckClientWhitelistedAsync(tenantId, documentId, It.IsAny<CancellationToken>()))
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
    public async Task Handle_ReturnsFalse_WhenClientIsNotWhitelisted()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var documentId = Guid.NewGuid();
        var mockClientService = new Mock<IClientService>();
        var mockLogger = new Mock<ILogger<CheckClientWhitelistedQueryHandler>>();
        var handler = new CheckClientWhitelistedQueryHandler(mockClientService.Object, mockLogger.Object);
        var query = new CheckClientWhitelistedQuery(tenantId, documentId);

        mockClientService.Setup(service => service.CheckClientWhitelistedAsync(tenantId, documentId, It.IsAny<CancellationToken>()))
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
