using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public sealed class MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<
        MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber,
        MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberTests>,
    ISpecificCzechIdentificationNumberTests
{
    public static TheoryData<Base.PositiveTestScenario> ValidScenarios =>
    [
        new()
        {
            PersonalIdentificationNumber = "0427282383",
            DateOfBirth = new DateOnly(2004, 7, 28),
            Sex = SexEnum.Male,
        },
        new()
        {
            PersonalIdentificationNumber = "2427280317",
            DateOfBirth = new DateOnly(2024, 7, 28),
            Sex = SexEnum.Male,
        },
    ];

    public static TheoryData<string> InvalidPatternScenarios => CommonScenarios.InvalidPattern1954AndAfterScenarios;

    public static TheoryData<string> InvalidYearScenarios => CommonScenarios.InvalidYear1954AndAfterScenarios;

    public static TheoryData<string> InvalidMaleMonthScenarios =>
        CommonScenarios.InvalidMaleMonth2004AndAfterExceptionalRuleScenarios;

    public static TheoryData<string> InvalidFemaleMonthScenarios => [];

    public static TheoryData<string> InvalidDateScenarios => CommonScenarios.InvalidMaleDate2004AndLaterScenarios;

    public static TheoryData<string> InvalidVerificationNumberScenarios =>
        CommonScenarios.InvalidMaleVerificationNumber2004AndAfterScenarios;
}