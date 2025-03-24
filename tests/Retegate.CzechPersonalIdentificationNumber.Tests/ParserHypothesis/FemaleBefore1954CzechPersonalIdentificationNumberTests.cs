using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public sealed class FemaleBefore1954CzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<FemaleBefore1954CzechPersonalIdentificationNumber,
        FemaleBefore1954CzechPersonalIdentificationNumberTests,
        FemaleBefore1954CzechPersonalIdentificationNumberScenarios>,
    ITestScenarios<FemaleBefore1954CzechPersonalIdentificationNumberScenarios>
{
    public static FemaleBefore1954CzechPersonalIdentificationNumberScenarios TestScenarios => new();
}