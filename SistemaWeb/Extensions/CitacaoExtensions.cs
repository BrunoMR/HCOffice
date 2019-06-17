namespace SistemaWeb.Extensions
{
    using System.Linq;
    using System.Text;
    using DTOLayer;

    public static class CitacaoExtensions
    {
        public static string BuildClass(this Citacao citacao)
        {
            if (citacao.Classe == null || citacao.Classe.Count <= 0)
                return null;

            var classe = new StringBuilder();

            var classeInternacional = citacao.Classe.FirstOrDefault(c => !string.IsNullOrWhiteSpace(c.Edicao));
            if (classeInternacional != null)
            {
                classe.Append(string.Format("NCL({0}) {1}", classeInternacional.Edicao, classeInternacional.Codigo));
            }
            else
            {
                citacao.Classe.ForEach(c =>
                {
                    for (int i = 0; i < c.SubClasse.Count; i++)
                    {
                        classe.Append(i == 0
                            ? string.Format("{0}.{1}", c.Codigo, c.SubClasse[i].Codigo)
                            : string.Format("/{0}", c.SubClasse[i].Codigo));
                    }
                });
            }

            return classe.ToString();
        }
    }
}
