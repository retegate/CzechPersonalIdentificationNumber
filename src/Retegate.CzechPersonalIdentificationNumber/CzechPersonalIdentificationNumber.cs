using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Retegate.CzechPersonalIdentificationNumber;

public partial class CzechPersonalIdentificationNumber : IParsable<CzechPersonalIdentificationNumber>
{
    private const string Pattern = @"^\s*\d{6}\s?\/?\s?\d{3,4}\s*$";

    public const ushort NineteenHundred = 1900;
    public const ushort TwoThousand = 2000;
    public const ushort NewNumberStyleStartYear = 1954;
    public const ushort PopulationBoomStartYear = 1974;
    public const ushort PopulationBoomEndYear = 1985;
    public const ushort NewEraPopulationBoomStartYear = 2004;
    public const byte WomenMonthOffset = 50;
    public const byte MenMonthOffsetInPopulationBoom = 20;
    public const byte WomenMonthOffsetInPopulationBoom = 70;
    public const byte ModuloDivider = 11;
    public const byte ModuloException = 10;

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

    public string CzechNormalizedPersonalIdentificationNumberFormattedWithSlash => $"{NormalizedCzechNormalizedPersonalIdentificationNumber[..6]}/{NormalizedCzechNormalizedPersonalIdentificationNumber[6..]}";

    public static CzechPersonalIdentificationNumber Parse(string potentialPersonalIdentificationNumber, IFormatProvider? formatProvider = null)
    {
        ArgumentNullException.ThrowIfNull(potentialPersonalIdentificationNumber);

        if (!ValidatePattern(potentialPersonalIdentificationNumber))
        {
            throw new FormatException("The personal identification number is not in the correct format YYMMDD(/)XXX(X).");
        }

        potentialPersonalIdentificationNumber = potentialPersonalIdentificationNumber.Trim();
        potentialPersonalIdentificationNumber = potentialPersonalIdentificationNumber.Replace("/", string.Empty);
        potentialPersonalIdentificationNumber = potentialPersonalIdentificationNumber.Replace(" ", string.Empty);

        var year = int.Parse(potentialPersonalIdentificationNumber[..2]);
        if (!ValidateYear(ref year))
        {
            throw new FormatException("The year part of the personal identification number is not in the correct format.");
        }

        var month = int.Parse(potentialPersonalIdentificationNumber[2..4]);
        var sex = SexEnum.Male;
        if (!ValidateMonth(year, ref month, ref sex))
        {
            throw new FormatException("The month part of the personal identification number is not in the correct format.");
        }

        var day = int.Parse(potentialPersonalIdentificationNumber[4..6]);
        if (!ValidateDay(year, month, day))
        {
            throw new FormatException("The day part of the personal identification number is not in the correct format (Such date of birth not exist).");
        }

        // can be inverted but this is more readable and consistent with the rest of the checks
        if (!ValidateModulo(year, potentialPersonalIdentificationNumber))
        {
            throw new FormatException("The personal identification number is not in the correct format (The modulo is not correct).");
        }

        return new CzechPersonalIdentificationNumber(potentialPersonalIdentificationNumber, new DateOnly(year, month, day), sex);
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

        //todo: testy
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

    internal static bool ValidateYear(ref int year)
    {
        year += (year > (DateTime.Now.Year - TwoThousand) ? NineteenHundred : TwoThousand);
        return year >= NineteenHundred && year <= DateTime.Now.Year;
    }

    internal static bool ValidateMonth(int year, ref int month, ref SexEnum sex)
    {
        if (year >= NewEraPopulationBoomStartYear && month >= WomenMonthOffsetInPopulationBoom)
        {
            sex = SexEnum.Female;
            month -= WomenMonthOffsetInPopulationBoom;
        }
        else if (month >= WomenMonthOffset)
        {
            sex = SexEnum.Female;
            month -= WomenMonthOffset;
        }
        else if (year >= NewEraPopulationBoomStartYear && month >= MenMonthOffsetInPopulationBoom)
        {
            sex = SexEnum.Male;
            month -= MenMonthOffsetInPopulationBoom;
        }
        else
        {
            sex = SexEnum.Male;
        }

        return month is >= 1 and <= 12;
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

        if (personalIdentificationNumber.Length != ModuloException)
        {
            return false;
        }

        if (number % ModuloDivider == 0)
        {
            return true;
        }

        if (year is < PopulationBoomStartYear or > PopulationBoomEndYear)
        {
            return false;
        }

        var partialNumber = ulong.Parse(personalIdentificationNumber[..^1]);
        ulong lastDigit = 0;
        for (ulong i = 0; i < ModuloDivider; i++)
        {
            if ((partialNumber + i) % ModuloDivider != 0)
                continue;

            lastDigit = i;
            break;
        }

        return lastDigit != ModuloException || personalIdentificationNumber.Last() == '0';
    }

    
}