using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public sealed class MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<
        MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber,
        MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberScenarios>,
    ITestScenarios<MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberScenarios>
{
    public static MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberScenarios TestScenarios =>
        new();
}