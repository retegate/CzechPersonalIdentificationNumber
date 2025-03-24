using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public sealed class FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<
        FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber,
        FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberTests,
        FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberScenarios>,
    ITestScenarios<FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberScenarios>
{
    public static FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberScenarios
        TestScenarios => new();
}