using Domain.Common;

namespace Domain.ValueObjects;

public class ProductCode(
    string code)
{
    public string Code { get; private set; } = Guard.AgainstNullOrEmpty(code, nameof(code));
}
