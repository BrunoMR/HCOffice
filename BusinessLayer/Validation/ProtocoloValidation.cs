namespace BusinessLayer.Validation
{
    using DTOLayer;
    using FluentValidation;

    public class ProtocoloValidation : AbstractValidator<ProtocoloImported>
    {
        public ProtocoloValidation()
        {
            RuleFor(protocolo => protocolo.Data)
                .IsValidDateTime()
                .WithMessage("Data do protocolo {0} não é válida!", protocolo => protocolo.Numero);
        }
    }
}
