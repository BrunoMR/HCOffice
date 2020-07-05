namespace BusinessLayer.Validation.ClasseNice
{
    using System.Text;
    using DTOLayer;
    using FluentValidation.Validators;
    using Utils;

    public class ClassesNiceProperty : PropertyValidator
    {
        public ClassesNiceProperty() : base("{ValidationMessage}") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var numeroProcesso = ((ProcessoImported)context.Instance).NumeroProcesso;
            var classeNiceLista = ((ProcessoImported)context.Instance).ListaClasseNice;

            string classeNiceConcat;
            var messages = new StringBuilder();

            classeNiceLista.ClassesNice.ForEach(x =>
            {
                var codigoNice = x.Codigo;

                if (codigoNice != null && !RegularExpressions.CodeClasseNiceFromTxt.IsMatch(codigoNice))
                {
                    classeNiceConcat = ClasseNegocio.BuildCodeClasseNice(codigoNice, numeroProcesso);
                    if (classeNiceConcat.Length <= 3)
                        messages.AppendLine(string.Format("O código NCL do Processo '{0}' não foi encontrado no arquivo!", numeroProcesso));
                }
                else
                    classeNiceConcat = codigoNice;

                if (string.IsNullOrWhiteSpace(messages.ToString()))
                {
                    var classeNegocio = new ClasseNegocio();
                    if (!classeNegocio.ExistsClasse(classeNiceConcat))
                        messages.AppendLine(string.Format("A classe Nice '{0}' não foi cadastrada do Processo '{1}'!",
                            classeNiceConcat,
                            numeroProcesso));
                }
            });
            
            if (string.IsNullOrWhiteSpace(messages.ToString()))
                return true;

            context.MessageFormatter.AppendArgument("ValidationMessage", messages.ToString());

            return false;
        }
    }
}