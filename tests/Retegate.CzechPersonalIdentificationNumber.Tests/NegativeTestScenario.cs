namespace Retegate.CzechPersonalIdentificationNumber.Tests;

public sealed class NegativeTestScenario
{
    public required string Name { get; set; }
    public required string PersonalIdentificationNumber { get; set; }
    public required string ExpectedExceptionMessage { get; set; }
}