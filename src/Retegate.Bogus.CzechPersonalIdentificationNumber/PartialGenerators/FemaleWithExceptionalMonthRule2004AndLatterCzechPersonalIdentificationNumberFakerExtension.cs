using Bogus;

namespace Retegate.Bogus.CzechPersonalIdentificationNumber.PartialGenerators;

public static class FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberFakerExtension
{
    public static Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber GenerateFemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber(this Faker faker, FormatEnum format)
    {
        return faker.GenerateCzechPersonalIdentificationNumber(
            CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleFemaleInNewEraPopulationBoom2004, format);
    }
}