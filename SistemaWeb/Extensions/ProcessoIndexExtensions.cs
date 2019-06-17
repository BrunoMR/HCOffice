using System;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using Utils;

namespace SistemaWeb.Extensions
{
    using DTOLayer.Indexes;
    using System.IO;
    using ViewModels;
    public static class ProcessoIndexExtensions
    {
        public static string BuildClass(this ProcessoIndex processo)
        {
            if (processo.Classe == null || processo.Classe.Count <= 0)
                return null;

            var classe = new StringBuilder();

            var classeInternacional = processo.Classe.FirstOrDefault(c => !string.IsNullOrWhiteSpace(c.Edicao));
            if (classeInternacional != null)
            {
                classe.Append(string.Format("NCL({0}) {1}", classeInternacional.Edicao, classeInternacional.Codigo));
            }
            else
            {
                processo.Classe.ForEach(c =>
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

        public static string BuildCfe4(this ProcessoIndex processo)
        {
            if (processo.Cfe4 == null || processo.Cfe4.Count <= 0)
                return null;

            var cfe4 = new StringBuilder();
            processo.Cfe4.ForEach(x =>
            {
                cfe4.Append(x.Codigo.Trim() + " - ");
            });

            cfe4.Remove(cfe4.ToString().LastIndexOf('-'), 2);

            return cfe4.ToString();
        }

        public static string BuildDescriptionCfe4(this ProcessoIndex processo)
        {
            if (processo.Cfe4 == null || processo.Cfe4.Count <= 0)
                return null;

            var cfe4 = new StringBuilder();
            processo.Cfe4.ForEach(x =>
            {
                cfe4.Append(x.Codigo.Trim() + ": " + x.Descricao.Trim() + " - ");
            });

            cfe4.Remove(cfe4.ToString().LastIndexOf('-'), 2);

            return cfe4.ToString();
        }

        public static string GetCodeLastDespacho(this ProcessoIndex processo)
        {
            return processo.Despacho?
                .FirstOrDefault(x => x.UltimoDespacho)?.Codigo;
        }

        public static string GetDescriptionLastDespacho(this ProcessoIndex processo)
        {
            return processo.Despacho?
                .FirstOrDefault(x => x.UltimoDespacho)?.DescricaoCompleta;
        }

        public static TipoApresentacaoViewModel GetTipoApresentacao(this ProcessoIndex processo)
        {
            var estilo = string.Empty;

            switch (processo.Apresentacao)
            {
                case "Nominativa":
                {
                    estilo = "bg-black";
                    break;
                }
                case "Mista":
                {
                    estilo = "bg-red";
                    break;
                }
                case "Figurativa":
                {
                    estilo = "bg-green";
                    break;
                }
                case "Tridimensional":
                {
                    estilo = "bg-purple";
                    break;
                }
            }

            if (string.IsNullOrEmpty(processo.Apresentacao))
            {
                return new TipoApresentacaoViewModel();
            }

            return new TipoApresentacaoViewModel
            {
                InicialNome = processo.Apresentacao.Substring(0, 1),
                Estilo = estilo,
                Nome = processo.Apresentacao
            };
        }
    }
}
