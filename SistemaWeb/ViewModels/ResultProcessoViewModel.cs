namespace SistemaWeb.ViewModels
{
    public class ResultProcessoViewModel
    {
        public ResultProcessoViewModel()
        {
            TipoApresentacao = new TipoApresentacaoViewModel();
        }

        public int? Id { get; set; }
        public string Numero { get; set; }
        public string Titular { get; set; }
        public string Marca { get; set; }
        public TipoApresentacaoViewModel TipoApresentacao { get; set; }
        public string DataDeposito { get; set; }
        public string DataConcessao { get; set; }
        public string Classe { get; set; }
        public string Cfe4 { get; set; }
        public string UltimoDespacho { get; set; }
        public string DespachoDescricaoCompleta { get; set; }
        public string Especificacao { get; set; }
    }

    public class TipoApresentacaoViewModel
    {
        public string InicialNome { get; set; } = string.Empty;
        public string Estilo { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
    }
}
