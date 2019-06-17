namespace BusinessLayer.Validation
{
    using DTOLayer;
    using FluentValidation;

    public class PrioridadeValidation : AbstractValidator<Prioridade>
    {
        public PrioridadeValidation()
        {
            RuleFor(prioridade => prioridade.Data)
                .IsValidDateTime()
                .WithMessage("A data da prioridade '{0}' não é válida!", prioridade => prioridade.Numero);
        }
    }
}
