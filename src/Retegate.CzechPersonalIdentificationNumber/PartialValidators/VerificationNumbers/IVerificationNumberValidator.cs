namespace Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

public interface IVerificationNumberValidator<TImplementation> : IInstance<TImplementation>
    where TImplementation : IVerificationNumberValidator<TImplementation>
{
    void ValidateOrThrow(ReadOnlySpan<char> personalIdentificationNumber);
}