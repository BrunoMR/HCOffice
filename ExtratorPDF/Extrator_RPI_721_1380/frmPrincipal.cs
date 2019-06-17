using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Extrator
{
    public partial class frmPrincipal : Form
    {
        private RPI Rpi;

        #region Eventos

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmOpen_Load(object sender, EventArgs e)
        {
            lblCarregarPDF.Text = "Seleciona e carrega arquivo PDF em memória.";
            lblExtrairImagens.Text = "Extrai imagens do arquivo PDF selecionado.";
            lblExtrairProcessos.Text = "Extrai processos com apresentação válida do arquivo PDF selecionado.";
            lblRenomearArquivos.Text = "Renomeia arquivos extraídos com números de processos correspondentes.";
            lblRelatorioExtracao.Text = "Relatório com informações do processo de extração.";

            // Inicia RPI
            Rpi = new RPI();
        }

        private void btnCarregarPDF_Click(object sender, EventArgs e)
        {
            #region Localizar arquivo PDF

            ofdPDF.InitialDirectory = this.Rpi.PathRepositorio;
            ofdPDF.Filter = "pdf files (*.pdf)|*.pdf";
            ofdPDF.FilterIndex = 2;
            ofdPDF.RestoreDirectory = false;

            if (ofdPDF.ShowDialog() == DialogResult.Cancel)
                return;

            #endregion

            this.Text = "Carregando PDF";
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                #region Reinicia processo de extração

                if (Rpi.DocPdf.Carregou)
                {
                    Rpi = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    Rpi = new RPI();
                }

                #endregion

                int _paginaInicial = 0;
                string[] _procsIgnorados = new string[] { };
                string[] _imagensIgnoradas = new string[] { };

                this.Rpi.DocPdf.Carregar
                    (
                        ofdPDF.FileName,
                        _paginaInicial,
                        Rpi.PathRepositorio,
                        _procsIgnorados,
                        _imagensIgnoradas
                    );

                MessageBox.Show
                    (
                        string.Format("O arquivo {0} foi carregado com sucesso.", Rpi.DocPdf.Filename),
                        this.Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
            }
            catch (Exception ex)
            {
                string message =
                    string.Format
                        (
                            "Não foi possível carregar o arquivo {0}.{1}{2}Descrição do erro:{3}[{4}]",
                            Rpi.DocPdf.Filename,
                            Environment.NewLine,
                            Environment.NewLine,
                            Environment.NewLine,
                            ex.Message
                        );

                MessageBox.Show(message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Text = string.Empty;
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnExtrairImagens_Click(object sender, EventArgs e)
        {
            this.Text = "Extraindo imagens";
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            DateTime dtInicio = DateTime.Now;

            try
            {
                Rpi.DocPdf.ExtrairImagens();

                string status =
                    string.Format("Foram extraídas {0} imagens.", Rpi.DocPdf.TotalImagens);

                MessageBox.Show(status, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnExtrairProcessos_Click(object sender, EventArgs e)
        {
            this.Text = "Extraindo processos";
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            DateTime dtInicio = DateTime.Now;

            try
            {
                Rpi.DocPdf.ExtrairProcessos();

                string status =
                    string.Format("Foram extraídos {0} processos.", Rpi.DocPdf.TotalProcessos);

                MessageBox.Show(status, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string message =
                    string.Format
                        (
                            "Não foi possível realizar a extração de processos.{0}{1}Descrição:{2}[{3}]",
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

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            using (frmRelatorio f = new frmRelatorio(this.Rpi))
            {
                f.ShowDialog();
            }
        }

        #endregion

        private void btnClasseTeste_Click(object sender, EventArgs e)
        {
            //Teste teste = new Teste();
            //teste.TestarRegex();

            //Cria um objeto do tipo Bitmap
            Bitmap objBitmap = new Bitmap(200, 200);

            ////Habilita o objeto bitmap criado para edição
            Graphics objGraphics = Graphics.FromImage(objBitmap);

            ////Desenha um retangulo com cores e dimensões especificas
            objGraphics.FillRectangle(new SolidBrush(Color.LightBlue), 0, 0, 200, 200);

            ////Desenha um circulo com cores e dimensões especificas
            //objGraphics.FillEllipse(new SolidBrush(Color.Blue), 3, 9, 10, 10);
            //objGraphics.FillEllipse(new SolidBrush(Color.Yellow), 4, 10, 8, 8);

            ////Adiciona um texto na imagem
            objGraphics.DrawString("Bertucci Soluções", new Font("Tahoma", 8), new SolidBrush(Color.Green), 16, 8);

            objBitmap.Save(@"C:\Users\Carol\Downloads\t.png", System.Drawing.Imaging.ImageFormat.Jpeg);
            objGraphics.Dispose();
            objBitmap.Dispose();
        }

        private void btnRenomearArquivos_Click(object sender, EventArgs e)
        {
            this.Text = "Renomeando imagens";
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            try
            {
                Rpi.RenomearImagensExtraidas();

                MessageBox.Show
                    (
                        "Todos os arquivos foram renomeados com sucesso.",
                        btnRenomearArquivos.Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
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

                MessageBox.Show(message, btnRenomearArquivos.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Text = string.Empty;
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
