namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

public sealed class PositiveTestScenario
{
    public required string PersonalIdentificationNumber { get; init; }
    public required DateOnly DateOfBirth { get; init; }
    public required SexEnum Sex { get; init; }
}