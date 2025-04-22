using System.Diagnostics.CodeAnalysis;

using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;

public sealed class MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber : CzechPersonalIdentificationNumber, IParsable<MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber>, IContainValidationScenario<Pattern1954AndAfterValidator, Year1954AndAfterParser, MaleMonthException2004AndAfterParser, VerificationNumber1954AndAfterValidator>
{
    private MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) : base(normalizedPersonalIdentificationNumber, dateOfBirth, SexEnum.Male)
    {
    }

    ///  <summary>
    ///  Parse the string to <see cref="MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber"/>.
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
    public static new MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber Parse(string potentialPersonalIdentificationNumber, IFormatProvider? _ = null)
    {
        return ParsingHelper.Parse(potentialPersonalIdentificationNumber, ValidationScenario, Ctor);
    }

    public static bool TryParse([NotNullWhen(true)] string? potentialPersonalIdentificationNumber, IFormatProvider? provider, [MaybeNullWhen(false)] out MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber result)
    {
        return ParsingHelper.TryParse(potentialPersonalIdentificationNumber, ValidationScenario, out result, Ctor);
    }

    private static MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber Ctor(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) => new(normalizedPersonalIdentificationNumber, dateOfBirth);
    
    public static ValidationScenario<Pattern1954AndAfterValidator, Year1954AndAfterParser, MaleMonthException2004AndAfterParser, VerificationNumber1954AndAfterValidator> ValidationScenario { get; } = new()
    {
        PatternValidator = Pattern1954AndAfterValidator.DefaultInstance, YearParser = Year1954AndAfterParser.DefaultInstance, MonthParser = MaleMonthException2004AndAfterParser.DefaultInstance, VerificationNumberValidator = VerificationNumber1954AndAfterValidator.DefaultInstance,
    };
}