namespace Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;

public sealed class FemaleMonthParser : IMonthParser<FemaleMonthParser>
{
    public MonthParserResult Parse(ReadOnlySpan<char> personalIdentificationNumber)
    {
        var month = byte.Parse(personalIdentificationNumber[2..4]);

        if (month is < 51 or > 62)
        {
            throw new FormatException(CzechPersonalIdentificationNumber.InvalidFemaleMonthMessage);
        }

        return new MonthParserResult() { Month = (byte)(month - 50), Sex = SexEnum.Female, };
    }

    public static FemaleMonthParser DefaultInstance { get; } = new();
}