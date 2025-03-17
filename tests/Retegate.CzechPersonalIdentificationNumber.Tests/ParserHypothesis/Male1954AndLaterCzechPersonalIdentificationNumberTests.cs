using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public sealed class Male1954AndLaterCzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<Male1954AndLaterCzechPersonalIdentificationNumber,
        Male1954AndLaterCzechPersonalIdentificationNumberTests>,
    ISpecificCzechIdentificationNumberTests
{
    public static TheoryData<Base.PositiveTestScenario> ValidScenarios =>
    [
        new()
        {
            PersonalIdentificationNumber = " 740505/ 6296",
            DateOfBirth = new DateOnly(1974, 5, 5),
            Sex = SexEnum.Male,
        },
        new()
        {
            PersonalIdentificationNumber = "5405054182", DateOfBirth = new DateOnly(1954, 5, 5), Sex = SexEnum.Male,
        },
        new()
        {
            PersonalIdentificationNumber = " 850728 / 3455 ",
            DateOfBirth = new DateOnly(1986, 7, 28),
            Sex = SexEnum.Male
        },
        new()
        {
            PersonalIdentificationNumber = "20 072 8/93 72",
            DateOfBirth = new DateOnly(2020, 7, 28),
            Sex = SexEnum.Male
        },
    ];

    public static TheoryData<string> InvalidPatternScenarios => CommonScenarios.InvalidPattern1954AndAfterScenarios;

    public static TheoryData<string> InvalidYearScenarios => CommonScenarios.InvalidYear1954AndAfterScenarios;

    public static TheoryData<string> InvalidMonthScenarios => CommonScenarios.InvalidMaleScenarios;

    public static TheoryData<string> InvalidDateScenarios => CommonScenarios.InvalidDateScenarios;

    public static TheoryData<string> InvalidVerificationNumberScenarios =>
        CommonScenarios.InvalidVerificationNumber1954AndAfterScenarios;
}