using Bogus;

namespace Retegate.Bogus.CzechPersonalIdentificationNumber.PartialGenerators;

public static class Female1954AndLaterCzechPersonalIdentificationNumberFakerExtension
{
    public static Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber GenerateFemale1954AndLaterCzechPersonalIdentificationNumber(this Faker faker, FormatEnum format)
    {
        return faker.GenerateCzechPersonalIdentificationNumber(
            CzechPersonalIdentificationNumberKindEnum.Female1954AndLaterWithNoExceptionalRules, format);
    }
}