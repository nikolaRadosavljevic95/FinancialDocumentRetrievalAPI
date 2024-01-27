using Application.Common;
using MediatR;

namespace Application.Commands;

public record AnonymizeFinancialDocumentCommand(
    string FinancialDocumentJson, 
    string ProductCode) : IRequest<Result<string>>;