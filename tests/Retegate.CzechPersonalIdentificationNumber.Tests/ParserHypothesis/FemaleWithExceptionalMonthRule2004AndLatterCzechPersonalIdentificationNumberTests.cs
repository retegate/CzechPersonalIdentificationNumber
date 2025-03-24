using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public sealed class FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<
        FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber,
        FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberScenarios>,
    ITestScenarios<FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberScenarios>
{
    public static FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberScenarios TestScenarios =>
        new();
}