namespace DTOLayer
{
    public class Configuracao
    {
        private int _id;
        private string _descricao;
        private string _valor;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public string Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        
    }
}
