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

public sealed class MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtensionTests 
    : ValidationTestsBase<Female1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests.TestValidator,
            TestModel, Female1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests>(new Female1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests.TestValidator(),
            x => new TestModel { PersonalIdentificationNumber = x }),
        ISpecificCzechIdentificationNumberTests
{
    public class TestValidator : AbstractValidator<TestModel>
    {
        public TestValidator()
        {
            RuleFor(x => x.PersonalIdentificationNumber)
                .MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber();
        }
    }
}