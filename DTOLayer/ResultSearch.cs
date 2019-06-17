namespace DTOLayer
{
    using System.Collections.Generic;
    using Indexes;
    public class ResultSearch
    {
        public long TotalProcesses { get; set; }
        public long TotalOfSearch { get; set; }
        public List<ProcessoIndex> ProcessoList  { get; set; }
    }
}
