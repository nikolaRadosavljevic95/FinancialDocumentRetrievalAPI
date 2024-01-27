using System.ComponentModel.DataAnnotations;

namespace FinancialDocumentRetrievalAPI.DTOs.Requests;

public class FinancialDocumentRequestDto
{
    public const string Route = "/financial-document";

    [Required]
    public string ProductCode { get; set; } = string.Empty;
    [Required]
    public Guid TenantId { get; set; }
    [Required]
    public Guid DocumentId { get; set; }
}
