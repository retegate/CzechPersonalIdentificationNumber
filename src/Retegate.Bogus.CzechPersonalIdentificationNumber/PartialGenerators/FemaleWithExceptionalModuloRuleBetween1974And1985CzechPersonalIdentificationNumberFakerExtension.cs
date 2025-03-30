using Bogus;

namespace Retegate.Bogus.CzechPersonalIdentificationNumber.PartialGenerators;

public static class FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberFakerExtension
{
    public static Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber GenerateFemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentification(this Faker faker, FormatEnum format)
    {
        return faker.GenerateCzechPersonalIdentificationNumber(
            CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleFemaleInPopulationBoom1974Till1985, format);
    }
}