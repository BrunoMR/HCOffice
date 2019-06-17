namespace DTOLayer.Indexes
{
    using System;
    using Nest;

    /// <summary>
    /// The despacho sync.
    /// </summary>
    [ElasticsearchType(IdProperty = "codigo", Name = "despacho")]
    public class DespachoSync
    {
        /// <summary>
        /// Gets or sets the process number.
        /// </summary>
        public string ProcessoNumero { get; set; }

        /// <summary>
        /// Gets or sets the rpi.
        /// </summary>
        public int Rpi { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public DateTime? Data { get; set; }

        /// <summary>
        /// Gets or sets the código.
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Gets or sets the descrição.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Gets or sets the descrição completa.
        /// </summary>
        public string DescricaoCompleta { get; set; }

        /// <summary>
        /// Gets or sets the despacho situação.
        /// </summary>
        public string DespachoSituacao { get; set; }

        /// <summary>
        /// Gets or sets the complemento.
        /// </summary>
        public string Complemento { get; set; }

        /// <summary>
        /// Gets or sets the despacho tipo.
        /// </summary>
        public string DespachoTipo { get; set; }

        /// <summary>
        /// Gets or sets the despacho tipo descrição.
        /// </summary>
        public string DespachoTipoDescricao { get; set; }

        /// <summary>
        /// Gets or sets the protocolo numero.
        /// </summary>
        public string ProtocoloNumero { get; set; }

        /// <summary>
        /// Gets or sets the protocolo código.
        /// </summary>
        public string ProtocoloCodigo { get; set; }

        /// <summary>
        /// Gets or sets the protocolo data.
        /// </summary>
        public DateTime? ProtocoloData { get; set; }

        /// <summary>
        /// Gets or sets the protocolo nome razão social.
        /// </summary>
        public string ProtocoloNomeRazaoSocial { get; set; }

        /// <summary>
        /// Gets or sets the protocolo país.
        /// </summary>
        public string ProtocoloPais { get; set; }

        /// <summary>
        /// Gets or sets the protocolo uf.
        /// </summary>
        public string ProtocoloUf { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether último despacho.
        /// </summary>
        public bool UltimoDespacho { get; set; }
    }
}
