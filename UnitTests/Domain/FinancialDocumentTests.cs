using Domain.Entities;

namespace UnitTests.Domain;

public class FinancialDocumentTests
{
    [Fact]
    public void Constructor_AssignsValues_And_CreatesEmptyTransactionsCollection()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        var clientId = Guid.NewGuid();
        var accountNumber = "1234567890";
        var balance = 100m;
        var currency = "USD";
        var isVerified = true;

        // Act
        var financialDocument = new FinancialDocument(tenantId, clientId, accountNumber, balance, currency, isVerified);

        // Assert
        Assert.Equal(tenantId, financialDocument.TenantId);
        Assert.Equal(clientId, financialDocument.ClientId);
        Assert.Equal(accountNumber, financialDocument.AccountNumber);
        Assert.Equal(balance, financialDocument.Balance);
        Assert.Equal(currency, financialDocument.Currency);
        Assert.Equal(isVerified, financialDocument.IsVerified);
        Assert.Empty(financialDocument.Transactions);
    }
}