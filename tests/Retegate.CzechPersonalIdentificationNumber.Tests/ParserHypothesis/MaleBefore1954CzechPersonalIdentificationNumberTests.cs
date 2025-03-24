using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public sealed class MaleBefore1954CzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<MaleBefore1954CzechPersonalIdentificationNumber,
        MaleBefore1954CzechPersonalIdentificationNumberTests, MaleBefore1954CzechPersonalIdentificationNumberScenarios>,
    ITestScenarios<MaleBefore1954CzechPersonalIdentificationNumberScenarios>
{
    public static MaleBefore1954CzechPersonalIdentificationNumberScenarios TestScenarios =>
        new();
}