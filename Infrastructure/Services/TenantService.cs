using Application.Interfaces;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TenantService(
    ApplicationDbContext _context) : ITenantService
{
    public async Task<bool> CheckTenantWhitelistedAsync(Guid tenantId, CancellationToken cancellationToken)
    {
        return await _context.Tenants
            .AsNoTracking()
            .AnyAsync(t => t.Id == tenantId && t.IsWhitelisted, cancellationToken: cancellationToken);
    }
}
