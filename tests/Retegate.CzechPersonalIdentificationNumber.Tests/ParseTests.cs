namespace Retegate.CzechPersonalIdentificationNumber.Tests;

public class ParseTests
{
    #region Parse

    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.ValidPersonalIdentificationNumbers), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void Parse_WithValidPersonalIdentificationNumber_ReturnsCzechPersonalIdentificationNumber(CzechPersonalIdentificationNumberFixture.ValidPersonalIdentificationNumberTestCase validPersonalIdentificationNumberTestCase)
    {
        // Arrange
        CzechPersonalIdentificationNumber czechPersonalIdentificationNumber = null!;

        // Act
        var action = () => czechPersonalIdentificationNumber = CzechPersonalIdentificationNumber.Parse(validPersonalIdentificationNumberTestCase.PersonalIdentificationNumber);

        // Assert
        action.ShouldNotThrow();
        czechPersonalIdentificationNumber.ShouldNotBeNull();
    }

    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.InvalidPersonalIdentificationNumberPattern), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void Parse_WithInvalidPersonalIdentificationNumberPattern_ThrowsFormatException(string invalidCzechPersonalIdentificationNumber)
    {
        // Arrange
        CzechPersonalIdentificationNumber czechPersonalIdentificationNumber = null!;

        // Act
        var action = () => czechPersonalIdentificationNumber = CzechPersonalIdentificationNumber.Parse(invalidCzechPersonalIdentificationNumber);

        // Assert
        var ex = action.ShouldThrow<FormatException>();
        czechPersonalIdentificationNumber.ShouldBeNull();
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidFormatMessage);
    }

    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.InvalidPersonalIdentificationNumberMonth), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void Parse_InvalidPersonalIdentificationNumberMonth_ThrowsFormatException(string invalidCzechPersonalIdentificationNumber)
    {
        // Arrange
        CzechPersonalIdentificationNumber czechPersonalIdentificationNumber = null!;

        // Act
        var action = () => czechPersonalIdentificationNumber = CzechPersonalIdentificationNumber.Parse(invalidCzechPersonalIdentificationNumber);

        // Assert
        var ex = action.ShouldThrow<FormatException>();
        czechPersonalIdentificationNumber.ShouldBeNull();
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidMonthMessage);
    }

    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.InvalidPersonalIdentificationNumberDay), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void Parse_InvalidPersonalIdentificationNumberDay_ThrowsFormatException(string invalidCzechPersonalIdentificationNumber)
    {
        // Arrange
        CzechPersonalIdentificationNumber czechPersonalIdentificationNumber = null!;

        // Act
        var action = () => czechPersonalIdentificationNumber = CzechPersonalIdentificationNumber.Parse(invalidCzechPersonalIdentificationNumber);

        // Assert
        var ex = action.ShouldThrow<FormatException>();
        czechPersonalIdentificationNumber.ShouldBeNull();
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidDayMessage);
    }

    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.InvalidPersonalIdentificationNumberModulo), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void Parse_InvalidPersonalIdentificationNumberModulo_ThrowsFormatException(string invalidCzechPersonalIdentificationNumber)
    {
        // Arrange
        CzechPersonalIdentificationNumber czechPersonalIdentificationNumber = null!;

        // Act
        var action = () => czechPersonalIdentificationNumber = CzechPersonalIdentificationNumber.Parse(invalidCzechPersonalIdentificationNumber);

        // Assert
        var ex = action.ShouldThrow<FormatException>();
        czechPersonalIdentificationNumber.ShouldBeNull();
        ex.ShouldNotBeNull();
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidModuloMessage);
    }

    #endregion Parse

    #region TryParse

    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.ValidPersonalIdentificationNumbers), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void TryParse_WithValidPersonalIdentificationNumber_ReturnsTrueAndCzechPersonalIdentificationNumber(CzechPersonalIdentificationNumberFixture.ValidPersonalIdentificationNumberTestCase validPersonalIdentificationNumberTestCase)
    {
        // Arrange

        // Act
        var result = CzechPersonalIdentificationNumber.TryParse(validPersonalIdentificationNumberTestCase.PersonalIdentificationNumber, out var czechPersonalIdentificationNumber);
        var alternativeResult = CzechPersonalIdentificationNumber.TryParse(validPersonalIdentificationNumberTestCase.PersonalIdentificationNumber, null, out var alternativeCzechPersonalIdentificationNumber);

        // Assert
        result.ShouldBeTrue();
        czechPersonalIdentificationNumber.ShouldNotBeNull();
        czechPersonalIdentificationNumber.NormalizedCzechNormalizedPersonalIdentificationNumber.ShouldBe(validPersonalIdentificationNumberTestCase.NormalizedPersonalIdentificationNumber);
        czechPersonalIdentificationNumber.Sex.ShouldBe(validPersonalIdentificationNumberTestCase.Sex);
        czechPersonalIdentificationNumber.DateOfBirth.ShouldBe(validPersonalIdentificationNumberTestCase.DateOfBirth);
        czechPersonalIdentificationNumber.CzechNormalizedPersonalIdentificationNumberFormattedWithSlash.ShouldBe(validPersonalIdentificationNumberTestCase.PersonalIdentificationNumberWithSlash);

        alternativeResult.ShouldBeTrue();

        alternativeCzechPersonalIdentificationNumber.ShouldBeEquivalentTo(czechPersonalIdentificationNumber);
    }

    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.InvalidOverallGroupedPersonalIdentificationNumbers), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void TryParse_WithInvalidPersonalIdentificationNumber_ReturnsFalseAndNull(string invalidCzechPersonalIdentificationNumber)
    {
        // Arrange

        // Act
        var result = CzechPersonalIdentificationNumber.TryParse(invalidCzechPersonalIdentificationNumber, out var czechPersonalIdentificationNumber);
        var alternativeResult = CzechPersonalIdentificationNumber.TryParse(invalidCzechPersonalIdentificationNumber, null, out var alternativeCzechPersonalIdentificationNumber);

        // Assert
        result.ShouldBeFalse();
        czechPersonalIdentificationNumber.ShouldBeNull();

        alternativeResult.ShouldBeFalse();
        alternativeCzechPersonalIdentificationNumber.ShouldBeNull();
    }

    #endregion TryParse
}