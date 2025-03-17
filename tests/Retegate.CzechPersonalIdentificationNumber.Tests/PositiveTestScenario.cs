namespace Retegate.CzechPersonalIdentificationNumber.Tests;

public sealed class PositiveTestScenario
{
    public required string Name { get; init; }
    public required string PersonalIdentificationNumber { get; init; }
    public required DateOnly ExpectedDateOfBirth { get; init; }
    public required SexEnum ExpectedSex { get; init; }
    public required Type ExpectedResolvingParserType { get; init; }

    public override string ToString()
    {
        return Name;
    }
}