using System.Text.RegularExpressions;

namespace Extrator_29032005
{
    /// <summary>
    /// Padrões de extração de conteúdo de RPI's geradas a partir de 29/03/2005
    /// </summary>
    public class Padrao_29032005
    {
        /// <Padrões representados pelas expressões regulares>
        /// 
        /// Número de processo
        ///   No
        ///   09 dígitos numéricos
        ///   01 espaço
        ///   Iniciando na posião 01 da string
        ///
        /// Apresentação
        ///   Apres.: 
        ///   01 espaço
        ///   Uma das três descrições válidas (Figurativa, Tridimensional ou mista)
        ///
        /// <Padrões representados pelas expressões regulares/>             
        ///        

        #region Expressões regulares compiladas

        //private static Regex _rgxNumeroProcesso
        //    = new Regex(@"No.[\s]?[0-9]{9}[\s]|[\s]No.[\s]?[0-9]{9}[\s]", RegexOptions.Compiled);

        private static Regex _rgxNumeroProcesso
            = new Regex
                (
                    @"No.[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[\s]" +
                    @"|[\s]No.[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[0-9]{1}[\s]?[\s]", 
                    RegexOptions.Compiled
                );

        private static Regex _rgxReplaceNumeroProcesso
            = new Regex(@"[^0-9]", RegexOptions.Compiled);

        private static Regex _rgxApresentacao
            = new Regex
                (
                    @"Apr[\s]?es.: M[\s]?i[\s]?s[\s]?t[\s]?a"
                  + @"|Apr[\s]?es.: T[\s]?r[\s]?i[\s]?d[\s]?i[\s]?m[\s]?e[\s]?n[\s]?s[\s]?i[\s]?o[\s]?n[\s]?a[\s]?l"
                  + @"|Apr[\s]?es.: F[\s]?i[\s]?g[\s]?u[\s]?r[\s]?a[\s]?t[\s]?i[\s]?v[\s]?a",
                  RegexOptions.Compiled
                );

        private static Regex _rgxReplaceApresentacao
           = new Regex(@"Apr[\s]?es.: ", RegexOptions.Compiled);

        private static Regex _rgxNumeroRPI
             = new Regex(@"RPI[\s]{1,3}[0-9]{4}", RegexOptions.Compiled);

        private static Regex _rgxReplaceNumeroRPI
            = new Regex(@"[^0-9]", RegexOptions.Compiled);

        // dd/mm/yyyy
        private static Regex _rgxDataValida
            = new Regex
                (
                    @"(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))",
                    RegexOptions.Compiled
                );

        #endregion

        private static Match _match = null;

        /// <summary>
        /// Método que verifica se o texto de entrada contém processo em formato válido
        /// </summary>
        /// <param name="entrada">Texto a ser analisado</param>
        /// <returns></returns>
        public static bool ContemProcesso(string entrada)
        {
            _match = _rgxNumeroProcesso.Match(entrada);

            if (_match.Success && _match.Index == 0)
                return true;

            return false;
        }

        /// <summary>
        /// Método que verifica se o texto de entrada contém processo em formato válido
        /// </summary>
        /// <param name="entrada">Texto a ser analisado</param>
        /// <param name="saida">String com número de processo</param>
        /// <returns></returns>
        public static bool ContemProcesso(string entrada, out string saida)
        {
            saida = string.Empty;

            if (ContemProcesso(entrada))
            {
                saida = _rgxReplaceNumeroProcesso.Replace(_match.Value, string.Empty);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método que verifica se o texto de entrada contém apresentação em formato válido
        /// </summary>
        /// <param name="entrada">Texto a ser analisado</param>
        /// <returns></returns>
        public static bool ContemApresentacao(string entrada)
        {
            _match = _rgxApresentacao.Match(entrada);

            if (_match.Success)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Método que verifica se o texto de entrada contém apresentação em formato válido
        /// </summary>
        /// <param name="entrada">Texto a ser analisado</param>
        /// <param name="saida">String com descrição da apresentação</param>
        /// <returns></returns>
        public static bool ContemApresentacao(string entrada, out string saida)
        {
            saida = string.Empty;

            if (ContemApresentacao(entrada))
            {
                saida = _rgxReplaceApresentacao.Replace(_match.Value, string.Empty);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método que verifica se o texto de entrada contém informações de cabeçalho
        /// </summary>
        /// <param name="entrada">Texto a ser analisado</param>
        /// <param name="saida">objeto com informações de cabeçalho</param>
        /// <returns></returns>
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
