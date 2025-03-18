using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public sealed class MaleBefore1954CzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<MaleBefore1954CzechPersonalIdentificationNumber,
        MaleBefore1954CzechPersonalIdentificationNumberTests>,
    ISpecificCzechIdentificationNumberTests
{
    public static TheoryData<Base.PositiveTestScenario> ValidScenarios =>
    [
        new()
        {
            PersonalIdentificationNumber = "530728149", DateOfBirth = new DateOnly(1953, 7, 28), Sex = SexEnum.Male,
        },
    ];

    public static TheoryData<string> InvalidPatternScenarios => CommonScenarios.InvalidPatternBefore1954Scenarios;

    public static TheoryData<string> InvalidYearScenarios => CommonScenarios.InvalidYearBefore1954Scenarios;

    public static TheoryData<string> InvalidMaleMonthScenarios => CommonScenarios.InvalidMaleMonthBefore1954Scenarios;
    public static TheoryData<string> InvalidFemaleMonthScenarios => [];

    public static TheoryData<string> InvalidDateScenarios => CommonScenarios.InvalidMaleDateBefore1954Scenarios;

    public static TheoryData<string> InvalidVerificationNumberScenarios => [];
}