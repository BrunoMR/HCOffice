using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Extrator
{
    public class DocumentoPDF
    {
        #region Váriáveis privadas

        private string _path;
        private string _filename;
        private int _paginaPrimeiroProcesso = 0;
        private Cabecalho _cabecalho;
        private string _pathExtracao;
        private PdfReader _pdfReader = null;
        private List<Pagina> _paginas = new List<Pagina>();
        private List<Processo> _processos = new List<Processo>();
        private List<Processo> _processosNCL = new List<Processo>();
        private List<Processo> _processosRepetidos = new List<Processo>();
        private List<Pagina> _paginasComDiferenca = new List<Pagina>();
        private List<Imagem> _imagens = new List<Imagem>();
        private List<Imagem> _imagensInvalidas = new List<Imagem>();
        private string[] _processosIgnorados;
        private string[] _imagensIgnoradas;
        private int _totalProcessos = 0;
        private int _totalProcessosNCL = 0;
        private int _totalImagens = 0;
        private bool _carregou = false;
        private bool _extraiuImagens = false;
        private bool _extraiuProcessos = false;
        private bool _extraiuProcessosNCL = false;

        #endregion

        #region Propriedades

        public string Path
        {
            get
            {
                return this._path;
            }
        }

        public string Filename
        {
            get
            {
                return this._filename;
            }
        }

        public int PaginaPrimeiroProcesso
        {
            get
            {
                return this._paginaPrimeiroProcesso;
            }
        }

        public Cabecalho Cabecalho
        {
            get
            {
                return this._cabecalho;
            }
        }

        public string PathExtracao
        {
            get
            {
                return this._pathExtracao;
            }
        }

        public PdfReader PdfReader
        {
            get
            {
                return this._pdfReader;
            }
        }

        public List<Pagina> Paginas
        {
            get
            {
                return this._paginas;
            }
        }

        /// <summary>
        /// Lista contendo todos os processos com apresentação válida do documento
        /// </summary>
        public List<Processo> Processos
        {
            get
            {
                if (this._extraiuProcessos && this._processos.Count == 0)
                {
                    this._processos = this.GetProcessos();
                }

                return this._processos;
            }
        }

        /// <summary>
        /// Lista contendo todos os processos com NCL válida do documento
        /// </summary>
        public List<Processo> ProcessosNCL
        {
            get
            {
                if (this._extraiuProcessosNCL && this._processosNCL.Count == 0)
                {
                    this._processosNCL = this.GetProcessosNCL();
                }

                return this._processosNCL;
            }
        }

        public List<Processo> ProcessosRepetidos
        {
            get
            {
                if (this._extraiuProcessos && this._processosRepetidos.Count == 0)
                {
                    this._processosRepetidos = this.GetProcessosRepetidos();
                }

                return this._processosRepetidos;
            }
        }

        public List<Pagina> PaginasComDiferenca
        {
            get
            {
                if (this._extraiuProcessos && this._extraiuImagens && this._paginasComDiferenca.Count == 0)
                    this._paginasComDiferenca = this.GetPaginasComDiferenca();

                return this._paginasComDiferenca;
            }
        }

        /// <summary>
        /// Lista contendo nome de todas as imagens documento
        /// </summary>
        public List<Imagem> Imagens
        {
            get
            {
                if (this._extraiuImagens && this._imagens.Count == 0)
                {
                    this._imagens = this.GetImagens();
                }

                return this._imagens;
            }
        }

        public List<Imagem> ImagensInvalidas
        {
            get
            {
                if (this._extraiuImagens && this._imagensInvalidas.Count == 0)
                {
                    this._imagensInvalidas = this.GetImagensInvalidas();
                }

                return this._imagensInvalidas;
            }
        }

        public string[] ProcessosIgnorados
        {
            get
            {
                return this._processosIgnorados;
            }
        }

        public string[] ImagensIgnoradas
        {
            get
            {
                return this._imagensIgnoradas;
            }
        }

        public int TotalProcessos
        {
            get
            {
                if (this._extraiuProcessos && this._totalProcessos == 0)
                {
                    this._totalProcessos = this.GetTotalProcessos();
                }

                return this._totalProcessos;
            }
        }

        public int TotalProcessosNCL
        {
            get
            {
                if (this._extraiuProcessosNCL && this._totalProcessosNCL == 0)
                {
                    this._totalProcessosNCL = this.GetTotalProcessosNCL();
                }

                return this._totalProcessosNCL;
            }
        }

        public int TotalImagens
        {
            get
            {
                if (this._extraiuImagens && this._totalImagens == 0)
                {
                    this._totalImagens = this.GetTotalimagens();
                }

                return this._totalImagens;
            }
        }

        public bool Carregou
        {
            get
            {
                return this._carregou;
            }
        }

        public bool ExtraiuImagens
        {
            get
            {
                return this._extraiuImagens;
            }
        }

        public bool ExtraiuProcessos
        {
            get
            {
                return this._extraiuProcessos;
            }
        }

        public bool ExtraiuProcessosNCL
        {
            get
            {
                return this._extraiuProcessosNCL;
            }
        }

        #endregion

        #region Métodos Públicos

        public void Carregar(string pathPDF, int paginaInicial, string pathRepositorio, string[] processosIgnorados, string[] imagensIgnoradas)
        {
            this._pdfReader = new PdfReader(pathPDF);

            this._path = pathPDF;
            this._filename = System.IO.Path.GetFileName(this._path);

            if (paginaInicial == 0)
                this._paginaPrimeiroProcesso = this.GetPaginaPrimeiroProcesso();
            else
                this._paginaPrimeiroProcesso = paginaInicial;

            this._cabecalho = GetCabecalhoPagina();

            this._pathExtracao =
                System.IO.Path.Combine(pathRepositorio, this._cabecalho.NumeroRPI);

            this._processosIgnorados = processosIgnorados;
            this._imagensIgnoradas = imagensIgnoradas;

            for (int pageNumber = 1; pageNumber <= this._pdfReader.NumberOfPages; pageNumber++)
            {
                this._paginas.Add(new Pagina(pageNumber));
            }

            // Seta flag de status
            this._carregou = true;
        }

        public void ExtrairProcessos()
        {
            #region Validação

            if (this._extraiuProcessos)
                throw new Exception("A extração de processos já foi realizada.");

            if (!this._carregou)
                throw new Exception("Nenhum arquivo PDf foi carregado.");

            #endregion

            string PageText;
            Processo processo = null;

            for (int pageNumber = this.PaginaPrimeiroProcesso; pageNumber <= this.PdfReader.NumberOfPages; pageNumber++)
            {
                // Extrai texto da página
                PageText = ExtrairTextoPagina(pageNumber);

                // Separa texto em linhas
                string[] split = PageText.Split(new Char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // Percorre linhas 
                for (int i = 0; i <= split.GetUpperBound(0); i++)
                {
                    string TextoRetorno;
                    string _LINHA = split[i].ToString();

                    if (Padrao.ContemProcesso(_LINHA, out TextoRetorno))
                    {
                        // Cria processo
                        processo = new Processo();
                        processo.Pagina = pageNumber;
                        processo.Numero = TextoRetorno;

                        // Recupera página
                        Pagina pagina = this._paginas.Where(p => p.Numero == processo.Pagina).SingleOrDefault();

                        // Adiciona processo na página
                        pagina.Processos.Add(processo);
                        processo = null;
                    }
                }
            }

            // Status
            this._extraiuProcessos = true;
        }

        public void ExtrairImagens()
        {
            #region Validação

            if (this._extraiuImagens)
                throw new Exception("A extração de imagens já foi realizada.");

            if (!this._carregou)
                throw new Exception("Nenhum arquivo PDf foi carregado.");

            #endregion

            string _pathImagens = System.IO.Path.Combine(this._pathExtracao, "Imagens");

            #region Criar diretório para extração da RPI

            if (Directory.Exists(_pathImagens))
                Directory.Delete(_pathImagens, true);

            // Aguarda 2 segundos para evitar erro
            Thread.Sleep(1000);

            Directory.CreateDirectory(_pathImagens);

            #endregion

            int posicaoPagina = 0;
            ImageRenderListener listener = null;
            Process procObj;
            Imagem imagem;
            int pageNumber = 1;

            PdfReaderContentParser parser =
                new PdfReaderContentParser(this.PdfReader);

            try
            {
                for (pageNumber = this.PaginaPrimeiroProcesso; pageNumber <= this.PdfReader.NumberOfPages; pageNumber++)
                {
                    parser.ProcessContent(pageNumber, (listener = new ImageRenderListener()));
                    posicaoPagina = 0;

                    if (listener.Images.Count > 0)
                    {
                        foreach (var pair in listener.Images)
                        {
                            posicaoPagina++;

                            // Cria objeto de negócio da imagem
                            imagem = new Imagem
                            {
                                Pagina = pageNumber,
                                Posicao = posicaoPagina,
                                IsInvalid = (pair.Value == ".inv")
                            };

                            // Imagens que devem ser ignoradas
                            if (this.ImagensIgnoradas != null &&
                                this.ImagensIgnoradas.Contains(imagem.NomeGerado))
                            {
                                continue;
                            }

                            // Gera imagem
                            pair.Key.Save
                                (
                                    System.IO.Path.Combine(_pathImagens, imagem.NomeGerado),
                                    ImageFormat.Png
                                );

                            // Recupera página
                            Pagina pagina = this._paginas.Where(p => p.Numero == imagem.Pagina).SingleOrDefault();

                            // Adiciona imagem na página
                            pagina.Imagens.Add(imagem);

                            // Destrói imagem
                            imagem = null;
                        }

                        #region Controla memória do processo

                        // Recupera infformações do processo 
                        procObj = Process.GetCurrentProcess();

                        // verifica o uso de memória ecaso exceda valor fixo, coleta e espera finalização dos objetos
                        if (procObj.PrivateMemorySize64 > 333434880)
                        {
                            parser = null;
                            listener.Dispose();

                            GC.Collect();
                            GC.WaitForPendingFinalizers();

                            parser = new PdfReaderContentParser(this.PdfReader);
                        }

                        #endregion
                    }
                }

                parser = null;
                listener.Dispose();

                GC.Collect();
                GC.WaitForPendingFinalizers();

                this._extraiuImagens = true;

            }
            catch (Exception ex)
            {
                throw new Exception
                    (
                        string.Format
                            (
                                "{0}{1}O erro ocorreu na página: {2}.",
                                ex.Message,
                                Environment.NewLine,
                                pageNumber.ToString()
                            )
                    );
            }
        }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Retorna lista com todos os processos do documento
        /// </summary>
        /// <returns></returns>
        private List<Processo> GetProcessos()
        {
            List<Processo> listRetorno = new List<Processo>();

            // Percorre páginas
            foreach (Pagina pag in this._paginas)
            {
                // Extrai processos da página
                foreach (Processo proc in pag.Processos)
                    listRetorno.Add(proc);
            }

            return listRetorno;
        }

        /// <summary>
        /// Retorna lista de todos os processos com NCL do documento
        /// </summary>
        /// <returns></returns>
        private List<Processo> GetProcessosNCL()
        {
            List<Processo> listRetorno = new List<Processo>();

            // Percorre páginas
            foreach (Pagina pag in this._paginas)
            {
                // Extrai processos da página
                foreach (Processo proc in pag.ProcessosNCL)
                    listRetorno.Add(proc);
            }

            return listRetorno;
        }

        /// <summary>
        /// Retorna lista com nome de todas as imagens do documento
        /// </summary>
        /// <returns></returns>
        private List<Imagem> GetImagens()
        {
            List<Imagem> listRetorno = new List<Imagem>();

            // Percorre páginas
            foreach (Pagina pag in this._paginas)
            {
                // Extrai processos da página
                foreach (Imagem img in pag.Imagens)
                    listRetorno.Add(img);
            }

            return listRetorno;
        }

        private int GetTotalProcessos()
        {
            // Retorna count da prorpiedade
            return this.Processos.Count;
        }

        private int GetTotalProcessosNCL()
        {
            // Retorna count da prorpiedade
            return this.ProcessosNCL.Count;
        }

        private int GetTotalimagens()
        {
            int total = 0;
            total = _paginas.Sum(p => p.Imagens.Count());

            return total;
        }

        /// <summary>
        /// Lista todas as páginas onde houver diferença entr total de imagens e total de processos com apresentação
        /// </summary>
        private List<Pagina> GetPaginasComDiferenca()
        {
            List<Pagina> listRetorno = new List<Pagina>();

            foreach (Pagina pagina in this._paginas)
            {
                if (pagina.Imagens.Count() != pagina.Processos.Count())
                    listRetorno.Add(pagina);
            }

            return listRetorno;
        }

        /// <summary>
        /// Retorna list de processos duplicados
        /// </summary>
        /// <returns></returns>
        private List<Processo> GetProcessosRepetidos()
        {
            List<Processo> listRetorno = new List<Processo>();

            // Agrupa processos com números repetidos
            IEnumerable<IGrouping<string, Processo>> grupo =
                this.Processos.GroupBy(x => x.Numero)
                               .Where(x => x.Count() > 1);

            // Percorre grupos 
            foreach (var groupProc in grupo)
            {
                // Percorre processos do grupo ignorando o primeiro elemento(Processo original)
                foreach (Processo proc in groupProc.Skip(1))
                    listRetorno.Add(proc);
            }

            return listRetorno;
        }

        /// <summary>
        /// Retorna lista com todas as imagens geradas automaticamente para substituir imagens inválidas
        /// </summary>
        /// <returns></returns>
        private List<Imagem> GetImagensInvalidas()
        {
            List<Imagem> listRetorno = new List<Imagem>();

            // Percorre páginas
            foreach (Pagina pag in this._paginas)
            {
                // Percorre imagens da página
                foreach (Imagem img in pag.Imagens)
                {
                    if (img.IsInvalid)
                        listRetorno.Add(img);
                }
            }

            return listRetorno;
        }

        private string ExtrairTextoPagina(int PageNumber)
        {
            string PageText;

            ITextExtractionStrategy itextextStrat =
                new SimpleTextExtractionStrategy();

            PageText =
                PdfTextExtractor.GetTextFromPage(this.PdfReader, PageNumber, itextextStrat);

            PageText =
                Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(PageText)));

            return PageText;
        }

        /// <summary>
        /// Retorna Data, número e pagina de início de extração da RPI
        /// </summary>
        /// <returns></returns>
        private int GetPaginaPrimeiroProcesso()
        {
            string PageText;
            int pagina = 1;

            for (int pageNumber = 1; pageNumber <= this.PdfReader.NumberOfPages; pageNumber++)
            {
                PageText = ExtrairTextoPagina(pageNumber);

                if (Padrao.ContemApresentacao(PageText))
                {
                    pagina = pageNumber;
                    break;
                }
            }

            return pagina;
        }

        private Cabecalho GetCabecalhoPagina()
        {
            string PageText;
            string data = string.Empty;
            string numeroRPI = string.Empty;
            Cabecalho cabecalho = new Cabecalho();

            for (int pageNumber = this._paginaPrimeiroProcesso; pageNumber <= this.PdfReader.NumberOfPages; pageNumber++)
            {
                PageText = ExtrairTextoPagina(pageNumber);

                // Extrai linhas                  
                string[] split = PageText.Split(new Char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                // Lê apenas a primeira linha
                string _LINHA = split[0].ToString();

                // Verifica padrões
                if (Padrao.ContemData(_LINHA, out data) && Padrao.ContemNumeroRPI(_LINHA, out numeroRPI))
                    break;
            }

            if (string.IsNullOrEmpty(data))
                cabecalho.Data = DateTime.Now;
            else
                cabecalho.Data = DateTime.Parse(data);

            if (string.IsNullOrEmpty(numeroRPI))
                cabecalho.NumeroRPI = this._filename;
            else
                cabecalho.NumeroRPI = numeroRPI;

            return cabecalho;
        }

        #endregion
    }
}
