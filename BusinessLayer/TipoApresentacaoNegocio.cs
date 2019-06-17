using System;

namespace BusinessLayer
{
    using System.Collections.Generic;
    using System.Linq;
    using DataLayer;
    using DTOLayer;
    using Utils;

    public class TipoApresentacaoNegocio : ITipoApresentacaoNegocio
    {
        private static List<TipoApresentacao> _tipoApresentacaoList;
        
        /// <summary>
        /// Retorna se existe a apresentação passada por parâmetro
        /// </summary>
        /// <param name="apresentacao">Tipo de apresentação que será pesquisada</param>
        /// <returns></returns>
        public bool ExistsTipoApresentacao(string apresentacao)
        {
            return _tipoApresentacaoList.Any(tipoApresentacao => tipoApresentacao.Descricao.RemoveDiacritics().Contains(apresentacao.RemoveDiacritics()));
        }

        public static void FindAllTipoApresentacao()
        {
            ITipoApresentacaoRepository tipoApresentacaoRepository = new TipoApresentacaoRepository();
            _tipoApresentacaoList = tipoApresentacaoRepository.GetAll();
        }

        public static int? FindByDescription(string apresentacao)
        {
            if (apresentacao == null)
                return null;
            var firstOrDefault = _tipoApresentacaoList.FirstOrDefault(x => x.Descricao.RemoveDiacritics().Contains(apresentacao.RemoveDiacritics()));
            return firstOrDefault?.Id;
        }

        #region CRUD

        public List<TipoApresentacao> GetAll()
        {
            try
            {
                ITipoApresentacaoRepository tipoApresentacaoRepository = new TipoApresentacaoRepository();
                return tipoApresentacaoRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public TipoApresentacao FindById(int id)
        {
            try
            {
                ITipoApresentacaoRepository tipoApresentacaoRepository = new TipoApresentacaoRepository();
                return tipoApresentacaoRepository.FindById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private TipoApresentacao Add(TipoApresentacao tipoApresentacao)
        {
            try
            {
                ITipoApresentacaoRepository tipoApresentacaoRepository = new TipoApresentacaoRepository();
                return tipoApresentacaoRepository.Add(tipoApresentacao);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private TipoApresentacao Update(TipoApresentacao tipoApresentacao)
        {
            try
            {
                ITipoApresentacaoRepository tipoApresentacaoRepository = new TipoApresentacaoRepository();
                return tipoApresentacaoRepository.Update(tipoApresentacao);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public TipoApresentacao Save(TipoApresentacao tipoApresentacao)
        {
            return tipoApresentacao?.Id == null 
                ? Add(tipoApresentacao) 
                : Update(tipoApresentacao);
        }

        #endregion CRUD

    }
}
