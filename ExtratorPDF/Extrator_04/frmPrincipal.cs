using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Imaging;

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
            lblExtrairNCL.Text = "Extrai processos com NCl válida do arquivo PDF selecionado e gera arquivo texto com informações extraídas.";

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
                int.TryParse(mskPagInicio.Text.Trim(), out _paginaInicial);

                string[] _procsIgnorados =
                    txtIgnorarProcessos.Text.Trim().Split(new Char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                string[] _imagensIgnoradas =
                    txtIgnorarImagens.Text.Trim().Split(new Char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

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

                //Limpa parâmetros
                txtIgnorarProcessos.Text = string.Empty;
                txtIgnorarImagens.Text = string.Empty;
                mskPagInicio.Text = string.Empty;
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

        private void btnExtrairNCL_Click(object sender, EventArgs e)
        {
            this.Text = "Extraindo processos com NCL";
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            DateTime dtInicio = DateTime.Now;

            try
            {
                Rpi.DocPdf.ExtrairProcessosNCL();
                Rpi.GerarArquivoNCl();

                string status =
                    string.Format
                    (
                        "Arquivo gerado com sucesso.{0}Foram extraídos {1} processos com NCL válida.",
                        Environment.NewLine,
                        Rpi.DocPdf.TotalProcessosNCL
                    );

                MessageBox.Show(status, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string message =
                    string.Format
                        (
                            "Não foi possível gerar o arquivo NCL.{0}{1}Descrição:{2}[{3}]",
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

        private void btnRotacionarImagens_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdImg = new OpenFileDialog();
            RotateFlipType rotateFlipType;
            string rotacao;

            this.Text = "Realizando extração";
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();

            try
            {
                #region Localizar arquivos

                ofdImg.InitialDirectory = this.Rpi.PathRepositorio;
                ofdImg.Title = "Localizar imagens";
                ofdImg.Filter = "Imagens (*.bmp;*.jpg;*.gif,*.png,*.tiff)|*.bmp;*.jpg;*.if;*.png;*.tiff";
                ofdImg.FilterIndex = 2;
                ofdImg.CheckFileExists = true;
                ofdImg.CheckPathExists = true;
                ofdImg.RestoreDirectory = false;
                ofdImg.Multiselect = true;

                if (ofdImg.ShowDialog() == DialogResult.Cancel)
                    return;

                #endregion

                #region  Sentido da rotação

                if (rdbFlipHorizontal.Checked)
                {
                    rotateFlipType = RotateFlipType.Rotate180FlipX;
                    rotacao = rdbFlipHorizontal.Text;
                }
                else
                {
                    rotateFlipType = RotateFlipType.Rotate180FlipY;
                    rotacao = rdbFlipVertical.Text;
                }

                #endregion

                string msgQuestion = string.Format
                    (
                        "As {0} imagens selecionadas serão rotacionadas {1}.{2}Confirma a operação?",
                        ofdImg.FileNames.Count(),
                        rotacao,
                        Environment.NewLine
                    );

                if (MessageBox.Show(
                        msgQuestion, btnRotacionarImagens.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                // Executa método
                Rpi.RotacionarImagens(ofdImg.FileNames, rotateFlipType);

                MessageBox.Show
                    ("Todas as imagens foram rotacionadas.", btnRotacionarImagens.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string message =
                    string.Format
                        (
                            "Ocorreu erro ao rotacionar as imagens.{0}{1}Descrição:{2}[{3}]",
                            Environment.NewLine,
                            Environment.NewLine,
                            Environment.NewLine,
                            ex.Message
                        );

                MessageBox.Show(message, btnRotacionarImagens.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.Text = string.Empty;
                Cursor.Current = Cursors.Default;
                ofdImg.Dispose();
            }
        }

        private void btnAdicionarProc_Click(object sender, EventArgs e)
        {
            string numProc = mskProc.TextoSemMascara().Trim();

            if (numProc.Length != 9)
            {
                MessageBox.Show("Número de processo inválido.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mskProc.Focus();
                return;
            }

            string processos = txtIgnorarProcessos.Text.Trim();

            if (processos.Length > 0)
                processos += "|";

            processos += numProc;
            txtIgnorarProcessos.Text = processos;

            mskProc.Text = string.Empty;
            mskProc.Focus();
        }

        private void btnAdicionarImg_Click(object sender, EventArgs e)
        {
            string _pag = mskImgPagina.TextoSemMascara().Trim();
            string _pos = mskImgPos.TextoSemMascara().Trim();

            #region Validação

            if (_pag.Length == 0)
            {
                MessageBox.Show("Página não informada.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mskImgPagina.Focus();
                return;
            }

            if (_pos.Length == 0)
            {
                MessageBox.Show("Posição não informada.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                mskImgPos.Focus();
                return;
            }

            #endregion

            Imagem imagem =
                new Imagem { Pagina = int.Parse(_pag), Posicao = int.Parse(_pos) };

            string imagens = txtIgnorarImagens.Text.Trim();

            if (imagens.Length > 0)
                imagens += "|";

            imagens += imagem.NomeGerado;
            txtIgnorarImagens.Text = imagens;

            mskImgPagina.Text = string.Empty;
            mskImgPos.Text = string.Empty;
            mskImgPagina.Focus();
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
    }
}
