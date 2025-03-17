using System.Diagnostics.CodeAnalysis;

using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;

public sealed class MaleBefore1954CzechPersonalIdentificationNumber : CzechPersonalIdentificationNumber, IParsable<MaleBefore1954CzechPersonalIdentificationNumber>, IContainValidationScenario<PatternBefore1954Validator, YearBefore1954Parser, MaleMonthParser, VerificationNumberBefore1954Validator>
{
    private MaleBefore1954CzechPersonalIdentificationNumber(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) : base(normalizedPersonalIdentificationNumber, dateOfBirth, SexEnum.Male)
    {
    }

    public static new MaleBefore1954CzechPersonalIdentificationNumber Parse(string potentialPersonalIdentificationNumber, IFormatProvider? _ = null)
    {
        return ParsingHelper.Parse(potentialPersonalIdentificationNumber, ValidationScenario, Ctor);
    }

    public static bool TryParse([NotNullWhen(true)] string? potentialPersonalIdentificationNumber, IFormatProvider? provider, [MaybeNullWhen(false)] out MaleBefore1954CzechPersonalIdentificationNumber result)
    {
        return ParsingHelper.TryParse(potentialPersonalIdentificationNumber, ValidationScenario, out result, Ctor);
    }

    private static MaleBefore1954CzechPersonalIdentificationNumber Ctor(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) => new(normalizedPersonalIdentificationNumber, dateOfBirth);

    public static ValidationScenario<PatternBefore1954Validator, YearBefore1954Parser, MaleMonthParser, VerificationNumberBefore1954Validator> ValidationScenario { get; } = new()
    {
        PatternValidator = PatternBefore1954Validator.DefaultInstance, YearParser = YearBefore1954Parser.DefaultInstance, MonthParser = MaleMonthParser.DefaultInstance, VerificationNumberValidator = VerificationNumberBefore1954Validator.DefaultInstance,
    };
}