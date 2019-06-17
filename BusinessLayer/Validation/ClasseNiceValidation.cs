namespace BusinessLayer.Validation
{
    using DTOLayer;
    using BusinessLayer.Validation.ClasseNice;
    using FluentValidation;

    public class ClasseNiceValidation : AbstractValidator<ProcessoImported>
    {
        public ClasseNiceValidation()
        {
            RuleFor(processo => processo)
                .SetValidator(new ClasseNiceProperty());
        }
        
    }
}
