using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extrator
{
    public class Pagina
    {
        public int Numero { get; set; }
        public List<Processo> Processos { get; set; }
        public List<Processo> ProcessosNCL { get; set; }
        public List<Imagem> Imagens { get; set; }
        public bool Marcacao { get; set; }

        public Pagina(int numero)
        {
            this.Numero = numero;
            this.Processos = new List<Processo>();
            this.ProcessosNCL = new List<Processo>();
            this.Imagens = new List<Imagem>();
        }
    }
}
