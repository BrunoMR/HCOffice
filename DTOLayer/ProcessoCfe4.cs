namespace DTOLayer
{
    using ServiceStack.DataAnnotations;
    using System.Collections.Generic;

    [Alias("PROCESSO_CFE4")]
    public class ProcessoCfe4
    {
        [AutoIncrement]
        public int? Id { get; set; }

        [Alias("ID_PROCESSO")]
        [References(typeof(Processo))]
        public int? ProcessoId { get; set; }

        [Alias("ID_CFE4")]
        [References(typeof(CFE4))]
        public int? Cfe4Id { get; set; }

        [Reference]
        public CFE4 Cfe4 { get; set; }
    }
}
