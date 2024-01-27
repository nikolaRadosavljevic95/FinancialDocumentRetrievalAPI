using Application.Common;
using Application.Interfaces;
using Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers;

public class ValidateProductCodeQueryHandler(
    IProductService _productService,
    ILogger<GetFinancialDocumentQueryHandler> _logger) : IRequestHandler<ValidateProductCodeQuery, Result<bool>>
{
    public async Task<Result<bool>> Handle(ValidateProductCodeQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating product code: {ProductCode}", request.ProductCode);

        var isValid = await _productService.ValidateProductCodeAsync(request.ProductCode, cancellationToken);

        if (isValid)
        {
            _logger.LogInformation("Product code: {ProductCode} is valid", request.ProductCode);
        }
        else
        {
            _logger.LogWarning("Product code: {ProductCode} is invalid", request.ProductCode);
        }

        return Result<bool>.Success(isValid);
    }
}
