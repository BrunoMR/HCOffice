namespace BusinessLayer.Validation
{
    using DTOLayer;
    using ClasseNice;
    using FluentValidation;

    public class ListaClasseNiceValidation : AbstractValidator<ProcessoImported>
    {
        public ListaClasseNiceValidation()
        {
            RuleFor(processo => processo)
                .SetValidator(new ClassesNiceProperty());
        }
    }
}