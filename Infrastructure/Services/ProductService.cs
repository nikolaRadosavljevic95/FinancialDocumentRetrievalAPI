using Application.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductService(
    ApplicationDbContext _context) : IProductService
{
    public async Task<bool> ValidateProductCodeAsync(string productCode, CancellationToken cancellationToken)
    {
        var productCodeValue = new ProductCode(productCode);

        return await _context.Products
            .AsNoTracking()
            .AnyAsync(p => p.ProductCode == productCodeValue, cancellationToken);
    }
}
