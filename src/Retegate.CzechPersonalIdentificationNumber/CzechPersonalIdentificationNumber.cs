using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Retegate.CzechPersonalIdentificationNumber;

public partial class CzechPersonalIdentificationNumber : IParsable<CzechPersonalIdentificationNumber>
{
    private const string Pattern = @"^\s*\d{6}\s?\/?\s?\d{3,4}\s*$";

    internal const ushort NineteenHundred = 1900;
    internal const ushort TwoThousand = 2000;
    internal const ushort NewNumberStyleStartYear = 1954;
    internal const ushort PopulationBoomStartYear = 1974;
    internal const ushort PopulationBoomEndYear = 1985;
    internal const ushort NewEraPopulationBoomStartYear = 2004;
    internal const byte WomenMonthOffset = 50;
    internal const byte MenMonthOffsetInPopulationBoom = 20;
    internal const byte WomenMonthOffsetInPopulationBoom = 70;
    internal const byte ModuloDivider = 11;
    internal const byte ModuloException = 10;
    internal const byte MonthsInYear = 12;
    internal const byte PersonalIdentificationNumberLengthAfterOldFormat = 10;
    internal const char ControlNumberOptionalSeparator = '/';

    internal const string NullInputFormatMessage = "The personal identification number cannot be null.";
    internal static readonly string InvalidFormatMessage = $"The personal identification number is not in the correct format YYMMDD({ControlNumberOptionalSeparator})XXX(X).";
    internal const string InvalidYearMessage = "The year part of the personal identification number is not in the correct format.";
    internal const string InvalidMonthMessage = "The month part of the personal identification number is not in the correct format.";
    internal const string InvalidDayMessage = "The day part of the personal identification number is not in the correct format (Such date of birth not exist).";
    internal const string InvalidModuloMessage = "The personal identification number is not in the correct format (The modulo is not correct).";

    [GeneratedRegex(Pattern)]
    private static partial Regex ValidationRegex();

    public static bool ValidatePattern(string personalIdentificationNumber)
    {
        return ValidationRegex().IsMatch(personalIdentificationNumber);
    }

    private CzechPersonalIdentificationNumber(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth, SexEnum sex)
    {
        NormalizedCzechNormalizedPersonalIdentificationNumber = normalizedPersonalIdentificationNumber;
        DateOfBirth = dateOfBirth;
        Sex = sex;
    }

    public string NormalizedCzechNormalizedPersonalIdentificationNumber { get; }
    public DateOnly DateOfBirth { get; }

    public SexEnum Sex { get; }

    public string CzechNormalizedPersonalIdentificationNumberFormattedWithSlash => $"{NormalizedCzechNormalizedPersonalIdentificationNumber[..6]}{ControlNumberOptionalSeparator}{NormalizedCzechNormalizedPersonalIdentificationNumber[6..]}";

    public static CzechPersonalIdentificationNumber Parse(string potentialPersonalIdentificationNumber, IFormatProvider? formatProvider = null)
    {
        if (potentialPersonalIdentificationNumber is null)
        {
            throw new FormatException(NullInputFormatMessage);
        }

        ArgumentNullException.ThrowIfNull(potentialPersonalIdentificationNumber);

        if (!ValidatePattern(potentialPersonalIdentificationNumber))
        {
            throw new FormatException(InvalidFormatMessage);
        }

        potentialPersonalIdentificationNumber = potentialPersonalIdentificationNumber.Trim();
        potentialPersonalIdentificationNumber = potentialPersonalIdentificationNumber.Replace(ControlNumberOptionalSeparator.ToString(), string.Empty);
        potentialPersonalIdentificationNumber = potentialPersonalIdentificationNumber.Replace(" ", string.Empty);

        var year = int.Parse(potentialPersonalIdentificationNumber[..2]);
        var validateShortYearResult = ValidateShortYear(year, potentialPersonalIdentificationNumber);
        if (!validateShortYearResult.IsValid)
        {
            throw new FormatException(InvalidYearMessage);
        }

        var month = int.Parse(potentialPersonalIdentificationNumber[2..4]);
        var validateMonthResult = ValidateMonth(validateShortYearResult.Year!.Value, month);
        if (!validateMonthResult.IsValid)
        {
            throw new FormatException(InvalidMonthMessage);
        }

        var day = int.Parse(potentialPersonalIdentificationNumber[4..6]);
        if (!ValidateDay(validateShortYearResult.Year.Value, validateMonthResult.Month!.Value, day))
        {
            throw new FormatException(InvalidDayMessage);
        }

        if (!ValidateModulo(validateShortYearResult.Year.Value, potentialPersonalIdentificationNumber))
        {
            throw new FormatException(InvalidModuloMessage);
        }

        return new CzechPersonalIdentificationNumber(potentialPersonalIdentificationNumber, new DateOnly(validateShortYearResult.Year.Value, validateMonthResult.Month!.Value, day), validateMonthResult.Sex!.Value);
    }

