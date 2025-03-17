using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.PartialValidators.VerificationNumbers;

public class VerificationNumber1954AndAfterValidatorTests
{
    [Theory]
    [InlineData("5401011234")]
    [InlineData("9912311234")]
    public void ValidateOrThrow_ValidVerificationNumber_DoesNotThrow(string personalIdentificationNumber)
    {
        // Arrange
        var validator = VerificationNumber1954AndAfterValidator.DefaultInstance;
        var span = personalIdentificationNumber.AsSpan();

        // Act & Assert
        validator.ValidateOrThrow(span);
    }

    [Theory]
    [InlineData("5401011235")]
    [InlineData("9912311235")]
    public void ValidateOrThrow_InvalidVerificationNumber_ThrowsFormatException(string personalIdentificationNumber)
    {
        // Arrange
        var validator = VerificationNumber1954AndAfterValidator.DefaultInstance;

        // Act & Assert
        var ex = Should.Throw<FormatException>(() =>
        {
            var span = personalIdentificationNumber.AsSpan();
            validator.ValidateOrThrow(span);
        });
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidVerificationNumberMessage);
    }
}