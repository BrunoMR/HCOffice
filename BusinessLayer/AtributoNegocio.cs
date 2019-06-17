namespace BusinessLayer
{
    using System.Collections.Generic;
    using System.Linq;
    using DataLayer;
    using DTOLayer;
    using Utils;

    public class AtributoNegocio : IAtributoNegocio
    {
        private readonly IAtributoRepository _atributoRepository = new AtributoRepository();
        private List<Atributo> _atributoList;

        public List<Atributo> AtributoList
        {
            get { return _atributoList ?? (_atributoList = GetAll()); }
            set { _atributoList = value; }
        }
        
        public Atributo FindByCode(string code)
        {
            return code == null 
                ? null 
                : AtributoList.FirstOrDefault(x => x.Codigo.RemoveDiacritics().Trim() == code.RemoveDiacritics().Trim());
        }

        #region CRUD

        public List<Atributo> GetAll()
        {
            return _atributoRepository.GetAll();
        }

        public Atributo FindById(int id)
        {
            return _atributoRepository.FindById(id);
        }

        public Atributo Save(Atributo atributo)
        {
            return atributo?.Id == null
                ? Add(atributo)
                : Update(atributo);
        }

        private Atributo Add(Atributo atributo)
        {
            return _atributoRepository.Add(atributo);
        }

        private Atributo Update(Atributo atributo)
        {
            return _atributoRepository.Update(atributo);
        }

        #endregion CRUD
    }
}
