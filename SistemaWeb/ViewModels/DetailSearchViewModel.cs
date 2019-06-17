namespace SistemaWeb.ViewModels
{
    using System.Collections.Generic;
    using DTOLayer;

    public class DetailSearchViewModel
    {
        public DetailProcessoViewModel Processo { get; set; }
        public List<CitacaoViewModel> CitacaoList { get; set; }
    }
}
