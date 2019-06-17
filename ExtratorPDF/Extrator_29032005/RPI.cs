using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Extrator_29032005
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

            //// Define repositório fixo
            //this._pathRepositorio =
            //     Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.Create);


            // Define repositório fixo (RPI's ANTIGAS)
            this._pathRepositorio = Path.Combine
                (
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.Create),
                    "_RPI's antigas",
                    "02 Diretório de extração"
                );
        }

        public void FinalizarExtracao_OLD(string processosIgnorados, string imagensIgnoradas)
        {
            string _pathImagens = Path.Combine(DocPdf.PathExtracao, "Imagens");
            string _pathJpeg = System.IO.Path.Combine(DocPdf.PathExtracao, "Imagens Jpeg");
            string _pathRepetidos = System.IO.Path.Combine(DocPdf.PathExtracao, "Imagens de processos repetidos");

            #region Validação

            if (this._renomeouImagensExtraidas)
                throw new Exception("As imagens extraídas já foram renomeadas. Não é possível executar o processo novamente.");

            if (!DocPdf.ExtraiuImagens)
                throw new Exception("A extração de imagens não foi realizada.");

            if (!DocPdf.ExtraiuProcessos)
                throw new Exception("A extração de processos não foi realizada.");

            if (DocPdf.TotalImagens != DocPdf.TotalProcessos)
                throw new Exception("Existe diferença entre o total de imagens e processos extraídos.");

            DirectoryInfo di = new DirectoryInfo(_pathImagens);

            if (!di.Exists)
                throw new Exception("O diretório onde as imagens extraídas do arquivo PDF foram geradas não foi encontrado.");

            #endregion

            List<Processo> procsRepetidos = new List<Processo>();

            // Percorre imagens
            for (int i = 0; i < DocPdf.Imagens.Count; i++)
            {
                Imagem imgAtual = DocPdf.Imagens[i];
                Processo procAtual = DocPdf.Processos[i];

                string _oldName = imgAtual.Nome;

                if (!File.Exists(Path.Combine(_pathImagens, _oldName)))
                    throw new Exception(string.Format("O arquivo '{0}' não foi encontrado.", _oldName));

                string _newName = string.Format("{0}.png", procAtual.Numero);

                // Verifica se arquivo já existe
                // Motivo: Processos repetidos
                if (!File.Exists(Path.Combine(_pathImagens, _newName)))
                {
                    // Renomear arquivos com número de processo
                    File.Move
                    (
                        Path.Combine(_pathImagens, _oldName),
                        Path.Combine(_pathImagens, _newName)
                    );

                    #region Copiar imagens Jpeg para outro diretório

                    if (imgAtual.isJpeg)
                    {
                        if (!Directory.Exists(_pathJpeg))
                            Directory.CreateDirectory(_pathJpeg);

                        File.Copy
                            (
                                Path.Combine(_pathImagens, _newName),
                                Path.Combine(_pathJpeg, _newName)
                            );
                    }

                    #endregion
                }
                else
                {
                    #region Renomear arquivos repetidos e salvar em outro diretório

                    // Verifica quantas vezes o número de processo já se repetiu;
                    int qtdeRepeticoes = procsRepetidos.Count(p => p.Numero == procAtual.Numero) + 1;
                    _newName = string.Format("Repetido_{0}_{1}.png", procAtual.Numero, qtdeRepeticoes);

                    //Adiciona proc corrente na lista de repetidos
                    procsRepetidos.Add(procAtual);

                    if (!Directory.Exists(_pathRepetidos))
                        Directory.CreateDirectory(_pathRepetidos);

                    File.Move
                        (
                            Path.Combine(_pathImagens, _oldName),
                            Path.Combine(_pathRepetidos, _newName)
                        );

                    #endregion
                }
            }

            // Status
            this._renomeouImagensExtraidas = true;
            this.GerarRelatorioRPI(processosIgnorados, imagensIgnoradas);
        }

        public void Extrair_OLD(string filepath)
        {

            #region  Libera memória com o Garbage Collector

            // Verifica se já existe arquivo em memória
            if (_docPdf.Carregou)
            {
                this._renomeouImagensExtraidas = false;
                _docPdf = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                this._docPdf = new DocumentoPDF();
            }

            #endregion

            try
            {
                this._docPdf.Carregar_OLD(filepath, 0, _pathRepositorio);
                this._docPdf.ExtrairProcessos_OLD(string.Empty);
                this._docPdf.ExtrairImagens_OLD(string.Empty);
                this.FinalizarExtracao_OLD(string.Empty, string.Empty);
            }
            catch (Exception ex)
            {
                this.GravarLog(ex);
            }
            finally
            {              
                this.GerarRelatorioRPI();
                this.AtualizarRelatorioGeral();
            }
        }

        private void GerarRelatorioRPI(string procIgnorado = "", string imgIgnorada = "")
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

            if (!string.IsNullOrEmpty(procIgnorado))
            {
                writer.WriteLine();
                writer.WriteLine("Processos ignorados:");
                writer.WriteLine(procIgnorado);
            }

            if (!string.IsNullOrEmpty(imgIgnorada))
            {
                writer.WriteLine();
                writer.WriteLine("Imagens ignoradas:");
                writer.WriteLine(imgIgnorada);
            }


            #endregion

            writer.Close();
            writer.Dispose();
        }       

        private void AtualizarRelatorioGeral()
        {
            StreamWriter writer;
            string filePath;

            filePath = Path.Combine(this._pathRepositorio, "Relatório geral.txt");

            if (File.Exists(filePath))
                writer = File.AppendText(filePath);
            else
                writer = File.CreateText(filePath);


            // Excreve no arquivo
            if (this._renomeouImagensExtraidas)
            {
                writer.WriteLine
                    (
                        string.Format("RPI {0} - Sim", this._docPdf.Cabecalho.NumeroRPI)
                    );
            }
            else
            {
                writer.WriteLine
                    (
                        string.Format("RPI {0} - Não", this._docPdf.Cabecalho.NumeroRPI)
                    );
            }

            writer.Close();
            writer.Dispose();
        }

        private void GravarLog(Exception exception)
        {
            StreamWriter writer;
            string filePath;

            filePath = Path.Combine(this._pathRepositorio, "log.txt");

            if (File.Exists(filePath))
                writer = File.AppendText(filePath);
            else
                writer = File.CreateText(filePath);


            // Excreve no arquivo
            writer.WriteLine("RPI: " + this._docPdf.Cabecalho.NumeroRPI);
            writer.WriteLine("Message: " + exception.Message);
            writer.WriteLine("target: " + exception.TargetSite);
            writer.WriteLine();
            writer.WriteLine();

            writer.Close();
            writer.Dispose();
        }
    }
}
