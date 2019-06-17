namespace BusinessLayer.Validation
{
    using DTOLayer;
    using FluentValidation;

    public class PrioridadesValidation : AbstractValidator<Prioridades>
    {
        public PrioridadesValidation()
        {
            RuleFor(prioridades => prioridades.Prioridade)
                .SetValidator(new PrioridadeValidation());
        }
    }
}
