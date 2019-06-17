using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ExtratorDeDados.Repository
{
    public abstract class ARepositorySelect<T> : ARepository
    {
        public abstract List<T> Buscar(T model);
        protected abstract DynamicParameters FilterParameters(T model);
        protected abstract void BuildWhere(T model, ref StringBuilder query);
    }
    
}
