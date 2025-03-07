using FluentValidation;
using FluentValidation.Results;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator;

/// <summary>
/// Based on the czech act 133/2000 from the czech collection of law. 
/// </summary>
public static class CzechPersonalIdentificationNumberValidationExtension
{
    internal const string InvalidCzechPersonalIdentificationNumberError = "Personal identification number is not valid.";

//todo: udělat test, že jde přepsat with messsage v return textu...fv dá jednu chybu a případně parser jich umí více podat a jsou posané pro upřesnění....ta
    public static IRuleBuilderOptions<T, string> CzechPersonalIdentificationNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var result = ruleBuilder
            .Custom((personalIdentificationNumber, context) =>
            {
                if (Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.TryParse(personalIdentificationNumber, out _))
                {
                    return;
                }

                context.AddFailure(new ValidationFailure(context.PropertyPath, InvalidCzechPersonalIdentificationNumberError));
            });

        return (result as IRuleBuilderOptions<T, string>)!;
    }
}