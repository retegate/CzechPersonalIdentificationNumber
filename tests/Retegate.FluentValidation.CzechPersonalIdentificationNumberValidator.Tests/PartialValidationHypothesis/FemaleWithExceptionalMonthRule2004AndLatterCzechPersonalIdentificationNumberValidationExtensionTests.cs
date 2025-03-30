using FluentValidation;

using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.PartialValidationHypothesis;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis.Base;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis;

public sealed class
    FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtensionTests()
    : ValidationTestsBase<FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtensionTests.TestValidator,
            TestModel, FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberScenarios>(
            new TestValidator(),
            x => new TestModel { PersonalIdentificationNumber = x }),
        ITestScenarios<FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberScenarios>
{
    public class TestValidator : AbstractValidator<TestModel>
    {
        public TestValidator()
        {
            RuleFor(x => x.PersonalIdentificationNumber)
                .FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber();
        }
    }

    public static FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberScenarios TestScenarios =>
        new();
}