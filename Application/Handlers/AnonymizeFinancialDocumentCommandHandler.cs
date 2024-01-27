using Application.Commands;
using Application.Common;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers;

public class AnonymizeFinancialDocumentCommandHandler(
    IFinancialDocumentService _financialDocumentService,
    ILogger<AnonymizeFinancialDocumentCommandHandler> _logger) : IRequestHandler<AnonymizeFinancialDocumentCommand, Result<string>>
{
    public Task<Result<string>> Handle(AnonymizeFinancialDocumentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Anonymizing financial document with Product Code: {ProductCode}", request.ProductCode);

        var result = _financialDocumentService.AnonymizeFinancialDocument(request.FinancialDocumentJson, request.ProductCode);

        if (string.IsNullOrWhiteSpace(result))
        {
            _logger.LogWarning("Failed to anonymize document for Product Code: {ProductCode}", request.ProductCode);
            return Task.FromResult(Result<string>.NotFound());
        }

        _logger.LogInformation("Document anonymization completed successfully for Product Code: {ProductCode}", request.ProductCode);
        return Task.FromResult(Result<string>.Success(result));
    }
}
