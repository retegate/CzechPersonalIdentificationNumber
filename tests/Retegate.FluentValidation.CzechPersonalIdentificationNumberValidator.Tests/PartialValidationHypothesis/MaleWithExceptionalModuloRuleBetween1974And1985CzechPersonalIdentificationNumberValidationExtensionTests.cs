using System.Diagnostics.CodeAnalysis;

using Retegate.CzechPersonalIdentificationNumber;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis;

public sealed class MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtensionTests : CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber, IParsable<MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtensionTests>, IContainValidationScenario<Pattern1954AndAfterValidator, Year1954AndAfterParser, MaleMonthParser, VerificationNumberExceptionsBetween1974And1985Validator>
{
    private MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtensionTests(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) : base(normalizedPersonalIdentificationNumber, dateOfBirth, SexEnum.Male)
    {
    }

    public static new MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtensionTests Parse(string potentialPersonalIdentificationNumber, IFormatProvider? _ = null)
    {
        return ParsingHelper.Parse(potentialPersonalIdentificationNumber, ValidationScenario, Ctor);
    }

    public static bool TryParse([NotNullWhen(true)] string? potentialPersonalIdentificationNumber, IFormatProvider? provider, [MaybeNullWhen(false)] out MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtensionTests result)
    {
        return ParsingHelper.TryParse(potentialPersonalIdentificationNumber, ValidationScenario, out result, Ctor);
    }

    private static MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtensionTests Ctor(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) => new(normalizedPersonalIdentificationNumber, dateOfBirth);

    public static ValidationScenario<Pattern1954AndAfterValidator, Year1954AndAfterParser, MaleMonthParser, VerificationNumberExceptionsBetween1974And1985Validator> ValidationScenario { get; } = new()
    {
        PatternValidator = Pattern1954AndAfterValidator.DefaultInstance, YearParser = Year1954AndAfterParser.DefaultInstance, MonthParser = MaleMonthParser.DefaultInstance, VerificationNumberValidator = VerificationNumberExceptionsBetween1974And1985Validator.DefaultInstance,
    };
}