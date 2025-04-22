using System.Diagnostics.CodeAnalysis;

using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;

public sealed class FemaleBefore1954CzechPersonalIdentificationNumber : CzechPersonalIdentificationNumber, IParsable<FemaleBefore1954CzechPersonalIdentificationNumber>, IContainValidationScenario<PatternBefore1954Validator, YearBefore1954Parser, FemaleMonthParser, VerificationNumberBefore1954Validator>
{
    private FemaleBefore1954CzechPersonalIdentificationNumber(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) : base(normalizedPersonalIdentificationNumber, dateOfBirth, SexEnum.Female)
    {
    }

    ///  <summary>
    ///  Parse the string to <see cref="FemaleBefore1954CzechPersonalIdentificationNumber"/>.
    ///  </summary>
    ///  <param name="potentialPersonalIdentificationNumber"></param>
    ///  <param name="_"></param>
    ///  <returns></returns>
    ///  <exception cref="FormatException"></exception>
    /// <para><see cref="Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.NullInputFormatMessage"/></para>
    /// <para><see cref="Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidFormatMessage"/></para>
    /// <para><see cref="Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidYearMessage"/></para>
    /// <para><see cref="Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidMaleMonthMessage"/></para>
    /// <para><see cref="Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidFemaleMonthMessage"/></para>
    /// <para><see cref="Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidDayMessage"/></para>
    /// <para><see cref="Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidVerificationNumberMessage"/></para>
    public static new FemaleBefore1954CzechPersonalIdentificationNumber Parse(string potentialPersonalIdentificationNumber, IFormatProvider? _ = null)
    {
        return ParsingHelper.Parse(potentialPersonalIdentificationNumber, ValidationScenario, Ctor);
    }

    public static bool TryParse([NotNullWhen(true)] string? potentialPersonalIdentificationNumber, IFormatProvider? provider, [MaybeNullWhen(false)] out FemaleBefore1954CzechPersonalIdentificationNumber result)
    {
        return ParsingHelper.TryParse(potentialPersonalIdentificationNumber, ValidationScenario, out result, Ctor);
    }

    private static FemaleBefore1954CzechPersonalIdentificationNumber Ctor(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) => new(normalizedPersonalIdentificationNumber, dateOfBirth);

    public static ValidationScenario<PatternBefore1954Validator, YearBefore1954Parser, FemaleMonthParser, VerificationNumberBefore1954Validator> ValidationScenario { get; } = new()
    {
        PatternValidator =  PatternBefore1954Validator.DefaultInstance, YearParser = YearBefore1954Parser.DefaultInstance, MonthParser = FemaleMonthParser.DefaultInstance, VerificationNumberValidator = VerificationNumberBefore1954Validator.DefaultInstance,
    };
}