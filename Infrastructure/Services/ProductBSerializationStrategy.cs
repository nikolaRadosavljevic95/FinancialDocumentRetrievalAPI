using Application.Interfaces;
using Domain.Entities;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class ProductBSerializationStrategy : IProductCodeSerializationStrategy
{
    public bool CanHandle(string productCode) => productCode == "ProductB";

    public string Serialize(FinancialDocument document)
    {
        var documentToSerialize = new
        {
            account_identifier = document.AccountNumber,
            current_balance = document.Balance,
            transactions = document.Transactions.Select(t => new
            {
                identifier = t.Id,
                transaction_amount = t.Amount,
                transaction_date = t.Date.ToString("MM-dd-yyyy"),
                payment_details = t.Description,
                expenditure_type = t.Category,
                vendor_name = t.VendorName
            }).ToList()
        };

        return JsonConvert.SerializeObject(documentToSerialize, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented
        });
    }
}