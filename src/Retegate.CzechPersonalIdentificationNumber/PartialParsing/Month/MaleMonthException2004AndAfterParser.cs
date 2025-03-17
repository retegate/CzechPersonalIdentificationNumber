namespace Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;

public sealed class MaleMonthException2004AndAfterParser : IMonthParser<MaleMonthException2004AndAfterParser>
{
    public MonthParserResult Parse(ReadOnlySpan<char> personalIdentificationNumber)
    {
        var month = byte.Parse(personalIdentificationNumber[2..4]);

        if (month is < 21 or > 32)
        {
            throw new FormatException(CzechPersonalIdentificationNumber.InvalidMaleMonthMessage);
        }

        return new MonthParserResult() { Month = (byte)(month - 20), Sex = SexEnum.Male, };
    }

    public static MaleMonthException2004AndAfterParser DefaultInstance { get; } = new();
}