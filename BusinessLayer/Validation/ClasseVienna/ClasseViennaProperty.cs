namespace BusinessLayer.Validation.ClasseVienna
{
    using System.Linq;
    using System.Text;
    using DTOLayer;
    using FluentValidation.Validators;

    public class ClasseViennaProperty : PropertyValidator
    {
        public ClasseViennaProperty() : base("{ValidationMessage}") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var numeroProcesso = ((ProcessoImported)context.Instance).NumeroProcesso;
            var classeVienna = ((ProcessoImported)context.Instance).ClasseVienna;
            var messages = new StringBuilder();

            foreach (var cfe4 in classeVienna.Cfe4S.Where(cfe4 => !Cf4Exists(cfe4.CodigoCfe4)))
            {
                messages.AppendLine(string.Format("Código '{0}' CFE4 do Processo '{1}' não foi cadastrado!",
                    cfe4.CodigoCfe4,
                    numeroProcesso));
            }

            if (string.IsNullOrWhiteSpace(messages.ToString()))
                return true;

            context.MessageFormatter.AppendArgument("ValidationMessage", messages.ToString());

            return false;
        }

        private bool Cf4Exists(string codigo)
        {
            return Cfe4Negocio.ExistsCfe4(codigo);
        }
    }
}
