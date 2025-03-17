namespace Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;

public sealed class MaleMonthParser : IMonthParser<MaleMonthParser>
{
    public MonthParserResult Parse(ReadOnlySpan<char> personalIdentificationNumber)
    {
        var month = byte.Parse(personalIdentificationNumber[2..4]);

        if (month is < 1 or > 12)
        {
            throw new FormatException(CzechPersonalIdentificationNumber.InvalidMaleMonthMessage);
        }

        return new MonthParserResult() { Month = month, Sex = SexEnum.Male, };
    }

    public static MaleMonthParser DefaultInstance { get; } = new();
}