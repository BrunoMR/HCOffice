namespace BusinessLayer.Validation
{
    using DTOLayer;
    using FluentValidation;

    public class MarcaValidation : AbstractValidator<ProcessoImported>
    {
        public MarcaValidation()
        {
            RuleFor(processo => processo.Marca.Apresentacao)
                .Must(ApresentationExists)
                .WithMessage("Apresentação '{0}' não foi cadastrada do Processo '{1}'!", 
                processo => processo.Marca.Apresentacao,
                processo => processo.NumeroProcesso);

            RuleFor(processo => processo.Marca.Natureza)
                .Must(NatureExists)
                .WithMessage("Natureza '{0}' não foi cadastrada do Processo '{1}'!",
                processo => processo.Marca.Natureza,
                processo => processo.NumeroProcesso);

        }

        private static bool ApresentationExists(string apresentacao)
        {
            ITipoApresentacaoNegocio tipoApresentacaoNegocio = new TipoApresentacaoNegocio();
            return tipoApresentacaoNegocio.ExistsTipoApresentacao(apresentacao);
        }

        private static bool NatureExists(string natureza)
        {
            ITipoNaturezaNegocio tipoNaturezaNegocio = new TipoNaturezaNegocio();
            return tipoNaturezaNegocio.ExistsTipoNatureza(natureza);
        }
    }
}
