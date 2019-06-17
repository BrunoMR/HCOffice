namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using DTOLayer;
    using ServiceStack.OrmLite;
    using System.Linq;
    using PagedList;
    public class ClasseAfinidadeRepository : ARepository, IClasseAfinidadeRepository
    {
        #region CRUD

        public IPagedList<ClasseAfinidade> GetAll(int pageNumber, int pageSize)
        {
            try
            {
                return ConnectionOrmLite.OpenConnection().LoadSelect<ClasseAfinidade>().ToPagedList(pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível procurar na tabela de Classe Afinidade!", ex.InnerException);
            }
        }

        public ClasseAfinidade FindByCodeClasse(ClasseAfinidade classeAfinidade)
        {
            try
            {
                return ConnectionOrmLite.OpenConnection()
                    .Single<ClasseAfinidade>(x => x.ClasseAId.Equals(classeAfinidade.ClasseAId) && x.ClasseBId.Equals(classeAfinidade.ClasseBId));
            }
            catch (Exception ex)
            {
                throw new Exception(string
                    .Format("Não foi possível pesquisar os códigos '{0}' e '{1}' na tabela de Classe Afinidade!", 
                    classeAfinidade.ClasseAId, 
                    classeAfinidade.ClasseBId), ex.InnerException);
            }
        }
        
        public ClasseAfinidade Add(ClasseAfinidade classeAfinidade)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Insert(classeAfinidade);
                return classeAfinidade;
            }
            catch (Exception ex)
            {
                throw new Exception(string
                    .Format("Não foi possível inserir na tabela de Classe Afinidade os códigos '{0}' e '{1}'!"
                    , classeAfinidade.ClasseAId,
                    classeAfinidade.ClasseBId), ex.InnerException);
            }
        }

        public ClasseAfinidade Update(ClasseAfinidade classeAfinidade)
        {
            try
            {
                ConnectionOrmLite.OpenConnection().Update(classeAfinidade);
                return classeAfinidade;
            }
            catch (Exception ex)
            {
                throw new Exception(string
                    .Format("Não foi possível atualizar na tabela de Classe Afinidade os códigos '{0} e {1}'!", 
                    classeAfinidade.ClasseAId,
                    classeAfinidade.ClasseBId), ex.InnerException);
            }
        }

        public void Remove(ClasseAfinidade classeAfinidade)
        {
            try
            {
                ConnectionOrmLite.OpenConnection()
                    .Delete<ClasseAfinidade>(x => x.ClasseAId.Equals(classeAfinidade.ClasseAId) && x.ClasseBId.Equals(classeAfinidade.ClasseBId));
            }
            catch (Exception ex)
            {
                throw new Exception(string
                    .Format("Não foi possível remover da tabela de Classe Afinidade os códigos '{0}' e '{1}'!", 
                    classeAfinidade.ClasseAId,
                    classeAfinidade.ClasseBId), ex.InnerException);
            }
        }

        #endregion CRUD
    }
}
