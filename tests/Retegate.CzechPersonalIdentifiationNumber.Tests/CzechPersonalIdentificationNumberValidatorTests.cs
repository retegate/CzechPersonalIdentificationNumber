using FluentValidation;
using Microsoft.Extensions.Localization;
using Moq;
using PavelKudrna.Demo.Validation.PersonalIdentificationNumber;

namespace PavelKudrna.Demo.Validation.Tests.Validators.PersonalIdentificationNumber;

public class CzechPersonalIdentificationNumberValidatorTests
{
    private class TestModel(string? personalIdentificationNumber)
    {
        public string? PersonalIdentificationNumber { get; } = personalIdentificationNumber;
    }

    private class TestModelValidator : AbstractValidator<TestModel>
    {
        public TestModelValidator(IStringLocalizer localizer)
        {
            RuleFor(x => x.PersonalIdentificationNumber)
                .CzechPersonalIdentificationNumber(localizer);
        }
    }

    private readonly Mock<IStringLocalizer> _localizerMock = new();
    private readonly TestModelValidator _validator;

    public CzechPersonalIdentificationNumberValidatorTests()
    {
        _localizerMock.Setup(localizer => localizer[It.IsAny<string>()])
            .Returns((string key) => new LocalizedString(key, key));

        _validator = new TestModelValidator(_localizerMock.Object);
    }

    [Fact]
    public void Validate_WithNull_ReturnsInvalidPatternFailure()
    {
        // Arrange
        var model = new TestModel(null);

        // Act
        var result =  _validator.Validate(model);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        result.Errors.First().ErrorMessage.Should().Be(Constants.NotNullErrorKey);
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
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
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
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        result.Errors.First().ErrorMessage.Should().Be(CzechPersonalIdentificationNumberValidationExtension.PatternErrorKey);
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
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        result.Errors.First().ErrorMessage.Should().Be(CzechPersonalIdentificationNumberValidationExtension.MonthErrorKey);
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
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        result.Errors.First().ErrorMessage.Should().Be(CzechPersonalIdentificationNumberValidationExtension.DayErrorKey);
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
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        result.Errors.First().ErrorMessage.Should().Be(CzechPersonalIdentificationNumberValidationExtension.ModuloErrorKey);
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
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle();
        result.Errors.First().ErrorMessage.Should().Be(Constants.NotEmptyErrorKey);
    }
}