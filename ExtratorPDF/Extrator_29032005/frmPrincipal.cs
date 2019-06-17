using System;
using System.Windows.Forms;
using System.Linq;
using System.IO;

namespace Extrator_29032005
{
    public partial class frmPrincipal : Form
    {
        private RPI Rpi;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmOpen_Load(object sender, EventArgs e)
        {
            // Inicia RPI
            Rpi = new RPI();

            string numero = "";
            for (int i = 4; i <= 45; i++)
            {
                if (numero.Length > 0)
                    numero += "|";

                numero += "00001117_" + i.ToString("D2");
            }
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            using (frmRelatorio f = new frmRelatorio(this.Rpi))
            {
                f.ShowDialog();
            }
        }

        private void rdbAutomatico_CheckedChanged(object sender, EventArgs e)
        {
            mskPaginaInicial.Text = string.Empty;
            mskPaginaInicial.Enabled = false;
        }

        private void rdbManual_CheckedChanged(object sender, EventArgs e)
        {
            mskPaginaInicial.Enabled = true;
            mskPaginaInicial.Focus();
        }

        private void lblProcessoAntigo_Click(object sender, EventArgs e)
        {
            DateTime dtInicio;

            #region Localizar arquivo PDF

            ofdPDF.InitialDirectory = this.Rpi.PathRepositorio;
            ofdPDF.Filter = "txt files (*.pdf)|*.pdf";
            ofdPDF.FilterIndex = 2;
            ofdPDF.RestoreDirectory = false;

            if (ofdPDF.ShowDialog() == DialogResult.Cancel)
                return;

            #endregion

            Application.DoEvents();
            Cursor.Current = Cursors.WaitCursor;

            try
            {

                #region  Reinicia processo de extração

                // Verifica se é a primeira vez
                if (Rpi.DocPdf.Carregou)
                {
                    Rpi = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    lblProcessoAntigo.Text = "Extrair processos [OLD]";
                    lblImagensAntigo.Text = "Extrair imagens[OLD]";
                    btnRenomearOLD.Text = "Finalizar extração [OLD]";

                    Rpi = new RPI();
                }

                #endregion

                dtInicio = DateTime.Now;

                int _paginaInicial =
                    rdbManual.Checked ? int.Parse(mskPaginaInicial.Text.Trim()) : 0;

                this.Rpi.DocPdf.Carregar_OLD(ofdPDF.FileName, _paginaInicial, Rpi.PathRepositorio);
                this.Rpi.DocPdf.ExtrairProcessos_OLD(txtProcsIgnorados.Text);

                string status =
                   string.Format
                   (
                       "RPI: {0}{1}Foram extraídos {2} processos.{3}{4}Duração: {5}",
                       Rpi.DocPdf.Cabecalho.NumeroRPI,
                       Environment.NewLine,
                       Rpi.DocPdf.TotalProcessos,
                       Environment.NewLine,
                       Environment.NewLine,
                       DateTime.Now.Subtract(dtInicio).FormatTime()
                   );

                MessageBox.Show(status, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblProcessoAntigo.Text = status;

            }
            catch (Exception ex)
            {
                string message =
                    string.Format("Erro:{0}[{1}]", Environment.NewLine, ex.Message);

                MessageBox.Show(message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void lblImagensAntigo_Click(object sender, EventArgs e)
        {
            this.Text = "Extraindo imagens";
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            DateTime dtInicio = DateTime.Now;

            try
            {
                Rpi.DocPdf.ExtrairImagens_OLD(txtImgIgnoradas.Text);

                string status =
                  string.Format
                  (
                      "RPI: {0}{1}Foram extraídos {2} imagens.{3}{4}Duração: {5}",
                      Rpi.DocPdf.Cabecalho.NumeroRPI,
                      Environment.NewLine,
                      Rpi.DocPdf.TotalImagens,
                      Environment.NewLine,
                      Environment.NewLine,
                      DateTime.Now.Subtract(dtInicio).FormatTime()
                  );

                MessageBox.Show(status, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblImagensAntigo.Text = status;
            }
            catch (Exception ex)
            {
                string message =
                    string.Format
                        (
                            "Não foi possível realizar a extração de imagens.{0}{0}Descrição:{2}[{3}]",
                            Environment.NewLine,
                            Environment.NewLine,
                            Environment.NewLine,
                            ex.Message
                        );

                MessageBox.Show(message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Text = string.Empty;
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnRenomearOLD_Click(object sender, EventArgs e)
        {
            this.Text = "Finalizando extração";
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            try
            {
                Rpi.FinalizarExtracao_OLD(txtProcsIgnorados.Text, txtImgIgnoradas.Text);

                string status =
                    "Todos os arquivos foram renomeados com sucesso.";

                MessageBox.Show(status, btnRenomearOLD.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnRenomearOLD.Text = status;
            }
            catch (Exception ex)
            {
                string message =
                    string.Format
                        (
                            "Não foi possível renomear as imagens extraídas.{0}{1} Descrição:{2}[{3}]",
                            Environment.NewLine,
                            Environment.NewLine,
                            Environment.NewLine,
                            ex.Message
                        );

                MessageBox.Show(message, btnRenomearOLD.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Text = string.Empty;
                Cursor.Current = Cursors.Default;
            }
        }

        private void llAutomatizar_Click(object sender, EventArgs e)
        {
            string filepath = string.Empty;

            this.Text = "Realizando extração";
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            try
            {
                #region Localizar arquivos

                ofdPDF.InitialDirectory = this.Rpi.PathRepositorio;
                ofdPDF.Filter = "txt files (*.pdf)|*.pdf";
                ofdPDF.FilterIndex = 2;
                ofdPDF.RestoreDirectory = false;
                ofdPDF.Multiselect = true;

                if (ofdPDF.ShowDialog() == DialogResult.Cancel)
                    return;

                #endregion

                foreach (string path in ofdPDF.FileNames)
                {
                    filepath = path;
                    Rpi.Extrair_OLD(filepath);
                }

                MessageBox.Show("A extração foi finalizada.", lblAutomatizar.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string message =
                    string.Format
                        (
                            "Ocorreu erro durante a extração.{0}{1}Descrição:{2}[{3}{4}Arquivo:{5}]",
                            Environment.NewLine,
                            Environment.NewLine,
                            Environment.NewLine,
                            ex.Message,
                            Environment.NewLine,
                            filepath
                        );

                MessageBox.Show(message, lblAutomatizar.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Text = string.Empty;
                Cursor.Current = Cursors.Default;
            }

        }
    }
}
