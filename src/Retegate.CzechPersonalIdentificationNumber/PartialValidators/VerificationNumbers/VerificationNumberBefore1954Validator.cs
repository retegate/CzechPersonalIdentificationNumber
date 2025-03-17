namespace Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

public sealed class VerificationNumberBefore1954Validator : IVerificationNumberValidator<VerificationNumberBefore1954Validator>
{
    public void ValidateOrThrow(ReadOnlySpan<char> personalIdentificationNumber) { }

    public static VerificationNumberBefore1954Validator DefaultInstance { get; } = new();
}