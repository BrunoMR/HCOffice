using Utils;

namespace ExtratorDeDados.Repository
{
    public abstract class ARepository
    {
        protected readonly Conexao Connection = Conexao.RetornaInstancia();

        public void Dispose()
        {
            Connection.FechaConexao();
        }
    }
}
