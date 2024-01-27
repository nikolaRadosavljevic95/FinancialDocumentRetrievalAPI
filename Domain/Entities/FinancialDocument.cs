using Domain.Common;

namespace Domain.Entities;

public class FinancialDocument(
    Guid tenantId, 
    Guid clientId, 
    string accountNumber, 
    decimal balance, 
    string currency,
    bool isVerified) : EntityBase
{
    public Guid TenantId { get; private set; } = tenantId;
    public Tenant? Tenant { get; private set; }
    public Guid ClientId { get; private set; } = clientId;
    public Client? Client { get; private set; }
    public string AccountNumber { get; private set; } = Guard.AgainstNullOrEmpty(accountNumber, nameof(accountNumber));
    public decimal Balance { get; private set; } = balance;
    public string Currency { get; private set; } = Guard.AgainstNullOrEmpty(currency, nameof(currency));
    public bool IsVerified { get; set; } = isVerified;
    public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();
}