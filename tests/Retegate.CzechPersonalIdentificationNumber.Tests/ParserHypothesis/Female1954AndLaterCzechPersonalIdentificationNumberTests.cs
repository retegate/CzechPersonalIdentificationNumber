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
            PersonalIdentificationNumber = "5455054143",
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
            PersonalIdentificationNumber = "1057289585",
            DateOfBirth = new DateOnly(2010, 7, 28),
            Sex = SexEnum.Female
        },
    ];

    public static TheoryData<string> InvalidPatternScenarios => CommonScenarios.InvalidPattern1954AndAfterScenarios;

    public static TheoryData<string> InvalidYearScenarios => CommonScenarios.InvalidYear1954AndAfterScenarios;

    public static TheoryData<string> InvalidMaleMonthScenarios => [];

    public static TheoryData<string> InvalidFemaleMonthScenarios =>
        CommonScenarios.InvalidFemaleMonth1954AndLaterScenarios;

    public static TheoryData<string> InvalidDateScenarios => CommonScenarios.InvalidFemaleDate1954AndLaterScenarios;

    public static TheoryData<string> InvalidVerificationNumberScenarios =>
        CommonScenarios.InvalidFemaleVerificationNumber1954AndAfterScenarios;
}