using Bogus;

namespace Retegate.Bogus.CzechPersonalIdentificationNumber.PartialGenerators;

public static class MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberFakerExtension
{
    public static Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber
        GenerateMaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber(
            this Faker faker, FormatEnum format)
    {
        return faker.GenerateCzechPersonalIdentificationNumber(
            CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleMaleInNewEraPopulationBoom2004, format);
    }
}