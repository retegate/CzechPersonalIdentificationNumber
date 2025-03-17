namespace Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

public sealed class VerificationNumberExceptionsBetween1974And1985Validator : IVerificationNumberValidator<VerificationNumberExceptionsBetween1974And1985Validator>
{
    public void ValidateOrThrow(ReadOnlySpan<char> personalIdentificationNumber)
    {
        if (personalIdentificationNumber[^1] != '0')
        {
            throw new FormatException(CzechPersonalIdentificationNumber.InvalidVerificationNumberMessage);
        }

        var number = ulong.Parse($"{personalIdentificationNumber[..^1]}10");

        if (number % CzechPersonalIdentificationNumber.VerificationNumberModuloDivider != 0)
        {
            throw new FormatException(CzechPersonalIdentificationNumber.InvalidVerificationNumberMessage);
        }
    }

    public static VerificationNumberExceptionsBetween1974And1985Validator DefaultInstance { get; } = new();
}