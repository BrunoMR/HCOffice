namespace BusinessLayer
{
    using DTOLayer;

    public class LogProcess
    {
        /// <summary>
        /// Aqui terá o valor do Processo em validação
        /// </summary>
        public static ProcessoImported CurrentProcesso { get; set; }

        /// <summary>
        /// Neste campo estará o último Processo validado
        /// </summary>
        public static ProcessoImported LastProcesso { get; set; }

        public static void ClearLogs()
        {
            CurrentProcesso = null;
            LastProcesso = null;
        }

        public static void PutCurrentProcess(ProcessoImported processo)
        {
            CurrentProcesso = processo;
        }

        public static void PutLastProcess(ProcessoImported processo)
        {
            LastProcesso = processo;
        }

        public static void UpdateCurrentAndLastProcess(ProcessoImported processo)
        {
            if (!processo.NumeroProcesso.Equals(CurrentProcesso?.NumeroProcesso))
            {
                LastProcesso = CurrentProcesso;
                CurrentProcesso = processo;
            }
        }
    }
}