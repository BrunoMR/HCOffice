using System.Text.RegularExpressions;

namespace Utils
{
    public static class RegularExpressions
    {
        public static Regex Sobrestadores = new Regex(@"^.*?\b(Processo[\s+]?(\d{9}))\b.*$", RegexOptions.Compiled);

        public static Regex FilesExtensions = new Regex(@"^.+\.(?i)(txt|xml)$", RegexOptions.Compiled);

        public static Regex DataRegex =
            new Regex(
                @"^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$",
                RegexOptions.Compiled);

        #region Txt

        public static Regex CodeClasseNiceFromTxt = new Regex(@"^N[0-9]{4}", RegexOptions.Compiled);

        public static Regex RpiRegex = new Regex(@"RPIM\-(([0-9]{4})\-([0-9]{2})\-([0-9]{2}))\-([0-9]+)",
            RegexOptions.Compiled);

        public static Regex ProcessoLineRegex = new Regex(@"^No\.([^ ]+)[\s]+([^ ]+)[\s]+([^\n]+)", RegexOptions.Compiled);

        public static Regex TitularNomeLineRegex = new Regex(@"^Tit.[\s+]?([^{\¢|\n}]+)",
            RegexOptions.Compiled);

        public static Regex TitularPaisEEstadoLineRegex = new Regex(@"\¢\((\w+)\/(\w+)\)|\¢\((\w+)", RegexOptions.Compiled);

        public static Regex CpfCnpjLineRegex = new Regex(@"^C\.N\.P\.J\.\/C\.I\.C\.\/N.INPI : ([^\n][\d]+)",
            RegexOptions.Compiled);

        public static Regex ApresentacaoLineRegex = new Regex(@"^Apres\.: ([^;]+) ; Nat\.: ([^\n]+)",
            RegexOptions.Compiled);

        public static Regex MarcaLineRegex = new Regex(@"^Marca\: ([^\n]+)", RegexOptions.Compiled);
        public static Regex CfeLineRegex = new Regex(@"^(CFE\(([0-9]+)\)) ([^\n]+)", RegexOptions.Compiled);
        public static Regex ClasseLineRegex = new Regex(@"^Clas\.Prod\/Serv: ([^\n]+)", RegexOptions.Compiled);
        public static Regex EspecificaoLineRegex = new Regex(@"^Especific.: ([^\n]+)", RegexOptions.Compiled);

        public static Regex NclLineRegex = new Regex(@"^(NCL\(([^\)]+)\))[ ]+([0-9]+)[ ]+([^\n]+)",
            RegexOptions.Compiled);

        public static Regex ApostilaLineRegex = new Regex(@"^Apostila: ([^\n]+)", RegexOptions.Compiled);

        public static Regex PrioridadeLineRegex = new Regex(@"^Prior\.:(.{18})(.{10})[ ]+([^ ]+)",
            RegexOptions.Compiled);

        public static Regex TextoComplementarLineRegex = new Regex(@"^\*([^\n]+)", RegexOptions.Compiled);
        public static Regex ProcuradorLineRegex = new Regex(@"^Procurador: ([^\n]+)", RegexOptions.Compiled);
        
        #endregion Txt

        #region Regex OldTxt

        public static Regex OldProcessoRegex = new Regex(@"^¢Processo:\s+(\d{9})", RegexOptions.Compiled);
        public static Regex OldMarcaRegex = new Regex(@"^¢Marca:[\s+]?([^\n]+)", RegexOptions.Compiled);
        
        public static Regex OldTitularNomeRegex = new Regex(@"^¢Titular:[\s+]?([^{\¢|\n}]+)", RegexOptions.Compiled);
        public static Regex OldTitularPaisEstadoRegex = new Regex(@"\¢\((\w+)\/(\w+)\)|\¢\((\w+)", RegexOptions.Compiled);
        
        public static Regex OldClasseRegex = new Regex(@"¢Classe:[\s+]?((\d{2})\/([^\n]+))", RegexOptions.Compiled);

        public static Regex OldNclRegex = new Regex(@"¢Classe:[\s+]?Ncl\((\d{1,2})\)[\s|\t]+(\d{1,2})",
            RegexOptions.Compiled);

        public static Regex OldNaturezaRegex = new Regex(@"¢Natureza:[^\w+]?(\w+)", RegexOptions.Compiled);
        public static Regex OldApresentacaoRegex = new Regex(@"¢Apresentação:[\s+]?(\w+)", RegexOptions.Compiled);

        public static Regex OldDepositoRegex = new Regex(@"¢Depósito:[^\w+]?((\d{2})\/(\d{2})\/(\d{4}))",
            RegexOptions.Compiled);

        public static Regex OldConcessaoRegex = new Regex(@"¢Concessão:[^\w+]?((\d{2})\/(\d{2})\/(\d{4}))",
            RegexOptions.Compiled);

        public static Regex OldEspecificacaoRegex = new Regex(@"¢Especificação:[^\w+]?([^\n]+)", RegexOptions.Compiled);
        public static Regex OldApostilaRegex = new Regex(@"¢Apostila:[^\w+]?([^\n]+)", RegexOptions.Compiled);

        public static Regex OldStartsLineDespachoRegex = new Regex(@"^¢((\d{2})\/(\d{2})\/(\d{4}))", RegexOptions.Compiled);
        public static Regex OldTextoComplementarRegex = new Regex(@"\t\¢([^\n]+)", RegexOptions.Compiled);

        #endregion Regex OldTxt

        #region TaxSystem

        public static Regex StartsLineC100 = new Regex(@"^|C100|", RegexOptions.Compiled);
        public static Regex StartsLineC170 = new Regex(@"^|C170|", RegexOptions.Compiled);
        public static Regex StartsLine0150 = new Regex(@"^|0150|", RegexOptions.Compiled);
        public static Regex StartsLine0400 = new Regex(@"^|0400|", RegexOptions.Compiled);
        //public static Regex C170Cfop = new Regex(@"\|([0-9]{4})\|", RegexOptions.Compiled);

        #endregion TaxSystem
    }
}