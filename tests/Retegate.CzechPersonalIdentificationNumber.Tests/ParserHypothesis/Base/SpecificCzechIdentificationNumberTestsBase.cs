namespace Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

public abstract class
    SpecificCzechIdentificationNumberTestsBase<TPersonalIdentificationNumber, TSpecificCzechIdentificationNumberTests>
    where TPersonalIdentificationNumber : CzechPersonalIdentificationNumber,
    IParsable<TPersonalIdentificationNumber>
    where TSpecificCzechIdentificationNumberTests : ISpecificCzechIdentificationNumberTests
{
    public static TheoryData<PositiveTestScenario> ValidScenariosOverride =>
        TSpecificCzechIdentificationNumberTests.ValidScenarios;

    [Theory]
    [MemberData(nameof(ValidScenariosOverride))]
    public void Parse_ValidInput_ReturnsIdentificationNumber(PositiveTestScenario scenario)
    {
        // Arrange and Act
        var parseResult =
            TPersonalIdentificationNumber.Parse(scenario.PersonalIdentificationNumber, null);
        var tryParseResult =
            TPersonalIdentificationNumber.TryParse(scenario.PersonalIdentificationNumber, null,
                out var tryParseOutput);

        // Assert
        parseResult.ShouldNotBeNull();
        parseResult.DateOfBirth.ShouldBe(scenario.DateOfBirth);
        parseResult.Sex.ShouldBe(scenario.Sex);

        tryParseResult.ShouldBeTrue();
        tryParseOutput.ShouldNotBeNull();

        tryParseOutput.ShouldBeEquivalentTo(parseResult);
    }

    [Fact]
    public void Parse_NullInput_ThrowsFormatException()
    {
        InvalidScenarioProcessing(null!, CzechPersonalIdentificationNumber.NullInputFormatMessage);
    }

    [Theory]
    [MemberData(nameof(InvalidPatternScenariosOverride))]
    public void Parse_InvalidPatternInput_ThrowsFormatException(string input)
    {
        InvalidScenarioProcessing(input, CzechPersonalIdentificationNumber.InvalidFormatMessage);
    }

    [Theory]
    [MemberData(nameof(InvalidYearScenariosOverride))]
    public void Parse_InvalidYearInput_ThrowsFormatException(string input)
    {
        InvalidScenarioProcessing(input, CzechPersonalIdentificationNumber.InvalidYearMessage);
    }

    [Theory]
    [MemberData(nameof(InvalidMaleMonthScenariosOverride), DisableDiscoveryEnumeration = true)]
    public void Parse_InvalidMaleMonthInput_ThrowsFormatException(string input)
    {
        InvalidScenarioProcessing(input, CzechPersonalIdentificationNumber.InvalidMaleMonthMessage);
    }

    [Theory]
    [MemberData(nameof(InvalidFemaleMonthScenariosOverride), DisableDiscoveryEnumeration = true)]
    public void Parse_InvalidFemaleMonthInput_ThrowsFormatException(string input)
    {
        InvalidScenarioProcessing(input, CzechPersonalIdentificationNumber.InvalidFemaleMonthMessage);
    }

    [Theory]
    [MemberData(nameof(InvalidDateScenariosOverride))]
    public void Parse_InvalidDateInput_ThrowsFormatException(string input)
    {
        InvalidScenarioProcessing(input, CzechPersonalIdentificationNumber.InvalidDayMessage);
    }

    [Theory]
    [MemberData(nameof(InvalidVerificationNumberScenariosOverride),
        DisableDiscoveryEnumeration = true)] //tolerates empty scenario collection
    public void Parse_InvalidVerificationNumberInput_ThrowsFormatException(string input)
    {
        InvalidScenarioProcessing(input, CzechPersonalIdentificationNumber.InvalidVerificationNumberMessage);
    }

    private void InvalidScenarioProcessing(string input, string expectedMessage)
    {
        // Arrange
        TPersonalIdentificationNumber parseResult = null!;
        var tryParseResult = true;
        TPersonalIdentificationNumber tryParseOutput = null!;

        // Act
        var action = () =>
        {
            tryParseResult =
                TPersonalIdentificationNumber.TryParse(input, null!, out tryParseOutput!);
            parseResult = TPersonalIdentificationNumber.Parse(input, null!);
        };

        // Assert
        var ex = action.ShouldThrow<FormatException>();
        ex.Message.ShouldContain(expectedMessage);

        parseResult.ShouldBeNull();
        tryParseOutput.ShouldBeNull();
        tryParseResult.ShouldBeFalse();
    }


    public static TheoryData<string> InvalidPatternScenariosOverride =>
        TSpecificCzechIdentificationNumberTests.InvalidPatternScenarios;

    public static TheoryData<string> InvalidYearScenariosOverride =>
        TSpecificCzechIdentificationNumberTests.InvalidYearScenarios;

    public static TheoryData<string>? InvalidMaleMonthScenariosOverride =>
        TSpecificCzechIdentificationNumberTests.InvalidMaleMonthScenarios;

    public static TheoryData<string>? InvalidFemaleMonthScenariosOverride =>
        TSpecificCzechIdentificationNumberTests.InvalidFemaleMonthScenarios;

    public static TheoryData<string> InvalidDateScenariosOverride =>
        TSpecificCzechIdentificationNumberTests.InvalidDateScenarios;

    public static TheoryData<string> InvalidVerificationNumberScenariosOverride =>
        TSpecificCzechIdentificationNumberTests.InvalidVerificationNumberScenarios;
}