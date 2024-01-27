using Application.Common;
using MediatR;

namespace Application.Queries;

public record ValidateProductCodeQuery(
    string ProductCode) : IRequest<Result<bool>>;