using System.Diagnostics.CodeAnalysis;

using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;

public sealed class FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber : CzechPersonalIdentificationNumber, IParsable<FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber>, IContainValidationScenario<Pattern1954AndAfterValidator, Year1954AndAfterParser, FemaleMonthParser, VerificationNumberExceptionsBetween1974And1985Validator>
{
    private FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) : base(normalizedPersonalIdentificationNumber, dateOfBirth, SexEnum.Female)
    {
    }

    public static new FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber Parse(string potentialPersonalIdentificationNumber, IFormatProvider? _ = null)
    {
        return ParsingHelper.Parse(potentialPersonalIdentificationNumber, ValidationScenario, Ctor);
    }

    public static bool TryParse([NotNullWhen(true)] string? potentialPersonalIdentificationNumber, IFormatProvider? provider, [MaybeNullWhen(false)] out FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber result)
    {
        return ParsingHelper.TryParse(potentialPersonalIdentificationNumber, ValidationScenario, out result, Ctor);
    }

    private static FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber Ctor(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) => new(normalizedPersonalIdentificationNumber, dateOfBirth);

    public static ValidationScenario<Pattern1954AndAfterValidator, Year1954AndAfterParser, FemaleMonthParser, VerificationNumberExceptionsBetween1974And1985Validator> ValidationScenario { get; } = new()
    {
        PatternValidator =  Pattern1954AndAfterValidator.DefaultInstance, YearParser = Year1954AndAfterParser.DefaultInstance, MonthParser = FemaleMonthParser.DefaultInstance, VerificationNumberValidator = VerificationNumberExceptionsBetween1974And1985Validator.DefaultInstance,
    };
}