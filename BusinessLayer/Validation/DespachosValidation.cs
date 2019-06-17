namespace BusinessLayer.Validation
{
    using DTOLayer;
    using BusinessLayer.Validation.Despacho;
    using FluentValidation;

    public class DespachosValidation : AbstractValidator<ProcessoImported>
    {
        public DespachosValidation()
        {
            RuleFor(processo => processo.Despachos.Despacho)
                .SetValidator(new DespachoProperty());
        }
    }
}
