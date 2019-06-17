namespace BusinessLayer.Validation
{
    using DTOLayer;
    using BusinessLayer.Validation.ClasseVienna;
    using FluentValidation;

    public class ClasseViennaValidation : AbstractValidator<ProcessoImported>
    {
        public ClasseViennaValidation()
        {
            RuleFor(processo => processo.ClasseVienna)
                .SetValidator(new ClasseViennaProperty());
        }
    }
}
