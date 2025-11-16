namespace Application.Attributes;

using System.ComponentModel.DataAnnotations;
using Application.Exceptions;

public class AppRequiredAttribute : RequiredAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null || (value is string str && string.IsNullOrWhiteSpace(str)))
        {
            var fieldName = validationContext.DisplayName ?? validationContext.MemberName;
                throw new BadRequestException("FieldRequired", fieldName!);
        }

        return ValidationResult.Success;
    }
}