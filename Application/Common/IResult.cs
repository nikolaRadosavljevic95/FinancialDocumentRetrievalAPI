namespace Application.Common;

public interface IResult
{
    bool IsSuccess { get; }
    string SuccessMessage { get; }
    List<string> Errors { get; }
    List<ValidationError> ValidationErrors { get; }
}
