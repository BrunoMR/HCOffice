namespace BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using DataLayer;
    using DTOLayer;

    /// <summary>
    /// The tipo pessoa negocio.
    /// </summary>
    public class TipoPessoaNegocio : ITipoPessoaNegocio
    {
        /// <summary>
        /// The _tipo pessoa repository.
        /// </summary>
        private readonly ITipoPessoaRepository _tipoPessoaRepository = new TipoPessoaRepository();

        #region CRUD

        public List<TipoPessoa> GetAll()
        {
            try
            {
                return _tipoPessoaRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public TipoPessoa FindByTipo(char tipo)
        {
            try
            {
                return _tipoPessoaRepository.FindByTipo(tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private TipoPessoa Add(TipoPessoa tipoPessoa)
        {
            try
            {
                _tipoPessoaRepository.Add(tipoPessoa);
                return tipoPessoa;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        private TipoPessoa Update(TipoPessoa tipoPessoa)
        {
            try
            {
                return _tipoPessoaRepository.Update(tipoPessoa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public TipoPessoa AddOrUpdate(TipoPessoa tipoPessoa)
        {
            return tipoPessoa.IsNew
                ? Add(tipoPessoa)
                : Update(tipoPessoa);
        }

        public void Remove(char tipo)
        {
            try
            {
                _tipoPessoaRepository.Remove(tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        #endregion CRUD
    }
}
