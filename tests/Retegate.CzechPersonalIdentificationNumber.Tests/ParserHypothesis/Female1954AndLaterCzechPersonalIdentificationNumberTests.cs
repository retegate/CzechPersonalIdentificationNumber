using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public class Female1954AndLaterCzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<Female1954AndLaterCzechPersonalIdentificationNumber,
        Female1954AndLaterCzechPersonalIdentificationNumberTests>,
    ISpecificCzechIdentificationNumberTests
{
    public static TheoryData<Base.PositiveTestScenario> ValidScenarios =>
    [
        new()
        {
            PersonalIdentificationNumber = " 745505/ 0119",
            DateOfBirth = new DateOnly(1974, 5, 5),
            Sex = SexEnum.Female,
        },
        new()
        {
            PersonalIdentificationNumber = "5455052437",
            DateOfBirth = new DateOnly(1954, 5, 5),
            Sex = SexEnum.Female,
        },
        new()
        {
            PersonalIdentificationNumber = " 866128 / 9120",
            DateOfBirth = new DateOnly(1986, 11, 28),
            Sex = SexEnum.Female
        },
        new()
        {
            PersonalIdentificationNumber = "105 728 38985",
            DateOfBirth = new DateOnly(2010, 7, 28),
            Sex = SexEnum.Female
        },
    ];

    public static TheoryData<string> InvalidPatternScenarios => CommonScenarios.InvalidPattern1954AndAfterScenarios;

    public static TheoryData<string> InvalidYearScenarios => CommonScenarios.InvalidYear1954AndAfterScenarios;

    public static TheoryData<string> InvalidMonthScenarios => CommonScenarios.InvalidFemaleMonthScenarios;

    public static TheoryData<string> InvalidDateScenarios =>CommonScenarios.InvalidDateScenarios;

    public static TheoryData<string> InvalidVerificationNumberScenarios =>
        CommonScenarios.InvalidVerificationNumber1954AndAfterScenarios;
}