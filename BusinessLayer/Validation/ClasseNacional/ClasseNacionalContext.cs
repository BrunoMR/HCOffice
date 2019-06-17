namespace BusinessLayer.Validation.ClasseNacional
{
    using DTOLayer;

    public class ClasseNacionalContext
    {
        private readonly IClasseNacionalProperty _classeNacionalProperty;

        public ClasseNacionalContext(IClasseNacionalProperty classeNacionalProperty)
        {
            _classeNacionalProperty = classeNacionalProperty;
        }

        public string Execute(ProcessoImported processo)
        {
            return _classeNacionalProperty.IsValidProperty(processo);
        }
    }
}
