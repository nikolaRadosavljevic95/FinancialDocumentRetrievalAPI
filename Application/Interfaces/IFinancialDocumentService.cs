namespace Application.Interfaces;

public interface IFinancialDocumentService
{
    Task<string> GetFinancialDocumentAsync(Guid tenantId, Guid documentId, string productCode, CancellationToken cancellationToken);
    string AnonymizeFinancialDocument(string json, string productCode);
}
