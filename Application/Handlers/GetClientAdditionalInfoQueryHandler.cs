using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers;

public class GetClientAdditionalInfoQueryHandler(
    IClientService _clientService,
    ILogger<GetClientAdditionalInfoQueryHandler> _logger) : IRequestHandler<GetClientAdditionalInfoQuery, Result<ClientAdditionalInfoDto>>
{
    public async Task<Result<ClientAdditionalInfoDto>> Handle(GetClientAdditionalInfoQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving additional client information for VAT {ClientVAT}", request.ClientVAT);

        var additionalInfo = await _clientService.GetAdditionalClientInfo(request.ClientVAT, cancellationToken);

        if (additionalInfo == null)
        {
            _logger.LogWarning("Additional client information not found for VAT {ClientVAT}", request.ClientVAT);
            return Result<ClientAdditionalInfoDto>.NotFound();
        }

        _logger.LogInformation("Successfully retrieved additional client information for VAT {ClientVAT}", request.ClientVAT);
        return Result<ClientAdditionalInfoDto>.Success(additionalInfo);
    }
}
