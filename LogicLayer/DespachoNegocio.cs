using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DTOLayer;
using ExtratorDeDados.Repository;

namespace ExtratorDeDados.Negocio
{
    public class DespachoNegocio
    {
        private static List<Despacho> _despachoList;
        
        #region Public Methods
        /// <summary>
        /// Retorna se existe o Despacho com o código passado por parâmetro
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public bool ExistsDespacho(string codigo)
        {
            return _despachoList.Any(despacho => despacho.Codigo.Contains(codigo));
            //Despacho modelDespacho = new Despacho()
            //{
            //    Codigo = codigo
            //};

            //return Search(modelDespacho).Count > 0;
        }

        public static void FindAllDespacho()
        {
            _despachoList = Search(new Despacho());
        }
        
        #endregion Public Methods

        #region Private Methods
        private static List<Despacho> Search(Despacho model)
        {
            ARepositorySelect<Despacho> aRepositorySelect = new DespachoRepository();

            return aRepositorySelect.Buscar(model);
        }

        #endregion Private Methods
    }
}
