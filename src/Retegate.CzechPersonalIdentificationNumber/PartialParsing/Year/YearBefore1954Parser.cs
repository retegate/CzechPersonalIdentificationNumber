namespace Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;

public sealed class YearBefore1954Parser : IYearParser<YearBefore1954Parser>
{
    public ushort Parse(ReadOnlySpan<char> personalIdentificationNumber)
    {
        var year = ushort.Parse(personalIdentificationNumber[..2]);

        if (year >= 54)
        {
            throw new FormatException(CzechPersonalIdentificationNumber.InvalidYearMessage);
        }

        return (ushort)(year + CzechPersonalIdentificationNumber.NineteenHundred);
    }

    public static YearBefore1954Parser DefaultInstance { get; } = new();
}