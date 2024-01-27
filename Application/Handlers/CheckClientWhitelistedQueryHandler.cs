using Application.Common;
using Application.Interfaces;
using Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers;

public class CheckClientWhitelistedQueryHandler(
    IClientService _clientService,
    ILogger<CheckClientWhitelistedQueryHandler> _logger) : IRequestHandler<CheckClientWhitelistedQuery, Result<bool>>
{
    public async Task<Result<bool>> Handle(CheckClientWhitelistedQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Checking if client with DocumentId {DocumentId} for TenantId {TenantId} is whitelisted", request.DocumentId, request.TenantId);
        
        var isWhitelisted = await _clientService.CheckClientWhitelistedAsync(request.TenantId, request.DocumentId, cancellationToken);

        if (isWhitelisted)
        {
            _logger.LogInformation("Client with DocumentId {DocumentId} for TenantId {TenantId} is whitelisted", request.DocumentId, request.TenantId);
        }
        else
        {
            _logger.LogWarning("Client with DocumentId {DocumentId} for TenantId {TenantId} is not whitelisted", request.DocumentId, request.TenantId);
        }

        return Result<bool>.Success(isWhitelisted);
    }
}