using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.PartialValidators.VerificationNumbers;

public class VerificationNumberBefore1954ValidatorTests
{
    [Theory]
    [InlineData("530101123")]
    [InlineData("490101123")]
    public void ValidateOrThrow_ValidVerificationNumber_DoesNotThrow(string personalIdentificationNumber)
    {
        // Arrange
        var validator = VerificationNumberBefore1954Validator.DefaultInstance;
        var span = personalIdentificationNumber.AsSpan();

        // Act & Assert
        validator.ValidateOrThrow(span);
    }

    [Theory]
    [InlineData("5301011223")]
    [InlineData("49010112")]
    public void ValidateOrThrow_InvalidVerificationNumber_ThrowsFormatException(string personalIdentificationNumber)
    {
        // Arrange
        var validator = VerificationNumberBefore1954Validator.DefaultInstance;

        // Act & Assert
        var ex = Should.Throw<FormatException>(() =>
        {
            var span = personalIdentificationNumber.AsSpan();
            validator.ValidateOrThrow(span);
        });
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidVerificationNumberMessage);
    }
}