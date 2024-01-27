using Application.DTOs;
using Application.Handlers;
using Application.Interfaces;
using Application.Queries;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTests.Utilities;

namespace UnitTests.Application.Handlers;

public class GetClientAdditionalInfoQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsSuccess_WhenClientAdditionalInfoIsFound()
    {
        // Arrange
        var clientVAT = "VAT123";
        var mockClientService = new Mock<IClientService>();
        var mockLogger = new Mock<ILogger<GetClientAdditionalInfoQueryHandler>>();
        var handler = new GetClientAdditionalInfoQueryHandler(mockClientService.Object, mockLogger.Object);
        var query = new GetClientAdditionalInfoQuery(clientVAT);

        var additionalInfo = new ClientAdditionalInfoDto("RN123", "Large");

        mockClientService.Setup(service => service.GetAdditionalClientInfo(clientVAT, It.IsAny<CancellationToken>()))
            .ReturnsAsync(additionalInfo);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("RN123", result.Value.RegistrationNumber);
        Assert.Equal("Large", result.Value.CompanyType);

        // Verify the log messages
        mockLogger.VerifyLog(LogLevel.Information, Times.Exactly(2));
        mockLogger.VerifyLog(LogLevel.Warning, Times.Never());
    }

    [Fact]
    public async Task Handle_ReturnsNotFound_WhenClientAdditionalInfoIsNotAvailable()
    {
        // Arrange
        var clientVAT = "VAT123";
        var mockClientService = new Mock<IClientService>();
        var mockLogger = new Mock<ILogger<GetClientAdditionalInfoQueryHandler>>();
        var handler = new GetClientAdditionalInfoQueryHandler(mockClientService.Object, mockLogger.Object);
        var query = new GetClientAdditionalInfoQuery(clientVAT);

        mockClientService.Setup(service => service.GetAdditionalClientInfo(clientVAT, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ClientAdditionalInfoDto?)null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);

        // Verify the log messages
        mockLogger.VerifyLog(LogLevel.Information, Times.Once());
        mockLogger.VerifyLog(LogLevel.Warning, Times.Once());
    }
}
