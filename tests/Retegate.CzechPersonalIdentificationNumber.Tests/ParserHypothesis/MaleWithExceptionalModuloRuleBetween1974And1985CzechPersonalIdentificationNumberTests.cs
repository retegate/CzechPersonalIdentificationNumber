using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public sealed class MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<
        MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber,
        MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberTests,
        MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberScenarios>,
    ITestScenarios<MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberScenarios>
{
    public static MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberScenarios
        TestScenarios =>
        new();
}