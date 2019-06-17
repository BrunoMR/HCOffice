namespace BusinessLayer.Validation
{
    using DTOLayer;
    using FluentValidation;

    public class ProcessoValidation : AbstractValidator<ProcessoImported>
    {
        public ProcessoValidation()
        {
            RuleFor(processo => processo)
                .Must(LoggingCurrentProcesso);

            RuleFor(processo => processo.NumeroProcesso)
                .NotNull()
                .NotEmpty()
                .WithMessage("Deve ser informado o número do Processo");

            RuleFor(processo => processo.DataDeposito)
                .IsValidDateTime()
                .WithMessage("Data depósito do processo '{0}' não é válida!", processo => processo.NumeroProcesso);

            RuleFor(processo => processo.DataConcessao)
                .IsValidDateTime()
                .WithMessage("Data concessão do processo '{0}' não é válida!", processo => processo.NumeroProcesso);

            RuleFor(processo => processo)
                .SetValidator(new TitularValidation())
                .When(processo => processo.Titulares != null);

            RuleFor(processo => processo)
                .SetValidator(new MarcaValidation())
                .When(processo => processo.Marca != null);

            RuleFor(processo => processo)
                .Must(proceso => proceso.Despachos != null)
                .WithMessage("Não existe Despacho no Processo '{0}'!", processo => processo.NumeroProcesso)
                .SetValidator(new DespachosValidation())
                .When(processo => processo.Despachos != null);

            RuleFor(processo => processo)
                .SetValidator(new ClasseViennaValidation())
                .When(processo => processo.ClasseVienna != null);

            RuleFor(processo => processo)
                .SetValidator(new ClasseNacionalValidation())
                .When(processo => processo.ClasseNacional != null);

            RuleFor(processo => processo)
                .SetValidator(new ClasseNiceValidation())
                .When(processo => processo.ClasseNice != null);

            RuleFor(processo => processo)
                .Must(LoggingLastProcesso);
        }

        /// <summary>
        /// Método irá guardar o Processo que está sendo validado
        /// </summary>
        /// <param name="processo"></param>
        /// <returns></returns>
        private static bool LoggingCurrentProcesso(ProcessoImported processo)
        {
            LogProcess.PutCurrentProcess(processo);
            return true;
        }

        /// <summary>
        /// Método irá guardar a informação do último Processo validado com sucesso
        /// </summary>
        /// <param name="processo"></param>
        /// <returns></returns>
        private static bool LoggingLastProcesso(ProcessoImported processo)
        {
            LogProcess.PutLastProcess(processo);
            return true;
        }
    }
}
