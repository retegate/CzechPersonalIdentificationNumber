using System.Diagnostics.CodeAnalysis;

using Retegate.CzechPersonalIdentificationNumber;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis;

public sealed class Male1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests : CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber, IParsable<Male1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests>, IContainValidationScenario<Pattern1954AndAfterValidator, Year1954AndAfterParser, MaleMonthParser, VerificationNumber1954AndAfterValidator>
{
    private Male1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) : base(normalizedPersonalIdentificationNumber, dateOfBirth, SexEnum.Male)
    {
    }

    public static new Male1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests Parse(string potentialPersonalIdentificationNumber, IFormatProvider? _ = null)
    {
        return ParsingHelper.Parse(potentialPersonalIdentificationNumber, ValidationScenario, Ctor);
    }

    public static bool TryParse([NotNullWhen(true)] string? potentialPersonalIdentificationNumber, IFormatProvider? provider, [MaybeNullWhen(false)] out Male1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests result)
    {
        return ParsingHelper.TryParse(potentialPersonalIdentificationNumber, ValidationScenario, out result, Ctor);
    }

    private static Male1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests Ctor(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) => new(normalizedPersonalIdentificationNumber, dateOfBirth);

    public static ValidationScenario<Pattern1954AndAfterValidator, Year1954AndAfterParser, MaleMonthParser, VerificationNumber1954AndAfterValidator> ValidationScenario { get; } = new()
    {
        PatternValidator = Pattern1954AndAfterValidator.DefaultInstance, YearParser = Year1954AndAfterParser.DefaultInstance, MonthParser = MaleMonthParser.DefaultInstance, VerificationNumberValidator = VerificationNumber1954AndAfterValidator.DefaultInstance,
    };
}