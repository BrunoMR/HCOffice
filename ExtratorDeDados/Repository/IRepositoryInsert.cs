using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ExtratorDeDados.Repository
{
    public interface IRepositoryInsert<T> where T : new()
    {
        void Adicionar(T model);
        DynamicParameters DynamicParameters(T model);
    }
}
