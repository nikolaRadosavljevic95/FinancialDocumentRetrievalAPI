using Application.Common;
using MediatR;

namespace Application.Queries;

public record CheckTenantWhitelistedQuery(
    Guid TenantId) : IRequest<Result<bool>>;