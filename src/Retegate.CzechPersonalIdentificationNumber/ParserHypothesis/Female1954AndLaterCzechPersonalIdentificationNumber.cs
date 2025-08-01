using System.Diagnostics.CodeAnalysis;

using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;

public sealed class Female1954AndLaterCzechPersonalIdentificationNumber : CzechPersonalIdentificationNumber,
    IParsable<Female1954AndLaterCzechPersonalIdentificationNumber>,
    IContainValidationScenario<Pattern1954AndAfterValidator, Year1954AndAfterParser, FemaleMonthParser,
        VerificationNumber1954AndAfterValidator>
{
    private Female1954AndLaterCzechPersonalIdentificationNumber(string normalizedPersonalIdentificationNumber,
        DateOnly dateOfBirth) : base(normalizedPersonalIdentificationNumber, dateOfBirth, SexEnum.Female)
    {
    }

    ///  <summary>
    ///  Parse the string to <see cref="Female1954AndLaterCzechPersonalIdentificationNumber"/>.
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
    public static new Female1954AndLaterCzechPersonalIdentificationNumber Parse(
        string potentialPersonalIdentificationNumber, IFormatProvider? _ = null)
    {
        return ParsingHelper.Parse(potentialPersonalIdentificationNumber, ValidationScenario, Ctor);
    }

    public static bool TryParse([NotNullWhen(true)] string? potentialPersonalIdentificationNumber,
        IFormatProvider? provider,
        [MaybeNullWhen(false)] out Female1954AndLaterCzechPersonalIdentificationNumber result)
    {
        return ParsingHelper.TryParse(potentialPersonalIdentificationNumber, ValidationScenario, out result, Ctor);
    }

    private static Female1954AndLaterCzechPersonalIdentificationNumber Ctor(
        string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) =>
        new(normalizedPersonalIdentificationNumber, dateOfBirth);

    public static
        ValidationScenario<Pattern1954AndAfterValidator, Year1954AndAfterParser, FemaleMonthParser,
            VerificationNumber1954AndAfterValidator> ValidationScenario { get; } = new()
    {
        PatternValidator = Pattern1954AndAfterValidator.DefaultInstance,
        YearParser = Year1954AndAfterParser.DefaultInstance,
        MonthParser = FemaleMonthParser.DefaultInstance,
        VerificationNumberValidator = VerificationNumber1954AndAfterValidator.DefaultInstance
    };
}