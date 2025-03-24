namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis;

public static class CommonScenarios
{

    public static TheoryData<string> InvalidMaleDate1954AndLaterScenarios =>
    [
        "540132/1234",
        "540100/1234",
    ];

    public static TheoryData<string> InvalidFemaleDate1954AndLaterScenarios =>
    [
        "045132/1234",
        "145100/1234",
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

    public static TheoryData<string> InvalidYear1954AndAfterScenarios => ["530101/1234"];

    public static TheoryData<string> InvalidYearBefore1954Scenarios =>
    [
        "540101/123",
    ];
}