using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Extrator
{
    public class RPI
    {
        private DocumentoPDF _docPdf;
        private string _pathRepositorio;
        private bool _renomeouImagensExtraidas;
        private bool _gerouArquivoNCL;

        #region Propriedades

        public DocumentoPDF DocPdf
        {
            get
            {
                return this._docPdf;
            }
        }

        public string PathRepositorio
        {
            get
            {
                return this._pathRepositorio;
            }
        }

        public bool RenomeouImagensExtraidas
        {
            get
            {
                return this._renomeouImagensExtraidas;
            }
        }

        public bool GerouArquivoNCL
        {
            get
            {
                return this._gerouArquivoNCL;
            }
        }

        #endregion

        public RPI()
        {
            this._docPdf = new DocumentoPDF();

            // Define repositório fixo
            this._pathRepositorio =
                 Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.Create);
        }

        public void RenomearImagensExtraidas()
        {
            #region Validação

            if (this._renomeouImagensExtraidas)
                throw new Exception("As imagens extraídas já foram renomeadas. Não é possível executar o processo novamente.");

            if (!DocPdf.ExtraiuImagens)
                throw new Exception("A extração de imagens não foi realizada.");

            if (!DocPdf.ExtraiuProcessos)
                throw new Exception("A extração de processos não foi realizada.");

            if (DocPdf.PaginasComDiferenca.Count > 0)
                throw new Exception("Existe diferença entre o total de imagens e processos extraídos.");

            if (DocPdf.ProcessosRepetidos.Count > 0)
                throw new Exception("Foram encontrados processos repetidos.");

            string _pathImagens = Path.Combine(DocPdf.PathExtracao, "Imagens");
            DirectoryInfo di = new DirectoryInfo(_pathImagens);

            if (!di.Exists)
                throw new Exception("O diretório onde as imagens extraídas do arquivo PDF foram geradas não foi encontrado.");

            #endregion

            // Percorre páginas
            foreach (Pagina pagina in DocPdf.Paginas)
            {
                // Percorre imagens da página
                for (int index = 0; index < pagina.Imagens.Count; index++)
                {
                    string _oldName =
                        Path.Combine(_pathImagens, pagina.Imagens[index].NomeGerado);

                    string _newName =
                        Path.Combine(_pathImagens, string.Format("{0}.png", pagina.Processos[index].Numero));

                    // Se o arquivo não for encontrado, aborta o processo
                    if (!File.Exists(_oldName))
                        throw new Exception(string.Format("O arquivo '{0}' não foi encontrado.", _oldName));

                    File.Move(_oldName, _newName);
                }
            }

            // Status
            this._renomeouImagensExtraidas = true;
            this.GerarRelatorioRPI();
        }

        public void GerarArquivoNCl()
        {
            #region Validação

            if (!DocPdf.ExtraiuProcessosNCL)
                throw new Exception("A extração de processos com NCL não foi realizada.");

            if (DocPdf.TotalProcessosNCL == 0)
                throw new Exception("Não foi encontrado nenhum processo com NCL válida.");

            #endregion

            string _pathNCL = Path.Combine(DocPdf.PathExtracao, "NCL");

            #region Criar diretório para geração do arquivo

            if (Directory.Exists(_pathNCL))
                Directory.Delete(_pathNCL, true);

            Directory.CreateDirectory(_pathNCL);

            #endregion

            // Formato de nome: RPIM-AAAA-MM-DD-nº RPI.txt
            string nomeArquivo =
                string.Format("RPIM-{0}-{1}.txt", DocPdf.Cabecalho.Data.FormatRPIDate(), DocPdf.Cabecalho.NumeroRPI);

            using (Stream stream = File.Open(Path.Combine(_pathNCL, nomeArquivo), FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    // Percorre processos com NCL
                    foreach (Processo proc in DocPdf.ProcessosNCL)
                    {
                        int ncl = int.Parse(proc.NCL);
                        string linha = string.Format("{0};{1}", proc.Numero, ncl.ToString("D2"));
                        writer.WriteLine(linha);
                    }
                }
            }

            // Status
            this._gerouArquivoNCL = true;
        }

        /// <summary>
        /// Cria bitmap da imagem em memória, rotaciona e salva novamente em disco                    
        /// </summary>
        public void RotacionarImagens(string[] fileNames, RotateFlipType rotateFlipType)
        {
            Bitmap bmp = null;

            try
            {
                foreach (string file in fileNames)
                {
                    bmp = new Bitmap(file);
                    bmp.RotateFlip(rotateFlipType);
                    bmp.Save(file, ImageFormat.Png);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bmp.Dispose();
            }

        }

        private void GerarRelatorioRPI()
        {
            StreamWriter writer;
            string filePath;

            filePath = Path.Combine
                (
                    DocPdf.PathExtracao,
                    string.Format("Relatorio-RPI-{0}.txt", DocPdf.Cabecalho.NumeroRPI)
                );

            if (File.Exists(filePath))
                File.Delete(filePath);

            writer = File.CreateText(filePath);

            #region Excreve no arquivo

            writer.WriteLine("Total de processos: " + this.DocPdf.TotalProcessos);
            writer.WriteLine("Total de imagens: " + this.DocPdf.TotalImagens);

            if (this.DocPdf.ProcessosRepetidos.Count > 0)
            {
                writer.WriteLine("Total de processos repetidos: " + this.DocPdf.ProcessosRepetidos.Count);
                writer.WriteLine();
                writer.WriteLine("Processos repetidos:");

                string procsRepetidos = string.Empty;
                foreach (Processo proc in this.DocPdf.ProcessosRepetidos)
                {
                    if (procsRepetidos.Length > 0)
                        procsRepetidos += "|";

                    procsRepetidos += proc.Numero;
                }

                writer.WriteLine(procsRepetidos);
            }

            if (this.DocPdf.ProcessosIgnorados.Count() > 0)
            {
                string _procs = "";

                for (int i = 0; i < this.DocPdf.ProcessosIgnorados.Length; i++)
                {
                    if (i > 0)
                        _procs += "|";

                    _procs += this.DocPdf.ProcessosIgnorados[i];
                }

                writer.WriteLine();
                writer.WriteLine("Processos ignorados:");
                writer.WriteLine(_procs);
            }

            if (this.DocPdf.ImagensIgnoradas.Count() > 0)
            {
                string _imagens = "";

                for (int i = 0; i < this.DocPdf.ImagensIgnoradas.Length; i++)
                {
                    if (i > 0)
                        _imagens += "|";

                    _imagens += this.DocPdf.ImagensIgnoradas[i];
                }

                writer.WriteLine();
                writer.WriteLine("Imagens ignoradas:");
                writer.WriteLine(_imagens);
            }


            #endregion

            writer.Close();
            writer.Dispose();
        }
    }
}
