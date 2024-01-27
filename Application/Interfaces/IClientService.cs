using Application.DTOs;

namespace Application.Interfaces;

public interface IClientService
{
    Task<ClientDto?> GetClientInfoAsync(Guid tenantId, Guid documentId, CancellationToken cancellationToken);
    Task<bool> CheckClientWhitelistedAsync(Guid tenantId, Guid documentId, CancellationToken cancellationToken);
    Task<ClientAdditionalInfoDto?> GetAdditionalClientInfo(string clientVAT, CancellationToken cancellationToken);
}
