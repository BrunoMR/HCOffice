using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExtratorDeDados.Util
{
    public static class StringExtension
    {
        public static string Ortografar(this string _this)
        {
            if (_this == null) return null;
            var resultado = _this.ToUpper();

            resultado = resultado.Replace(" ", "");
            resultado = resultado.Replace("Å", "A");
            resultado = resultado.Replace("Á", "A");
            resultado = resultado.Replace("À", "A");
            resultado = resultado.Replace("Â", "A");
            resultado = resultado.Replace("Ä", "A");
            resultado = resultado.Replace("Ã", "A");
            resultado = resultado.Replace("AA", "A");
            resultado = resultado.Replace("É", "E");
            resultado = resultado.Replace("È", "E");
            resultado = resultado.Replace("Ê", "E");
            resultado = resultado.Replace("Ë", "E");
            resultado = resultado.Replace("EE", "E");
            resultado = resultado.Replace("Í", "I");
            resultado = resultado.Replace("Ì", "I");
            resultado = resultado.Replace("Î", "I");
            resultado = resultado.Replace("Ï", "I");
            resultado = resultado.Replace("II", "I");
            resultado = resultado.Replace("Ó", "O");
            resultado = resultado.Replace("Ò", "O");
            resultado = resultado.Replace("Ô", "O");
            resultado = resultado.Replace("Õ", "O");
            resultado = resultado.Replace("Ö", "O");
            resultado = resultado.Replace("OO", "O");
            resultado = resultado.Replace("Ú", "U");
            resultado = resultado.Replace("Ù", "U");
            resultado = resultado.Replace("Ü", "U");
            resultado = resultado.Replace("Û", "U");
            resultado = resultado.Replace("UU", "U");
            resultado = resultado.Replace("1", "UM");
            resultado = resultado.Replace("2", "DOIS");
            resultado = resultado.Replace("3", "TRÊS");
            resultado = resultado.Replace("4", "QUATRO");
            resultado = resultado.Replace("5", "CINCO");
            resultado = resultado.Replace("6", "SEIS");
            resultado = resultado.Replace("7", "SETE");
            resultado = resultado.Replace("8", "OITO");
            resultado = resultado.Replace("9", "NOVE");
            resultado = resultado.Replace("0", "ZERO");

            resultado = Regex.Replace(resultado, "([BCDFGHJKLMNQRSTVWXYZ])H([AEIOU])", "$1$2");
            resultado = resultado.Replace("PH", "F");

            resultado = Regex.Replace(resultado, "XH([AEIOU])", "X$1");
            resultado = resultado.Replace("AHRA", "ARA");

            resultado = Regex.Replace(resultado, "ZH([AEIOU])", "Z$1");

            resultado = Regex.Replace(resultado, "H([AEIOU])", "$1");


            resultado = resultado.Replace("Å", "A");
            resultado = resultado.Replace("Á", "A");
            resultado = resultado.Replace("À", "A");
            resultado = resultado.Replace("Â", "A");
            resultado = resultado.Replace("Ä", "A");
            resultado = resultado.Replace("Ã", "A");
            resultado = resultado.Replace("AA", "A");
            resultado = resultado.Replace("É", "E");
            resultado = resultado.Replace("È", "E");
            resultado = resultado.Replace("Ê", "E");
            resultado = resultado.Replace("Ë", "E");
            resultado = resultado.Replace("EE", "E");
            resultado = resultado.Replace("Í", "I");
            resultado = resultado.Replace("Ì", "I");
            resultado = resultado.Replace("Î", "I");
            resultado = resultado.Replace("Ï", "I");
            resultado = resultado.Replace("II", "I");
            resultado = resultado.Replace("Ó", "O");
            resultado = resultado.Replace("Ò", "O");
            resultado = resultado.Replace("Ô", "O");
            resultado = resultado.Replace("Õ", "O");
            resultado = resultado.Replace("Ö", "O");
            resultado = resultado.Replace("OO", "O");
            resultado = resultado.Replace("Ú", "U");
            resultado = resultado.Replace("Ù", "U");
            resultado = resultado.Replace("Ü", "U");
            resultado = resultado.Replace("Û", "U");
            resultado = resultado.Replace("UU", "U");
            resultado = resultado.Replace(" ", "");
            resultado = resultado.Replace("'", "");
            resultado = resultado.Replace("1/2", "H");
            resultado = resultado.Replace("1/4", "K");
            resultado = resultado.Replace("\"", "");
            resultado = resultado.Replace("!", "");
            resultado = resultado.Replace("@", "A");
            resultado = resultado.Replace("#", "");
            resultado = resultado.Replace("$", "S");
            resultado = resultado.Replace("%", "PORCENTO");
            resultado = resultado.Replace("¨", "");
            resultado = resultado.Replace("&", "E");
            resultado = resultado.Replace("*", "");
            resultado = resultado.Replace("(", "");
            resultado = resultado.Replace(")", "");
            resultado = resultado.Replace("-", "");
            resultado = resultado.Replace("-", "");
            resultado = resultado.Replace("_", "");
            resultado = resultado.Replace("=", "");
            resultado = resultado.Replace("+", "MAIS");
            resultado = resultado.Replace("§", "");
            resultado = resultado.Replace("´", "");
            resultado = resultado.Replace("`", "");
            resultado = resultado.Replace("^", "");
            resultado = resultado.Replace("~", "");
            resultado = resultado.Replace("{", "");
            resultado = resultado.Replace("}", "");
            resultado = resultado.Replace("[", "");
            resultado = resultado.Replace("]", "");
            resultado = resultado.Replace(";", "");
            resultado = resultado.Replace(":", "");
            resultado = resultado.Replace(".", "");
            resultado = resultado.Replace(",", "");
            resultado = resultado.Replace("<", "");
            resultado = resultado.Replace(">", "");
            resultado = resultado.Replace("/", "");
            resultado = resultado.Replace("?", "");
            resultado = resultado.Replace("\\", "");
            resultado = resultado.Replace("|", "");
            resultado = resultado.Replace("BB", "B");
            resultado = resultado.Replace("CC", "C");
            resultado = resultado.Replace("Ç", "C");
            resultado = resultado.Replace("CE", "SE");
            resultado = resultado.Replace("CI", "SI");
            resultado = resultado.Replace("C", "K");
            resultado = resultado.Replace("CH", "K");
            resultado = resultado.Replace("TT", "T");
            resultado = resultado.Replace("T", "D");
            resultado = resultado.Replace("DD", "D");
            resultado = resultado.Replace("PH", "F");
            resultado = resultado.Replace("FF", "F");
            resultado = resultado.Replace("GG", "G");
            resultado = resultado.Replace("G", "J");
            resultado = resultado.Replace("HH", "H");
            resultado = resultado.Replace("INHA", "");
            resultado = resultado.Replace("INHAS", "");
            resultado = resultado.Replace("INHO", "");
            resultado = resultado.Replace("INHOS", "");
            resultado = resultado.Replace("JJ", "J");
            resultado = resultado.Replace("KK", "K");
            resultado = resultado.Replace("LL", "L");
            resultado = resultado.Replace("NN", "N");
            resultado = resultado.Replace("Ñ", "N");
            resultado = resultado.Replace("N", "M");
            resultado = resultado.Replace("MM", "M");
            resultado = resultado.Replace("PP", "P");
            resultado = resultado.Replace("P", "B");
            resultado = resultado.Replace("QQ", "Q");
            resultado = resultado.Replace("QU", "K");
            resultado = resultado.Replace("Q", "K");
            resultado = resultado.Replace("RR", "R");
            resultado = resultado.Replace("SS", "S");
            resultado = resultado.Replace("SH", "X");
            resultado = resultado.Replace("VV", "V");
            resultado = resultado.Replace("WW", "V");
            resultado = resultado.Replace("W", "V");
            resultado = resultado.Replace("XHA", "XA");
            resultado = resultado.Replace("XX", "X");
            resultado = resultado.Replace("YY", "I");
            resultado = resultado.Replace("Y", "I");
            resultado = resultado.Replace("ZZ", "S");
            resultado = resultado.Replace("Z", "S");
            resultado = resultado.Replace("º", "");
            resultado = resultado.Replace("°", "");
            resultado = resultado.Replace("ª", "");
            resultado = resultado.Replace(" ", "");
            resultado = resultado.Replace("BABY", "BEBE");

            resultado = Regex.Replace(resultado, "([AEIOU])L([BCDFGHJKLMNPQRSTVWXYZ])", "$1U$2");

            //--como também se a letra “L” for a última letra da palavra ortografada.
            if (resultado.EndsWith("L") && resultado.Length > 1) resultado = resultado.Substring(0, resultado.Length - 2) + "U";
            //IF (SUBSTRING(@RET, LEN(@RET), 1) = 'L') SET @RET = SUBSTRING(@RET, 0, LEN(@RET)-1) + 'U'


            //--8 - Incluir regra para ser executada após o término da mudança ortográfica para que a letra “H” 
            //--seja excluída do termo ortografado, quando:
            //--- Antes da letra “H” seja uma consoante e após uma vogal;

            resultado = Regex.Replace(resultado, "([BCDFGHJKLMNPQRSTVWXYZ])H([AEIOU])", "$1$2");
            //--- Antes da letra “H” seja uma vogal e após uma consoante;
            resultado = Regex.Replace(resultado, "([AEIOU])H([BCDFGHJKLMNPQRSTVWXYZ])", "$1$2");
            //--- A letra “H” seja a última letra da palavra, e a letra anterior seja VOGAL.
            if (resultado.EndsWith("H") && resultado.Length > 1)
            {
                resultado = resultado.Remove(resultado.Length - 1);
            }

            resultado = resultado.Replace("AA", "A");
            resultado = resultado.Replace("EE", "E");
            resultado = resultado.Replace("II", "I");
            resultado = resultado.Replace("OO", "O");
            resultado = resultado.Replace("UU", "U");

            return resultado.ToUpper();
        }

        public static object VerificarData(this string data)
        {
            DateTime resultado;
            if (DateTime.TryParse(data, out resultado))
                return resultado;

            return null;
        }

        public static string SeDataMaiorQueMinimo(this string data)
        {
            return DateTime.Parse(data) == DateTime.MinValue
                ? null
                : data;
        }

        public static bool IsCnpj(this string cnpj)
        {
            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            var tempCnpj = cnpj.Substring(0, 12);
            var soma = 0;
            for (var i = 0; i < 12; i++)
                soma += Int32.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            var resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            var digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (var i = 0; i < 13; i++)
                soma += Int32.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static bool IsCpf(this string cpf)
        {
            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            var tempCpf = cpf.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
                soma += Int32.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            var resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            var digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += Int32.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        public static string RemoveDiacritics(this String s)
        {
            var normalizedString = s.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
            {
                stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }
    }
}
