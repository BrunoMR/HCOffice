using System.Collections;
using System.Collections.Generic;
using DTOLayer;

namespace SistemaWeb.ViewModels
{
    public class ResultSearchViewModel
    {
        public int TotalProcesses { get; set; }
        public int TotalOfSearch { get; set; }
        //public Pagination Pagination { get; set; }
        public List<ResultProcessoViewModel> ProcessoList { get; set; }
    }
}
