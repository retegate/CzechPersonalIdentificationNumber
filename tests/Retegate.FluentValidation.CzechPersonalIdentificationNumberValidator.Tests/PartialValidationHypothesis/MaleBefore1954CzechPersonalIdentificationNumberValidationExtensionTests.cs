using FluentValidation;

using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.PartialValidationHypothesis;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis.Base;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis;

public sealed class MaleBefore1954CzechPersonalIdentificationNumberValidationExtensionTests()
    : ValidationTestsBase<MaleBefore1954CzechPersonalIdentificationNumberValidationExtensionTests.TestValidator,
            TestModel, MaleBefore1954CzechPersonalIdentificationNumberScenarios>(
            new TestValidator(),
            x => new TestModel { PersonalIdentificationNumber = x }),
        ITestScenarios<MaleBefore1954CzechPersonalIdentificationNumberScenarios>
{
    public class TestValidator : AbstractValidator<TestModel>
    {
        public TestValidator()
        {
            RuleFor(x => x.PersonalIdentificationNumber)
                .MaleBefore1954CzechPersonalIdentificationNumber();
        }
    }

    public static MaleBefore1954CzechPersonalIdentificationNumberScenarios TestScenarios => new();
}