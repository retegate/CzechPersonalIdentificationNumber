using FluentValidation;

using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.PartialValidationHypothesis;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis.Base;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis;

public sealed class
    FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtensionTests()
    : ValidationTestsBase<FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtensionTests.TestValidator,
            TestModel, FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberScenarios>(
            new TestValidator(),
            x => new TestModel { PersonalIdentificationNumber = x }),
        ITestScenarios<FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberScenarios>
{
    public class TestValidator : AbstractValidator<TestModel>
    {
        public TestValidator()
        {
            RuleFor(x => x.PersonalIdentificationNumber)
                .FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber();
        }
    }

    public static FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberScenarios
        TestScenarios => new();
}