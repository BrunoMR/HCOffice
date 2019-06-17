namespace BusinessLayer.Validation.Despacho
{
    using System.Text;
    using DTOLayer;
    using FluentValidation.Validators;

    public class DespachoProperty : PropertyValidator
    {
        public DespachoProperty() : base("{ValidationMessage}") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var numeroProcesso = ((ProcessoImported)context.Instance).NumeroProcesso;
            var despachos = ((ProcessoImported)context.Instance).Despachos;
            var messages = new StringBuilder();

            foreach (var despacho in despachos.Despacho)
            {
                if (!CodeExistis(despacho.Codigo))
                    messages.AppendLine(string.Format("O despacho '{0}' do Processo '{1}' não foi cadastrado!", 
                        despacho.Codigo,
                        numeroProcesso));
            }

            if (string.IsNullOrWhiteSpace(messages.ToString()))
                return true;

            context.MessageFormatter.AppendArgument("ValidationMessage", messages.ToString());

            return false;
        }

        private static bool CodeExistis(string codigo)
        {
            DespachoNegocio despachoNegocio = new DespachoNegocio();
            return despachoNegocio.ExistsDespacho(codigo);
        }
    }
}
