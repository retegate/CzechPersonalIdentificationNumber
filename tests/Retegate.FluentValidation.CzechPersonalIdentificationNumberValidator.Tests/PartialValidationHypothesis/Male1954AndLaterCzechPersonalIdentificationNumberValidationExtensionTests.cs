using FluentValidation;

using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.PartialValidationHypothesis;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis.Base;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis;

public sealed class Male1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests()
    : ValidationTestsBase<Male1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests.TestValidator,
            TestModel, Male1954AndLaterCzechPersonalIdentificationNumberScenarios>(
            new TestValidator(),
            x => new TestModel { PersonalIdentificationNumber = x }),
        ITestScenarios<Male1954AndLaterCzechPersonalIdentificationNumberScenarios>
{
    public class TestValidator : AbstractValidator<TestModel>
    {
        public TestValidator()
        {
            RuleFor(x => x.PersonalIdentificationNumber)
                .Male1954AndLaterCzechPersonalIdentificationNumber();
        }
    }

    public static Male1954AndLaterCzechPersonalIdentificationNumberScenarios TestScenarios => new();
}