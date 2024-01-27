using Application.Common;
using Application.Interfaces;
using Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers;

public class GetFinancialDocumentQueryHandler(
    IFinancialDocumentService _financialDocumentService,
    ILogger<GetFinancialDocumentQueryHandler> _logger) : IRequestHandler<GetFinancialDocumentQuery, Result<string>>
{
    public async Task<Result<string>> Handle(GetFinancialDocumentQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Retrieving financial document for TenantId: {TenantId}, DocumentId: {DocumentId}, ProductCode: {ProductCode}",
            request.TenantId, request.DocumentId, request.ProductCode);

        var documentData = await _financialDocumentService.GetFinancialDocumentAsync(request.TenantId, request.DocumentId, request.ProductCode, cancellationToken);

        if (documentData == null)
        {
            _logger.LogWarning("No financial document found for TenantId: {TenantId}, DocumentId: {DocumentId}, ProductCode: {ProductCode}",
                    request.TenantId, request.DocumentId, request.ProductCode);
            return Result<string>.NotFound();
        }

        _logger.LogInformation("Successfully retrieved financial document for TenantId: {TenantId}, DocumentId: {DocumentId}, ProductCode: {ProductCode}",
                request.TenantId, request.DocumentId, request.ProductCode);

        return Result<string>.Success(documentData);
    }
}