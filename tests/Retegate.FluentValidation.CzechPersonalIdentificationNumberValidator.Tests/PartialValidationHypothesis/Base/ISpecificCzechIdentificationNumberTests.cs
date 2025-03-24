using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis.Base;

public interface ISpecificCzechIdentificationNumberTests
{
    static abstract TheoryData<PositiveTestScenario> ValidPersonalIdentificationNumbers { get; }
    static abstract TheoryData<string> InvalidPatternScenarios { get; }
    static abstract TheoryData<string>? InvalidMaleMonthScenarios { get; }
    static abstract TheoryData<string>? InvalidFemaleMonthScenarios { get; }
    static abstract TheoryData<string> InvalidDateScenarios { get; }
    static abstract TheoryData<string> InvalidVerificationNumberScenarios { get; }
}