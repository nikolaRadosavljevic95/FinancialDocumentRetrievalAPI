namespace Application.Interfaces;

public interface IProductService
{
    Task<bool> ValidateProductCodeAsync(string productCode, CancellationToken cancellationToken);
}
