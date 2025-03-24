using System.Diagnostics.CodeAnalysis;

using Retegate.CzechPersonalIdentificationNumber;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis;

public sealed class FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtensionTests : CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber, IParsable<FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtensionTests>, IContainValidationScenario<Pattern1954AndAfterValidator, Year1954AndAfterParser, FemaleMonthException2004AndAfterParser, VerificationNumber1954AndAfterValidator>
{
    private FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtensionTests(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) : base(normalizedPersonalIdentificationNumber, dateOfBirth, SexEnum.Female)
    {
    }

    public static new FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtensionTests Parse(string potentialPersonalIdentificationNumber, IFormatProvider? _ = null)
    {
        return ParsingHelper.Parse(potentialPersonalIdentificationNumber, ValidationScenario, Ctor);
    }

    public static bool TryParse([NotNullWhen(true)] string? potentialPersonalIdentificationNumber, IFormatProvider? provider, [MaybeNullWhen(false)] out FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtensionTests result)
    {
        return ParsingHelper.TryParse(potentialPersonalIdentificationNumber, ValidationScenario, out result, Ctor);
    }

    private static FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtensionTests Ctor(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) => new(normalizedPersonalIdentificationNumber, dateOfBirth);

    public static ValidationScenario<Pattern1954AndAfterValidator, Year1954AndAfterParser, FemaleMonthException2004AndAfterParser, VerificationNumber1954AndAfterValidator> ValidationScenario { get; } = new()
    {
        PatternValidator =  Pattern1954AndAfterValidator.DefaultInstance, YearParser = Year1954AndAfterParser.DefaultInstance, MonthParser = FemaleMonthException2004AndAfterParser.DefaultInstance, VerificationNumberValidator = VerificationNumber1954AndAfterValidator.DefaultInstance,
    };
}