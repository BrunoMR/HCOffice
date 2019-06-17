
using ServiceStack.DataAnnotations;

namespace DTOLayer
{
    [Alias("TIPO_PESSOA")]
    public class TipoPessoa
    {
        [PrimaryKey]
        public char Tipo { get; set; }
        public string Descricao { get; set; }
        public int Ordem { get; set; }
        [Ignore]
        public bool IsNew { get; set; }
    }
}
