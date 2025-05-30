using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;

public sealed class MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberScenarios : ISpecificCzechIdentificationNumberTests
{
    public static TheoryData<Base.PositiveTestScenario> ValidScenarios =>
    [
        new()
        {
            PersonalIdentificationNumber = "7407287370",
            DateOfBirth = new DateOnly(1974, 7, 28),
            Sex = SexEnum.Male,
        },
        new()
        {
            PersonalIdentificationNumber = "8507287150",
            DateOfBirth = new DateOnly(1985, 7, 28),
            Sex = SexEnum.Male,
        },
    ];

    public static TheoryData<string> InvalidPatternScenarios => CommonScenarios.InvalidPattern1954AndAfterScenarios;

    public static TheoryData<string> InvalidYearScenarios => CommonScenarios.InvalidYear1954AndAfterScenarios;

    public static TheoryData<string> InvalidMaleMonthScenarios =>
        CommonScenarios.InvalidFemaleDate1954AndLaterScenarios;

    public static TheoryData<string> InvalidFemaleMonthScenarios => [];

    public static TheoryData<string> InvalidDateScenarios => CommonScenarios.InvalidMaleDate1954AndLaterScenarios;

    public static TheoryData<string> InvalidVerificationNumberScenarios => ["8507287152"];
}