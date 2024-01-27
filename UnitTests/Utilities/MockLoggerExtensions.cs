using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace UnitTests.Utilities;

public static class MockLoggerExtensions
{
    public static void VerifyLog<T>(
        this Mock<ILogger<T>> mockLogger,
        LogLevel level,
        Times times)
    {
        mockLogger.Verify(
            x => x.Log(
                level,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            times);
    }
}