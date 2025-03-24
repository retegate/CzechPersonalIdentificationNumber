using System.Diagnostics.CodeAnalysis;

using FluentValidation;

using Retegate.CzechPersonalIdentificationNumber;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;
using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.Patterns;
using Retegate.CzechPersonalIdentificationNumber.PartialValidators.VerificationNumbers;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.PartialValidationHypothesis;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis.Base;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis;

public sealed class FemaleBefore1954CzechPersonalIdentificationNumberValidationExtensionTests 
    : ValidationTestsBase<FemaleBefore1954CzechPersonalIdentificationNumberValidationExtensionTests.TestValidator,
        TestModel, FemaleBefore1954CzechPersonalIdentificationNumberValidationExtensionTests>(new TestValidator(),
        x => new TestModel { PersonalIdentificationNumber = x }),
    ISpecificCzechIdentificationNumberTests
{
    public class TestValidator : AbstractValidator<TestModel>
    {
        public TestValidator()
        {
            RuleFor(x => x.PersonalIdentificationNumber)
                .FemaleBefore1954CzechPersonalIdentificationNumber();
        }
    }
}