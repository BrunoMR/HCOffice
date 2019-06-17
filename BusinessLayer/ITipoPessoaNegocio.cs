namespace BusinessLayer
{
    using System.Collections.Generic;
    using DTOLayer;

    public interface ITipoPessoaNegocio
    {
        #region CRUD

        List<TipoPessoa> GetAll();
        TipoPessoa FindByTipo(char tipo);
        TipoPessoa AddOrUpdate(TipoPessoa tipoPessoa);
        void Remove(char tipo);

        #endregion CRUD
    }
}
