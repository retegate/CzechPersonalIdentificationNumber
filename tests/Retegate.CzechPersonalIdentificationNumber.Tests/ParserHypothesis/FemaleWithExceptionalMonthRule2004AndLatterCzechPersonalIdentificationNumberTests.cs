using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public sealed class FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<
        FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber,
        FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberTests>,
    ISpecificCzechIdentificationNumberTests
{
    public static TheoryData<Base.PositiveTestScenario> ValidScenarios =>
    [
        new()
        {
            PersonalIdentificationNumber = "0473241813",
            DateOfBirth = new DateOnly(2004, 03, 24),
            Sex = SexEnum.Female,
        },
        new()
        {
            PersonalIdentificationNumber = "0482249724",
            DateOfBirth = new DateOnly(2004, 12, 24),
            Sex = SexEnum.Female,
        },
        new()
        {
            PersonalIdentificationNumber = "2482249033",
            DateOfBirth = new DateOnly(2024, 12, 24),
            Sex = SexEnum.Female,
        },
    ];

    public static TheoryData<string> InvalidPatternScenarios => CommonScenarios.InvalidPattern1954AndAfterScenarios;

    public static TheoryData<string> InvalidYearScenarios => CommonScenarios.InvalidYear1954AndAfterScenarios;

    public static TheoryData<string> InvalidMaleMonthScenarios => [];

    public static TheoryData<string> InvalidFemaleMonthScenarios =>
        CommonScenarios.InvalidFemaleMonth2004AndAfterExceptionalRuleScenarios;

    public static TheoryData<string> InvalidDateScenarios => CommonScenarios.InvalidFemaleDate2004AndLaterScenarios;

    public static TheoryData<string> InvalidVerificationNumberScenarios =>
        CommonScenarios.InvalidFemaleVerificationNumber2004AndAfterScenarios;
}