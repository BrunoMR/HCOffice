using System.ComponentModel.DataAnnotations.Schema;

namespace DTOLayer
{
    using ServiceStack.DataAnnotations;
    using System;

    [Alias("PROCESSO_DESPACHO")]
    public class ProcessoDespacho
    {
        [AutoIncrement]
        public int? Id { get; set; }

        [Alias("ID_PROCESSO")]
        [References(typeof(Processo))]
        public int? ProcessoId { get; set; }

        [Alias("ID_DESPACHO")]
        [References(typeof(Despacho))]
        [Column("ID_DESPACHO")]
        public int? DespachoId { get; set; }

        [Reference]
        public Despacho Despacho { get; set; }

        [Alias("NUMERO_RPI")]
        public int? RpiId { get; set; }

        [Reference]
        public Rpi Rpi { get; set; }

        [Alias("DATA_DESPACHO")]
        public DateTime DataDespacho { get; set; }

        [Alias("ID_PROTOCOLO")]
        public int? ProtocoloId { get; set; }

        [Reference]
        public Protocolo Protocolo { get; set; }

        public string Complemento { get; set; }
    }
}
