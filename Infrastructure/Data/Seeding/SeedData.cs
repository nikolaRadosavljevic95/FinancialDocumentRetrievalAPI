using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Data.Contexts;

namespace Infrastructure.Data.Seeding;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Check if the database is already seeded
        if (context.Products.Any())
        {
            return; // Database has been seeded
        }

        // Insert clients
        var client1 = new Client("VAT12345", "RN12345", CompanyType.Large, true);
        var client2 = new Client("VAT12346", "RN12346", CompanyType.Medium, false);
        var client3 = new Client("VAT12347", "RN12347", CompanyType.Small, true);
        context.Clients.AddRange(client1, client2, client3);

        // Insert products
        var productA = new Product("ProductA") { Id = Guid.NewGuid() };
        var productB = new Product("ProductB") { Id = Guid.NewGuid() };
        context.Products.AddRange(productA, productB);

        // Insert tenants
        var tenant1 = new Tenant(true) { Id = Guid.NewGuid() };
        var tenant2 = new Tenant(false) { Id = Guid.NewGuid() };
        context.Tenants.AddRange(tenant1, tenant2);

        // Insert financial documents
        var financialDocument1 = new FinancialDocument(tenant1.Id, client1.Id, "Acc12345", 1000.00m, "USD", true) { Id = Guid.NewGuid() };
        var financialDocument2 = new FinancialDocument(tenant2.Id, client2.Id, "Acc12346", 2000.00m, "EUR", false) { Id = Guid.NewGuid() };
        context.FinancialDocuments.AddRange(financialDocument1, financialDocument2);

        // Insert transactions
        var transaction1 = new Transaction(financialDocument1.Id, 100.00m, DateTime.UtcNow, "Transaction 1", "Category1", "Vendor1") { Id = Guid.NewGuid() };
        var transaction2 = new Transaction(financialDocument2.Id, 200.00m, DateTime.UtcNow.AddDays(-1), "Transaction 2", "Category2", "Vendor2") { Id = Guid.NewGuid() };
        context.Transactions.AddRange(transaction1, transaction2);

        context.SaveChanges();
    }
}
