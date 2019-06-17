namespace BusinessLayer
{
    using DTOLayer;
    using PagedList;
    public interface IClasseAfinidadeNegocio
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
