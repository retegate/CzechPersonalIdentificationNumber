namespace Retegate.Bogus.CzechPersonalIdentificationNumber;

[AttributeUsage(AttributeTargets.Field)]
public sealed class SexMappingAttribute(ScenariosChoiceEnum scenariosChoice) : Attribute
{
    public ScenariosChoiceEnum ScenariosChoice { get; } = scenariosChoice;
}