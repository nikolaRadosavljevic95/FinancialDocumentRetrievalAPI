namespace Application.Common;

/// <summary>
/// Represents the outcome of an operation, with a potential return value.
/// </summary>
/// <typeparam name="T">The type of the return value, if the operation is successful.</typeparam>
public class Result<T> : IResult
{
    /// <summary>
    /// Gets the value of the operation if successful.
    /// </summary>
    /// <value>The operation result value if <see cref="IsSuccess"/> is <c>true</c>; otherwise, <c>default(T)</c>.</value>
    /// <remarks>
    /// <para>Accessing <see cref="Value"/> when <see cref="IsSuccess"/> is <c>false</c> is invalid and may throw an exception.</para>
    /// <para>Always check <see cref="IsSuccess"/> before accessing <see cref="Value"/>, or use <see cref="HasValue"/> to ensure that a value is present.</para>
    /// </remarks>
    public T? Value { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    /// <value><c>true</c> if the operation succeeded; otherwise, <c>false</c>.</value>
    public bool IsSuccess => Status == ResultStatus.Ok;

    /// <summary>
    /// Gets a value indicating whether the operation was successful and a non-null value is present.
    /// </summary>
    /// <value>
    /// <c>true</c> if the operation was successful and the value is not null; otherwise, <c>false</c>.
    /// </value>
    /// <remarks>
    /// This property is a convenience shorthand for checking both <see cref="IsSuccess"/> and non-nullity of <see cref="Value"/>.
    /// It should be used when you expect a result that must contain a non-null value upon success.
    /// </remarks>
    public bool HasValue => IsSuccess && Value != null;

    /// <summary>
    /// Indicates the status of the result, which can represent success, error, or other outcome states.
    /// </summary>
    public ResultStatus Status { get; private set; }

    /// <summary>
    /// A message associated with a successful outcome.
    /// </summary>
    public string SuccessMessage { get; private set; } = string.Empty;

    /// <summary>
    /// A list of error messages associated with an unsuccessful outcome.
    /// </summary>
    public List<string> Errors { get; private set; } = [];

    /// <summary>
    /// A list of validation errors associated with an outcome deemed invalid due to model state validation.
    /// </summary>
    public List<ValidationError> ValidationErrors { get; private set; } = [];

    // Parameterless constructor for specific cases like NotFound
    private Result(ResultStatus status)
    {
        Status = status;
    }

    // Constructor for success result
    private Result(T value, string successMessage = "")
    {
        Value = value;
        Status = ResultStatus.Ok;
        SuccessMessage = successMessage;
    }

    // Constructor for error result
    private Result(List<string> errors, ResultStatus status)
    {
        Errors = errors;
        Status = status;
    }

    // Constructor for validation error result
    private Result(List<ValidationError> validationErrors)
    {
        ValidationErrors = validationErrors;
        Status = ResultStatus.Invalid;
    }

    /// <summary>
    /// Creates a success result with the provided value and optional success message.
    /// </summary>
    /// <param name="value">The value to be returned if the operation was successful.</param>
    /// <param name="successMessage">An optional success message.</param>
    /// <returns>A success result object.</returns>
    public static Result<T> Success(T value, string successMessage = "")
    {
        return new Result<T>(value, successMessage);
    }

    /// <summary>
    /// Creates an error result with the provided list of error messages.
    /// </summary>
    /// <param name="errors">A list of strings representing error messages.</param>
    /// <returns>An error result object.</returns>
    public static Result<T> Error(List<string> errors)
    {
        return new Result<T>(errors, ResultStatus.Error);
    }

    /// <summary>
    /// Creates an invalid result with the provided list of validation errors.
    /// </summary>
    /// <param name="validationErrors">A list of ValidationError objects representing validation errors.</param>
    /// <returns>An invalid result object.</returns>
    public static Result<T> Invalid(List<ValidationError> validationErrors)
    {
        return new Result<T>(validationErrors);
    }

    /// <summary>
    /// Creates a result indicating that the requested resource was not found.
    /// </summary>
    /// <returns>A not found result object.</returns>
    public static Result<T> NotFound()
    {
        return new Result<T>(ResultStatus.NotFound);
    }
}
