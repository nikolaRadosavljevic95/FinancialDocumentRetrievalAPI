using Application.Common;
using Application.DTOs;
using MediatR;

namespace Application.Queries;

public record GetClientAdditionalInfoQuery(
    string ClientVAT) : IRequest<Result<ClientAdditionalInfoDto>>;