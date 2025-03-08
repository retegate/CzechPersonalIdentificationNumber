using FluentValidation;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests;

public class ValidationTests
{
    private class TestModel(string personalIdentificationNumber)
    {
        public string PersonalIdentificationNumber { get; } = personalIdentificationNumber;
    }

    private class TestModelValidator : AbstractValidator<TestModel>
    {
        public TestModelValidator()
        {
            RuleFor(x => x.PersonalIdentificationNumber)
                .CzechPersonalIdentificationNumber();
        }
    }

    private readonly TestModelValidator _validator = new();

    [Fact]
    public void Validate_WithNull_ReturnsInvalidPatternFailure()
    {
        // Arrange
        var model = new TestModel(null!);

        // Act
        var result = _validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ErrorMessage.ShouldBe(CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.NullInputFormatMessage);
    }


    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.ValidPersonalIdentificationNumbers), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void Validate_WithValid_ReturnsSuccess(CzechPersonalIdentificationNumberFixture.ValidPersonalIdentificationNumberTestCase testCase)
    {
        // Arrange
        var model = new TestModel(testCase.PersonalIdentificationNumber);

        // Act
        var result = _validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.InvalidPersonalIdentificationNumberPattern), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void ValidatePattern_WithInvalidPattern_ReturnsInvalidPatternFailure(string personalIdentificationNumber)
    {
        // Arrange
        var model = new TestModel(personalIdentificationNumber);

        // Act
        var result = _validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ErrorMessage.ShouldBe(CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidFormatMessage);
    }

    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.InvalidPersonalIdentificationNumberMonth), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void ValidateYear_WithInvalidMonth_ReturnsInvalidYearFailure(string personalIdentificationNumber)
    {
        // Arrange
        var model = new TestModel(personalIdentificationNumber);

        // Act
        var result = _validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ErrorMessage.ShouldBe(CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidMonthMessage);
    }

    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.InvalidPersonalIdentificationNumberDay), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void ValidateMonth_WithInvalidDay_ReturnsInvalidDayFailure(string personalIdentificationNumber)
    {
        // Arrange
        var model = new TestModel(personalIdentificationNumber);

        // Act
        var result = _validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ErrorMessage.ShouldBe(CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidDayMessage);
    }

    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.InvalidPersonalIdentificationNumberModulo), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void ValidateMonth_WithInvalidModuloRule_ReturnsInvalidModuloFailure(string personalIdentificationNumber)
    {
        // Arrange
        var model = new TestModel(personalIdentificationNumber);

        // Act
        var result = _validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ErrorMessage.ShouldBe(CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidModuloMessage);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_WithEmpty_ReturnsInvalidPatternFailure(string personalIdentificationNumber)
    {
        // Arrange
        var model = new TestModel(personalIdentificationNumber);

        // Act
        var result = _validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ErrorMessage.ShouldBe(CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidFormatMessage);
    }
}