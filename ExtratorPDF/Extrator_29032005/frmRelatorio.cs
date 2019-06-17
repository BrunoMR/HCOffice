using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Extrator_29032005
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
            // Inicializa textos
            lblpdf.Text = "Não informado";
            lblNumRPI.Text = "Não informado";
            lnkPathExtracao.Text = "Não informado";
            lblDataRPI.Text = "Não informado";
            lblTotalImagens.Text = "0";
            lblTotalProcComApresentacao.Text = "0";
            lblTotalProcComNCL.Text = "0";

            #region Popula tela

            if (Rpi.DocPdf.Carregou)
            {
                lblpdf.Text = Rpi.DocPdf.Filename;
                lblNumRPI.Text = Rpi.DocPdf.Cabecalho.NumeroRPI;
                lblDataRPI.Text = Rpi.DocPdf.Cabecalho.Data.ToShortDateString();
            }
            else
            {
                lblpdf.Text = "Não informado";
                lblNumRPI.Text = "Não informado";
                lblDataRPI.Text = "Não informado";
            }


            lnkPathExtracao.Text = Rpi.DocPdf.PathExtracao;
            lblTotalImagens.Text = Rpi.DocPdf.TotalImagens.ToString();
            lblTotalProcComApresentacao.Text = Rpi.DocPdf.TotalProcessos.ToString();
            lblTotalProcComNCL.Text = Rpi.DocPdf.TotalProcessosNCL.ToString();

            PopulaDiferencas(Rpi.DocPdf.PaginasComDiferenca);
            PopulaProcessosRepetidos(Rpi.DocPdf.ProcessosRepetidos);

            #endregion
        }

        private void PopulaDiferencas(List<Pagina> list)
        {
            if (list.Count == 0)
            {
                dtgDiferencaPag.Visible = false;
                lblDiferençaPag.Visible = true;
            }
            else
            {
                dtgDiferencaPag.Visible = true;
                lblDiferençaPag.Visible = false;
            }

            dtgDiferencaPag.Rows.Clear();

            DataGridViewRow linha;
            DataGridViewTextBoxCell cPagina;
            DataGridViewTextBoxCell cImagem;
            DataGridViewTextBoxCell cProcesso;
            DataGridViewTextBoxCell cTotDifProc;
            DataGridViewTextBoxCell cTotDifImg;

            int _totDifProc = 0;
            int _totDifImg = 0;

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
                cTotDifProc = new DataGridViewTextBoxCell();
                cTotDifImg = new DataGridViewTextBoxCell();

                // popula células
                cPagina.Value = pagina.Numero;
                cProcesso.Value = pagina.Processos.Count;
                cImagem.Value = pagina.Imagens.Count;

                // Diferença
                int qtdeProc = pagina.Processos.Count;
                int qtdeImg = pagina.Imagens.Count;

                if (qtdeProc < qtdeImg)
                {
                    // Faltando processos
                    int _dif = qtdeImg - qtdeProc;

                    _totDifProc -= _dif;
                    _totDifImg += _dif;
                }
                else
                {
                    // Faltando imagens
                    int _dif = qtdeProc - qtdeImg;

                    _totDifImg -= _dif;
                    _totDifProc += _dif;
                }

                cTotDifProc.Value = _totDifProc;
                cTotDifImg.Value = _totDifImg;

                linha.Cells.Add(cPagina);
                linha.Cells.Add(cProcesso);
                linha.Cells.Add(cImagem);
                linha.Cells.Add(cTotDifProc);
                linha.Cells.Add(cTotDifImg);

                //adiciona linha a grid
                dtgDiferencaPag.Rows.Add(linha);
            }

            linha = null;
            cPagina = null;
            cProcesso = null;
            cImagem = null;
            cTotDifProc = null;
            cTotDifImg = null;
        }

        private void PopulaProcessosRepetidos(List<Processo> list)
        {
            if (list.Count == 0)
            {
                dtgProcRepetido.Visible = false;
                lblProcRepetido.Visible = true;
                lblTotalProcRepetido.Visible = false;
            }
            else
            {
                dtgProcRepetido.Visible = true;
                lblProcRepetido.Visible = false;
                lblTotalProcRepetido.Visible = true;
                lblTotalProcRepetido.Text = string.Format("Total: {0}", Rpi.DocPdf.ProcessosRepetidos.Count.ToString());

            }

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

        private void lnkPathExtracao_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Link para path extração
            if (Directory.Exists(lnkPathExtracao.Text))
                Process.Start(Rpi.DocPdf.PathExtracao);
        }

        private void btnPdfOld_Click(object sender, EventArgs e)
        {
            // Objetivo:
            // Verificar, na lista de páginas com diferença, todas as páginas onde a diferença seja compensada na página seguinte

            if (Rpi.DocPdf.PaginasComDiferenca.Count < 2)
            {
                MessageBox.Show("Existem menos que duas páginnas na lista. Não é o suficiente para commparação.");
                return;
            }

            // Percorre páginas com diferença
            for (int index = 1; index < Rpi.DocPdf.PaginasComDiferenca.Count; index++)
            {
                int totProcAnt = Rpi.DocPdf.PaginasComDiferenca[index - 1].Processos.Count;
                int totImgAnt = Rpi.DocPdf.PaginasComDiferenca[index - 1].Imagens.Count;

                int diferencaAnt = (totProcAnt > totImgAnt) ? totProcAnt - totImgAnt : totImgAnt - totProcAnt;

                int totProcAtual = Rpi.DocPdf.PaginasComDiferenca[index].Processos.Count;
                int totImgAtual = Rpi.DocPdf.PaginasComDiferenca[index].Imagens.Count;

                int diferencaAtual = (totProcAtual > totImgAtual) ? totProcAtual - totImgAtual : totImgAtual - totProcAtual;

                if (diferencaAnt == diferencaAtual)
                {
                    // Marca páginas
                    Rpi.DocPdf.PaginasComDiferenca[index - 1].Marcacao = true;
                    Rpi.DocPdf.PaginasComDiferenca[index].Marcacao = true;

                }
            }

            // Popula lista
            if (Rpi.DocPdf.PaginasComDiferenca.Count > 1)
            {
                PopulaDiferencas(Rpi.DocPdf.PaginasComDiferenca);
                dtgDiferencaPag.Visible = true;
                lblDiferençaPag.Visible = false;
            }

        }
    }
}
