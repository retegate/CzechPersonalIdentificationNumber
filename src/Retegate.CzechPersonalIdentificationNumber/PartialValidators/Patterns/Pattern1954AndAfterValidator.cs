using System.Text.RegularExpressions;

namespace Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;

public sealed class Pattern1954AndAfterValidator : IPatternValidator<Pattern1954AndAfterValidator>
{
    internal const string Pattern = @"^\s*\d{6}\s?\/?\s?\d{4}\s*$";

    public void ValidateOrThrow(ReadOnlySpan<char> personalIdentificationNumber)
    {
        if (!Regex.IsMatch(personalIdentificationNumber, Pattern))
        {
            throw new FormatException(CzechPersonalIdentificationNumber.InvalidFormatMessage);
        }
    }

    public static Pattern1954AndAfterValidator DefaultInstance { get; } = new();
}