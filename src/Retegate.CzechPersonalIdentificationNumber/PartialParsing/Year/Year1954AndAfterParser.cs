namespace Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;

public sealed class Year1954AndAfterParser : IYearParser<Year1954AndAfterParser>
{
    public ushort Parse(ReadOnlySpan<char> personalIdentificationNumber)
    {
        var year = ushort.Parse(personalIdentificationNumber[..2]);

        if (year is >= 54 and <= 99)
        {
            return (ushort)(year + CzechPersonalIdentificationNumber.NineteenHundred);
        }

        if (year <= DateTime.Now.Year - CzechPersonalIdentificationNumber.TwoThousand)
        {
            return (ushort)(year + CzechPersonalIdentificationNumber.TwoThousand);
        }

        throw new FormatException(CzechPersonalIdentificationNumber.InvalidYearMessage);
    }

    public static Year1954AndAfterParser DefaultInstance { get; } = new();
}