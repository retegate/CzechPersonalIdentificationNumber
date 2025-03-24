using FluentValidation;

using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.PartialValidationHypothesis;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis.Base;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis;

public sealed class
    MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtensionTests()
    : ValidationTestsBase<Female1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests.TestValidator,
            TestModel, MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberScenarios>(
            new Female1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests.TestValidator(),
            x => new TestModel { PersonalIdentificationNumber = x }),
        ITestScenarios<MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberScenarios>
{
    public class TestValidator : AbstractValidator<TestModel>
    {
        public TestValidator()
        {
            RuleFor(x => x.PersonalIdentificationNumber)
                .MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber();
        }
    }

    public static MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberScenarios
        TestScenarios => new();
}