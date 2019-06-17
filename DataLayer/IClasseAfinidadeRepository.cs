using System.Collections.Generic;
using System.Threading.Tasks;
using DTOLayer;
using PagedList;

namespace DataLayer
{
    public interface IClasseAfinidadeRepository
    {
        #region CRUD

        IPagedList<ClasseAfinidade> GetAll(int pageNumber, int pageSize);
        ClasseAfinidade FindByCodeClasse(ClasseAfinidade classeAfinidade);
        ClasseAfinidade Add(ClasseAfinidade classeAfinidade);
        ClasseAfinidade Update(ClasseAfinidade classeAfinidade);
        void Remove(ClasseAfinidade classeAfinidade);

        #endregion CRUD
    }
}
