namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis.Base;

public sealed class SpecificCzechIdentificationNumberTests
{
    static abstract TheoryData<PositiveTestScenario> ValidPersonalIdentificationNumbers { get; }
    static abstract TheoryData<string> InvalidPatternScenarios { get; }
    static abstract TheoryData<string>? InvalidMaleMonthScenarios { get; }
    static abstract TheoryData<string>? InvalidFemaleMonthScenarios { get; }
    static abstract TheoryData<string> InvalidDateScenarios { get; }
    static abstract TheoryData<string> InvalidVerificationNumberScenarios { get; }
}