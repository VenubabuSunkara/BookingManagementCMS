using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;

namespace CMS.CustomValidationAttributes;

public class DateComparisonAttribute(string comparisonProperty) : ValidationAttribute
{
    public string GetErrorMessage() => "End date must be greater than the start date.";

    /// <summary>
    /// Compare dates
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(comparisonProperty ?? string.Empty);
        if (property == null)
            return new ValidationResult($"Unknown property: {comparisonProperty}");

        var comparisonValue = DateOnly.FromDateTime((DateTime)property.GetValue(validationContext.ObjectInstance)!);

        var currentValue = DateOnly.FromDateTime((DateTime)value!);

        if (currentValue < comparisonValue)
            return new ValidationResult(ErrorMessage ?? GetErrorMessage());

        return ValidationResult.Success;
    }
}
