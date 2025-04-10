namespace Retegate.Bogus.CzechPersonalIdentificationNumber;

/// <summary>
/// Females are even, males are odd. 0 is arbitrary person.
/// </summary>
public enum CzechPersonalIdentificationNumberKindEnum : byte
{
    ArbitraryPerson = 0,
    ArbitraryMale = 1,
    ArbitraryFemale = 2,

    [SexMapping(ScenariosChoiceEnum.Male)] MaleBefore1954 = 3,

    [SexMapping(ScenariosChoiceEnum.Female)]
    FemaleBefore1954 = 4,
    [SexMapping(ScenariosChoiceEnum.Male)] Male1954AndLaterWithNoExceptionalRules = 5,

    [SexMapping(ScenariosChoiceEnum.Female)]
    Female1954AndLaterWithNoExceptionalRules = 6,
    [SexMapping(ScenariosChoiceEnum.Male)] ExceptionalRuleMaleInPopulationBoom1974Till1985 = 7,
    [SexMapping(ScenariosChoiceEnum.Female)] ExceptionalRuleFemaleInPopulationBoom1974Till1985 = 8,
    [SexMapping(ScenariosChoiceEnum.Male)] ExceptionalRuleMaleInNewEraPopulationBoom2004 = 9,

    [SexMapping(ScenariosChoiceEnum.Female)]
    ExceptionalRuleFemaleInNewEraPopulationBoom2004 = 10
}