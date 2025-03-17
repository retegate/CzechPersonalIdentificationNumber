using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.PartialValidators.Patterns;

public class Pattern1954AndAfterValidatorTests
{
    [Theory]
    [InlineData("540101/1234")]
    [InlineData("5401011234")]
    [InlineData(" 5401011234 ")]
    public void ValidateOrThrow_ValidPattern_DoesNotThrow(string personalIdentificationNumber)
    {
        // Arrange
        var validator = Pattern1954AndAfterValidator.DefaultInstance;
        var span = personalIdentificationNumber.AsSpan();

        // Act & Assert
        validator.ValidateOrThrow(span);
    }

    [Theory]
    [InlineData("540101/123")]
    [InlineData("54010112345")]
    [InlineData("540101-1234")]
    public void ValidateOrThrow_InvalidPattern_ThrowsFormatException(string personalIdentificationNumber)
    {
        // Arrange
        var validator = Pattern1954AndAfterValidator.DefaultInstance;

        // Act & Assert
        var ex = Should.Throw<FormatException>(() =>
        {
            var span = personalIdentificationNumber.AsSpan();
            validator.ValidateOrThrow(span);
        });
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidFormatMessage);
    }
}