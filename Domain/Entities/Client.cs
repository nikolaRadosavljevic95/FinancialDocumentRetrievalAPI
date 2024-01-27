using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Client(
    string vat,
    string registrationNumber,
    CompanyType companyType,
    bool isWhitelisted) : EntityBase
{
    public string Vat { get; private set; } = Guard.AgainstNullOrEmpty(vat, nameof(vat));
    public string RegistrationNumber { get; private set; } = Guard.AgainstNullOrEmpty(registrationNumber, nameof(registrationNumber));
    public CompanyType CompanyType { get; private set; } = companyType;
    public bool IsWhitelisted { get; private set; } = isWhitelisted;
}
