using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Queries;

public record GetClientInfoQuery(
    Guid TenantId, 
    Guid DocumentId) : IRequest<Result<ClientDto>>;
