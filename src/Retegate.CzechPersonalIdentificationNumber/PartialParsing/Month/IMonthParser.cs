namespace Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;

public interface IMonthParser<TMonthParser> : IInstance<TMonthParser>
{
    MonthParserResult Parse(ReadOnlySpan<char> personalIdentificationNumber);
}