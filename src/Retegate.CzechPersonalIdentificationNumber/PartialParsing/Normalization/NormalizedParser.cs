namespace Retegate.CzechPersonalIdentificationNumber.PartialParsing.Normalization;

internal static class NormalizedParser
{
    public static string Parse(string personalIdentificationNumber)
    {
        return personalIdentificationNumber
            .Replace(CzechPersonalIdentificationNumber.ControlNumberOptionalSeparator.ToString(), string.Empty)
            .Replace(" ", string.Empty);
    }
}