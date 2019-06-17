using System.Text.RegularExpressions;

namespace Extrator
{
    /// <summary>
    /// Padrões de extração de conteúdo de RPI's geradas a partir de 23/07/2013
    /// </summary>
    public class Padrao
    {
        #region Expressões regulares compiladas (Padrões)

        //private static Regex _rgxNumeroProcesso
        //  = new Regex(@"[0-9]{9}", RegexOptions.Compiled);

        private static Regex _rgxNumeroProcesso
            = new Regex(@"¢No. [0-9]{9}", RegexOptions.Compiled);

        private static Regex _rgxApresentacao
            = new Regex(@"Mista |Figurativa |Tridimensional ", RegexOptions.Compiled);

        private static Regex _rgxNumeroRPI
               = new Regex(@"RPI[\s][0-9]{4}", RegexOptions.Compiled);

        private static Regex _rgxReplaceNumeroRPI
            = new Regex(@"[^0-9]", RegexOptions.Compiled);

        // dd/mm/yyyy
        private static Regex _rgxDataValida
            = new Regex
                (
                    @"(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))",
                    RegexOptions.Compiled
                );

        private static Regex _rgxNomeImagem
            = new Regex(@"[0-9]{8}_[0-9]{2}.png", RegexOptions.Compiled);

        #endregion

        private static Match _match = null;
        public static bool ContemProcesso(string entrada)
        {
            _match = _rgxNumeroProcesso.Match(entrada); 

            if (_match.Success && _match.Index == 0)
                return true;

            return false;
        }
        public static bool ContemProcesso(string entrada, out string saida)
        {
            saida = string.Empty;

            if (ContemProcesso(entrada))
            {
                saida = _match.Value.Replace("¢No. ", "");
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ContemApresentacao(string entrada)
        {
            _match = _rgxApresentacao.Match(entrada);

            if (_match.Success && _match.Index == 0)
                return true;
            else
                return false;
        }
        public static bool ContemApresentacao(string entrada, out string saida)
        {
            saida = string.Empty;

            if (ContemApresentacao(entrada))
            {
                saida = _match.Value.TrimEnd();
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ContemData(string entrada, out string saida)
        {
            saida = string.Empty;

            _match = _rgxDataValida.Match(entrada);

            if (_match.Success)
            {
                saida = _match.Value;
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ContemNumeroRPI(string entrada, out string saida)
        {
            saida = string.Empty;

            _match = _rgxNumeroRPI.Match(entrada);

            if (_match.Success)
            {
                saida = _rgxReplaceNumeroRPI.Replace(_match.Value, string.Empty);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
