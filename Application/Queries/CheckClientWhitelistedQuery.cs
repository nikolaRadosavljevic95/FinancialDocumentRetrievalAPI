using Application.Common;
using MediatR;

namespace Application.Queries;

public record CheckClientWhitelistedQuery(
    Guid TenantId, 
    Guid DocumentId) : IRequest<Result<bool>>;
