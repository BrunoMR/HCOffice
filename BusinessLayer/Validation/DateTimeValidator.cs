namespace BusinessLayer.Validation
{
    using System.Globalization;
    using System;
    using FluentValidation;
    using FluentValidation.Validators;

    public class DateTimeValidator<T> : PropertyValidator
    {
        public DateTimeValidator() : base("A data não é válida") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null) return true;

            if (!(context.PropertyValue is string)) return false;

            DateTime buffer;
            //return DateTime.TryParse((string) context.PropertyValue, out buffer);
            return DateTime.TryParse((string)context.PropertyValue, CultureInfo.CreateSpecificCulture("pt-BR"), DateTimeStyles.AssumeLocal, out buffer);
        }
    }

    public static class StaticDateTimeValidator
    {
        public static IRuleBuilderOptions<T, TProperty> IsValidDateTime<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new DateTimeValidator<TProperty>());
        }
    }
}
