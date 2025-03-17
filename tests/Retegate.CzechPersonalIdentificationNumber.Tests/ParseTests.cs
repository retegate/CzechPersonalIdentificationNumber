using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;

namespace Retegate.CzechPersonalIdentificationNumber.Tests;

public class ParseTests
{
    public static TheoryData<PositiveTestScenario> ValidScenarios =>
    [
        new()
        {
            Name = "Male before 1954",
            PersonalIdentificationNumber = "530410134",
            ExpectedDateOfBirth = new DateOnly(1953, 04, 10),
            ExpectedSex = SexEnum.Male,
            ExpectedResolvingParserType = typeof(MaleBefore1954CzechPersonalIdentificationNumber)
        },
        new()
        {
            Name = "Male before 1954 (slashed format)",
            PersonalIdentificationNumber = " 530 410 / 134",
            ExpectedDateOfBirth = new DateOnly(1953, 04, 10),
            ExpectedSex = SexEnum.Male,
            ExpectedResolvingParserType = typeof(MaleBefore1954CzechPersonalIdentificationNumber)
        },
        new()
        {
            Name = "Female before 1954",
            PersonalIdentificationNumber = "535410271",
            ExpectedDateOfBirth = new DateOnly(1953, 04, 10),
            ExpectedSex = SexEnum.Female,
            ExpectedResolvingParserType = typeof(FemaleBefore1954CzechPersonalIdentificationNumber)
        },
        new()
        {
            Name = "Male 1954 and after",
            PersonalIdentificationNumber = "76121 1 /4136 ",
            ExpectedDateOfBirth = new DateOnly(1976, 12, 11),
            ExpectedSex = SexEnum.Male,
            ExpectedResolvingParserType = typeof(Male1954AndLaterCzechPersonalIdentificationNumber)
        },
        new()
        {
            Name = "Female 1954 and after",
            PersonalIdentificationNumber = "765211/2622",
            ExpectedDateOfBirth = new DateOnly(1976, 2, 11),
            ExpectedSex = SexEnum.Female,
            ExpectedResolvingParserType = typeof(Female1954AndLaterCzechPersonalIdentificationNumber)
        },
        new()
        {
            Name = "Male with verification number rule exception between 1974 and 1985",
            PersonalIdentificationNumber = "7608111290",
            ExpectedDateOfBirth = new DateOnly(1976, 8, 11),
            ExpectedSex = SexEnum.Male,
            ExpectedResolvingParserType =
                typeof(MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber)
        },
        new()
        {
            Name = "Female with verification number rule exception between 1974 and 1985",
            PersonalIdentificationNumber = "7658112450",
            ExpectedDateOfBirth = new DateOnly(1976, 8, 11),
            ExpectedSex = SexEnum.Female,
            ExpectedResolvingParserType =
                typeof(FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber)
        },
        new()
        {
            Name = "Male with month rule exception after 2004",
            PersonalIdentificationNumber = "0428119604",
            ExpectedDateOfBirth = new DateOnly(2004, 8, 11),
            ExpectedSex = SexEnum.Male,
            ExpectedResolvingParserType =
                typeof(MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber)
        },
        new()
        {
            Name = "Female with month rule exception after 2004",
            PersonalIdentificationNumber = "047811/5308",
            ExpectedDateOfBirth = new DateOnly(2004, 8, 11),
            ExpectedSex = SexEnum.Female,
            ExpectedResolvingParserType =
                typeof(FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber)
        },
    ];

