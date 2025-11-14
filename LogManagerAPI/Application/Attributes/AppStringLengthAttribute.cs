namespace Application.Attributes;

using System.ComponentModel.DataAnnotations;
using Application.Exceptions;

public class AppStringLengthAttribute(int maximumLength) : StringLengthAttribute(maximumLength)
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string str && str.Length > MaximumLength)
        {
            var fieldName = validationContext.DisplayName ?? validationContext.MemberName;
                throw new BadRequestException("FieldTooLong", fieldName!, MaximumLength);
        }

        return ValidationResult.Success;
    }
}