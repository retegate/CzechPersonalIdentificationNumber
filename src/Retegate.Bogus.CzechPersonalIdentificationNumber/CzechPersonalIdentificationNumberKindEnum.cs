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
    [SexMapping(ScenariosChoiceEnum.Male)] MaleAfter1954 = 5,

    [SexMapping(ScenariosChoiceEnum.Female)]
    FemaleAfter1954 = 6,
    [SexMapping(ScenariosChoiceEnum.Male)] ExceptionalMaleInPopulationBoom1974Till1985 = 7,
    [SexMapping(ScenariosChoiceEnum.Male)] ExceptionalFemaleInPopulationBoom1974Till1985 = 8,
    [SexMapping(ScenariosChoiceEnum.Male)] ExceptionalMaleInNewEraPopulationBoom2004 = 9,

    [SexMapping(ScenariosChoiceEnum.Female)]
    ExceptionalFemaleInNewEraPopulationBoom2004 = 10
}