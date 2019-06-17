using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DTOLayer;

namespace DataLayer
{
    public interface IRpiRepository
    {
        void Add(RpiImported rpi, SqlTransaction transaction);
        DynamicParameters DynamicParameters(RpiImported rpi);
        List<Rpi> GetAll();
    }
}
