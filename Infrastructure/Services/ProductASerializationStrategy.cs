using Application.Interfaces;
using Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Infrastructure.Services;

public class ProductASerializationStrategy : IProductCodeSerializationStrategy
{
    public bool CanHandle(string productCode) => productCode == "ProductA";

    public string Serialize(FinancialDocument document)
    {
        var documentToSerialize = new
        {
            account_number = document.AccountNumber,
            balance = document.Balance,
            currency = document.Currency,
            transactions = document.Transactions.Select(t => new
            {
                transaction_id = t.Id,
                amount = t.Amount,
                date = t.Date.ToString("dd/MM/yyyy"),
                description = t.Description,
                category = t.Category
            }).ToList()
        };

        return JsonConvert.SerializeObject(documentToSerialize, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented
        });
    }
}
