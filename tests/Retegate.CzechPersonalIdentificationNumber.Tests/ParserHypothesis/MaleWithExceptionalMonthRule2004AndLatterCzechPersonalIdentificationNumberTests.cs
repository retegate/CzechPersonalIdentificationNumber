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
        ["546410/1234", "540301/1234", "542001/1234", "543301/1234"];

    public static TheoryData<string> InvalidFemaleMonthScenarios => [];

    public static TheoryData<string> InvalidDateScenarios => ["042132/1234", "142100/1234"];

    public static TheoryData<string> InvalidVerificationNumberScenarios => [" 042505 / 4514"];
}