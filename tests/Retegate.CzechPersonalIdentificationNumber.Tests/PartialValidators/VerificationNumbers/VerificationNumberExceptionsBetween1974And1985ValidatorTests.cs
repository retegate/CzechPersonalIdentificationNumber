using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.PartialValidators.VerificationNumbers;

public class VerificationNumberExceptionsBetween1974And1985ValidatorTests
{
    [Theory]
    [InlineData("7401014070")]
    [InlineData("8501012750")]
    public void ValidateOrThrow_ValidVerificationNumber_DoesNotThrow(string personalIdentificationNumber)
    {
        // Arrange
        var validator = VerificationNumberExceptionsBetween1974And1985Validator.DefaultInstance;
        var span = personalIdentificationNumber.AsSpan();

        // Act & Assert
        validator.ValidateOrThrow(span);
    }

    [Theory]
    [InlineData("7401011231")]
    [InlineData("8501011231")]
    public void ValidateOrThrow_InvalidVerificationNumber_ThrowsFormatException(string personalIdentificationNumber)
    {
        // Arrange
        var validator = VerificationNumberExceptionsBetween1974And1985Validator.DefaultInstance;

        // Act & Assert
        var ex = Should.Throw<FormatException>(() =>
        {
            var span = personalIdentificationNumber.AsSpan();
            validator.ValidateOrThrow(span);
        });
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidVerificationNumberMessage);
    }
}