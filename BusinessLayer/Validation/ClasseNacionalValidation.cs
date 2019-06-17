namespace BusinessLayer.Validation
{
    using DTOLayer;
    using BusinessLayer.Validation.ClasseNacional;
    using FluentValidation;

    public class ClasseNacionalValidation : AbstractValidator<ProcessoImported>
    {
        public ClasseNacionalValidation()
        {
            RuleFor(processo => processo.ClasseNacional)
                .SetValidator(new ClasseNacionalProperty());
        }
    }
}
