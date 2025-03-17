using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.CzechPersonalIdentificationNumber;

internal interface IContainValidationScenario<TPatternValidator, TYearParser, TMonthParser, TVerificationNumberValidator>
    where TPatternValidator : class, IPatternValidator<TPatternValidator>
    where TYearParser : class, IYearParser<TYearParser>
    where TMonthParser : class, IMonthParser<TMonthParser>
    where TVerificationNumberValidator : class, IVerificationNumberValidator<TVerificationNumberValidator>
{
    static abstract ValidationScenario<TPatternValidator, TYearParser, TMonthParser, TVerificationNumberValidator> ValidationScenario { get; }
}