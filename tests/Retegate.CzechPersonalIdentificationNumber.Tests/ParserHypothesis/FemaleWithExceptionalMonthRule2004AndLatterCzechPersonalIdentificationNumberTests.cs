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
        ["546101/1234", "545301/1234", "547001/1234", "548301/1234"];

    public static TheoryData<string> InvalidDateScenarios => ["047132/1234", "147100/1234",];

    public static TheoryData<string> InvalidVerificationNumberScenarios => [" 047505 /4516"];
}