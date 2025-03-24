using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public class Female1954AndLaterCzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<Female1954AndLaterCzechPersonalIdentificationNumber,
        Female1954AndLaterCzechPersonalIdentificationNumberTests,
        Female1954AndLaterCzechPersonalIdentificationNumberScenarios>,
    ITestScenarios<Female1954AndLaterCzechPersonalIdentificationNumberScenarios>
{
    public static Female1954AndLaterCzechPersonalIdentificationNumberScenarios TestScenarios => new();
}