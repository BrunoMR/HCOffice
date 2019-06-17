namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DataLayer;
    using DTOLayer;
    using Utils;

    public class DespachoNegocio : IDespachoNegocio
    {
        /// <summary>
        /// The _despacho list.
        /// </summary>
        private static List<Despacho> _despachoList;

        /// <summary>
        /// The _despacho repository.
        /// </summary>
        private readonly IDespachoRepository _despachoRepository = new DespachoRepository();

        #region Public Methods

        #region CRUD

        public List<Despacho> GetAll()
        {
            try
            {
                return _despachoRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Despacho FindById(int id)
        {
            try
            {
                return _despachoRepository.FindById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private Despacho Add(Despacho despacho)
        {
            try
            {
                return _despachoRepository.Add(despacho);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private Despacho Update(Despacho despacho)
        {
            try
            {
                return _despachoRepository.Update(despacho);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public Despacho Save(Despacho despacho)
        {
            return despacho?.Id == null
                ? Add(despacho)
                : Update(despacho);
        }

        #endregion CRUD

        /// <summary>
        /// Retorna se existe o Despacho com o código passado por parâmetro
        /// </summary>
        /// <param name="codigo">Code of Despacho</param>
        /// <returns>Returns if found the Despacho</returns>
        public bool ExistsDespacho(string codigo)
        {
            return _despachoList.Any(despacho => despacho.Codigo.Equals(codigo));
        }

        /// <summary>
        /// The find all despacho.
        /// </summary>
        public static void FindAllDespacho()
        {
            IDespachoRepository despachoRepository = new DespachoRepository();
            _despachoList = despachoRepository.GetAll();
        }

        public static void BuildAdditionalTextOfDespacho(RpiImported rpi)
        {
            rpi.Processo
                .AsParallel()
                .ForAll(pro =>
                {
                    pro.Despachos.Despacho
                    .Where(des => des.Complemento != null)
                    .ToList()
                    .ForEach(des =>
                    {
                        var processosSobrestadores = RegularExpressions.Sobrestadores.Match(des.Complemento);
                        if (processosSobrestadores.Success)
                        {
                            var startsSobrestadores = des.Complemento.IndexOf(processosSobrestadores.Groups[1].Value, StringComparison.Ordinal);

                            var despachoDetails = string.Empty;
                            if (startsSobrestadores > 0)
                                despachoDetails = "Detalhes do Despacho: " + des.Complemento.Substring(0, startsSobrestadores) + Environment.NewLine + Environment.NewLine;

                            var despachoSobrestadores = "Sobrestadores: " + des.Complemento.Substring(startsSobrestadores);

                            des.Complemento = despachoDetails + despachoSobrestadores;
                        }
                    });
                });
        }

        public static void BuildPetitionOfDespacho(RpiImported rpi)
        {
            IAtributoNegocio atributoNegocio = new AtributoNegocio();

            rpi.Processo
                .AsParallel()
                .ForAll(pro =>
                {
                    pro.Despachos.Despacho
                    .Where(des => !string.IsNullOrWhiteSpace(des.Protocolo?.CodigoServico))
                    .ToList()
                    .AsParallel()
                    .ForAll(des =>
                    {
                        var complementoDespacho = new StringBuilder();

                        if (!string.IsNullOrWhiteSpace(des.Complemento))
                            complementoDespacho.AppendLine($"Detalhes do Despacho: {des.Complemento}");

                        complementoDespacho.AppendLine($"Protocolo: {des.Protocolo.Numero} Serv: {des.Protocolo.CodigoServico} Data: {des.Protocolo.Data}");

                        // Código para buscar de atributos
                        if (!string.IsNullOrWhiteSpace(des.Protocolo.CodigoServico))
                        {
                            complementoDespacho.AppendLine(
                                $"Petição (tipo): {atributoNegocio.FindByCode(des.Protocolo.CodigoServico)?.Descricao}");
                        }
                        
                        if (des.Protocolo.Requerente != null)
                        {
                            string paisUf;
                            if (string.IsNullOrWhiteSpace(des.Protocolo.Requerente.Uf))
                                paisUf = des.Protocolo.Requerente.Pais;
                            else
                                paisUf = des.Protocolo.Requerente.Pais + "/" + des.Protocolo.Requerente.Uf;

                            complementoDespacho.AppendLine($"Requerente: {des.Protocolo.Requerente.Nome} ({paisUf})");
                        }

                        if (!string.IsNullOrWhiteSpace(des.Protocolo.Procurador))
                            complementoDespacho.AppendLine($"Procurador: {des.Protocolo.Procurador}");

                        if (des.Protocolo.Cessionario != null)
                            complementoDespacho.AppendLine($"Cessionário: {des.Protocolo.Cessionario.NomeRazaoSocial}");

                        des.Complemento = complementoDespacho.ToString();
                    });
                });
        }
        
        #endregion Public Methods
    }
}
