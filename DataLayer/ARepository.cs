using DataLayer.Connections;

namespace DataLayer
{
    public abstract class ARepository
    {
        protected readonly ConnectionDapper ConnectionDapper = ConnectionDapper.RetornaInstancia();
        protected readonly OrmLiteConnection ConnectionOrmLite = OrmLiteConnection.GetInstance();

        public void Dispose()
        {
            ConnectionDapper.FechaConexao();
        }
    }
}
