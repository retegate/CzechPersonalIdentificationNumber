using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.CzechPersonalIdentificationNumber;

public sealed class ValidationScenario<TPatternValidator, TYearParser, TMonthParser, TVerificationNumberValidator>
    where TPatternValidator : class, IPatternValidator<TPatternValidator>
    where TYearParser : class, IYearParser<TYearParser>
    where TMonthParser : class, IMonthParser<TMonthParser>
    where TVerificationNumberValidator : class, IVerificationNumberValidator<TVerificationNumberValidator>
{
    public required TPatternValidator PatternValidator { get; init; }
    public required TYearParser YearParser { get; init; }
    public required TMonthParser MonthParser { get; init; }
    public required TVerificationNumberValidator VerificationNumberValidator { get; init; }
}