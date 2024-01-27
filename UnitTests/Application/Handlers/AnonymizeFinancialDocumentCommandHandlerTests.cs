using Application.Commands;
using Application.Handlers;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTests.Utilities;

namespace UnitTests.Application.Handlers;

public class AnonymizeFinancialDocumentCommandHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsSuccess_WhenDocumentIsAnonymized()
    {
        // Arrange
        var mockFinancialDocumentService = new Mock<IFinancialDocumentService>();
        var mockLogger = new Mock<ILogger<AnonymizeFinancialDocumentCommandHandler>>();
        var handler = new AnonymizeFinancialDocumentCommandHandler(mockFinancialDocumentService.Object, mockLogger.Object);
        var command = new AnonymizeFinancialDocumentCommand("documentJson", "ProductCode");
        var anonymizedJson = "anonymizedJson";

        mockFinancialDocumentService
            .Setup(service => service.AnonymizeFinancialDocument(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(anonymizedJson);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(anonymizedJson, result.Value);

        // Verify the log messages
        mockLogger.VerifyLog(LogLevel.Information, Times.Exactly(2));
        mockLogger.VerifyLog(LogLevel.Warning, Times.Never());
    }

    [Fact]
    public async Task Handle_ReturnsNotFound_WhenDocumentAnonymizationFails()
    {
        // Arrange
        var mockFinancialDocumentService = new Mock<IFinancialDocumentService>();
        var mockLogger = new Mock<ILogger<AnonymizeFinancialDocumentCommandHandler>>();
        var handler = new AnonymizeFinancialDocumentCommandHandler(mockFinancialDocumentService.Object, mockLogger.Object);
        var command = new AnonymizeFinancialDocumentCommand("documentJson", "ProductCode");

        mockFinancialDocumentService
            .Setup(service => service.AnonymizeFinancialDocument(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(string.Empty);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);

        // Verify the log messages
        mockLogger.VerifyLog(LogLevel.Information, Times.Once());
        mockLogger.VerifyLog(LogLevel.Warning, Times.Once());
    }
}