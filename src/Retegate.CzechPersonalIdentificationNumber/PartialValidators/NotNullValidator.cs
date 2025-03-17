namespace Retegate.CzechPersonalIdentificationNumber.PartialValidators;

internal static class NotNullValidator
{
    internal static void ValidateOrThrow(string? personalIdentificationNumber)
    {
        if (personalIdentificationNumber is null)
        {
            throw new FormatException(CzechPersonalIdentificationNumber.NullInputFormatMessage);
        }
    }
}