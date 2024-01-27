namespace Application.Interfaces;

public interface ITenantService
{
    Task<bool> CheckTenantWhitelistedAsync(Guid tenantId, CancellationToken cancellationToken);
}