    [Theory]
    [MemberData(nameof(ValidScenarios))]
    public void Parse_WithValidPersonalIdentificationNumber_ReturnsCzechPersonalIdentificationNumber(
        PositiveTestScenario scenario)
    {
        // Arrange
        CzechPersonalIdentificationNumber parseResult = null!;
        var tryParseResult = false;
        CzechPersonalIdentificationNumber tryParseOutput = null!;

        // Act
        var action = () =>
        {
            parseResult =
                CzechPersonalIdentificationNumber.Parse(scenario.PersonalIdentificationNumber);

            tryParseResult =
                CzechPersonalIdentificationNumber.TryParse(scenario.PersonalIdentificationNumber, null,
                    out tryParseOutput!);
        };

        // Assert
        action.ShouldNotThrow();

        parseResult.ShouldNotBeNull();
        parseResult.DateOfBirth.ShouldBe(scenario.ExpectedDateOfBirth);
        parseResult.Sex.ShouldBe(scenario.ExpectedSex);
        parseResult.ShouldBeOfType(scenario.ExpectedResolvingParserType);

        tryParseResult.ShouldBeTrue();
        tryParseOutput.ShouldNotBeNull();

        parseResult.ShouldBeEquivalentTo(tryParseOutput);
    }

    [Fact]
    public void Parse_WithNullPersonalIdentificationNumber_ThrowsFormatException()
    {
        GeneralNegativeScenario(new NegativeTestScenario()
        {
            PersonalIdentificationNumber = null!,
            ExpectedExceptionMessage = CzechPersonalIdentificationNumber.NullInputFormatMessage,
            Name = "NullInput",
        });
    }

    public static TheoryData<NegativeTestScenario> InvalidPatternPersonalIdentificationNumberScenarios =>
    [
        new()
        {
            Name = "Invalid pattern too short for both main personal identification number lengths",
            PersonalIdentificationNumber = "12345612",
            ExpectedExceptionMessage = CzechPersonalIdentificationNumber.InvalidFormatMessage
        },
        new()
        {
            Name = "Invalid pattern too long for both main personal identification number lengths",
            PersonalIdentificationNumber = "12345612345",
            ExpectedExceptionMessage = CzechPersonalIdentificationNumber.InvalidFormatMessage
        },
        new()
        {
            Name = "Invalid pattern with forbidden character",
            PersonalIdentificationNumber = "A234561234",
            ExpectedExceptionMessage = CzechPersonalIdentificationNumber.InvalidFormatMessage
        },
    ];

    [Theory]
    [MemberData(nameof(InvalidPatternPersonalIdentificationNumberScenarios))]
    public void Parse_WithInvalidPatternPersonalIdentificationNumber_ThrowsFormatException(
        NegativeTestScenario scenario)
    {
        // Arrange
        CzechPersonalIdentificationNumber parseResult = null!;
        var tryParseResult = false;
        CzechPersonalIdentificationNumber tryParseOutput = null!;

        // Act  
        var action = () =>
        {
            parseResult = CzechPersonalIdentificationNumber.Parse(scenario.PersonalIdentificationNumber);
            tryParseResult =
                CzechPersonalIdentificationNumber.TryParse(scenario.PersonalIdentificationNumber, null,
                    out tryParseOutput!);
        };

        // Assert
        var ex = action.ShouldThrow<FormatException>();
        ex.Message.ShouldBe(scenario.ExpectedExceptionMessage);

        parseResult.ShouldBeNull();
        tryParseResult.ShouldBeFalse();
        tryParseOutput.ShouldBeNull();
    }

    public static TheoryData<NegativeTestScenario> InvalidYearPersonalIdentificationNumberScenarios =>
    [
        new()
        {
            Name = "Year our of the range (greater than expected according the overall length)",
            PersonalIdentificationNumber = "541224123",
            ExpectedExceptionMessage = CzechPersonalIdentificationNumber.InvalidYearMessage
        },
        new()
        {
            Name = "Year our of the range (shorter than expected according the overall length)",
            PersonalIdentificationNumber = "5312241234",
            ExpectedExceptionMessage = CzechPersonalIdentificationNumber.InvalidYearMessage
        },
    ];

    [Theory]
    [MemberData(nameof(InvalidYearPersonalIdentificationNumberScenarios))]
    public void Parse_WithInvalidYearPersonalIdentificationNumber_ThrowsFormatException(NegativeTestScenario scenario)
    {
        GeneralNegativeScenario(scenario);
    }

