using Domain.Entities;
using Domain.ValueObjects;

namespace UnitTests.Domain;

public class ClientTests
{
    [Fact]
    public void Constructor_AssignsValuesCorrectly()
    {
        // Arrange
        var vat = "VAT123";
        var registrationNumber = "RN123";
        var companyType = CompanyType.Large;
        var isWhitelisted = true;

        // Act
        var client = new Client(vat, registrationNumber, companyType, isWhitelisted);

        // Assert
        Assert.Equal(vat, client.Vat);
        Assert.Equal(registrationNumber, client.RegistrationNumber);
        Assert.Equal(companyType, client.CompanyType);
        Assert.Equal(isWhitelisted, client.IsWhitelisted);
    }

    [Theory]
    [InlineData(null)]
    public void Client_Constructor_ThrowsArgumentNullException_ForNullVat(string invalidVat)
    {
        // Arrange
        var registrationNumber = "RN123";
        var companyType = CompanyType.Medium;
        var isWhitelisted = true;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new Client(invalidVat, registrationNumber, companyType, isWhitelisted));

        // Assert
        Assert.Equal("vat", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    public void Client_Constructor_ThrowsArgumentException_ForEmptyVat(string invalidVat)
    {
        // Arrange
        var registrationNumber = "RN123";
        var companyType = CompanyType.Medium;
        var isWhitelisted = true;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            new Client(invalidVat, registrationNumber, companyType, isWhitelisted));

        Assert.Equal("vat", exception.ParamName);
    }

    [Theory]
    [InlineData(null)]
    public void Constructor_ThrowsArgumentNullException_ForNullRegistrationNumber(string invalidRegistrationNumber)
    {
        // Arrange
        var vat = "VAT123";
        var companyType = CompanyType.Large;
        var isWhitelisted = true;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() =>
            new Client(vat, invalidRegistrationNumber, companyType, isWhitelisted));

        // Assert
        Assert.Equal("registrationNumber", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    public void Constructor_ThrowsArgumentException_ForEmptyRegistrationNumber(string invalidRegistrationNumber)
    {
        // Arrange
        var vat = "VAT123";
        var companyType = CompanyType.Large;
        var isWhitelisted = true;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() =>
            new Client(vat, invalidRegistrationNumber, companyType, isWhitelisted));
        Assert.Equal("registrationNumber", exception.ParamName);
    }
}
