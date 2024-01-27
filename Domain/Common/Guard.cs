using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Domain.Common;

public static class Guard
{
    public static T AgainstNull<T>(
        [NotNull][ValidatedNotNull] T? input,
        [CallerArgumentExpression(nameof(input))] string? parameterName = null,
        string? message = null)
    {
        if (input is null)
        {
            throw new ArgumentNullException(parameterName, message ?? $"Argument {parameterName} cannot be null.");
        }

        return input;
    }

    public static string AgainstNullOrEmpty(
        string? input,
        [CallerArgumentExpression(nameof(input))] string? parameterName = null,
        string? message = null)
    {
        Guard.AgainstNull(input, parameterName, message);

        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException(message ?? $"Argument {parameterName} cannot be null or empty.", parameterName);
        }

        return input;
    }
}
