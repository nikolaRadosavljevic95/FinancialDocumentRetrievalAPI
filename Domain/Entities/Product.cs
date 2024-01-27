using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Product : EntityBase
{
    private Product() { }

    public Product(string productCode) : this()
    {
        ProductCode = new ProductCode(productCode);
    }

    public ProductCode ProductCode { get; private set; } = null!;
}
