using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;

public sealed class FemaleBefore1954CzechPersonalIdentificationNumberScenarios : ISpecificCzechIdentificationNumberTests
{
    public static Female1954AndLaterCzechPersonalIdentificationNumberScenarios TestScenarios => new();

    public static TheoryData<Base.PositiveTestScenario> ValidScenarios =>
    [
        new()
        {
            PersonalIdentificationNumber = "536224178",
            DateOfBirth = new DateOnly(1953, 12, 24),
            Sex = SexEnum.Female,
        },
    ];

    public static TheoryData<string> InvalidPatternScenarios => CommonScenarios.InvalidPatternBefore1954Scenarios;

    public static TheoryData<string> InvalidYearScenarios => CommonScenarios.InvalidYearBefore1954Scenarios;

    public static TheoryData<string> InvalidMaleMonthScenarios => [];

    public static TheoryData<string> InvalidFemaleMonthScenarios => ["450101/123", "531201/234", "535001/234"];

    public static TheoryData<string> InvalidDateScenarios => ["535132/123", "535100/123"];

    public static TheoryData<string> InvalidVerificationNumberScenarios => [];
}