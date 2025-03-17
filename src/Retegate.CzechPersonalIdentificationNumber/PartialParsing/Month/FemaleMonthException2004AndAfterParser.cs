namespace Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;

public sealed class FemaleMonthException2004AndAfterParser : IMonthParser<FemaleMonthException2004AndAfterParser>
{
    public MonthParserResult Parse(ReadOnlySpan<char> personalIdentificationNumber)
    {
        var month = byte.Parse(personalIdentificationNumber[2..4]);

        if (month is < 71 or > 82)
        {
            throw new FormatException(CzechPersonalIdentificationNumber.InvalidFemaleMonthMessage);
        }

        return new MonthParserResult() { Month = (byte)(month - 70), Sex = SexEnum.Female, };
    }

    public static FemaleMonthException2004AndAfterParser DefaultInstance { get; } = new();
}