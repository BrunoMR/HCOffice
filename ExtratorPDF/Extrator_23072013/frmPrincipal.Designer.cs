namespace Extrator
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ofdPDF = new System.Windows.Forms.OpenFileDialog();
            this.mskPagInicio = new System.Windows.Forms.MaskedTextBox();
            this.flatTabControl1 = new FlatTabControl.FlatTabControl();
            this.tbpParametros = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mskProc = new System.Windows.Forms.MaskedTextBox();
            this.btnAdicionarProc = new System.Windows.Forms.Button();
            this.txtIgnorarProcessos = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.mskImgPos = new System.Windows.Forms.MaskedTextBox();
            this.mskImgPagina = new System.Windows.Forms.MaskedTextBox();
            this.btnAdicionarImg = new System.Windows.Forms.Button();
            this.txtIgnorarImagens = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblIgnorarImagens = new System.Windows.Forms.Label();
            this.tbpExtração = new System.Windows.Forms.TabPage();
            this.lblRenomearArquivos = new System.Windows.Forms.Label();
            this.lblExtrairProcessos = new System.Windows.Forms.Label();
            this.lblExtrairImagens = new System.Windows.Forms.Label();
            this.lblRelatorioExtracao = new System.Windows.Forms.Label();
            this.btnRelatorio = new System.Windows.Forms.Button();
            this.lblExtrairNCL = new System.Windows.Forms.Label();
            this.btnExtrairNCL = new System.Windows.Forms.Button();
            this.lblCarregarPDF = new System.Windows.Forms.Label();
            this.btnCarregarPDF = new System.Windows.Forms.Button();
            this.btnExtrairProcessos = new System.Windows.Forms.Button();
            this.btnExtrairImagens = new System.Windows.Forms.Button();
            this.btnRenomearArquivos = new System.Windows.Forms.Button();
            this.tbOutras = new System.Windows.Forms.TabPage();
            this.rdbFlipVertical = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rdbFlipHorizontal = new System.Windows.Forms.RadioButton();
            this.btnRotacionarImagens = new System.Windows.Forms.Button();
            this.btnClasseTeste = new System.Windows.Forms.Button();
            this.flatTabControl1.SuspendLayout();
            this.tbpParametros.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tbpExtração.SuspendLayout();
            this.tbOutras.SuspendLayout();
            this.SuspendLayout();
            // 
            // ofdPDF
            // 
            this.ofdPDF.FileName = "openFileDialog1";
            // 
            // mskPagInicio
            // 
            this.mskPagInicio.BackColor = System.Drawing.Color.White;
            this.mskPagInicio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskPagInicio.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.mskPagInicio.ForeColor = System.Drawing.Color.DimGray;
            this.mskPagInicio.Location = new System.Drawing.Point(68, 58);
            this.mskPagInicio.Mask = "00000";
            this.mskPagInicio.Name = "mskPagInicio";
            this.mskPagInicio.PromptChar = ' ';
            this.mskPagInicio.Size = new System.Drawing.Size(67, 25);
            this.mskPagInicio.TabIndex = 8;
            this.mskPagInicio.ValidatingType = typeof(int);
            // 
            // flatTabControl1
            // 
            this.flatTabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.flatTabControl1.Controls.Add(this.tbpParametros);
            this.flatTabControl1.Controls.Add(this.tbpExtração);
            this.flatTabControl1.Controls.Add(this.tbOutras);
            this.flatTabControl1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.flatTabControl1.ItemSize = new System.Drawing.Size(180, 35);
            this.flatTabControl1.Location = new System.Drawing.Point(7, 10);
            this.flatTabControl1.Name = "flatTabControl1";
            this.flatTabControl1.Padding = new System.Drawing.Point(25, 25);
            this.flatTabControl1.SelectedIndex = 0;
            this.flatTabControl1.Size = new System.Drawing.Size(643, 379);
            this.flatTabControl1.TabIndex = 1;
            this.flatTabControl1.TabStop = false;
            // 
            // tbpParametros
            // 
            this.tbpParametros.BackColor = System.Drawing.Color.Transparent;
            this.tbpParametros.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tbpParametros.Controls.Add(this.panel4);
            this.tbpParametros.Controls.Add(this.panel1);
            this.tbpParametros.Controls.Add(this.panel3);
            this.tbpParametros.Controls.Add(this.lblIgnorarImagens);
            this.tbpParametros.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.tbpParametros.ForeColor = System.Drawing.Color.DimGray;
            this.tbpParametros.Location = new System.Drawing.Point(4, 39);
            this.tbpParametros.Name = "tbpParametros";
            this.tbpParametros.Padding = new System.Windows.Forms.Padding(12);
            this.tbpParametros.Size = new System.Drawing.Size(635, 336);
            this.tbpParametros.TabIndex = 5;
            this.tbpParametros.Text = "Parâmetros";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.mskPagInicio);
            this.panel4.Controls.Add(this.label3);
            this.panel4.ForeColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(4, 226);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(312, 107);
            this.panel4.TabIndex = 1562;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label14.ForeColor = System.Drawing.Color.DimGray;
            this.label14.Location = new System.Drawing.Point(7, 31);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(298, 19);
            this.label14.TabIndex = 1555;
            this.label14.Text = "Página onde será iniciada a extração.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.label15.ForeColor = System.Drawing.Color.DimGray;
            this.label15.Location = new System.Drawing.Point(5, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(113, 25);
            this.label15.TabIndex = 1554;
            this.label15.Text = "Página inicial";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.DimGray;
            this.label3.Location = new System.Drawing.Point(7, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 1538;
            this.label3.Text = "Número:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.mskProc);
            this.panel1.Controls.Add(this.btnAdicionarProc);
            this.panel1.Controls.Add(this.txtIgnorarProcessos);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(312, 218);
            this.panel1.TabIndex = 1561;
            // 
            // mskProc
            // 
            this.mskProc.BackColor = System.Drawing.Color.White;
            this.mskProc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskProc.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.mskProc.ForeColor = System.Drawing.Color.Black;
            this.mskProc.Location = new System.Drawing.Point(10, 81);
            this.mskProc.Mask = "999999999";
            this.mskProc.Name = "mskProc";
            this.mskProc.PromptChar = ' ';
            this.mskProc.Size = new System.Drawing.Size(113, 26);
            this.mskProc.TabIndex = 1;
            this.mskProc.ValidatingType = typeof(int);
            // 
            // btnAdicionarProc
            // 
            this.btnAdicionarProc.BackColor = System.Drawing.Color.DimGray;
            this.btnAdicionarProc.FlatAppearance.BorderSize = 0;
            this.btnAdicionarProc.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnAdicionarProc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnAdicionarProc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarProc.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnAdicionarProc.ForeColor = System.Drawing.Color.White;
            this.btnAdicionarProc.Location = new System.Drawing.Point(131, 77);
            this.btnAdicionarProc.Name = "btnAdicionarProc";
            this.btnAdicionarProc.Size = new System.Drawing.Size(88, 34);
            this.btnAdicionarProc.TabIndex = 2;
            this.btnAdicionarProc.Text = "Adicionar";
            this.btnAdicionarProc.UseVisualStyleBackColor = false;
            this.btnAdicionarProc.Click += new System.EventHandler(this.btnAdicionarProc_Click);
            // 
            // txtIgnorarProcessos
            // 
            this.txtIgnorarProcessos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIgnorarProcessos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtIgnorarProcessos.Location = new System.Drawing.Point(10, 117);
            this.txtIgnorarProcessos.Multiline = true;
            this.txtIgnorarProcessos.Name = "txtIgnorarProcessos";
            this.txtIgnorarProcessos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIgnorarProcessos.Size = new System.Drawing.Size(293, 92);
            this.txtIgnorarProcessos.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(7, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(298, 38);
            this.label4.TabIndex = 1555;
            this.label4.Text = "Números de processos que devem ser ignorados durante a extração.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.label5.ForeColor = System.Drawing.Color.DimGray;
            this.label5.Location = new System.Drawing.Point(5, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 25);
            this.label5.TabIndex = 1554;
            this.label5.Text = "Ignorar processos";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.mskImgPos);
            this.panel3.Controls.Add(this.mskImgPagina);
            this.panel3.Controls.Add(this.btnAdicionarImg);
            this.panel3.Controls.Add(this.txtIgnorarImagens);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label13);
            this.panel3.ForeColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(320, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(312, 218);
            this.panel3.TabIndex = 1560;
            // 
            // mskImgPos
            // 
            this.mskImgPos.BackColor = System.Drawing.Color.White;
            this.mskImgPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskImgPos.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.mskImgPos.ForeColor = System.Drawing.Color.Black;
            this.mskImgPos.Location = new System.Drawing.Point(93, 81);
            this.mskImgPos.Mask = "99";
            this.mskImgPos.Name = "mskImgPos";
            this.mskImgPos.PromptChar = ' ';
            this.mskImgPos.Size = new System.Drawing.Size(30, 26);
            this.mskImgPos.TabIndex = 5;
            this.mskImgPos.ValidatingType = typeof(int);
            // 
            // mskImgPagina
            // 
            this.mskImgPagina.BackColor = System.Drawing.Color.White;
            this.mskImgPagina.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mskImgPagina.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.mskImgPagina.ForeColor = System.Drawing.Color.Black;
            this.mskImgPagina.Location = new System.Drawing.Point(10, 81);
            this.mskImgPagina.Mask = "99999999";
            this.mskImgPagina.Name = "mskImgPagina";
            this.mskImgPagina.PromptChar = ' ';
            this.mskImgPagina.Size = new System.Drawing.Size(77, 26);
            this.mskImgPagina.TabIndex = 4;
            this.mskImgPagina.ValidatingType = typeof(int);
            // 
            // btnAdicionarImg
            // 
            this.btnAdicionarImg.BackColor = System.Drawing.Color.DimGray;
            this.btnAdicionarImg.FlatAppearance.BorderSize = 0;
            this.btnAdicionarImg.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnAdicionarImg.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnAdicionarImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdicionarImg.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnAdicionarImg.ForeColor = System.Drawing.Color.White;
            this.btnAdicionarImg.Location = new System.Drawing.Point(131, 77);
            this.btnAdicionarImg.Name = "btnAdicionarImg";
            this.btnAdicionarImg.Size = new System.Drawing.Size(88, 34);
            this.btnAdicionarImg.TabIndex = 6;
            this.btnAdicionarImg.Text = "Adicionar";
            this.btnAdicionarImg.UseVisualStyleBackColor = false;
            this.btnAdicionarImg.Click += new System.EventHandler(this.btnAdicionarImg_Click);
            // 
            // txtIgnorarImagens
            // 
            this.txtIgnorarImagens.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIgnorarImagens.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtIgnorarImagens.Location = new System.Drawing.Point(10, 117);
            this.txtIgnorarImagens.Multiline = true;
            this.txtIgnorarImagens.Name = "txtIgnorarImagens";
            this.txtIgnorarImagens.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIgnorarImagens.Size = new System.Drawing.Size(293, 92);
            this.txtIgnorarImagens.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label12.ForeColor = System.Drawing.Color.DimGray;
            this.label12.Location = new System.Drawing.Point(7, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(298, 38);
            this.label12.TabIndex = 1555;
            this.label12.Text = "Página e posição de imagens que devem ser ignoradas durante a extração.";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.label13.ForeColor = System.Drawing.Color.DimGray;
            this.label13.Location = new System.Drawing.Point(5, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(142, 25);
            this.label13.TabIndex = 1554;
            this.label13.Text = "Ignorar imagens";
            // 
            // lblIgnorarImagens
            // 
            this.lblIgnorarImagens.BackColor = System.Drawing.Color.Black;
            this.lblIgnorarImagens.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIgnorarImagens.ForeColor = System.Drawing.Color.White;
            this.lblIgnorarImagens.Location = new System.Drawing.Point(327, 144);
            this.lblIgnorarImagens.Name = "lblIgnorarImagens";
            this.lblIgnorarImagens.Size = new System.Drawing.Size(297, 47);
            this.lblIgnorarImagens.TabIndex = 1545;
            this.lblIgnorarImagens.Text = "Página e posição das imagens que devem ser ignoradas na extração.";
            // 
            // tbpExtração
            // 
            this.tbpExtração.Controls.Add(this.lblRenomearArquivos);
            this.tbpExtração.Controls.Add(this.lblExtrairProcessos);
            this.tbpExtração.Controls.Add(this.lblExtrairImagens);
            this.tbpExtração.Controls.Add(this.lblRelatorioExtracao);
            this.tbpExtração.Controls.Add(this.btnRelatorio);
            this.tbpExtração.Controls.Add(this.lblExtrairNCL);
            this.tbpExtração.Controls.Add(this.btnExtrairNCL);
            this.tbpExtração.Controls.Add(this.lblCarregarPDF);
            this.tbpExtração.Controls.Add(this.btnCarregarPDF);
            this.tbpExtração.Controls.Add(this.btnExtrairProcessos);
            this.tbpExtração.Controls.Add(this.btnExtrairImagens);
            this.tbpExtração.Controls.Add(this.btnRenomearArquivos);
            this.tbpExtração.ForeColor = System.Drawing.Color.DimGray;
            this.tbpExtração.Location = new System.Drawing.Point(4, 39);
            this.tbpExtração.Name = "tbpExtração";
            this.tbpExtração.Padding = new System.Windows.Forms.Padding(3);
            this.tbpExtração.Size = new System.Drawing.Size(635, 336);
            this.tbpExtração.TabIndex = 6;
            this.tbpExtração.Text = "Extração";
            // 
            // lblRenomearArquivos
            // 
            this.lblRenomearArquivos.BackColor = System.Drawing.Color.DimGray;
            this.lblRenomearArquivos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblRenomearArquivos.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblRenomearArquivos.ForeColor = System.Drawing.Color.White;
            this.lblRenomearArquivos.Location = new System.Drawing.Point(327, 144);
            this.lblRenomearArquivos.Name = "lblRenomearArquivos";
            this.lblRenomearArquivos.Size = new System.Drawing.Size(297, 67);
            this.lblRenomearArquivos.TabIndex = 1529;
            this.lblRenomearArquivos.Text = "Renomear arquivos extraídos com número de processo correspondente.";
            this.lblRenomearArquivos.Click += new System.EventHandler(this.btnRenomearArquivos_Click);
            // 
            // lblExtrairProcessos
            // 
            this.lblExtrairProcessos.BackColor = System.Drawing.Color.DimGray;
            this.lblExtrairProcessos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblExtrairProcessos.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblExtrairProcessos.ForeColor = System.Drawing.Color.White;
            this.lblExtrairProcessos.Location = new System.Drawing.Point(326, 33);
            this.lblExtrairProcessos.Name = "lblExtrairProcessos";
            this.lblExtrairProcessos.Size = new System.Drawing.Size(297, 67);
            this.lblExtrairProcessos.TabIndex = 1527;
            this.lblExtrairProcessos.Text = "Extrai processos com apresentação válida do arquivo PDF selecionado.";
            this.lblExtrairProcessos.Click += new System.EventHandler(this.btnExtrairProcessos_Click);
            // 
            // lblExtrairImagens
            // 
            this.lblExtrairImagens.BackColor = System.Drawing.Color.DimGray;
            this.lblExtrairImagens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblExtrairImagens.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblExtrairImagens.ForeColor = System.Drawing.Color.White;
            this.lblExtrairImagens.Location = new System.Drawing.Point(11, 145);
            this.lblExtrairImagens.Name = "lblExtrairImagens";
            this.lblExtrairImagens.Size = new System.Drawing.Size(297, 67);
            this.lblExtrairImagens.TabIndex = 1524;
            this.lblExtrairImagens.Text = "Cria diretório e extrai imagens do arquivo PDF selecionado.";
            this.lblExtrairImagens.Click += new System.EventHandler(this.btnExtrairImagens_Click);
            // 
            // lblRelatorioExtracao
            // 
            this.lblRelatorioExtracao.BackColor = System.Drawing.Color.DimGray;
            this.lblRelatorioExtracao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblRelatorioExtracao.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblRelatorioExtracao.ForeColor = System.Drawing.Color.White;
            this.lblRelatorioExtracao.Location = new System.Drawing.Point(327, 255);
            this.lblRelatorioExtracao.Name = "lblRelatorioExtracao";
            this.lblRelatorioExtracao.Size = new System.Drawing.Size(297, 67);
            this.lblRelatorioExtracao.TabIndex = 1525;
            this.lblRelatorioExtracao.Text = "Relatório com informações do processo de extração.";
            this.lblRelatorioExtracao.Click += new System.EventHandler(this.btnRelatorio_Click);
            // 
            // btnRelatorio
            // 
            this.btnRelatorio.BackColor = System.Drawing.Color.DimGray;
            this.btnRelatorio.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnRelatorio.FlatAppearance.BorderSize = 0;
            this.btnRelatorio.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnRelatorio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnRelatorio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnRelatorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRelatorio.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnRelatorio.ForeColor = System.Drawing.Color.White;
            this.btnRelatorio.Location = new System.Drawing.Point(320, 226);
            this.btnRelatorio.Name = "btnRelatorio";
            this.btnRelatorio.Size = new System.Drawing.Size(312, 107);
            this.btnRelatorio.TabIndex = 7;
            this.btnRelatorio.Text = "Relatório de extração";
            this.btnRelatorio.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnRelatorio.UseVisualStyleBackColor = false;
            this.btnRelatorio.Click += new System.EventHandler(this.btnRelatorio_Click);
            // 
            // lblExtrairNCL
            // 
            this.lblExtrairNCL.BackColor = System.Drawing.Color.DimGray;
            this.lblExtrairNCL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblExtrairNCL.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblExtrairNCL.ForeColor = System.Drawing.Color.White;
            this.lblExtrairNCL.Location = new System.Drawing.Point(11, 255);
            this.lblExtrairNCL.Name = "lblExtrairNCL";
            this.lblExtrairNCL.Size = new System.Drawing.Size(297, 67);
            this.lblExtrairNCL.TabIndex = 1528;
            this.lblExtrairNCL.Text = "Extrai processos com NCl válida do arquivo PDF selecionado e gera arquivo texto c" +
    "om informações extraídas.";
            this.lblExtrairNCL.Click += new System.EventHandler(this.btnExtrairNCL_Click);
            // 
            // btnExtrairNCL
            // 
            this.btnExtrairNCL.BackColor = System.Drawing.Color.DimGray;
            this.btnExtrairNCL.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnExtrairNCL.FlatAppearance.BorderSize = 0;
            this.btnExtrairNCL.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnExtrairNCL.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnExtrairNCL.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnExtrairNCL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtrairNCL.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnExtrairNCL.ForeColor = System.Drawing.Color.White;
            this.btnExtrairNCL.Location = new System.Drawing.Point(4, 226);
            this.btnExtrairNCL.Name = "btnExtrairNCL";
            this.btnExtrairNCL.Size = new System.Drawing.Size(312, 107);
            this.btnExtrairNCL.TabIndex = 6;
            this.btnExtrairNCL.Text = "Gerar arquivo NCL";
            this.btnExtrairNCL.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExtrairNCL.UseVisualStyleBackColor = false;
            this.btnExtrairNCL.Click += new System.EventHandler(this.btnExtrairNCL_Click);
            // 
            // lblCarregarPDF
            // 
            this.lblCarregarPDF.BackColor = System.Drawing.Color.DimGray;
            this.lblCarregarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCarregarPDF.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblCarregarPDF.ForeColor = System.Drawing.Color.White;
            this.lblCarregarPDF.Location = new System.Drawing.Point(12, 33);
            this.lblCarregarPDF.Name = "lblCarregarPDF";
            this.lblCarregarPDF.Size = new System.Drawing.Size(297, 67);
            this.lblCarregarPDF.TabIndex = 1526;
            this.lblCarregarPDF.Text = "Seleciona e carrega arquivo PDF em memória.";
            this.lblCarregarPDF.Click += new System.EventHandler(this.btnCarregarPDF_Click);
            // 
            // btnCarregarPDF
            // 
            this.btnCarregarPDF.BackColor = System.Drawing.Color.DimGray;
            this.btnCarregarPDF.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnCarregarPDF.FlatAppearance.BorderSize = 0;
            this.btnCarregarPDF.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCarregarPDF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnCarregarPDF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnCarregarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCarregarPDF.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnCarregarPDF.ForeColor = System.Drawing.Color.White;
            this.btnCarregarPDF.Location = new System.Drawing.Point(4, 4);
            this.btnCarregarPDF.Name = "btnCarregarPDF";
            this.btnCarregarPDF.Size = new System.Drawing.Size(312, 107);
            this.btnCarregarPDF.TabIndex = 2;
            this.btnCarregarPDF.Text = "Carregar PDF";
            this.btnCarregarPDF.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCarregarPDF.UseVisualStyleBackColor = false;
            this.btnCarregarPDF.Click += new System.EventHandler(this.btnCarregarPDF_Click);
            // 
            // btnExtrairProcessos
            // 
            this.btnExtrairProcessos.BackColor = System.Drawing.Color.DimGray;
            this.btnExtrairProcessos.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnExtrairProcessos.FlatAppearance.BorderSize = 0;
            this.btnExtrairProcessos.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnExtrairProcessos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnExtrairProcessos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnExtrairProcessos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtrairProcessos.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnExtrairProcessos.ForeColor = System.Drawing.Color.White;
            this.btnExtrairProcessos.Location = new System.Drawing.Point(320, 4);
            this.btnExtrairProcessos.Name = "btnExtrairProcessos";
            this.btnExtrairProcessos.Size = new System.Drawing.Size(312, 107);
            this.btnExtrairProcessos.TabIndex = 3;
            this.btnExtrairProcessos.Text = "Extrair processos";
            this.btnExtrairProcessos.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExtrairProcessos.UseVisualStyleBackColor = false;
            this.btnExtrairProcessos.Click += new System.EventHandler(this.btnExtrairProcessos_Click);
            // 
            // btnExtrairImagens
            // 
            this.btnExtrairImagens.BackColor = System.Drawing.Color.DimGray;
            this.btnExtrairImagens.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnExtrairImagens.FlatAppearance.BorderSize = 0;
            this.btnExtrairImagens.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnExtrairImagens.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnExtrairImagens.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnExtrairImagens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtrairImagens.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnExtrairImagens.ForeColor = System.Drawing.Color.White;
            this.btnExtrairImagens.Location = new System.Drawing.Point(4, 115);
            this.btnExtrairImagens.Name = "btnExtrairImagens";
            this.btnExtrairImagens.Size = new System.Drawing.Size(312, 107);
            this.btnExtrairImagens.TabIndex = 4;
            this.btnExtrairImagens.Text = "Extrair Imagens";
            this.btnExtrairImagens.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExtrairImagens.UseVisualStyleBackColor = false;
            this.btnExtrairImagens.Click += new System.EventHandler(this.btnExtrairImagens_Click);
            // 
            // btnRenomearArquivos
            // 
            this.btnRenomearArquivos.BackColor = System.Drawing.Color.DimGray;
            this.btnRenomearArquivos.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnRenomearArquivos.FlatAppearance.BorderSize = 0;
            this.btnRenomearArquivos.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnRenomearArquivos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnRenomearArquivos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnRenomearArquivos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRenomearArquivos.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnRenomearArquivos.ForeColor = System.Drawing.Color.White;
            this.btnRenomearArquivos.Location = new System.Drawing.Point(320, 115);
            this.btnRenomearArquivos.Name = "btnRenomearArquivos";
            this.btnRenomearArquivos.Size = new System.Drawing.Size(312, 107);
            this.btnRenomearArquivos.TabIndex = 5;
            this.btnRenomearArquivos.Text = "Renomear imagens extraídas";
            this.btnRenomearArquivos.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnRenomearArquivos.UseVisualStyleBackColor = false;
            this.btnRenomearArquivos.Click += new System.EventHandler(this.btnRenomearArquivos_Click);
            // 
            // tbOutras
            // 
            this.tbOutras.Controls.Add(this.rdbFlipVertical);
            this.tbOutras.Controls.Add(this.label1);
            this.tbOutras.Controls.Add(this.rdbFlipHorizontal);
            this.tbOutras.Controls.Add(this.btnRotacionarImagens);
            this.tbOutras.ForeColor = System.Drawing.Color.DimGray;
            this.tbOutras.Location = new System.Drawing.Point(4, 39);
            this.tbOutras.Name = "tbOutras";
            this.tbOutras.Size = new System.Drawing.Size(635, 336);
            this.tbOutras.TabIndex = 8;
            this.tbOutras.Text = "Outras funcionalidades";
            // 
            // rdbFlipVertical
            // 
            this.rdbFlipVertical.AutoSize = true;
            this.rdbFlipVertical.BackColor = System.Drawing.Color.DimGray;
            this.rdbFlipVertical.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.rdbFlipVertical.FlatAppearance.BorderSize = 0;
            this.rdbFlipVertical.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.rdbFlipVertical.ForeColor = System.Drawing.Color.White;
            this.rdbFlipVertical.Location = new System.Drawing.Point(137, 60);
            this.rdbFlipVertical.Name = "rdbFlipVertical";
            this.rdbFlipVertical.Size = new System.Drawing.Size(100, 21);
            this.rdbFlipVertical.TabIndex = 3;
            this.rdbFlipVertical.Text = "180º Vertical";
            this.rdbFlipVertical.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DimGray;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(297, 24);
            this.label1.TabIndex = 1532;
            this.label1.Text = "Rotacionar imagens no sentido selecionado.";
            // 
            // rdbFlipHorizontal
            // 
            this.rdbFlipHorizontal.AutoSize = true;
            this.rdbFlipHorizontal.BackColor = System.Drawing.Color.DimGray;
            this.rdbFlipHorizontal.Checked = true;
            this.rdbFlipHorizontal.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.rdbFlipHorizontal.FlatAppearance.BorderSize = 0;
            this.rdbFlipHorizontal.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.rdbFlipHorizontal.ForeColor = System.Drawing.Color.White;
            this.rdbFlipHorizontal.Location = new System.Drawing.Point(14, 60);
            this.rdbFlipHorizontal.Name = "rdbFlipHorizontal";
            this.rdbFlipHorizontal.Size = new System.Drawing.Size(117, 21);
            this.rdbFlipHorizontal.TabIndex = 2;
            this.rdbFlipHorizontal.TabStop = true;
            this.rdbFlipHorizontal.Text = "180º Horizontal";
            this.rdbFlipHorizontal.UseVisualStyleBackColor = false;
            // 
            // btnRotacionarImagens
            // 
            this.btnRotacionarImagens.BackColor = System.Drawing.Color.DimGray;
            this.btnRotacionarImagens.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.btnRotacionarImagens.FlatAppearance.BorderSize = 0;
            this.btnRotacionarImagens.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.btnRotacionarImagens.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DimGray;
            this.btnRotacionarImagens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRotacionarImagens.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnRotacionarImagens.ForeColor = System.Drawing.Color.White;
            this.btnRotacionarImagens.Location = new System.Drawing.Point(4, 4);
            this.btnRotacionarImagens.Name = "btnRotacionarImagens";
            this.btnRotacionarImagens.Size = new System.Drawing.Size(312, 107);
            this.btnRotacionarImagens.TabIndex = 1;
            this.btnRotacionarImagens.Text = "Rotacionar imagens";
            this.btnRotacionarImagens.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnRotacionarImagens.UseVisualStyleBackColor = false;
            this.btnRotacionarImagens.Click += new System.EventHandler(this.btnRotacionarImagens_Click);
            // 
            // btnClasseTeste
            // 
            this.btnClasseTeste.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnClasseTeste.FlatAppearance.BorderSize = 0;
            this.btnClasseTeste.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btnClasseTeste.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnClasseTeste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClasseTeste.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnClasseTeste.ForeColor = System.Drawing.Color.White;
            this.btnClasseTeste.Location = new System.Drawing.Point(463, 345);
            this.btnClasseTeste.Name = "btnClasseTeste";
            this.btnClasseTeste.Size = new System.Drawing.Size(228, 53);
            this.btnClasseTeste.TabIndex = 1539;
            this.btnClasseTeste.Text = "Classe teste";
            this.btnClasseTeste.UseVisualStyleBackColor = false;
            this.btnClasseTeste.Visible = false;
            this.btnClasseTeste.Click += new System.EventHandler(this.btnClasseTeste_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(659, 396);
            this.Controls.Add(this.btnClasseTeste);
            this.Controls.Add(this.flatTabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrincipal";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extrator";
            this.Load += new System.EventHandler(this.frmOpen_Load);
            this.flatTabControl1.ResumeLayout(false);
            this.tbpParametros.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tbpExtração.ResumeLayout(false);
            this.tbOutras.ResumeLayout(false);
            this.tbOutras.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdPDF;
        private System.Windows.Forms.MaskedTextBox mskPagInicio;
        private FlatTabControl.FlatTabControl flatTabControl1;
        private System.Windows.Forms.TabPage tbpParametros;
        private System.Windows.Forms.TabPage tbpExtração;
        private System.Windows.Forms.Label lblRenomearArquivos;
        private System.Windows.Forms.Label lblExtrairNCL;
        private System.Windows.Forms.Button btnExtrairNCL;
        private System.Windows.Forms.Label lblRelatorioExtracao;
        private System.Windows.Forms.Button btnRelatorio;
        private System.Windows.Forms.Label lblCarregarPDF;
        private System.Windows.Forms.Label lblExtrairImagens;
        private System.Windows.Forms.Label lblExtrairProcessos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClasseTeste;
        private System.Windows.Forms.Label lblIgnorarImagens;
        private System.Windows.Forms.Button btnCarregarPDF;
        private System.Windows.Forms.TabPage tbOutras;
        private System.Windows.Forms.Button btnExtrairProcessos;
        private System.Windows.Forms.Button btnExtrairImagens;
        private System.Windows.Forms.Button btnRenomearArquivos;
        private System.Windows.Forms.RadioButton rdbFlipVertical;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdbFlipHorizontal;
        private System.Windows.Forms.Button btnRotacionarImagens;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.MaskedTextBox mskImgPos;
        private System.Windows.Forms.MaskedTextBox mskImgPagina;
        private System.Windows.Forms.Button btnAdicionarImg;
        private System.Windows.Forms.TextBox txtIgnorarImagens;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MaskedTextBox mskProc;
        private System.Windows.Forms.Button btnAdicionarProc;
        private System.Windows.Forms.TextBox txtIgnorarProcessos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
    }
}