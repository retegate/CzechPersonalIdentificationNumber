using System.Diagnostics.CodeAnalysis;

using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;

public sealed class Male1954AndLaterCzechPersonalIdentificationNumber : CzechPersonalIdentificationNumber, IParsable<Male1954AndLaterCzechPersonalIdentificationNumber>, IContainValidationScenario<Pattern1954AndAfterValidator, Year1954AndAfterParser, MaleMonthParser, VerificationNumber1954AndAfterValidator>
{
    private Male1954AndLaterCzechPersonalIdentificationNumber(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) : base(normalizedPersonalIdentificationNumber, dateOfBirth, SexEnum.Male)
    {
    }

    public static new Male1954AndLaterCzechPersonalIdentificationNumber Parse(string potentialPersonalIdentificationNumber, IFormatProvider? _ = null)
    {
        return ParsingHelper.Parse(potentialPersonalIdentificationNumber, ValidationScenario, Ctor);
    }

    public static bool TryParse([NotNullWhen(true)] string? potentialPersonalIdentificationNumber, IFormatProvider? provider, [MaybeNullWhen(false)] out Male1954AndLaterCzechPersonalIdentificationNumber result)
    {
        return ParsingHelper.TryParse(potentialPersonalIdentificationNumber, ValidationScenario, out result, Ctor);
    }

    private static Male1954AndLaterCzechPersonalIdentificationNumber Ctor(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) => new(normalizedPersonalIdentificationNumber, dateOfBirth);

    public static ValidationScenario<Pattern1954AndAfterValidator, Year1954AndAfterParser, MaleMonthParser, VerificationNumber1954AndAfterValidator> ValidationScenario { get; } = new()
    {
        PatternValidator = Pattern1954AndAfterValidator.DefaultInstance, YearParser = Year1954AndAfterParser.DefaultInstance, MonthParser = MaleMonthParser.DefaultInstance, VerificationNumberValidator = VerificationNumber1954AndAfterValidator.DefaultInstance,
    };
}