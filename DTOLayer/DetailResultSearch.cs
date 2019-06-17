namespace DTOLayer
{
    using System.Collections.Generic;
    using Indexes;
    public class DetailResultSearch
    {
        public ProcessoIndex Processo { get; set; }
        public List<Citacao> CitacaoList  { get; set; }
    }
}
