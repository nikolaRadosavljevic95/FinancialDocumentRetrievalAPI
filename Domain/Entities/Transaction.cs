using Domain.Common;

namespace Domain.Entities;

public class Transaction(
    Guid financialDocumentId,
    decimal amount, 
    DateTime date, 
    string description, 
    string category,
    string vendorName) : EntityBase
{
    public Guid FinancialDocumentId { get; private set; } = financialDocumentId;
    public decimal Amount { get; set; } = amount;
    public DateTime Date { get; set; } = date;
    public string Description { get; set; } = Guard.AgainstNullOrEmpty(description, nameof(description));
    public string Category { get; set; } = Guard.AgainstNullOrEmpty(category, nameof(category));
    public string VendorName { get; private set; } = Guard.AgainstNullOrEmpty(vendorName, nameof(vendorName));
}
