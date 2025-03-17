using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;
using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public sealed class FemaleBefore1954CzechPersonalIdentificationNumberTests :
    SpecificCzechIdentificationNumberTestsBase<FemaleBefore1954CzechPersonalIdentificationNumber,
        FemaleBefore1954CzechPersonalIdentificationNumberTests>,
    ISpecificCzechIdentificationNumberTests
{
    public static TheoryData<Base.PositiveTestScenario> ValidScenarios =>
    [
        new()
        {
            PersonalIdentificationNumber = "5312241785",
            DateOfBirth = new DateOnly(1953, 12, 24),
            Sex = SexEnum.Female,
        },
    ];

    public static TheoryData<string> InvalidPatternScenarios => CommonScenarios.InvalidPatternBefore1954Scenarios;

    public static TheoryData<string> InvalidYearScenarios => CommonScenarios.InvalidYearBefore1954Scenarios;

    public static TheoryData<string> InvalidMonthScenarios => CommonScenarios.InvalidFemaleMonthScenarios;

    public static TheoryData<string> InvalidDateScenarios =>CommonScenarios.InvalidDateScenarios;

    public static TheoryData<string> InvalidVerificationNumberScenarios => [];
}