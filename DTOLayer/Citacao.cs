namespace DTOLayer
{
    using System.Collections.Generic;
    using Indexes;
    using System;

    public class Citacao
    {
        public string Numero { get; set; }
        public DateTime? DataDeposito { get; set; }
        public string Marca { get; set; }
        public List<ClasseSync> Classe { get; set; }
        public List<DespachoSync> Despacho { get; set; }
        public string Apresentacao { get; set; }
        public string Titular { get; set; }
    }
}
