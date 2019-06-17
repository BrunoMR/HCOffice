namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using DTOLayer;
    using ServiceStack.OrmLite;

    public class AtributoRepository : ARepository, IAtributoRepository
    {
        #region CRUD

        public List<Atributo> GetAll()
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().Select<Atributo>();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível procurar na tabela de Atributo!", ex.InnerException);
            }
        }

        public Atributo FindById(int id)
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().SingleById<Atributo>(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível procurar o Atributo com ID = '{id}'!", ex.InnerException);
            }
        }

        public Atributo Add(Atributo atributo)
        {
            try
            {
                atributo.Id = (int)ConnectionOrmLite.OpenConnection().Insert(atributo, true);
                return atributo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível inserir o Atributo '{atributo.Codigo}'!", ex.InnerException);
            }
        }

        public Atributo Update(Atributo atributo)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Update(atributo);
                return atributo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível atualizar o Atributo '{atributo.Codigo}'!", ex.InnerException);
            }
        }

        public void Remove(int id)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().DeleteById<Atributo>(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível excluir o Atributo com ID = '{id}'!", ex.InnerException);
            }
        }

        #endregion CRUD
    }
}
