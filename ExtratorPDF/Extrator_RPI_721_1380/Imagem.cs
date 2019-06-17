
using System.Drawing.Imaging;
namespace Extrator
{
    public class Imagem
    {
        public int Pagina { get; set; }
        public int Posicao { get; set; }
        public bool IsInvalid { get; set; }

        private string _nomeGerado; 
        public string NomeGerado
        {
            get
            {
                _nomeGerado = string.Format
                (
                    "{0}_{1}.png", this.Pagina.ToString("D8"), this.Posicao.ToString("D2")
                );

                return this._nomeGerado;
            }
        }
    }
}
