using Bogus;

namespace Retegate.Bogus.CzechPersonalIdentificationNumber.PartialGenerators;

public static class FemaleFakerExtension
{
    public static Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber
        GenerateFemaleCzechPersonalIdentificationNumber(this Faker faker, FormatEnum format)
    {
        return faker.GenerateCzechPersonalIdentificationNumber(
            CzechPersonalIdentificationNumberKindEnum.ArbitraryFemale, format);
    }
}