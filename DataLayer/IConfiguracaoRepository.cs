using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DTOLayer;

namespace DataLayer
{
    public interface IConfiguracaoRepository
    {
        List<Configuracao> Find(Configuracao model);

        void BuildWhere(Configuracao model, ref StringBuilder query);

        DynamicParameters FilterParameters(Configuracao model);
    }
}
