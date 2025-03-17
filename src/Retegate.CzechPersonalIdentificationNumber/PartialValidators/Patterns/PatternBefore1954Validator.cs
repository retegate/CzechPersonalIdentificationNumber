using System.Text.RegularExpressions;

namespace Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;

public sealed class PatternBefore1954Validator : IPatternValidator<PatternBefore1954Validator>
{
    internal const string Pattern = @"^\s*\d{6}\s?\/?\s?\d{3}\s*$";

    public void ValidateOrThrow(ReadOnlySpan<char> personalIdentificationNumber)
    {
        if (!Regex.IsMatch(personalIdentificationNumber, Pattern))
        {
            throw new FormatException(CzechPersonalIdentificationNumber.InvalidFormatMessage);
        }
    }

    public static PatternBefore1954Validator DefaultInstance { get; } = new();
}