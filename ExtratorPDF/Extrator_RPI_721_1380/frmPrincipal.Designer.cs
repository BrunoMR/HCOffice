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
            this.btnClasseTeste = new System.Windows.Forms.Button();
            this.lblRenomearArquivos = new System.Windows.Forms.Label();
            this.lblExtrairProcessos = new System.Windows.Forms.Label();
            this.lblExtrairImagens = new System.Windows.Forms.Label();
            this.lblRelatorioExtracao = new System.Windows.Forms.Label();
            this.btnRelatorio = new System.Windows.Forms.Button();
            this.lblCarregarPDF = new System.Windows.Forms.Label();
            this.btnCarregarPDF = new System.Windows.Forms.Button();
            this.btnExtrairProcessos = new System.Windows.Forms.Button();
            this.btnExtrairImagens = new System.Windows.Forms.Button();
            this.btnRenomearArquivos = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ofdPDF
            // 
            this.ofdPDF.FileName = "openFileDialog1";
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
            this.btnClasseTeste.Location = new System.Drawing.Point(386, 255);
            this.btnClasseTeste.Name = "btnClasseTeste";
            this.btnClasseTeste.Size = new System.Drawing.Size(228, 53);
            this.btnClasseTeste.TabIndex = 1550;
            this.btnClasseTeste.Text = "Classe teste";
            this.btnClasseTeste.UseVisualStyleBackColor = false;
            this.btnClasseTeste.Visible = false;
            // 
            // lblRenomearArquivos
            // 
            this.lblRenomearArquivos.BackColor = System.Drawing.Color.Teal;
            this.lblRenomearArquivos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblRenomearArquivos.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblRenomearArquivos.ForeColor = System.Drawing.Color.White;
            this.lblRenomearArquivos.Location = new System.Drawing.Point(327, 144);
            this.lblRenomearArquivos.Name = "lblRenomearArquivos";
            this.lblRenomearArquivos.Size = new System.Drawing.Size(297, 67);
            this.lblRenomearArquivos.TabIndex = 1549;
            this.lblRenomearArquivos.Text = "Renomear arquivos extraídos com número de processo correspondente.";
            this.lblRenomearArquivos.Click += new System.EventHandler(this.btnRenomearArquivos_Click);
            // 
            // lblExtrairProcessos
            // 
            this.lblExtrairProcessos.BackColor = System.Drawing.Color.Teal;
            this.lblExtrairProcessos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblExtrairProcessos.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblExtrairProcessos.ForeColor = System.Drawing.Color.White;
            this.lblExtrairProcessos.Location = new System.Drawing.Point(326, 33);
            this.lblExtrairProcessos.Name = "lblExtrairProcessos";
            this.lblExtrairProcessos.Size = new System.Drawing.Size(297, 67);
            this.lblExtrairProcessos.TabIndex = 1548;
            this.lblExtrairProcessos.Text = "Extrai processos com apresentação válida do arquivo PDF selecionado.";
            this.lblExtrairProcessos.Click += new System.EventHandler(this.btnExtrairProcessos_Click);
            // 
            // lblExtrairImagens
            // 
            this.lblExtrairImagens.BackColor = System.Drawing.Color.Teal;
            this.lblExtrairImagens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblExtrairImagens.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblExtrairImagens.ForeColor = System.Drawing.Color.White;
            this.lblExtrairImagens.Location = new System.Drawing.Point(11, 145);
            this.lblExtrairImagens.Name = "lblExtrairImagens";
            this.lblExtrairImagens.Size = new System.Drawing.Size(297, 67);
            this.lblExtrairImagens.TabIndex = 1545;
            this.lblExtrairImagens.Text = "Cria diretório e extrai imagens do arquivo PDF selecionado.";
            this.lblExtrairImagens.Click += new System.EventHandler(this.btnExtrairImagens_Click);
            // 
            // lblRelatorioExtracao
            // 
            this.lblRelatorioExtracao.BackColor = System.Drawing.Color.Teal;
            this.lblRelatorioExtracao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblRelatorioExtracao.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblRelatorioExtracao.ForeColor = System.Drawing.Color.White;
            this.lblRelatorioExtracao.Location = new System.Drawing.Point(11, 255);
            this.lblRelatorioExtracao.Name = "lblRelatorioExtracao";
            this.lblRelatorioExtracao.Size = new System.Drawing.Size(297, 67);
            this.lblRelatorioExtracao.TabIndex = 1546;
            this.lblRelatorioExtracao.Text = "Relatório com informações do processo de extração.";
            this.lblRelatorioExtracao.Click += new System.EventHandler(this.btnRelatorio_Click);
            // 
            // btnRelatorio
            // 
            this.btnRelatorio.BackColor = System.Drawing.Color.Teal;
            this.btnRelatorio.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btnRelatorio.FlatAppearance.BorderSize = 0;
            this.btnRelatorio.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnRelatorio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.btnRelatorio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.btnRelatorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRelatorio.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnRelatorio.ForeColor = System.Drawing.Color.White;
            this.btnRelatorio.Location = new System.Drawing.Point(4, 226);
            this.btnRelatorio.Name = "btnRelatorio";
            this.btnRelatorio.Size = new System.Drawing.Size(312, 107);
            this.btnRelatorio.TabIndex = 1544;
            this.btnRelatorio.Text = "Relatório de extração";
            this.btnRelatorio.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnRelatorio.UseVisualStyleBackColor = false;
            this.btnRelatorio.Click += new System.EventHandler(this.btnRelatorio_Click);
            // 
            // lblCarregarPDF
            // 
            this.lblCarregarPDF.BackColor = System.Drawing.Color.Teal;
            this.lblCarregarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCarregarPDF.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblCarregarPDF.ForeColor = System.Drawing.Color.White;
            this.lblCarregarPDF.Location = new System.Drawing.Point(12, 33);
            this.lblCarregarPDF.Name = "lblCarregarPDF";
            this.lblCarregarPDF.Size = new System.Drawing.Size(297, 67);
            this.lblCarregarPDF.TabIndex = 1547;
            this.lblCarregarPDF.Text = "Seleciona e carrega arquivo PDF em memória.";
            this.lblCarregarPDF.Click += new System.EventHandler(this.btnCarregarPDF_Click);
            // 
            // btnCarregarPDF
            // 
            this.btnCarregarPDF.BackColor = System.Drawing.Color.Teal;
            this.btnCarregarPDF.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btnCarregarPDF.FlatAppearance.BorderSize = 0;
            this.btnCarregarPDF.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnCarregarPDF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.btnCarregarPDF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.btnCarregarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCarregarPDF.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnCarregarPDF.ForeColor = System.Drawing.Color.White;
            this.btnCarregarPDF.Location = new System.Drawing.Point(4, 4);
            this.btnCarregarPDF.Name = "btnCarregarPDF";
            this.btnCarregarPDF.Size = new System.Drawing.Size(312, 107);
            this.btnCarregarPDF.TabIndex = 1540;
            this.btnCarregarPDF.Text = "Carregar PDF";
            this.btnCarregarPDF.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCarregarPDF.UseVisualStyleBackColor = false;
            this.btnCarregarPDF.Click += new System.EventHandler(this.btnCarregarPDF_Click);
            // 
            // btnExtrairProcessos
            // 
            this.btnExtrairProcessos.BackColor = System.Drawing.Color.Teal;
            this.btnExtrairProcessos.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btnExtrairProcessos.FlatAppearance.BorderSize = 0;
            this.btnExtrairProcessos.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnExtrairProcessos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.btnExtrairProcessos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.btnExtrairProcessos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtrairProcessos.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnExtrairProcessos.ForeColor = System.Drawing.Color.White;
            this.btnExtrairProcessos.Location = new System.Drawing.Point(320, 4);
            this.btnExtrairProcessos.Name = "btnExtrairProcessos";
            this.btnExtrairProcessos.Size = new System.Drawing.Size(312, 107);
            this.btnExtrairProcessos.TabIndex = 1541;
            this.btnExtrairProcessos.Text = "Extrair processos";
            this.btnExtrairProcessos.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExtrairProcessos.UseVisualStyleBackColor = false;
            this.btnExtrairProcessos.Click += new System.EventHandler(this.btnExtrairProcessos_Click);
            // 
            // btnExtrairImagens
            // 
            this.btnExtrairImagens.BackColor = System.Drawing.Color.Teal;
            this.btnExtrairImagens.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btnExtrairImagens.FlatAppearance.BorderSize = 0;
            this.btnExtrairImagens.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnExtrairImagens.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.btnExtrairImagens.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.btnExtrairImagens.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExtrairImagens.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnExtrairImagens.ForeColor = System.Drawing.Color.White;
            this.btnExtrairImagens.Location = new System.Drawing.Point(4, 115);
            this.btnExtrairImagens.Name = "btnExtrairImagens";
            this.btnExtrairImagens.Size = new System.Drawing.Size(312, 107);
            this.btnExtrairImagens.TabIndex = 1542;
            this.btnExtrairImagens.Text = "Extrair Imagens";
            this.btnExtrairImagens.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnExtrairImagens.UseVisualStyleBackColor = false;
            this.btnExtrairImagens.Click += new System.EventHandler(this.btnExtrairImagens_Click);
            // 
            // btnRenomearArquivos
            // 
            this.btnRenomearArquivos.BackColor = System.Drawing.Color.Teal;
            this.btnRenomearArquivos.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.btnRenomearArquivos.FlatAppearance.BorderSize = 0;
            this.btnRenomearArquivos.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnRenomearArquivos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Teal;
            this.btnRenomearArquivos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.btnRenomearArquivos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRenomearArquivos.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnRenomearArquivos.ForeColor = System.Drawing.Color.White;
            this.btnRenomearArquivos.Location = new System.Drawing.Point(320, 115);
            this.btnRenomearArquivos.Name = "btnRenomearArquivos";
            this.btnRenomearArquivos.Size = new System.Drawing.Size(312, 107);
            this.btnRenomearArquivos.TabIndex = 1543;
            this.btnRenomearArquivos.Text = "Renomear imagens extraídas";
            this.btnRenomearArquivos.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnRenomearArquivos.UseVisualStyleBackColor = false;
            this.btnRenomearArquivos.Click += new System.EventHandler(this.btnRenomearArquivos_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(636, 337);
            this.Controls.Add(this.btnClasseTeste);
            this.Controls.Add(this.lblRenomearArquivos);
            this.Controls.Add(this.lblExtrairProcessos);
            this.Controls.Add(this.lblExtrairImagens);
            this.Controls.Add(this.lblRelatorioExtracao);
            this.Controls.Add(this.btnRelatorio);
            this.Controls.Add(this.lblCarregarPDF);
            this.Controls.Add(this.btnCarregarPDF);
            this.Controls.Add(this.btnExtrairProcessos);
            this.Controls.Add(this.btnExtrairImagens);
            this.Controls.Add(this.btnRenomearArquivos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrincipal";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extrator | RPI 723 - 1380";
            this.Load += new System.EventHandler(this.frmOpen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdPDF;
        private System.Windows.Forms.Button btnRenomearArquivos;
        private System.Windows.Forms.Button btnExtrairImagens;
        private System.Windows.Forms.Button btnExtrairProcessos;
        private System.Windows.Forms.Button btnCarregarPDF;
        private System.Windows.Forms.Label lblCarregarPDF;
        private System.Windows.Forms.Button btnRelatorio;
        private System.Windows.Forms.Label lblRelatorioExtracao;
        private System.Windows.Forms.Label lblExtrairImagens;
        private System.Windows.Forms.Label lblExtrairProcessos;
        private System.Windows.Forms.Label lblRenomearArquivos;
        private System.Windows.Forms.Button btnClasseTeste;
    }
}