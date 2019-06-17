namespace BusinessLayer
{
    using DTOLayer;
    using FluentValidation.Results;
    using System.Collections.Generic;

    public interface IRpiNegocio
    {
        //static RpiImported CurrentRpi { get; set; }
        List<ValidationResult> ProcessRpi(List<RpiImported> rpis);
        List<Rpi> GetAll();
    }
}
