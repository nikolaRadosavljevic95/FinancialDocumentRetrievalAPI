using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ClientService(
    ApplicationDbContext _context) : IClientService
{
    public async Task<bool> CheckClientWhitelistedAsync(Guid tenantId, Guid documentId, CancellationToken cancellationToken)
    {
        var document = await _context.FinancialDocuments
            .AsNoTracking()
            .Include(fd => fd.Client)
            .SingleOrDefaultAsync(fd => fd.TenantId == tenantId 
                && fd.Id == documentId, cancellationToken);

        if (document == null || document.Client == null)
            return false;

        return document.Client.IsWhitelisted;
    }

    public async Task<ClientAdditionalInfoDto?> GetAdditionalClientInfo(string clientVAT, CancellationToken cancellationToken)
    {
        var client = await _context.Clients
            .AsNoTracking()
            .Where(c => c.Vat == clientVAT)
            .Select(c => new ClientAdditionalInfoDto(
                c.RegistrationNumber,
                c.CompanyType.ToString()))
            .FirstOrDefaultAsync(cancellationToken);

        return client;
    }

    public async Task<ClientDto?> GetClientInfoAsync(Guid tenantId, Guid documentId, CancellationToken cancellationToken)
    {
        var client = await _context.FinancialDocuments
            .AsNoTracking()
            .Where(fd => fd.TenantId == tenantId && fd.Id == documentId)
            .Select(fd => fd.Client != null ?
                new ClientDto(fd.Client.Id, fd.Client.Vat) : null)
            .FirstOrDefaultAsync(cancellationToken);
        
        return client;
    }
}
