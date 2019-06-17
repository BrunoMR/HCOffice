using System.Collections.Generic;
using System.Text;
using Dapper;
using DTOLayer;

namespace DataLayer
{
    public interface ITipoPessoaRepository
    {
        #region CRUD

        List<TipoPessoa> GetAll();
        TipoPessoa FindByTipo(char tipo);
        TipoPessoa Add(TipoPessoa tipoPessoa);
        TipoPessoa Update(TipoPessoa tipoPessoa);
        void Remove(char tipo);

        #endregion CRUD
    }
}
