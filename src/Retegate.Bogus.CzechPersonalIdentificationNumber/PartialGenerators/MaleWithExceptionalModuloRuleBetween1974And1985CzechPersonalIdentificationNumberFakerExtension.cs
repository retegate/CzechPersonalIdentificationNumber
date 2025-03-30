using Bogus;

namespace Retegate.Bogus.CzechPersonalIdentificationNumber.PartialGenerators;

public static class MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberFakerExtension
{
    public static Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber GenerateMaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber(this Faker faker, FormatEnum format)
    {
        return faker.GenerateCzechPersonalIdentificationNumber(
            CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleMaleInPopulationBoom1974Till1985, format);
    }
}