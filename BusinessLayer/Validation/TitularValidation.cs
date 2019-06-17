namespace BusinessLayer.Validation
{
    using System.Linq;

    using DTOLayer;
    using FluentValidation;
    using Utils;

    public class TitularValidation : AbstractValidator<ProcessoImported>
    {
        public TitularValidation()
        {
            //RuleFor(processo => processo.Titulares)
            //    .Must(CpfCnpjValido)
            //    .When(processo => processo.Titulares.Titular.CpfCnpj != null)
            //    .WithMessage("CPF/CNPJ '{0}' do Processo '{1}' não é válido!",
            //    processo => processo.Titulares?.Titular.CpfCnpj,
            //    processo => processo.NumeroProcesso);

            RuleFor(processo => processo.Titulares)
                .Must(CountryExists)
                .When(processo => processo.Titulares?.Titular?.First().Pais != null)
                .WithMessage("País '{0}' não foi cadastrado do Processo '{1}'!",
                processo => processo.Titulares?.Titular.First().Pais,
                processo => processo.NumeroProcesso);

        }

        private static bool CpfCnpjValido(Titulares titulares)
        {
            var cpfCnpjInpi = titulares.Titular?.First().CpfCnpj.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
            var isValid = false;
            if (cpfCnpjInpi != null)
                switch (cpfCnpjInpi.Length)
                {
                    case 11:
                        isValid = cpfCnpjInpi.IsCpf();
                        break;
                    case 14:
                        isValid = cpfCnpjInpi.IsCnpj();
                        break;
                    default:
                        isValid = true;
                        break;
                }

            return isValid;
        }

        /// <summary>
        /// The country exists.
        /// </summary>
        /// <param name="titulares">
        /// The titulares.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool CountryExists(Titulares titulares)
        {
            IPaisNegocio paisNegocio = new PaisNegocio();
            return paisNegocio.CountryExists(titulares?.Titular?.First().Pais);
        }
    }
}
