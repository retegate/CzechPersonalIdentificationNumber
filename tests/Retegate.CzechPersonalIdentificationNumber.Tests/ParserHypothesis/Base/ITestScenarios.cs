namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

public interface ITestScenarios<TTestScenarios>
where TTestScenarios : ISpecificCzechIdentificationNumberTests
{
    static abstract TTestScenarios TestScenarios { get; }
}