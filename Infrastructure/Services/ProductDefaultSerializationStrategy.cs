using Application.Interfaces;
using Domain.Entities;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class ProductDefaultSerializationStrategy : IProductCodeSerializationStrategy
{
    public bool CanHandle(string productCode) => productCode == "Default";

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
                date = t.Date.ToString("yyyy/MM/dd"),
                description = t.Description,
                category = t.Category
            }).ToList(),
            is_verified = document.IsVerified
        };

        return JsonConvert.SerializeObject(documentToSerialize, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented
        });
    }
}
