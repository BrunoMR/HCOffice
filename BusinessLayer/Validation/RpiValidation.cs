namespace BusinessLayer.Validation
{
    using DTOLayer;
    using FluentValidation;

    public class RpiValidation : AbstractValidator<RpiImported>
    {
        public RpiValidation()
        {
            RuleFor(rpi => rpi.DataRpi)
                .IsValidDateTime()
                .WithMessage("A data da RPI '{0}' não é válida!", rpi => rpi.NumeroRpi);

            RuleFor(rpi => rpi.NumeroRpi)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("RPI '{0}' Inválida!", rpi => rpi.NumeroRpi);

            RuleFor(rpi => rpi.Processo)
                .SetCollectionValidator(new ProcessoValidation());
        }
    }
}
