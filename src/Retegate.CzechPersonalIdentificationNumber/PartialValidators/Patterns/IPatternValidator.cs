namespace Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;

public interface IPatternValidator<TImplementation> : IInstance<TImplementation>
    where TImplementation : IPatternValidator<TImplementation>
{
    void ValidateOrThrow(ReadOnlySpan<char> personalIdentificationNumber);
}