using System.Diagnostics.CodeAnalysis;

using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Date;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Normalization;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.CzechPersonalIdentificationNumber;

internal static class ParsingHelper
{
    public static TIdentificationNumber Parse<TIdentificationNumber, TPatternValidator, TYearParser, TMonthParser, TVerificationNumberValidator>(
        string potentialPersonalIdentificationNumber,
        ValidationScenario<TPatternValidator, TYearParser, TMonthParser, TVerificationNumberValidator> scenario,
        Func<string, DateOnly, TIdentificationNumber> ctor
    )
        where TIdentificationNumber : CzechPersonalIdentificationNumber, IParsable<TIdentificationNumber>
        where TPatternValidator : class, IPatternValidator<TPatternValidator>
        where TYearParser : class, IYearParser<TYearParser>
        where TMonthParser : class, IMonthParser<TMonthParser>
        where TVerificationNumberValidator : class, IVerificationNumberValidator<TVerificationNumberValidator>
    {
        NotNullValidator.ValidateOrThrow(potentialPersonalIdentificationNumber);

        var normalizedPotentialPersonalIdentificationNumber = NormalizedParser.Parse(potentialPersonalIdentificationNumber);

        var potentialPersonalIdentificationNumberSpan = normalizedPotentialPersonalIdentificationNumber.AsSpan();

        scenario.PatternValidator.ValidateOrThrow(potentialPersonalIdentificationNumberSpan);

        var year = scenario.YearParser.Parse(potentialPersonalIdentificationNumberSpan);

        var monthResult = scenario.MonthParser.Parse(potentialPersonalIdentificationNumberSpan);

        var dateOfBirth = DateOfBirthParser.Parse(year, monthResult.Month, potentialPersonalIdentificationNumberSpan);

        scenario.VerificationNumberValidator.ValidateOrThrow(potentialPersonalIdentificationNumberSpan);

        return ctor(normalizedPotentialPersonalIdentificationNumber, dateOfBirth);
    }

    public static bool TryParse<TIdentificationNumber, TPatternValidator, TYearParser, TMonthParser, TVerificationNumberValidator>(
        [NotNullWhen(true)] string? potentialPersonalIdentificationNumber,
        ValidationScenario<TPatternValidator, TYearParser, TMonthParser, TVerificationNumberValidator> scenario,
        [MaybeNullWhen(false)] out TIdentificationNumber result,
        Func<string, DateOnly, TIdentificationNumber> ctor
    )
        where TIdentificationNumber : CzechPersonalIdentificationNumber, IParsable<TIdentificationNumber>
        where TPatternValidator : class, IPatternValidator<TPatternValidator>
        where TYearParser : class, IYearParser<TYearParser>
        where TMonthParser : class, IMonthParser<TMonthParser>
        where TVerificationNumberValidator : class, IVerificationNumberValidator<TVerificationNumberValidator>
    {
        try
        {
            result = Parse(potentialPersonalIdentificationNumber!, scenario, ctor);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }
}