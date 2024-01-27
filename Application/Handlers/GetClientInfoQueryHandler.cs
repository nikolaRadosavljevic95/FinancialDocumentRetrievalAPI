using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers;

public class GetClientInfoQueryHandler(
    IClientService _clientService,
    ILogger<GetClientInfoQueryHandler> _logger) : IRequestHandler<GetClientInfoQuery, Result<ClientDto>>
{
    public async Task<Result<ClientDto>> Handle(GetClientInfoQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving client information for TenantId: {TenantId}, DocumentId: {DocumentId}", request.TenantId, request.DocumentId);

        var clientInfo = await _clientService.GetClientInfoAsync(request.TenantId, request.DocumentId, cancellationToken);

        if (clientInfo == null)
        {
            _logger.LogWarning("Client information not found for TenantId: {TenantId}, DocumentId: {DocumentId}", request.TenantId, request.DocumentId);
            return Result<ClientDto>.NotFound();
        }

        _logger.LogInformation("Successfully retrieved client information for TenantId: {TenantId}, DocumentId: {DocumentId}", request.TenantId, request.DocumentId);
        return Result<ClientDto>.Success(clientInfo);
    }
}
