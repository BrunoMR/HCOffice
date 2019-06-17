namespace BusinessLayer.Validation.ClasseNacional
{
    using System.Text;
    using DTOLayer;
    using FluentValidation.Validators;

    public class ClasseNacionalProperty : PropertyValidator
    {
        public ClasseNacionalProperty() : base("{ValidationMessage}") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var processo = ((ProcessoImported)context.Instance);
            var messages = new StringBuilder();

            var classeNacionalContextFirst = new ClasseNacionalContext(new ClasseNacionalPropertyFirst());
            messages.AppendLine(classeNacionalContextFirst.Execute(processo));

            var classeNacionalContextSecond = new ClasseNacionalContext(new ClasseNacionalPropertySecond());
            messages.AppendLine(classeNacionalContextSecond.Execute(processo));

            var classeNacionalContextThird = new ClasseNacionalContext(new ClasseNacionalPropertyThird());
            messages.AppendLine(classeNacionalContextThird.Execute(processo));
            
            if (string.IsNullOrWhiteSpace(messages.ToString()))
                return true;

            context.MessageFormatter.AppendArgument("ValidationMessage", messages.ToString());
            return false;
        }

    }
}
