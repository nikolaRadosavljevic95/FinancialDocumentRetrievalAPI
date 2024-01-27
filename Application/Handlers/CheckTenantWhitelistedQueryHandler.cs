using Application.Common;
using Application.Interfaces;
using Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers;

public class CheckTenantWhitelistedQueryHandler(
    ITenantService _tenantService,
    ILogger<CheckTenantWhitelistedQueryHandler> _logger) : IRequestHandler<CheckTenantWhitelistedQuery, Result<bool>>
{
    public async Task<Result<bool>> Handle(CheckTenantWhitelistedQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Checking if tenant with ID {TenantId} is whitelisted", request.TenantId);

        var isWhitelisted = await _tenantService.CheckTenantWhitelistedAsync(request.TenantId, cancellationToken);

        if (isWhitelisted)
        {
            _logger.LogInformation("Tenant with ID {TenantId} is whitelisted", request.TenantId);
        }
        else
        {
            _logger.LogWarning("Tenant with ID {TenantId} is not whitelisted", request.TenantId);
        }

        return Result<bool>.Success(isWhitelisted);
    }
}
