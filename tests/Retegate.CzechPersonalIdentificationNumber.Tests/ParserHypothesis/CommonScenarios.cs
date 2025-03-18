namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public static class CommonScenarios
{
    public static TheoryData<string> InvalidFemaleMonthBefore1954Scenarios =>
        ["540101/1234", "541201/1234", "545001/1234"];

    public static TheoryData<string> InvalidMaleBefore1954Scenarios =>
    [
        "540001/1234", "541301/1234", "545101/1234"
    ];

    jak toto vyřešit, aby to bylo správně? to
    public static TheoryData<string> InvalidFemaleMonth1954AndLaterScenarios =>
        [{"540101/1234",CzechPersonalIdentificationNumber.}, {"541201/1234", {"545001/1234"];

    public static TheoryData<string> InvalidMale1954AndLaterScenarios =>
    [
        "540001/1234", "541301/1234", "545101/1234"
    ];

    public static TheoryData<string> InvalidFemaleMonth2004AndAfterExceptionalRuleScenarios =>
    [
        "546101/1234", "545301/1234", "547001/1234", "548301/1234"
    ];

    public static TheoryData<string> InvalidMaleMonth2004AndAfterExceptionalRuleScenarios =>
    [
        "5464101/1234", "540301/1234", "542001/1234", "543301/1234"
    ];

    public static TheoryData<string> InvalidMaleDate1954AndLaterScenarios =>
    [
        "540132/1234",
        "540100/1234",
    ];

    public static TheoryData<string> InvalidFemaleDate1954AndLaterScenarios =>
    [
        "545132/1234",
        "545100/1234",
    ];

    public static TheoryData<string> InvalidMaleDateBefore1954Scenarios =>
    [
        "530132/123",
        "530100/123",
    ];

    public static TheoryData<string> InvalidFemaleDateBefore1954Scenarios =>
    [
        "535132/123",
        "535100/123",
    ];

    public static TheoryData<string> InvalidPatternBefore1954Scenarios =>
    [
        "1234561234",
        "123456/12",
        "123456 1234",
        "123456*123",
        "a23456123",
        "1a34561234",
        "123456123b"
    ];

    public static TheoryData<string> InvalidPattern1954AndAfterScenarios =>
    [
        "123456123",
        "123456/123",
        "123456 12345",
        "123456*1234",
        "a234561234",
        "1a34561234",
        "123456123b"
    ];

    public static TheoryData<string> InvalidVerificationNumber1954AndAfterScenarios => [" 745505/ 8791"];

    public static TheoryData<string> InvalidYear1954AndAfterScenarios => ["530101/1234"];

    public static TheoryData<string> InvalidYearBefore1954Scenarios =>
    [
        "540101/123",
    ];
}