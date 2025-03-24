using FluentValidation;

using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.PartialValidationHypothesis;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis.Base;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis;

public sealed class Female1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests()
    : ValidationTestsBase<Female1954AndLaterCzechPersonalIdentificationNumberValidationExtensionTests.TestValidator,
            TestModel, Female1954AndLaterCzechPersonalIdentificationNumberScenarios>(new TestValidator(),
            x => new TestModel { PersonalIdentificationNumber = x }),
        ITestScenarios<Female1954AndLaterCzechPersonalIdentificationNumberScenarios>
{
    public class TestValidator : AbstractValidator<TestModel>
    {
        public TestValidator()
        {
            RuleFor(x => x.PersonalIdentificationNumber)
                .Female1954AndLaterCzechPersonalIdentificationNumber();
        }
    }

    public static Female1954AndLaterCzechPersonalIdentificationNumberScenarios TestScenarios => new();
}