using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public sealed class Male1954AndLaterCzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<Male1954AndLaterCzechPersonalIdentificationNumber,
        Male1954AndLaterCzechPersonalIdentificationNumberScenarios>,
    ITestScenarios<Male1954AndLaterCzechPersonalIdentificationNumberScenarios>
{
    public static Male1954AndLaterCzechPersonalIdentificationNumberScenarios TestScenarios =>
        new();
}