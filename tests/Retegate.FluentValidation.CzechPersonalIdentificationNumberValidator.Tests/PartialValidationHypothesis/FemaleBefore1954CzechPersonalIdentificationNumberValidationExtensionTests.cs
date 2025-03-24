using FluentValidation;

using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.PartialValidationHypothesis;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis.Base;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis;

public sealed class FemaleBefore1954CzechPersonalIdentificationNumberValidationExtensionTests()
    : ValidationTestsBase<FemaleBefore1954CzechPersonalIdentificationNumberValidationExtensionTests.TestValidator,
            TestModel, FemaleBefore1954CzechPersonalIdentificationNumberScenarios>(new TestValidator(),
            x => new TestModel { PersonalIdentificationNumber = x }),
        ITestScenarios<FemaleBefore1954CzechPersonalIdentificationNumberScenarios>
{
    public class TestValidator : AbstractValidator<TestModel>
    {
        public TestValidator()
        {
            RuleFor(x => x.PersonalIdentificationNumber)
                .FemaleBefore1954CzechPersonalIdentificationNumber();
        }
    }

    public static FemaleBefore1954CzechPersonalIdentificationNumberScenarios TestScenarios => new();
}