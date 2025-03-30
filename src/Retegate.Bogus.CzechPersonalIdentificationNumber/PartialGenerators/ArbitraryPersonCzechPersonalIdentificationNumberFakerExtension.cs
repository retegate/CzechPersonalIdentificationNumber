using Bogus;

namespace Retegate.Bogus.CzechPersonalIdentificationNumber.PartialGenerators;

public static class ArbitraryPersonCzechPersonalIdentificationNumberFakerExtension
{
    public static Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber
        GenerateArbitraryPersonCzechPersonalIdentificationNumber(this Faker faker, FormatEnum format)
    {
        return faker.GenerateCzechPersonalIdentificationNumber(
            CzechPersonalIdentificationNumberKindEnum.ArbitraryPerson, format);
    }
}