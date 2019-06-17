using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Extrator
{
    public partial class frmRelatorio : Form
    {
        private RPI Rpi;

        public frmRelatorio(RPI rpi)
        {
            InitializeComponent();

            Rpi = rpi;
        }

        private void frmRelatorio_Load(object sender, EventArgs e)
        {
            #region Inicializa texto dos controles

            lblRPI.Text = "Nenhum arquivo carregado.";
            lblProcessos.Text = "Extração de processos não realizada.";
            lblProcessosNCL.Text = "Geração de arquivo NCL não realizada.";
            lblImagens.Text = "Extração de imagens não realizada.";
            lnkPathExtracao.Text = "Não informado.";
            lblPagDiferença.Text = "Não há diferenças para exibir.";
            lblProcessosRepetidos.Text = "Não há processos para exibir.";
            lnkPathExtracao.Text = "Indefinido";

            #endregion

            #region Informações do arquivo PDF/RPI

            if (Rpi.DocPdf.Carregou)
            {
                lblRPI.Text = string.Format
                    (
                        "Nº {0} em {1} | {2} | {3} páginas | {4} | Pág. Início {5}",
                        Rpi.DocPdf.Cabecalho.NumeroRPI,
                        Rpi.DocPdf.Cabecalho.Data.ToShortDateString(),
                        Rpi.DocPdf.Filename,
                        Rpi.DocPdf.PdfReader.NumberOfPages,
                        Util.FileSizeFormat(Rpi.DocPdf.PdfReader.FileLength),
                        Rpi.DocPdf.PaginaPrimeiroProcesso
                    );

                lnkPathExtracao.Text = Rpi.DocPdf.PathExtracao;
            }

            #endregion

            #region informações de imagens, processos e NCL extraídos

            if (Rpi.DocPdf.ExtraiuProcessos)
            {
                lblProcessos.Text =
                    string.Format("Foram extraídos {0} processos", Rpi.DocPdf.TotalProcessos);
            }

            if (Rpi.DocPdf.ExtraiuProcessosNCL)
            {
                lblProcessosNCL.Text =
                    string.Format("Foram extraídos {0} processos", Rpi.DocPdf.TotalProcessosNCL);
            }

            if (Rpi.DocPdf.ExtraiuImagens)
            {
                lblImagens.Text =
                    string.Format("Foram extraídas {0} imagens", Rpi.DocPdf.TotalImagens);
            }

            #endregion

            #region Processos e imagens ignoradas

            if (Rpi.DocPdf.ProcessosIgnorados != null &&
                Rpi.DocPdf.ProcessosIgnorados.Count() > 0)
            {
                string _procs = "";

                for (int i = 0; i < Rpi.DocPdf.ProcessosIgnorados.Length; i++)
                {
                    if (i > 0)
                        _procs += "|";

                    _procs += Rpi.DocPdf.ProcessosIgnorados[i];
                }

                txtProcsIgnorados.Text = _procs;
            }


            if (Rpi.DocPdf.ImagensIgnoradas != null &&
                Rpi.DocPdf.ImagensIgnoradas.Count() > 0)
            {
                string _imagens = "";

                for (int i = 0; i < Rpi.DocPdf.ImagensIgnoradas.Length; i++)
                {
                    if (i > 0)
                        _imagens += "|";

                    _imagens += Rpi.DocPdf.ImagensIgnoradas[i];
                }

                txtImagensIgnoradas.Text = _imagens;
            }

            #endregion

            #region Páginas com diferença

            int _totalPagDif = Rpi.DocPdf.PaginasComDiferenca.Count;

            if (_totalPagDif > 0)
            {
                lblPagDiferença.Text =
                    string.Format("Foram encontradas {0} páginas com diferença.", _totalPagDif);

                PopulaDiferencas(Rpi.DocPdf.PaginasComDiferenca);
            }

            #endregion

            #region Processos repetidos

            int _totalProcRepetido = Rpi.DocPdf.ProcessosRepetidos.Count;

            if (_totalProcRepetido > 0)
            {
                lblProcessosRepetidos.Text =
                    string.Format("Foram encontrados {0} processos repetidos.", _totalProcRepetido);

                PopulaProcessosRepetidos(Rpi.DocPdf.ProcessosRepetidos);
            }

            #endregion

            #region Imagens inválidas

            int _totalimgInvalidas = Rpi.DocPdf.ImagensInvalidas.Count;

            if (_totalimgInvalidas > 0)
            {
                lblImgInvalida.Text =
                    string.Format("Foram encontrados {0} imagens inválidas.", _totalimgInvalidas);

                PopulaImagensInvalidas(Rpi.DocPdf.ImagensInvalidas);
            }

            #endregion
        }

        private void lnkPathExtracao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Link para path extração
            if (Directory.Exists(lnkPathExtracao.Text))
                Process.Start(Rpi.DocPdf.PathExtracao);
        }

        private void PopulaDiferencas(List<Pagina> list)
        {
            dtgDiferencaPag.Rows.Clear();

            DataGridViewRow linha;
            DataGridViewTextBoxCell cPagina;
            DataGridViewTextBoxCell cProcesso;
            DataGridViewTextBoxCell cImagem;

            foreach (Pagina pagina in list)
            {
                // cria linha e define estilo
                linha = new DataGridViewRow();
                linha.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                linha.Height = 28;

                if (pagina.Marcacao)
                {
                    linha.DefaultCellStyle.BackColor = Color.Black;
                    linha.DefaultCellStyle.ForeColor = Color.White;
                }

                // cria células
                cPagina = new DataGridViewTextBoxCell();
                cProcesso = new DataGridViewTextBoxCell();
                cImagem = new DataGridViewTextBoxCell();

                // popula células
                cPagina.Value = pagina.Numero;
                cProcesso.Value = pagina.Processos.Count;
                cImagem.Value = pagina.Imagens.Count;

                linha.Cells.Add(cPagina);
                linha.Cells.Add(cProcesso);
                linha.Cells.Add(cImagem);

                //adiciona linha a grid
                dtgDiferencaPag.Rows.Add(linha);
            }

            linha = null;
            cPagina = null;
            cProcesso = null;
            cImagem = null;
        }

        private void PopulaProcessosRepetidos(List<Processo> list)
        {
            dtgProcRepetido.Rows.Clear();

            DataGridViewRow linha;
            DataGridViewTextBoxCell cPagina;
            DataGridViewTextBoxCell cProcesso;

            foreach (Processo processo in list)
            {
                // cria linha e define estilo
                linha = new DataGridViewRow();
                linha.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                linha.Height = 28;

                // cria células
                cPagina = new DataGridViewTextBoxCell();
                cProcesso = new DataGridViewTextBoxCell();

                // popula células
                cPagina.Value = processo.Pagina;
                cProcesso.Value = processo.Numero;

                linha.Cells.Add(cPagina);
                linha.Cells.Add(cProcesso);

                //adiciona linha a grid
                dtgProcRepetido.Rows.Add(linha);
            }

            linha = null;
            cPagina = null;
            cProcesso = null;
        }

        private void PopulaImagensInvalidas(List<Imagem> list)
        {
            dtgImgInvalida.Rows.Clear();

            DataGridViewRow linha;
            DataGridViewTextBoxCell cPagina;
            DataGridViewTextBoxCell cPosicao;
            DataGridViewTextBoxCell cNome;

            foreach (Imagem img in list)
            {
                // cria linha e define estilo
                linha = new DataGridViewRow();
                linha.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                linha.Height = 28;

                // cria células
                cPagina = new DataGridViewTextBoxCell();
                cPosicao = new DataGridViewTextBoxCell();
                cNome = new DataGridViewTextBoxCell();

                // popula células
                cPagina.Value = img.Pagina;
                cPosicao.Value = img.Posicao;
                cNome.Value = img.NomeGerado;

                linha.Cells.Add(cPagina);
                linha.Cells.Add(cPosicao);
                linha.Cells.Add(cNome);

                //adiciona linha a grid
                dtgImgInvalida.Rows.Add(linha);
            }

            linha = null;
            cPagina = null;
            cPosicao = null;
        }

    }
}
