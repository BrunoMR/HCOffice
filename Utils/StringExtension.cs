using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Utils
{
    public static class StringExtensions
    {
        #region Private Methods

        private static string RemoveRepetedChars(this string input)
        {
            var regex = new Regex("(.)(?<=\\1\\1)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return regex.Replace(input, string.Empty);
        }

        private static string NotSpell(this string _this)
        {
            if (_this == null) return null;
            var result = _this.ToUpper();

            result = result.Replace("Å", "A");
            result = result.Replace("Á", "A");
            result = result.Replace("À", "A");
            result = result.Replace("Â", "A");
            result = result.Replace("Ä", "A");
            result = result.Replace("Ã", "A");
            result = result.Replace("É", "E");
            result = result.Replace("È", "E");
            result = result.Replace("Ê", "E");
            result = result.Replace("Ë", "E");
            result = result.Replace("Í", "I");
            result = result.Replace("Ì", "I");
            result = result.Replace("Î", "I");
            result = result.Replace("Ï", "I");
            result = result.Replace("Ó", "O");
            result = result.Replace("Ò", "O");
            result = result.Replace("Ô", "O");
            result = result.Replace("Õ", "O");
            result = result.Replace("Ö", "O");
            result = result.Replace("Ú", "U");
            result = result.Replace("Ù", "U");
            result = result.Replace("Ü", "U");
            result = result.Replace("Û", "U");
            result = result.Replace("Y", "I");

            result = result.Replace("$", "S");
            result = result.Replace("+", "MAIS");
            result = result.Replace("%", "");
            result = result.Replace("&", "");
            result = result.Replace("@", "A");
            result = result.Replace("\"", "");
            result = result.Replace("'", "");
            result = result.Replace("!", " ");
            result = result.Replace("?", " ");
            result = result.Replace("¿", " ");
            result = result.Replace("¨", "");
            result = result.Replace("*", "");
            result = result.Replace("(", "");
            result = result.Replace(")", "");
            result = result.Replace("-", " ");
            result = result.Replace("_", " ");
            result = result.Replace("=", " ");
            result = result.Replace("|", " ");
            result = result.Replace("\\", " ");
            result = result.Replace("/", " ");
            result = result.Replace("´", "");
            result = result.Replace("`", "");
            result = result.Replace("^", "");
            result = result.Replace("~", "");
            result = result.Replace(",", " ");
            result = result.Replace(".", " ");
            result = result.Replace(";", " ");
            result = result.Replace(":", " ");
            result = result.Replace("ª", "");
            result = result.Replace("º", "");
            result = result.Replace("°", "");
            result = result.Replace("µ", "");
            result = result.Replace("ß", "");
            result = result.Replace("¢", "");
            result = result.Replace("£", "");
            result = result.Replace("¦", "");
            result = result.Replace("¹", "");
            result = result.Replace("²", "");
            result = result.Replace("³", "");
            result = result.Replace("½", "");
            result = result.Replace("¼", "");
            result = result.Replace("¾", "");
            result = result.Replace("÷", "");
            result = result.Replace("{", "");
            result = result.Replace("}", "");
            result = result.Replace("[", "");
            result = result.Replace("]", "");
            result = result.Replace("§", "");
            result = result.Replace("<", "");
            result = result.Replace(">", "");

            result = result.RemoveRepetedChars();

            result = result.Replace("CHA", "XA");
            result = result.Replace("CHE", "XE");
            result = result.Replace("CHI", "XI");
            result = result.Replace("CHO", "XO");
            result = result.Replace("CHU", "XU");
            result = result.Replace("SHA", "XA");
            result = result.Replace("SHE", "XE");
            result = result.Replace("SHI", "XI");
            result = result.Replace("SHO", "XO");
            result = result.Replace("SHU", "XU");
            result = result.Replace("CH", "K");
            result = result.Replace("SH", "X");

            result = result.Replace("BURGUER", "BURGER");

            result = result.Replace("INHO", "");
            result = result.Replace("INHOS", "");
            result = result.Replace("INHA", "");
            result = result.Replace("INHAS", "");

            result = result.Replace("CE", "SE");
            result = result.Replace("CI", "SI");
            result = result.Replace("GE", "JE");
            result = result.Replace("GI", "JI");
            result = result.Replace("C", "K");
            result = result.Replace("Ç", "S");
            result = result.Replace("PH", "F");
            result = result.Replace("XH", "X");
            result = result.Replace("Ñ", "N");
            result = result.Replace("QQ", "K");
            result = result.Replace("QU", "K");
            result = result.Replace("Q", "K");
            result = result.Replace("W", "V");

            result = result.Replace("KLA", "KRA");
            result = result.Replace("KLE", "KRE");
            result = result.Replace("KLI", "KRI");
            result = result.Replace("KLO", "KRO");
            result = result.Replace("KLU", "KRU");


            result = result.Replace("1", "UM");
            result = result.Replace("2", "DOIS");
            result = result.Replace("3", "TRES");
            result = result.Replace("4", "KATRO");
            result = result.Replace("5", "SINKO");
            result = result.Replace("6", "SEIS");
            result = result.Replace("7", "SETE");
            result = result.Replace("8", "OITO");
            result = result.Replace("9", "NOVE");
            result = result.Replace("0", "ZERO");

            result = Regex.Replace(result, "XH([AEIOU])", "X$1");
            result = result.Replace("AHRA", "ARA");

            result = Regex.Replace(result, "ZH([AEIOU])", "Z$1");
            result = Regex.Replace(result, "H([AEIOU])", "$1");

            result = Regex.Replace(result, "([AEIOU])L([BCDFGHJKLMNPQRSTVWXYZ])", "$1U$2");

            //--como também se a letra “L” for a última letra da palavra ortografada.
            if (result.EndsWith("L") && result.Length > 1)
                result = result.Substring(0, result.Length - 1) + "U";

            if (result.EndsWith("N") && result.Length > 1)
                result = result.Substring(0, result.Length - 1) + "M";

            //--8 - Incluir regra para ser executada após o término da mudança ortográfica para que a letra “H” 
            //--seja excluída do termo ortografado, quando:
            //--- Antes da letra “H” seja uma consoante e após uma vogal;

            result = Regex.Replace(result, "([BCDFGHJKLMNPQRSTVWXYZ])H([AEIOU])", "$1$2");
            //--- Antes da letra “H” seja uma vogal e após uma consoante;
            result = Regex.Replace(result, "([AEIOU])H([BCDFGHJKLMNPQRSTVWXYZ])", "$1$2");
            //--- A letra “H” seja a última letra da palavra, e a letra anterior seja VOGAL.
            if (result.EndsWith("H") && result.Length > 1)
            {
                result = result.Remove(result.Length - 1);
            }

            result = result.RemoveRepetedChars();

            return result.ToUpper().Trim();
        }

        private static string NotSpellOutNoSpace(this string _this)
        {
            if (_this == null) return null;
            var result = _this.ToUpper();

            result = result.Replace("Å", "A");
            result = result.Replace("Á", "A");
            result = result.Replace("À", "A");
            result = result.Replace("Â", "A");
            result = result.Replace("Ä", "A");
            result = result.Replace("Ã", "A");
            result = result.Replace("É", "E");
            result = result.Replace("È", "E");
            result = result.Replace("Ê", "E");
            result = result.Replace("Ë", "E");
            result = result.Replace("Í", "I");
            result = result.Replace("Ì", "I");
            result = result.Replace("Î", "I");
            result = result.Replace("Ï", "I");
            result = result.Replace("Ó", "O");
            result = result.Replace("Ò", "O");
            result = result.Replace("Ô", "O");
            result = result.Replace("Õ", "O");
            result = result.Replace("Ö", "O");
            result = result.Replace("Ú", "U");
            result = result.Replace("Ù", "U");
            result = result.Replace("Ü", "U");
            result = result.Replace("Û", "U");
            result = result.Replace("Y", "I");

            result = result.Replace("$", "S");
            result = result.Replace("+", "MAIS");
            result = result.Replace("%", "");
            result = result.Replace("&", "");
            result = result.Replace("@", "A");
            result = result.Replace("\"", "");
            result = result.Replace("'", "");
            result = result.Replace("!", " ");
            result = result.Replace("?", " ");
            result = result.Replace("¿", " ");
            result = result.Replace("¨", "");
            result = result.Replace("*", "");
            result = result.Replace("(", "");
            result = result.Replace(")", "");
            result = result.Replace("-", " ");
            result = result.Replace("_", " ");
            result = result.Replace("=", " ");
            result = result.Replace("|", " ");
            result = result.Replace("\\", " ");
            result = result.Replace("/", " ");
            result = result.Replace("´", "");
            result = result.Replace("`", "");
            result = result.Replace("^", "");
            result = result.Replace("~", "");
            result = result.Replace(",", " ");
            result = result.Replace(".", " ");
            result = result.Replace(";", " ");
            result = result.Replace(":", " ");
            result = result.Replace("ª", "");
            result = result.Replace("º", "");
            result = result.Replace("°", "");
            result = result.Replace("µ", "");
            result = result.Replace("ß", "");
            result = result.Replace("¢", "");
            result = result.Replace("£", "");
            result = result.Replace("¦", "");
            result = result.Replace("¹", "");
            result = result.Replace("²", "");
            result = result.Replace("³", "");
            result = result.Replace("½", "");
            result = result.Replace("¼", "");
            result = result.Replace("¾", "");
            result = result.Replace("÷", "");
            result = result.Replace("{", "");
            result = result.Replace("}", "");
            result = result.Replace("[", "");
            result = result.Replace("]", "");
            result = result.Replace("§", "");
            result = result.Replace("<", "");
            result = result.Replace(">", "");

            result = result.RemoveRepetedChars();

            result = result.Replace("CHA", "XA");
            result = result.Replace("CHE", "XE");
            result = result.Replace("CHI", "XI");
            result = result.Replace("CHO", "XO");
            result = result.Replace("CHU", "XU");
            result = result.Replace("SHA", "XA");
            result = result.Replace("SHE", "XE");
            result = result.Replace("SHI", "XI");
            result = result.Replace("SHO", "XO");
            result = result.Replace("SHU", "XU");
            result = result.Replace("CH", "K");
            result = result.Replace("SH", "X");

            result = result.Replace("BURGUER", "BURGER");

            result = result.Replace("INHO", "");
            result = result.Replace("INHOS", "");
            result = result.Replace("INHA", "");
            result = result.Replace("INHAS", "");

            result = result.Replace("CE", "SE");
            result = result.Replace("CI", "SI");
            result = result.Replace("GE", "JE");
            result = result.Replace("GI", "JI");
            result = result.Replace("C", "K");
            result = result.Replace("Ç", "S");
            result = result.Replace("PH", "F");
            result = result.Replace("XH", "X");
            result = result.Replace("Ñ", "N");
            result = result.Replace("QQ", "K");
            result = result.Replace("QU", "K");
            result = result.Replace("Q", "K");
            result = result.Replace("W", "V");

            result = result.Replace("KLA", "KRA");
            result = result.Replace("KLE", "KRE");
            result = result.Replace("KLI", "KRI");
            result = result.Replace("KLO", "KRO");
            result = result.Replace("KLU", "KRU");

            result = result.Replace("1", "UM");
            result = result.Replace("2", "DOIS");
            result = result.Replace("3", "TRES");
            result = result.Replace("4", "KATRO");
            result = result.Replace("5", "SINKO");
            result = result.Replace("6", "SEIS");
            result = result.Replace("7", "SETE");
            result = result.Replace("8", "OITO");
            result = result.Replace("9", "NOVE");
            result = result.Replace("0", "ZERO");

            result = Regex.Replace(result, "XH([AEIOU])", "X$1");
            result = result.Replace("AHRA", "ARA");

            result = Regex.Replace(result, "ZH([AEIOU])", "Z$1");
            result = Regex.Replace(result, "H([AEIOU])", "$1");

            result = Regex.Replace(result, "([AEIOU])L([BCDFGHJKLMNPQRSTVWXYZ])", "$1U$2");

            //--como também se a letra “L” for a última letra da palavra ortografada.
            if (result.EndsWith("L") && result.Length > 1)
                result = result.Substring(0, result.Length - 1) + "U";

            if (result.EndsWith("N") && result.Length > 1)
                result = result.Substring(0, result.Length - 1) + "M";

            //--8 - Incluir regra para ser executada após o término da mudança ortográfica para que a letra “H” 
            //--seja excluída do termo ortografado, quando:
            //--- Antes da letra “H” seja uma consoante e após uma vogal;

            result = Regex.Replace(result, "([BCDFGHJKLMNPQRSTVWXYZ])H([AEIOU])", "$1$2");
            //--- Antes da letra “H” seja uma vogal e após uma consoante;
            result = Regex.Replace(result, "([AEIOU])H([BCDFGHJKLMNPQRSTVWXYZ])", "$1$2");
            //--- A letra “H” seja a última letra da palavra, e a letra anterior seja VOGAL.
            if (result.EndsWith("H") && result.Length > 1)
            {
                result = result.Remove(result.Length - 1);
            }

            result = result.Replace(" ", "");
            result = result.RemoveRepetedChars();

            return result.ToUpper().Trim();
        }

        private static string RetrieveWordWithAsterisk(char[] characters, int index)
        {
            var word = string.Empty;
            for (var i = 0; i < characters.Length; i++)
                word += i == index
                    ? "**"
                    : characters[i].ToString();

            return word;
        }

        private static string GetRadicalWithoutBoundWord(string word, int limit)
        {
            return word.Length < limit
                ? word
                : word.Substring(0, limit);
        }

        private static int? GetAmountLettersOfPrefix(this string _this)
        {
            int? result = null;
            var myswitch = new Dictionary<Func<int, bool>, Action>
            {
                { x => x == 3 , () => result = (int)PrefixAmountLetters.Two },
                { x => x >= 4 && x <= 7 , () => result = (int)PrefixAmountLetters.Three },
                { x => x >= 8 && x <= 10 , () => result = (int)PrefixAmountLetters.Four },
                { x => x >= 11 && x <= 13 , () => result = (int)PrefixAmountLetters.Five },
                { x => x >= 14 , () => result = (int)PrefixAmountLetters.Six }
            };
            myswitch.First(x => x.Key(_this.Length)).Value();

            return result;
        }

        private static int? GetAmountLettersOfSuffix(this string _this)
        {
            int? result = null;
            var myswitch = new Dictionary<Func<int, bool>, Action>
            {
                { x => x == 3 , () => result = (int)SuffixAmountLetters.Two },
                { x => x >= 4 && x <= 7 , () => result = (int)SuffixAmountLetters.Three },
                { x => x >= 8 && x <= 10 , () => result = (int)SuffixAmountLetters.Four },
                { x => x >= 11 && x <= 13 , () => result = (int)SuffixAmountLetters.Five },
                { x => x >= 14 , () => result = (int)SuffixAmountLetters.Six }
            };
            myswitch.First(x => x.Key(_this.Length)).Value();

            return result;
        }

        #endregion Private Methods

        #region Public Methods

        public static string Spell(this string _this)
        {
            if (_this == null) return null;
            var result = _this.ToUpper();

            result = result.Replace(" ", "");
            result = result.Replace("Å", "A");
            result = result.Replace("Á", "A");
            result = result.Replace("À", "A");
            result = result.Replace("Â", "A");
            result = result.Replace("Ä", "A");
            result = result.Replace("Ã", "A");
            result = result.Replace("É", "E");
            result = result.Replace("È", "E");
            result = result.Replace("Ê", "E");
            result = result.Replace("Ë", "E");
            result = result.Replace("Í", "I");
            result = result.Replace("Ì", "I");
            result = result.Replace("Î", "I");
            result = result.Replace("Ï", "I");
            result = result.Replace("Ó", "O");
            result = result.Replace("Ò", "O");
            result = result.Replace("Ô", "O");
            result = result.Replace("Õ", "O");
            result = result.Replace("Ö", "O");
            result = result.Replace("Ú", "U");
            result = result.Replace("Ù", "U");
            result = result.Replace("Ü", "U");
            result = result.Replace("Û", "U");

            result = result.Replace("$", "S");
            result = result.Replace("+", "MAIS");
            result = result.Replace("%", "");
            result = result.Replace("&", "");
            result = result.Replace("@", "A");
            result = result.Replace("\"", "");
            result = result.Replace("'", "");
            result = result.Replace("!", "");
            result = result.Replace("?", "");
            result = result.Replace("¿", "");
            result = result.Replace("¨", "");
            result = result.Replace("*", "");
            result = result.Replace("(", "");
            result = result.Replace(")", "");
            result = result.Replace("-", "");
            result = result.Replace("_", "");
            result = result.Replace("=", "");
            result = result.Replace("|", "");
            result = result.Replace("\\", "");
            result = result.Replace("/", "");
            result = result.Replace("´", "");
            result = result.Replace("`", "");
            result = result.Replace("^", "");
            result = result.Replace("~", "");
            result = result.Replace(",", "");
            result = result.Replace(".", "");
            result = result.Replace(";", "");
            result = result.Replace(":", "");
            result = result.Replace("ª", "");
            result = result.Replace("º", "");
            result = result.Replace("°", "");
            result = result.Replace("µ", "");
            result = result.Replace("ß", "");
            result = result.Replace("¢", "");
            result = result.Replace("£", "");
            result = result.Replace("¦", "");
            result = result.Replace("¹", "");
            result = result.Replace("²", "");
            result = result.Replace("³", "");
            result = result.Replace("½", "");
            result = result.Replace("¼", "");
            result = result.Replace("¾", "");
            result = result.Replace("÷", "");
            result = result.Replace("{", "");
            result = result.Replace("}", "");
            result = result.Replace("[", "");
            result = result.Replace("]", "");
            result = result.Replace("§", "");
            result = result.Replace("<", "");
            result = result.Replace(">", "");
            result = result.Replace("#", "");

            result = result.RemoveRepetedChars();

            result = result.Replace("CHA", "XA");
            result = result.Replace("CHE", "XE");
            result = result.Replace("CHI", "XI");
            result = result.Replace("CHO", "XO");
            result = result.Replace("CHU", "XU");
            result = result.Replace("CH", "K");
            result = result.Replace("SH", "X");

            result = result.Replace("BURGUER", "BURGER");

            result = result.Replace("INHO", "");
            result = result.Replace("INHOS", "");
            result = result.Replace("INHA", "");
            result = result.Replace("INHAS", "");

            result = result.Replace("CE", "SE");
            result = result.Replace("CI", "SI");
            result = result.Replace("GE", "JE");
            result = result.Replace("GI", "JI");

            result = result.Replace("Ç", "S");
            result = result.Replace("C", "K");
            result = result.Replace("T", "D");
            result = result.Replace("PH", "F");
            result = result.Replace("XH", "X");
            result = result.Replace("N", "M");
            result = result.Replace("Ñ", "M");
            result = result.Replace("P", "B");
            result = result.Replace("QU", "K");
            result = result.Replace("Q", "K");
            result = result.Replace("Y", "I");
            result = result.Replace("Z", "S");

            result = result.Replace("KLA", "KRA");
            result = result.Replace("KLE", "KRE");
            result = result.Replace("KLI", "KRI");
            result = result.Replace("KLO", "KRO");
            result = result.Replace("KLU", "KRU");

            result = result.Replace("1", "UM");
            result = result.Replace("2", "DOIS");
            result = result.Replace("3", "DRES");
            result = result.Replace("4", "KADRO");
            result = result.Replace("5", "SIMKO");
            result = result.Replace("6", "SEIS");
            result = result.Replace("7", "SEDE");
            result = result.Replace("8", "OIDO");
            result = result.Replace("9", "MOVE");
            result = result.Replace("0", "SERO");

            result = result.Replace("PH", "F");
            result = result.Replace("W", "V");

            result = Regex.Replace(result, "XH([AEIOU])", "X$1");
            result = result.Replace("AHRA", "ARA");

            result = Regex.Replace(result, "ZH([AEIOU])", "Z$1");
            result = Regex.Replace(result, "H([AEIOU])", "$1");

            result = Regex.Replace(result, "([AEIOU])L([BCDFGHJKLMNPQRSTVWXYZ])", "$1U$2");

            //--como também se a letra “L” for a última letra da palavra ortografada.
            if (result.EndsWith("L") && result.Length > 1)
                result = result.Substring(0, result.Length - 1) + "U";

            //--8 - Incluir regra para ser executada após o término da mudança ortográfica para que a letra “H” 
            //--seja excluída do termo ortografado, quando:
            //--- Antes da letra “H” seja uma consoante e após uma vogal;

            result = Regex.Replace(result, "([BCDFGHJKLMNPQRSTVWXYZ])H([AEIOU])", "$1$2");
            //--- Antes da letra “H” seja uma vogal e após uma consoante;
            result = Regex.Replace(result, "([AEIOU])H([BCDFGHJKLMNPQRSTVWXYZ])", "$1$2");
            //--- A letra “H” seja a última letra da palavra, e a letra anterior seja VOGAL.
            if (result.EndsWith("H") && result.Length > 1)
            {
                result = result.Remove(result.Length - 1);
            }

            result = result.RemoveRepetedChars();

            return result.ToUpper().Trim();
        }

        public static string NotSpellManyWords(this string _this)
        {
            if (_this == null) return null;
            var words = _this.SplitWords();

            var result = string.Empty;

            words.ForEach(x => result += x.NotSpell() + " ");

            return result.Trim();
        }

        public static string NotSpellOutNoSpaceManyWords(this string _this)
        {
            if (_this == null) return null;
            var words = _this.SplitWords();

            var result = string.Empty;

            words.ForEach(x => result += x.NotSpellOutNoSpace());

            return result.Trim();
        }

        public static string RetirarVogais(this string _this)
        {
            if (_this == null) return null;
            var result = _this.ToUpper();

            result = result.Replace("Å", "A");
            result = result.Replace("Á", "A");
            result = result.Replace("À", "A");
            result = result.Replace("Â", "A");
            result = result.Replace("Ä", "A");
            result = result.Replace("Ã", "A");
            result = result.Replace("É", "E");
            result = result.Replace("È", "E");
            result = result.Replace("Ê", "E");
            result = result.Replace("Ë", "E");
            result = result.Replace("Í", "I");
            result = result.Replace("Ì", "I");
            result = result.Replace("Î", "I");
            result = result.Replace("Ï", "I");
            result = result.Replace("Ó", "O");
            result = result.Replace("Ò", "O");
            result = result.Replace("Ô", "O");
            result = result.Replace("Õ", "O");
            result = result.Replace("Ö", "O");
            result = result.Replace("Ú", "U");
            result = result.Replace("Ù", "U");
            result = result.Replace("Ü", "U");
            result = result.Replace("Û", "U");
            result = result.Replace("Y", "I");

            result = result.Replace("$", "S");
            result = result.Replace("+", "MAIS");
            result = result.Replace("%", "");
            result = result.Replace("&", "");
            result = result.Replace("@", "A");
            result = result.Replace("\"", "");
            result = result.Replace("'", "");
            result = result.Replace("!", " ");
            result = result.Replace("?", " ");
            result = result.Replace("¿", " ");
            result = result.Replace("¨", "");
            result = result.Replace("*", "");
            result = result.Replace("(", "");
            result = result.Replace(")", "");
            result = result.Replace("-", " ");
            result = result.Replace("_", " ");
            result = result.Replace("=", " ");
            result = result.Replace("|", " ");
            result = result.Replace("\\", " ");
            result = result.Replace("/", " ");
            result = result.Replace("´", "");
            result = result.Replace("`", "");
            result = result.Replace("^", "");
            result = result.Replace("~", "");
            result = result.Replace(",", " ");
            result = result.Replace(".", " ");
            result = result.Replace(";", " ");
            result = result.Replace(":", " ");
            result = result.Replace("ª", "");
            result = result.Replace("º", "");
            result = result.Replace("°", "");
            result = result.Replace("µ", "");
            result = result.Replace("ß", "");
            result = result.Replace("¢", "");
            result = result.Replace("£", "");
            result = result.Replace("¦", "");
            result = result.Replace("¹", "");
            result = result.Replace("²", "");
            result = result.Replace("³", "");
            result = result.Replace("½", "");
            result = result.Replace("¼", "");
            result = result.Replace("¾", "");
            result = result.Replace("÷", "");
            result = result.Replace("{", "");
            result = result.Replace("}", "");
            result = result.Replace("[", "");
            result = result.Replace("]", "");
            result = result.Replace("§", "");
            result = result.Replace("<", "");
            result = result.Replace(">", "");

            result = Regex.Replace(result, "([AEIOU])", "");

            return result.ToUpper().Trim();
        }

        public static List<string> SplitWords(this string _this)
        {
            if (_this == null) return null;
            var result = _this;

            string[] separators = new string[] { ",", ".", "!", "\'", " ", "\'s" };

            return result.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static List<string> Phonetic(this string _this)
        {
            if (_this == null) return null;

            var word = _this.ToLower().ToCharArray();
            var result = new List<string> { _this.ToLower() };

            for (int i = 0; i < _this.Length; i++)
                result.Add(RetrieveWordWithAsterisk(word, i));

            return result;
        }

        public static object VerificarData(this string data)
        {
            DateTime result;
            if ((DateTime.TryParse(data, CultureInfo.CreateSpecificCulture("pt-BR"), DateTimeStyles.AssumeLocal, out result))
                || (DateTime.TryParse(data, out result)))
                return result;

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

        public static string RemoveDiacritics(this string _this)
        {
            var normalizedString = _this.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
            {
                stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        public static string ToLowerFirstChar(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return char.ToLowerInvariant(value[0]) + value.Substring(1);
        }

        public static List<string> Radicalizar(this string _this)
        {
            if (string.IsNullOrWhiteSpace(_this))
                return null;

            _this = _this.ToUpper();
            var result = new List<string>();
            var amountLetters = 4;

            for (int i = 1; i < _this.Length - (amountLetters - 2); i++)
            {
                result.Add(GetRadicalWithoutBoundWord(_this.Substring(i - 1), amountLetters));
            }

            return result;
        }

        public static string GetStringReverse(this string _this)
        {
            if (string.IsNullOrWhiteSpace(_this))
                return null;

            var word = _this.ToCharArray();
            Array.Reverse(word);
            return new string(word);
        }

        public static List<string> Radicalize(this string _this)
        {
            if (string.IsNullOrWhiteSpace(_this))
                return null;

            _this = _this.ToUpper();
            var result = new List<string>();
            var amountLetters = 4;

            for (int i = 1; i < _this.Length - (amountLetters - 2); i++)
            {
                result.Add(GetRadicalWithoutBoundWord(_this.Substring(i - 1), amountLetters));
            }

            return result;
        }

        public static string GetPrefix(this string _this)
        {
            if (string.IsNullOrWhiteSpace(_this) || _this.Length < 3)
                return null;

            var amountLetters = _this.GetAmountLettersOfPrefix();

            return amountLetters != null
                ? _this.Substring(0, amountLetters.Value)
                : null;
        }

        public static string GetSuffix(this string _this)
        {
            if (string.IsNullOrWhiteSpace(_this) || _this.Length < 3)
                return null;

            var amountLetters = _this.GetAmountLettersOfSuffix();

            return amountLetters != null
                ? _this.Substring(_this.Length - amountLetters.Value, amountLetters.Value)
                : null;
        }

        public static List<string> GetRadical(this string _this)
        {
            if (string.IsNullOrWhiteSpace(_this))
                return null;

            var result = new List<string>();

            var amountLetters = _this.GetAmountLettersOfRadical();
            var repeatTimes = _this.Length == amountLetters
                ? 2
                : _this.Length - (amountLetters / 2);

            for (var i = 1; i < repeatTimes; i++)
            {
                result.Add(GetRadicalWithoutBoundWord(_this.Substring(i - 1), amountLetters.Value));
            }

            //I was too lazy to look for the perfect logic then did this
            result.RemoveAll(x => x.Length < amountLetters);

            return result.Count < 1
                ? null
                : result;
        }

        public static int? GetAmountLettersOfRadical(this string _this)
        {
            int? result = null;
            var myswitch = new Dictionary<Func<int, bool>, Action>
            {
                { x => x == 1 , () => result = (int)RadicalAmountLetters.One },
                { x => x == 2 , () => result = (int)RadicalAmountLetters.Two },
                { x => x == 3 , () => result = (int)RadicalAmountLetters.Three },
                { x => x >= 4 && x <= 7 , () => result = (int)RadicalAmountLetters.Four },
                { x => x >= 8 && x <= 10 , () => result = (int)RadicalAmountLetters.Six },
                { x => x >= 11 && x <= 13 , () => result = (int)RadicalAmountLetters.Seven },
                { x => x >= 14 && x <= 16 , () => result = (int)RadicalAmountLetters.Eight },
                { x => x >= 17 , () => result = (int)RadicalAmountLetters.Nine }
            };
            myswitch.First(x => x.Key(_this.Length)).Value();

            return result;
        }

        #endregion Public Methods
        
    }
}
