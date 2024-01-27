using Domain.Entities;

namespace UnitTests.Domain;

public class TransactionTests
{
    [Fact]
    public void Constructor_AssignsValuesCorrectly()
    {
        // Arrange
        var financialDocumentId = Guid.NewGuid();
        var amount = 100m;
        var date = DateTime.UtcNow;
        var description = "Transaction Description";
        var category = "Sales";
        var vendorName = "Vendor Inc";

        // Act
        var transaction = new Transaction(financialDocumentId, amount, date, description, category, vendorName);

        // Assert
        Assert.Equal(financialDocumentId, transaction.FinancialDocumentId);
        Assert.Equal(amount, transaction.Amount);
        Assert.Equal(date, transaction.Date);
        Assert.Equal(description, transaction.Description);
        Assert.Equal(category, transaction.Category);
        Assert.Equal(vendorName, transaction.VendorName);
    }
}
