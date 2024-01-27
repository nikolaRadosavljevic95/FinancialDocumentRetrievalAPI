using Domain.Entities;

namespace Application.Interfaces;

public interface IProductCodeSerializationStrategy
{
    bool CanHandle(string productCode);
    string Serialize(FinancialDocument document);
}
