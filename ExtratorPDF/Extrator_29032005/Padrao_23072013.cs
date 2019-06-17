using System;
using System.Text.RegularExpressions;

namespace Extrator_29032005
{
    /// <summary>
    /// Padrões de extração de conteúdo de RPI's geradas a partir de 23/07/2013
    /// </summary>
    public class Padrao_23072013
    {
        /// <Descrição dos padrões representados pelas expressões regulares>
        /// 
        /// Número de processo (Padrão 01)
        ///     Quebra de linha
        ///     Espaço opicional
        ///     Sequência de 9 dígitos numéricos
        ///     Espaço
        ///
        /// Número de processo (Padrão 02)
        ///     01 Espaço 
        ///     09 dígitos numéricos
        ///     Espaço
        /// 
        /// Número de processo (Padrão 03)
        ///   Número de Processo:
        ///   01 Espaço
        ///   09 dígitos numéricos
        ///   - (hífen)
        ///          
        /// Apresentação
        ///     Apresentação: 
        ///     01 espaço
        ///     Descrição válida (Figurativa, Tridimensional ou mista)
        ///   
        /// NCL
        ///     NCL 
        ///     Abre parêntese
        ///     Valor válido (7, 07, 8, 08, 9, 09, 10)
        ///     Fecha parêntese
        ///   
        /// <Padrões representados pelas expressões regulares/>             
        ///        

        #region Expressões regulares compiladas

        private static Regex _rgxNumeroProcesso01
          = new Regex(@"[\n][\s]?[0-9]{9}[\s]", RegexOptions.Compiled);

        private static Regex _rgxNumeroProcesso02
          = new Regex(@"[\s][0-9]{9}[\s]", RegexOptions.Compiled);

        private static Regex _rgxNumeroProcesso03
           = new Regex(@"[Número de Processo: ][0-9]{9}[-]", RegexOptions.Compiled);

        private static Regex _rgxReplaceNumeroProcesso
            = new Regex(@"[^0-9]", RegexOptions.Compiled);

        private static Regex _rgxApresentacao
            = new Regex(@"Apresentação: Mista|Apresentação: Figurativa|Apresentação: Tridimensional", RegexOptions.Compiled);

        private static Regex _rgxReplaceApresentacao
           = new Regex(@"Apresentação: ", RegexOptions.Compiled);

        private static Regex _rgxNCL
            = new Regex(@"NCL\(0?7\)|NCL\(0?8\)|NCL\(0?9\)|NCL\(10\)", RegexOptions.Compiled);

        private static Regex _rgxReplaceNCL
            = new Regex(@"[^0-9]", RegexOptions.Compiled);

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

        #endregion

        private static Match _match = null;

        /// <summary>
        /// Método que verifica se o texto de entrada contém processo em formato válido
        /// </summary>
        /// <param name="entrada">Texto a ser analisado</param>
        /// <returns></returns>
        public static bool ContemProcesso(string entrada)
        {
            _match = _rgxNumeroProcesso01.Match(entrada);

            if (_match.Success)
                return true;

            _match = _rgxNumeroProcesso02.Match(entrada);

            if (_match.Success && _match.Index == 0)
                return true;

            _match = _rgxNumeroProcesso03.Match(entrada);

            if (_match.Success)
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
        /// Método que verifica se o texto de entrada contém NCL em formato válido
        /// </summary>
        /// <param name="entrada">Texto a ser analisado</param>
        /// <param name="saida">String com número da NCL</param>
        /// <returns></returns>
        public static bool ContemNCL(string entrada)
        {
            _match = _rgxNCL.Match(entrada);

            if (_match.Success && _match.Index == 0)
                return true;
            else
                return false;
        }

        public static bool ContemNCL(string entrada, out string saida)
        {
            saida = string.Empty;

            if (ContemNCL(entrada))
            {
                saida = _rgxReplaceNCL.Replace(_match.Value, string.Empty);
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
