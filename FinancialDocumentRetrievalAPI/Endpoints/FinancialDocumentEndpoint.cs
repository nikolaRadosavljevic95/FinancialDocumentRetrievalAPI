using Application.Commands;
using Application.Common;
using Application.Queries;
using Domain.ValueObjects;
using FastEndpoints;
using FinancialDocumentRetrievalAPI.DTOs;
using FinancialDocumentRetrievalAPI.DTOs.Requests;
using FinancialDocumentRetrievalAPI.DTOs.Responses;
using MediatR;

namespace FinancialDocumentRetrievalAPI.Endpoints;

/// <summary>
/// Endpoint for retrieving and anonymizing a financial document.
/// </summary>
public class FinancialDocumentEndpoint(
    IMediator _mediator) : Endpoint<FinancialDocumentRequestDto, FinancialDocumentResponseDto>
{
    public override void Configure()
    {
        Get(FinancialDocumentRequestDto.Route);
        AllowAnonymous(); // Allow access without authentication for this endpoint.
    }

    /// <summary>
    /// Handles the incoming request to retrieve and anonymize a financial document.
    /// </summary>
    /// <param name="request">The incoming request containing identifiers and product code.</param>
    /// <param name="cancellationToken">A token to cancel the operation if necessary.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public override async Task HandleAsync(FinancialDocumentRequestDto request,
        CancellationToken cancellationToken)
    {
        // Validate product code to ensure it's supported
        var productCodeResult = await _mediator.Send(new ValidateProductCodeQuery(request.ProductCode), cancellationToken);

        // Exit if the product code is not supported, responding with HTTP 403 Forbidden
        if (productCodeResult.IsSuccess && !productCodeResult.Value)
        {
            await SendForbiddenAsync(cancellationToken);
            return;
        }

        // Check if tenant is on the whitelist
        var whitelistedTenantResult = await _mediator.Send(new CheckTenantWhitelistedQuery(request.TenantId), cancellationToken);

        // Exit if the tenant is not whitelisted, responding with HTTP 403 Forbidden
        if (!whitelistedTenantResult.IsSuccess || !whitelistedTenantResult.Value)
        {
            await SendForbiddenAsync(cancellationToken);
            return;
        }

        // Retrieve client information based on tenant and document IDs
        var clientInfoResult = await _mediator.Send(new GetClientInfoQuery(request.TenantId, request.DocumentId), cancellationToken);

        // If client info is not found, respond with HTTP 404 Not Found
        if (clientInfoResult.Status == ResultStatus.NotFound || clientInfoResult.Value == null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        // Ensure the client is whitelisted before proceeding
        var whitelistedClientResult = await _mediator.Send(new CheckClientWhitelistedQuery(request.TenantId, request.DocumentId), cancellationToken);

        // Exit if the client is not whitelisted, responding with HTTP 403 Forbidden
        if (!whitelistedClientResult.IsSuccess || !whitelistedClientResult.Value)
        {
            await SendForbiddenAsync(cancellationToken);
            return;
        }

        // Get additional client information such as registration number and company type
        var clientAdditionalInfoResult = await _mediator.Send(new GetClientAdditionalInfoQuery(clientInfoResult.Value.ClientVAT), cancellationToken);

        // Exit if the company type is 'Small', as per business rules, responding with HTTP 403 Forbidden
        if (clientAdditionalInfoResult.Status == ResultStatus.NotFound || clientAdditionalInfoResult.Value == null ||
            (Enum.TryParse<CompanyType>(clientAdditionalInfoResult.Value.CompanyType, true, out var companyTypeEnum) && companyTypeEnum == CompanyType.Small))
        {
            await SendForbiddenAsync(cancellationToken);
            return;
        }

        // Retrieve the financial document content in serialized form
        var financialDocumentResult = await _mediator.Send(new GetFinancialDocumentQuery(request.TenantId, request.DocumentId, request.ProductCode), cancellationToken);

        // Throw an exception if the financial document data is missing
        if (string.IsNullOrEmpty(financialDocumentResult.Value))
        {
            throw new InvalidOperationException("Financial document data is missing.");
        }

        // Enrich the intial response with the additional client information.
        FinancialDocumentResponseDto response = new(
            Data: financialDocumentResult.Value,
            Company: new CompanyDto(
                RegistrationNumber: clientAdditionalInfoResult.Value.RegistrationNumber,
                CompanyType: clientAdditionalInfoResult.Value.CompanyType));

        // Anonymize the financial document data before sending the response
        var anonymizedData = await _mediator.Send(new AnonymizeFinancialDocumentCommand(financialDocumentResult.Value, request.ProductCode), cancellationToken);

        // Check the success of the anonymization process and update the response
        if (!anonymizedData.IsSuccess || anonymizedData.Value == null)
        {
            throw new InvalidOperationException("An error occurred during the anonymization process.");
        }

        // Finalize the response with anonymized financial document data
        response = response with { Data = anonymizedData.Value };

        // Send the final response with anonymized data and client details
        await SendOkAsync(response, cancellationToken);
    }
}
