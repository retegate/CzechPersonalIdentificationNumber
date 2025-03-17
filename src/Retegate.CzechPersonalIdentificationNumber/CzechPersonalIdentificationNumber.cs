using System.Diagnostics.CodeAnalysis;

using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;

namespace Retegate.CzechPersonalIdentificationNumber;

public class CzechPersonalIdentificationNumber : IParsable<CzechPersonalIdentificationNumber>
{
    internal const ushort NineteenHundred = 1900;
    internal const ushort TwoThousand = 2000;
    internal const ushort NewNumberStyleStartYear = 1954;
    internal const ushort PopulationBoomStartYear = 1974;
    internal const ushort PopulationBoomEndYear = 1985;
    internal const ushort NewEraPopulationBoomStartYear = 2004;
    internal const byte VerificationNumberModuloDivider = 11;
    internal const char ControlNumberOptionalSeparator = '/';

    internal const string NullInputFormatMessage = "The personal identification number cannot be null.";
    internal static readonly string InvalidFormatMessage = $"The personal identification number is not in the correct format YYMMDD({ControlNumberOptionalSeparator})XXX(X).";
    internal const string InvalidYearMessage = "The year part of the personal identification number is not in the correct format.";
    internal const string InvalidMaleMonthMessage = "The month part of the male personal identification number is not in the correct format.";
    internal const string InvalidFemaleMonthMessage = "The month part of the female personal identification number is not in the correct format.";
    internal const string InvalidDayMessage = "The day part of the personal identification number is not in the correct format (Such date of birth not exist).";
    internal const string InvalidVerificationNumberMessage = "The personal identification number is not in the correct format (The verification end number is not correct).";

    internal static readonly Dictionary<string, byte> InvalidFormatMessages = new()
    {
        { NullInputFormatMessage, 0 },
        { InvalidFormatMessage, 10 },
        { InvalidYearMessage, 20 },
        { InvalidMaleMonthMessage, 30 },
        { InvalidFemaleMonthMessage, 30 },
        { InvalidDayMessage, 40 },
        { InvalidVerificationNumberMessage, 50 }
    };

    internal CzechPersonalIdentificationNumber(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth, SexEnum sex)
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
        var validationHypotheses = new List<Func<string, IFormatProvider?, CzechPersonalIdentificationNumber>>
        {
            Female1954AndLaterCzechPersonalIdentificationNumber.Parse,
            FemaleBefore1954CzechPersonalIdentificationNumber.Parse,
            FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber.Parse,
            FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber.Parse,
            Male1954AndLaterCzechPersonalIdentificationNumber.Parse,
            MaleBefore1954CzechPersonalIdentificationNumber.Parse,
            MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber.Parse,
            MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber.Parse
        };

        var maximumScore = 0;
        var bestErrorMessage = NullInputFormatMessage;

        foreach (var hypothesis in validationHypotheses)
        {
            try
            {
                return hypothesis(potentialPersonalIdentificationNumber, formatProvider);
            }
            catch (FormatException formatException)
            {
                var score = InvalidFormatMessages[formatException.Message];
                if (score > maximumScore)
                {
                    maximumScore = score;
                    bestErrorMessage = formatException.Message;
                }
            }
        }

        throw new FormatException(bestErrorMessage);
    }

    public static bool TryParse(string? potentialPersonalIdentificationNumber, IFormatProvider? formatProvider, [MaybeNullWhen(false)] out CzechPersonalIdentificationNumber result)
    {
        try
        {
            result = Parse(potentialPersonalIdentificationNumber!);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }
}