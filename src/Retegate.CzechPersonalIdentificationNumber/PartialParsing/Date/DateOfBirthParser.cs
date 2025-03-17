namespace Retegate.CzechPersonalIdentificationNumber.PartialParsing.Date;

internal static class DateOfBirthParser
{
    public static DateOnly Parse(ushort year, byte month, ReadOnlySpan<char> personalIdentificationNumber)
    {
        var day = byte.Parse(personalIdentificationNumber[4..6]);

        if (DateOnly.TryParse($"{year:D4}-{month:D2}-{day:D2}", out var dateOfBirth))
        {
            return dateOfBirth;
        }

        throw new FormatException(CzechPersonalIdentificationNumber.InvalidDayMessage);
    }
}