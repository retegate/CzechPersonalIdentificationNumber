using Retegate.CzechPersonalIdentificationNumber.PartialValidators;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.PartialValidators;

public class NotNullValidatorTests
{
    [Theory]
    [InlineData("1234567890")]
    [InlineData("540101/1234")]
    public void ValidateOrThrow_NotNullInput_DoesNotThrow(string personalIdentificationNumber)
    {
        // Act & Assert
        NotNullValidator.ValidateOrThrow(personalIdentificationNumber);
    }

    [Fact]
    public void ValidateOrThrow_NullInput_ThrowsFormatException()
    {
        // Act & Assert
        var ex = Should.Throw<FormatException>(() => NotNullValidator.ValidateOrThrow(null));
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.NullInputFormatMessage);
    }
}