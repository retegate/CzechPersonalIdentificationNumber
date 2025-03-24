using FluentValidation;

using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Base;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis.Base;

public abstract class ValidationTestsBase<TValidator, TModel, TTestScenarios>(
    TValidator validator,
    Func<string, TModel> modelCtor)
    where TValidator : AbstractValidator<TModel>
    where TModel : class
    where TTestScenarios : ISpecificCzechIdentificationNumberTests
{
    [Fact]
    public void Validate_WithNull_ReturnsInvalidPatternFailure()
    {
        // Arrange
        var model = modelCtor(null!);

        // Act
        var result = validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ErrorMessage.ShouldBe(CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber
            .NullInputFormatMessage);
    }


    [Theory]
    [MemberData(nameof(ValidScenariosOverride))]
    public void Validate_WithValid_ReturnsSuccess(PositiveTestScenario testCase)
    {
        // Arrange
        var model = modelCtor(testCase.PersonalIdentificationNumber);

        // Act
        var result = validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Theory]
    [MemberData(nameof(InvalidPatternScenariosOverride))]
    public void ValidatePattern_WithInvalidPattern_ReturnsInvalidPatternFailure(string personalIdentificationNumber)
    {
        // Arrange
        var model = modelCtor(personalIdentificationNumber);

        // Act
        var result = validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ErrorMessage
            .ShouldBe(CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidFormatMessage);
    }

    [Theory]
    [MemberData(nameof(InvalidFemaleMonthScenariosOverride))]
    public void ValidateYear_WithFemaleInvalidMonth_ReturnsInvalidYearFailure(string personalIdentificationNumber)
    {
        // Arrange
        var model = modelCtor(personalIdentificationNumber);

        // Act
        var result = validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ErrorMessage
            .ShouldBe(CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidFemaleMonthMessage);
    }

    [Theory]
    [MemberData(nameof(InvalidMaleMonthScenariosOverride), DisableDiscoveryEnumeration = true)]
    public void ValidateYear_WithMaleInvalidMonth_ReturnsInvalidMaleMonthMessage(string personalIdentificationNumber)
    {
        // Arrange
        var model = modelCtor(personalIdentificationNumber);

        // Act
        var result = validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ErrorMessage
            .ShouldBe(CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidMaleMonthMessage);
    }

    [Theory]
    [MemberData(nameof(InvalidDateScenariosOverride))]
    public void ValidateMonth_WithInvalidDay_ReturnsInvalidDayFailure(string personalIdentificationNumber)
    {
        // Arrange
        var model = modelCtor(personalIdentificationNumber);

        // Act
        var result = validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ErrorMessage
            .ShouldBe(CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidDayMessage);
    }

    [Theory]
    [MemberData(nameof(InvalidVerificationNumberScenariosOverride))]
    public void ValidateMonth_WithInvalidModuloRule_ReturnsInvalidModuloFailure(string personalIdentificationNumber)
    {
        // Arrange
        var model = modelCtor(personalIdentificationNumber);

        // Act
        var result = validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ErrorMessage
            .ShouldBe(CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber
                .InvalidVerificationNumberMessage);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void Validate_WithEmpty_ReturnsInvalidPatternFailure(string personalIdentificationNumber)
    {
        // Arrange
        var model = modelCtor(personalIdentificationNumber);

        // Act
        var result = validator.Validate(model);

        // Assert
        result.ShouldNotBeNull();
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.First().ErrorMessage
            .ShouldBe(CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidFormatMessage);
    }

    public static TheoryData<PositiveTestScenario> ValidScenariosOverride => TTestScenarios.ValidScenarios;

    public static TheoryData<string> InvalidPatternScenariosOverride => TTestScenarios.InvalidPatternScenarios;

    public static TheoryData<string>? InvalidMaleMonthScenariosOverride => TTestScenarios.InvalidMaleMonthScenarios;

    public static TheoryData<string>? InvalidFemaleMonthScenariosOverride => TTestScenarios.InvalidFemaleMonthScenarios;

    public static TheoryData<string> InvalidDateScenariosOverride => TTestScenarios.InvalidDateScenarios;

    public static TheoryData<string> InvalidVerificationNumberScenariosOverride =>
        TTestScenarios.InvalidVerificationNumberScenarios;
}