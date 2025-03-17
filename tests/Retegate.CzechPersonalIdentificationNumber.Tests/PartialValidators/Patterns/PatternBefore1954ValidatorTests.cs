using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.PartialValidators.Patterns;

public class PatternBefore1954ValidatorTests
{
    [Theory]
    [InlineData("530101/123")]
    [InlineData("530101123")]
    [InlineData(" 530101123 ")]
    public void ValidateOrThrow_ValidPattern_DoesNotThrow(string personalIdentificationNumber)
    {
        // Arrange
        var validator = PatternBefore1954Validator.DefaultInstance;
        var span = personalIdentificationNumber.AsSpan();

        // Act & Assert
        validator.ValidateOrThrow(span);
    }

    [Theory]
    [InlineData("530101/12")]
    [InlineData("5301011234")]
    [InlineData("530101-123")]
    public void ValidateOrThrow_InvalidPattern_ThrowsFormatException(string personalIdentificationNumber)
    {
        // Arrange
        var validator = PatternBefore1954Validator.DefaultInstance;

        // Act & Assert
        var ex = Should.Throw<FormatException>(() =>
        {
            var span = personalIdentificationNumber.AsSpan();
            validator.ValidateOrThrow(span);
        });
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidFormatMessage);
    }
}