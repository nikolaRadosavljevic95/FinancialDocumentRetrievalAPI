using Application.Interfaces;
using Autofac.Features.Indexed;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Services;

public class FinancialDocumentService(
    ApplicationDbContext _context,
    IIndex<string, IProductCodeSerializationStrategy> _serializationStrategies) : IFinancialDocumentService
{
    public string AnonymizeFinancialDocument(string json, string productCode)
    {
        var jObject = JObject.Parse(json);
        AnonymizeFields(jObject, productCode);
        return jObject.ToString();
    }

    public async Task<string> GetFinancialDocumentAsync(Guid tenantId, Guid documentId, string productCode, CancellationToken cancellationToken)
    {
        var document = await _context.FinancialDocuments
            .AsNoTracking()
            .Include(doc => doc.Transactions)
            .FirstOrDefaultAsync(doc => doc.TenantId == tenantId && doc.Id == documentId, cancellationToken)
            ?? throw new KeyNotFoundException("Document not found.");

        // Retrieve the appropriate strategy based on the product code
        var strategy = _serializationStrategies[productCode] ?? _serializationStrategies["Default"];

        // Use the strategy to serialize the document
        return strategy.Serialize(document);
    }

    private static void AnonymizeFields(JObject jObject, string productCode)
    {
        var config = GetAnonymizationConfig(productCode);

        foreach (var property in jObject.Properties().ToList())
        {
            if (config.TryGetValue(property.Name, out string? value))
            {
                if (value == "hash")
                {
                    property.Value = "#####";
                }
            }
            else
            {
                property.Value = "#####";
            }
        }
    }

    private static Dictionary<string, string> GetAnonymizationConfig(string productCode)
    {
        var config = new Dictionary<string, Dictionary<string, string>>
        {
            ["ProductA"] = new Dictionary<string, string>
        {
            { "account_number", "hash" },
        },
            ["ProductB"] = new Dictionary<string, string>
        {
            { "account_identifier", "hash" },
            { "transactions", "leave" },
        },
            // Default configuration for other products
            ["Default"] = new Dictionary<string, string>
            {
                { "account_number", "hash" },
                { "balance", "hash" },
                { "currency", "leave" },
                { "transaction_id", "hash" },
                { "amount", "hash" },
                { "date", "hash" },
                { "description", "hash" },
                { "category", "leave" }
            }
        };

        return config.TryGetValue(productCode, out var productConfig) ? productConfig : config["Default"];
    }
}
