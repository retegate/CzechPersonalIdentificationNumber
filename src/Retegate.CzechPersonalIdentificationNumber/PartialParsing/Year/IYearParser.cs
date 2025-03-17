namespace Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;

public interface IYearParser<TYearParser> : IInstance<TYearParser>
{
    ushort Parse(ReadOnlySpan<char> personalIdentificationNumber);
}