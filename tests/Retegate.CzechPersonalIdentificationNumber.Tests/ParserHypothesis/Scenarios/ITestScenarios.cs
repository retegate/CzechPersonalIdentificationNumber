using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;

public interface ITestScenarios<TTestScenarios>
where TTestScenarios : ISpecificCzechIdentificationNumberTests
{
    static abstract TTestScenarios TestScenarios { get; }
}