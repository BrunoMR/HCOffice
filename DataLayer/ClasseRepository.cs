namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using DTOLayer;
    using ServiceStack.OrmLite;

    public class ClasseRepository : ARepository, IClasseRepository
    {
        #region CRUD

        public List<Classe> GetAll()
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().Select<Classe>();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível procurar na tabela de Classe!", ex.InnerException);
            }
        }

        public Classe FindByCodeClasse(string code)
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().Single<Classe>(x => x.NumeroClasse == code);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível pesquisar o código '{0}' na tabela de Classe!", code), ex.InnerException);
            }
        }
        
        public void Add(Classe classe)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Insert(classe);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível inserir na tabela de Classe o código '{0}'!", classe.NumeroClasse), ex.InnerException);
            }
        }

        public Classe Update(Classe classe)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Update(classe);
                return classe;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível atualizar na tabela de Classe o código '{0}'!", classe.NumeroClasse), ex.InnerException);
            }
        }

        public void Remove(string code)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Delete<Classe>(x => x.NumeroClasse == code);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível remover da tabela de Classe o código '{0}'!", code), ex.InnerException);
            }
        }

        #endregion CRUD
    }
}
