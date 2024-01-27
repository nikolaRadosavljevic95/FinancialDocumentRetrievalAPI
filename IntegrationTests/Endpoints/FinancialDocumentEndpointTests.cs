using Application.Common;
using Application.Queries;
using FinancialDocumentRetrievalAPI.DTOs.Requests;
using MediatR;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace IntegrationTests.Endpoints;

public class FinancialDocumentEndpointTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly Mock<IMediator> _mediatorMock;

    public FinancialDocumentEndpointTests(CustomWebApplicationFactory<Program> factory)
    {
        _mediatorMock = new Mock<IMediator>();
        factory.Server.PreserveExecutionContext = true;

        // Configure the test services to use the mocked IMediator
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddScoped(_ => _mediatorMock.Object);
            });
        }).CreateClient();
    }

    [Fact]
    public async Task FinancialDocumentEndpoint_ReturnsForbidden_WhenProductCodeIsInvalid()
    {
        // Arrange
        var invalidProductCode = "InvalidCode";
        var tenantId = Guid.NewGuid().ToString();
        var documentId = Guid.NewGuid().ToString();

        var requestUrl = $"{FinancialDocumentRequestDto.Route}?ProductCode={invalidProductCode}&TenantId={tenantId}&DocumentId={documentId}";

        _mediatorMock.Setup(m => m.Send(It.IsAny<ValidateProductCodeQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(Result<bool>.Success(false)); // Return false for invalid product code

        // Act
        var response = await _client.GetAsync(requestUrl);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.Forbidden, response.StatusCode);
    }
}
