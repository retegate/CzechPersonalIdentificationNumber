using System.Diagnostics.CodeAnalysis;

using Retegate.CzechPersonalIdentificationNumber;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis;

public sealed class FemaleBefore1954CzechPersonalIdentificationNumberValidationExtensionTests : CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber, IParsable<FemaleBefore1954CzechPersonalIdentificationNumberValidationExtensionTests>, IContainValidationScenario<PatternBefore1954Validator, YearBefore1954Parser, FemaleMonthParser, VerificationNumberBefore1954Validator>
{
    private FemaleBefore1954CzechPersonalIdentificationNumberValidationExtensionTests(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) : base(normalizedPersonalIdentificationNumber, dateOfBirth, SexEnum.Female)
    {
    }

    public static new FemaleBefore1954CzechPersonalIdentificationNumberValidationExtensionTests Parse(string potentialPersonalIdentificationNumber, IFormatProvider? _ = null)
    {
        return ParsingHelper.Parse(potentialPersonalIdentificationNumber, ValidationScenario, Ctor);
    }

    public static bool TryParse([NotNullWhen(true)] string? potentialPersonalIdentificationNumber, IFormatProvider? provider, [MaybeNullWhen(false)] out FemaleBefore1954CzechPersonalIdentificationNumberValidationExtensionTests result)
    {
        return ParsingHelper.TryParse(potentialPersonalIdentificationNumber, ValidationScenario, out result, Ctor);
    }

    private static FemaleBefore1954CzechPersonalIdentificationNumberValidationExtensionTests Ctor(string normalizedPersonalIdentificationNumber, DateOnly dateOfBirth) => new(normalizedPersonalIdentificationNumber, dateOfBirth);

    public static ValidationScenario<PatternBefore1954Validator, YearBefore1954Parser, FemaleMonthParser, VerificationNumberBefore1954Validator> ValidationScenario { get; } = new()
    {
        PatternValidator =  PatternBefore1954Validator.DefaultInstance, YearParser = YearBefore1954Parser.DefaultInstance, MonthParser = FemaleMonthParser.DefaultInstance, VerificationNumberValidator = VerificationNumberBefore1954Validator.DefaultInstance,
    };
}