namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

public interface ISpecificCzechIdentificationNumberTests
{
    static abstract TheoryData<PositiveTestScenario> ValidScenarios { get; }
    static abstract TheoryData<string> InvalidPatternScenarios { get; }
    static abstract TheoryData<string> InvalidYearScenarios { get; }
    static abstract TheoryData<string>? InvalidMaleMonthScenarios { get; }
    static abstract TheoryData<string>? InvalidFemaleMonthScenarios { get; }
    static abstract TheoryData<string> InvalidDateScenarios { get; }
    static abstract TheoryData<string> InvalidVerificationNumberScenarios { get; }
}