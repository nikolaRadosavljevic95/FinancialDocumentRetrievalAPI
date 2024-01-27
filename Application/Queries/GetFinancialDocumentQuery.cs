using Application.Common;
using MediatR;

namespace Application.Queries;

public record GetFinancialDocumentQuery(
    Guid TenantId, 
    Guid DocumentId, 
    string ProductCode) : IRequest<Result<string>>;
