using Domain.Common;

namespace Domain.Entities;

public class Tenant(
    bool isWhitelisted) : EntityBase
{
    public bool IsWhitelisted { get; private set; } = isWhitelisted;
}
