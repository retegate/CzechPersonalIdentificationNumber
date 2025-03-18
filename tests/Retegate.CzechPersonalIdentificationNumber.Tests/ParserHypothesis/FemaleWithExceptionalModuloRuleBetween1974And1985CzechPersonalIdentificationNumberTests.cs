using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public sealed class FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<
        FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber,
        FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberTests>,
    ISpecificCzechIdentificationNumberTests
{
    public static TheoryData<Base.PositiveTestScenario> ValidScenarios =>
    [
        new()
        {
            PersonalIdentificationNumber = "7462242050",
            DateOfBirth = new DateOnly(1974, 12, 24),
            Sex = SexEnum.Female,
        },
        new()
        {
            PersonalIdentificationNumber = "8562247550",
            DateOfBirth = new DateOnly(1986, 12, 24),
            Sex = SexEnum.Female
        },
    ];

    public static TheoryData<string> InvalidPatternScenarios => CommonScenarios.InvalidPattern1954AndAfterScenarios;

    public static TheoryData<string> InvalidYearScenarios => CommonScenarios.InvalidYear1954AndAfterScenarios;

    public static TheoryData<string,string> InvalidMonthScenarios => CommonScenarios.InvalidFemaleMonth1954AndLaterScenarios;

    public static TheoryData<string> InvalidDateScenarios => CommonScenarios.InvalidFemaleDate1954AndLaterScenarios;

    public static TheoryData<string> InvalidVerificationNumberScenarios => ["7462242051"];
}