# Introduction

When developing .NET applications, it's beneficial to have access to modern tools and extensions that improve efficiency
and developer-friendliness.  
One such case, specific to the Czech Republic, is the **Czech Personal Identification Number (rodné číslo)**.  
(A very similar format is likely used in the Slovak Republic due to shared historical roots, but this extension focuses
solely on the specifications of the **Czech Personal Identification Number (rodné číslo)** as used in the Czech Republic
and its predecessor, Czechoslovakia.)

This extension allows you to:

- Parse
- Validate
- Generate (for testing purposes only; not intended for distribution or issuing of new **Czech Personal Identification
  Numbers (rodná čísla)**)

To understand the structure and validation rules of the **Czech Personal Identification Number (rodné číslo)**, two
sources were referenced:

- [Ministry of the Interior – Rodné číslo](https://mv.gov.cz/clanek/rady-a-sluzby-dokumenty-rodne-cislo.aspx)
- [Wikipedia – Rodné číslo](https://cs.wikipedia.org/wiki/Rodn%C3%A9_%C4%8D%C3%ADslo)

# Structure

## Parser

The core of the extension is the `CzechPersonalIdentificationNumber` class, which holds the **rodné číslo** string
either in the format  
`YYMMDD/XXX(X)` or `YYYYMMDDXXX(X)`. It also extracts the date of birth, the person's gender, and provides an
alternative formatting option.  
The `YYMMDD` part encodes the date of birth and gender, while `XXX(X)` is the control number.

The class implements the `IParsable` interface, which includes methods for parsing and validation.  
It has several derived classes that reflect rules valid for specific historical periods.  
From the references above, multiple eras with differing rules for valid **Czech Personal Identification Numbers (rodná
čísla)** can be identified.  
These parser/validator/generator classes are named to best represent the corresponding time period and gender rules.

As of 2025-04-10 (Thursday), the eras can be summarized as:

- **Before 1953** – Format: `YYMMDD/XXX`. Control number has 3 digits with no further validation.
- **1954 and later** – Format: `YYMMDD/XXXX`. Control number has 4 digits and must be divisible by 11,  
  with exceptions between 1974–1985 due to population growth and ID limitations.
- **2004 and later** – Format: `YYYYMMDD/XXXX`. Same control rule (divisible by 11), but the gender-encoding rules in
  the date part were expanded.

The `YYMMDD` section encodes both birth date and gender, where the month (`MM`) also encodes gender.

### Usage
The parsing via Parse method provider either `CzechPersonalIdentificationNumber` or throws `FormatException` if the input is invalid. Messages of the exception are listed in method xml comment documentation (The IDE usually provides pop-up context).

### Installation

```bash
dotnet add package Retegate.CzechPersonalIdentificationNumber
```

### Parser Example
General person identification number parsing:
```c#
var personalNumber = CzechPersonalIdentificationNumber.Parse("8303055133");
    
var parsingResult = CzechPersonalIdentificationNumber.TryParse("8303055133", out var personalNumber);
```
or
```c#
var personalNumber = CzechPersonalIdentificationNumber.Parse("830305/5133");
    
var parsingResult = CzechPersonalIdentificationNumber.TryParse("830305/5133", out var personalNumber);
```

Specific rule for specific era and gender (e.g., exceptional verification number modulo exception in population boom between 1974 and 1985):
```c#
var personalNumber = FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber.Parse("835305/6400")
    
var parsingResult = FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber.TryParse("8353056400", out var personalNumber)
```

## Validator

Validation is built using the popular .NET library **FluentValidation**.  
The project provides a general validator that wraps around individual validation rules depending on the relevant time
period and gender.

This extension does not directly provide `AbstractValidator<T>` implementations,  
but instead offers **fluent rules** for properties of your own models.  
You can define your own validator inheriting from `AbstractValidator<T>` and apply validation rules to a string property
that holds the **Czech Personal Identification Number (rodné číslo)**.

### Installation

```bash
dotnet add package Retegate.FluentValidation.CzechPersonalIdentificationNumber
```

### Validation example
General czech personal identification number validation:
```c#
public class SomeModelValidator : AbstractValidator<SomeModel>
{
    public SomeModelValidator()
    {
        RuleFor(x => x.PersonalNumber)
            .CzechPersonalIdentificationNumber();
    }
}
```

... and then usage according to the FluentValidation library:
```c#
var validator = new SomeModelValidator();

var result = validator.Validate(new SomeModel
{
    PersonalNumber = "830305/5133"
});
```

Specific era and gender rule validation:
```c#
public class SomeModelValidatorWithSpecificPersonalNumber : AbstractValidator<SomeModel>
{
    public SomeModelValidator()
    {
        RuleFor(x => x.PersonalNumber)
            .FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber();
    }
}
```
... and then usage according to the FluentValidation library:
```c#
var validator = new SomeModelValidatorWithSpecificPersonalNumber();

var result = validator.Validate(new SomeModel
{
    PersonalNumber = "830305/5133"
});
```

## Generator

The generator is based on the popular .NET library **Bogus**.  
It provides generators tailored to the individual rules defined for each era and gender.

The generator is intended for testing purposes only.  
It is somewhat stochastic and does not account for actual assigned numbers on a specific date of birth.

### Installation

```bash
dotnet add package Retegate.Bogus.CzechPersonalIdentificationNumber
```

### Generator example
Arbitrary generation of a **Czech Personal Identification Number (rodné číslo)**:
```c#
var faker = new Faker();

var personalNumber = faker.GenerateCzechPersonalIdentificationNumber(CzechPersonalIdentificationNumberKindEnum
                .ArbitraryPerson);
```

Generating a **Czech Personal Identification Number (rodné číslo)** for a specific date of birth and rule (certain rules are overlapping in time):
```c#
var faker = new Faker();

var personalNumber = faker.GenerateCzechPersonalIdentificationNumber(CzechPersonalIdentificationNumberKindEnum
                .ExceptionalRuleFemaleInPopulationBoom1974Till1985);
```

# Contribution

If you find this extension useful and run into any issues, feel free to report them or contact me at
retegate@retegate.com.

Any help with **code reviews** and test coverage is welcome — especially suggestions with reasoning on how to improve
maintainability or align the code with modern .NET practices.

**Todo:** A helpful contribution would be collecting known exceptions to the official rules —  
that is, special **Czech Personal Identification Numbers (rodná čísla)** issued despite not fulfilling the defined
rules.  
If available, the SHA-256 hashes (preferably without slashes or spaces) of these values could be used to extend the
parser and validator —  
**not the generator**, as the original values would be unknown and thus unverifiable.

# License

This project is completely free to use under the MIT License.

# Donation

The issue of working with **Czech Personal Identification Numbers (rodná čísla)** has come up on several of my past
projects,  
but there was rarely enough time to solve it thoroughly and correctly.  
I'm happy that this tool finally exists. If you find it useful and would like to show appreciation,  
feel free to buy me a coffee — it'll help me create more helpful open-source utilities like this one.

![Buy me a coffee](Donation.png)

If you need a donation agreement, feel free to contact me at retegate@retegate.com.  

Thank you for all your support!