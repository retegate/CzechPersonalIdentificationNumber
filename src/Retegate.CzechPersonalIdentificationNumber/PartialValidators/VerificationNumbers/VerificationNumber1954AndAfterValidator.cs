namespace Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

public sealed class VerificationNumber1954AndAfterValidator : IVerificationNumberValidator<VerificationNumber1954AndAfterValidator>
{
    public void ValidateOrThrow(ReadOnlySpan<char> personalIdentificationNumber)
    {
        var number = ulong.Parse(personalIdentificationNumber);

        if (number % CzechPersonalIdentificationNumber.VerificationNumberModuloDivider != 0)
        {
            throw new FormatException(CzechPersonalIdentificationNumber.InvalidVerificationNumberMessage);
        }
    }

    public static VerificationNumber1954AndAfterValidator DefaultInstance { get; } = new();
}