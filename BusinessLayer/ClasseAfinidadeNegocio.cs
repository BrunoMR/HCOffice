namespace BusinessLayer
{
    using DataLayer;
    using DTOLayer;
    using PagedList;
    public class ClasseAfinidadeNegocio : IClasseAfinidadeNegocio
    {
        #region CRUD

        public IPagedList<ClasseAfinidade> GetAll(int pageNumber, int pageSize)
        {
            IClasseAfinidadeRepository classeAfinidadeRepository = new ClasseAfinidadeRepository();
            return classeAfinidadeRepository.GetAll(pageNumber, pageSize);
        }

        public ClasseAfinidade FindByCodeClasse(ClasseAfinidade classeAfinidade)
        {
            IClasseAfinidadeRepository classeAfinidadeRepository = new ClasseAfinidadeRepository();
            return classeAfinidadeRepository.FindByCodeClasse(classeAfinidade);
        }

        public ClasseAfinidade Add(ClasseAfinidade classeAfinidade)
        {
            IClasseAfinidadeRepository classeAfinidadeRepository = new ClasseAfinidadeRepository();
            return classeAfinidadeRepository.Add(classeAfinidade);
        }

        public ClasseAfinidade Update(ClasseAfinidade classeAfinidade)
        {
            IClasseAfinidadeRepository classeAfinidadeRepository = new ClasseAfinidadeRepository();
            return classeAfinidadeRepository.Update(classeAfinidade);
        }

        public void Remove(ClasseAfinidade classeAfinidade)
        {
            IClasseAfinidadeRepository classeAfinidadeRepository = new ClasseAfinidadeRepository();
            classeAfinidadeRepository.Remove(classeAfinidade);
        }

        #endregion CRUD   
    }
}