    public static TheoryData<NegativeTestScenario> InvalidMonthPersonalIdentificationNumberScenarios =>
    [
        new()
        {
            Name = "Zero month index",
            PersonalIdentificationNumber = "1200011234",
            ExpectedExceptionMessage = CzechPersonalIdentificationNumber.InvalidFemaleMonthMessage
        },
        new()
        {
            Name = "Zero month index",
            PersonalIdentificationNumber = "1213011234",
            ExpectedExceptionMessage = CzechPersonalIdentificationNumber.InvalidFemaleMonthMessage
        },
        new()
        {
            Name = "Zero month index",
            PersonalIdentificationNumber = "1250011234",
            ExpectedExceptionMessage = CzechPersonalIdentificationNumber.InvalidFemaleMonthMessage
        },
        new()
        {
            Name = "Zero month index",
            PersonalIdentificationNumber = "1263011234",
            ExpectedExceptionMessage = CzechPersonalIdentificationNumber.InvalidFemaleMonthMessage
        },
    ];

    [Theory]
    [MemberData(nameof(InvalidMonthPersonalIdentificationNumberScenarios))]
    public void Parse_WithInvalidMonthPersonalIdentificationNumber_ThrowsFormatException(NegativeTestScenario scenario)
    {
        GeneralNegativeScenario(scenario);
    }

    public static TheoryData<NegativeTestScenario> InvalidDayPersonalIdentificationNumberScenarios =>
    [
        new()
        {
            Name = "Zero day value",
            PersonalIdentificationNumber = "830300/1234",
            ExpectedExceptionMessage = CzechPersonalIdentificationNumber.InvalidDayMessage
        },
        new()
        {
            Name = "Too large day value",
            PersonalIdentificationNumber = "830332/1234",
            ExpectedExceptionMessage = CzechPersonalIdentificationNumber.InvalidDayMessage
        },
    ];

    [Theory]
    [MemberData(nameof(InvalidDayPersonalIdentificationNumberScenarios))]
    public void Parse_WithInvalidDayPersonalIdentificationNumber_ThrowsFormatException(NegativeTestScenario scenario)
    {
        GeneralNegativeScenario(scenario);
    }

    public static TheoryData<NegativeTestScenario> InvalidVerificationNumberPersonalIdentificationNumberScenarios =>
    [
        new()
        {
            Name = "Modulo fail 1954 and after",
            PersonalIdentificationNumber = "5404105541",
            ExpectedExceptionMessage = CzechPersonalIdentificationNumber.InvalidVerificationNumberMessage
        },
        new()
        {
            Name = "Modulo fail between 1974 and 1985",
            PersonalIdentificationNumber = "7504105529",
            ExpectedExceptionMessage = CzechPersonalIdentificationNumber.InvalidVerificationNumberMessage
        },
    ];

    [Theory]
    [MemberData(nameof(InvalidVerificationNumberPersonalIdentificationNumberScenarios))]
    public void Parse_WithInvalidVerificationNumberPersonalIdentificationNumber_ThrowsFormatException(
        NegativeTestScenario scenario)
    {
        GeneralNegativeScenario(scenario);
    }

    private void GeneralNegativeScenario(NegativeTestScenario scenario)
    {
        // Arrange
        CzechPersonalIdentificationNumber parseResult = null!;
        var tryParseResult = false;
        CzechPersonalIdentificationNumber tryParseOutput = null!;

        // Act  
        var action = () =>
        {
            parseResult = CzechPersonalIdentificationNumber.Parse(scenario.PersonalIdentificationNumber);
            tryParseResult =
                CzechPersonalIdentificationNumber.TryParse(scenario.PersonalIdentificationNumber, null,
                    out tryParseOutput!);
        };

        // Assert
        var ex = action.ShouldThrow<FormatException>();
        ex.Message.ShouldBe(scenario.ExpectedExceptionMessage);

        parseResult.ShouldBeNull();
        tryParseResult.ShouldBeFalse();
        tryParseOutput.ShouldBeNull();
    }
}