    public static bool TryParse(string potentialPersonalIdentificationNumber, [MaybeNullWhen(false)] out CzechPersonalIdentificationNumber result) =>
        TryParse(potentialPersonalIdentificationNumber, null, out result);

    public static bool TryParse(string? potentialPersonalIdentificationNumber, IFormatProvider? formatProvider, [MaybeNullWhen(false)] out CzechPersonalIdentificationNumber result)
    {
        if (potentialPersonalIdentificationNumber is null)
        {
            result = null;
            return false;
        }

        try
        {
            result = Parse(potentialPersonalIdentificationNumber);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }

    internal sealed class ValidateShortYearResult
    {
        public int? Year { get; init; }
        public required bool IsValid { get; init; }
    }

    internal static ValidateShortYearResult ValidateShortYear(int year, string personalIdentificationNumber)
    {
        if (year is < 0 or > 99)
        {
            return new ValidateShortYearResult { IsValid = false };
        }

        if (personalIdentificationNumber.Length == 9)
        {
            return new ValidateShortYearResult { IsValid = true, Year = NineteenHundred + year, };
        }

        return new ValidateShortYearResult { IsValid = true, Year = (year > DateTime.Now.Year - TwoThousand ? NineteenHundred : TwoThousand) + year };
    }

    internal sealed class ValidateMonthResult
    {
        public int? Month { get; init; }
        public SexEnum? Sex { get; init; }
        public required bool IsValid { get; init; }
    }

    internal static ValidateMonthResult ValidateMonth(int year, int month)
    {
        SexEnum sex;
        if (year >= NewEraPopulationBoomStartYear && month is >= WomenMonthOffsetInPopulationBoom and <= WomenMonthOffsetInPopulationBoom + MonthsInYear)
        {
            sex = SexEnum.Female;
            month -= WomenMonthOffsetInPopulationBoom;
        }
        else if (month is >= WomenMonthOffset and <= WomenMonthOffset + MonthsInYear)
        {
            sex = SexEnum.Female;
            month -= WomenMonthOffset;
        }
        else if (year >= NewEraPopulationBoomStartYear && month is >= MenMonthOffsetInPopulationBoom and <= MenMonthOffsetInPopulationBoom + MonthsInYear)
        {
            sex = SexEnum.Male;
            month -= MenMonthOffsetInPopulationBoom;
        }
        else
        {
            sex = SexEnum.Male;
        }

        var isValid = month is >= 1 and <= 12;

        return new ValidateMonthResult() { IsValid = isValid, Month = isValid ? month : null, Sex = isValid ? sex : null };
    }

    internal static bool ValidateDay(int year, int month, int day)
    {
        var possibleDate = $"{year}-{month:D2}-{day:D2}";
        return DateOnly.TryParse(possibleDate, out _);
    }

    public static bool ValidateModulo(int year, string personalIdentificationNumber)
    {
        var number = ulong.Parse(personalIdentificationNumber);

        if (year < NewNumberStyleStartYear)
        {
            return personalIdentificationNumber.Length == 9;
        }

        if (personalIdentificationNumber.Length != PersonalIdentificationNumberLengthAfterOldFormat)
        {
            return false;
        }

        if (number % ModuloDivider == 0)
        {
            return true;
        }

        if (personalIdentificationNumber.Last() != '0')
        {
            return false;
        }

        if (year > PopulationBoomEndYear)
        {
            return false;
        }

        personalIdentificationNumber = $"{personalIdentificationNumber[..^1]}10";

        number = ulong.Parse(personalIdentificationNumber);

        return number % ModuloDivider == 0;
    }
}