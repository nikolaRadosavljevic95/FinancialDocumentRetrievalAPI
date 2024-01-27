using Domain.Common;

namespace UnitTests.Domain;

public class GuardTests
{
    [Fact]
    public void AgainstNullOrEmpty_ThrowsArgumentNullException_WhenInputIsNull()
    {
        // Arrange
        string? nullInput = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => Guard.AgainstNullOrEmpty(nullInput));

        // Assert
        Assert.Equal("nullInput", exception.ParamName);
        Assert.StartsWith("Argument nullInput cannot be null.", exception.Message);
    }

    [Fact]
    public void AgainstNullOrEmpty_ThrowsArgumentException_WhenInputIsEmpty()
    {
        // Arrange
        string emptyInput = string.Empty;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => Guard.AgainstNullOrEmpty(emptyInput));

        // Assert
        Assert.Equal("emptyInput", exception.ParamName);
        Assert.StartsWith("Argument emptyInput cannot be null or empty.", exception.Message);
    }

    [Fact]
    public void AgainstNullOrEmpty_ReturnsInput_WhenInputIsNotNullOrEmpty()
    {
        // Arrange
        var validInput = "valid";

        // Act
        var result = Guard.AgainstNullOrEmpty(validInput);

        // Assert
        Assert.Equal(validInput, result);
    }

    [Fact]
    public void AgainstNull_ThrowsArgumentNullException_WhenInputIsNull()
    {
        // Arrange
        object? nullInput = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => Guard.AgainstNull(nullInput));

        // Assert
        Assert.Equal("nullInput", exception.ParamName);
        Assert.StartsWith("Argument nullInput cannot be null.", exception.Message);
    }

    [Fact]
    public void AgainstNull_ReturnsInput_WhenInputIsNotNull()
    {
        // Arrange
        var validInput = new object();

        // Act
        var result = Guard.AgainstNull(validInput);

        // Assert
        Assert.Equal(validInput, result);
    }
}
