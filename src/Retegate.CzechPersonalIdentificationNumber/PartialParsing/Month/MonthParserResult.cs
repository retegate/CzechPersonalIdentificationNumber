namespace Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;

public sealed class MonthParserResult
{
    public required byte Month { get; init; }
    public required SexEnum Sex { get; init; }
}