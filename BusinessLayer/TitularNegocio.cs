namespace BusinessLayer
{
    using System.Linq;

    using DTOLayer;

    public class TitularNegocio
    {
        /// <summary>The remove second titular.</summary>
        /// <param name="rpi">The rpi.</param>
        public static void RemoveSecondTitular(RpiImported rpi)
        {
            rpi.Processo
                .AsParallel()
                .ForAll(pro =>
                    {
                        if (pro.Titulares != null)
                        {
                            var existsAndMoreThanOne = pro.Titulares.Titular.Any() && pro.Titulares.Titular.Count > 1;
                            if (existsAndMoreThanOne)
                            {
                                pro.Titulares.Titular.RemoveAll(x => string.IsNullOrWhiteSpace(x.Pais));
                            }
                        }
                    });
        }
    }
}