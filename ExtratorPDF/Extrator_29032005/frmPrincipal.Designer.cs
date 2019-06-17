namespace Extrator_29032005
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
            this.btnRelatorio = new System.Windows.Forms.Button();
            this.mskPaginaInicial = new System.Windows.Forms.MaskedTextBox();
            this.rdbAutomatico = new System.Windows.Forms.RadioButton();
            this.rdbManual = new System.Windows.Forms.RadioButton();
            this.lblProcessoAntigo = new System.Windows.Forms.Label();
            this.lblImagensAntigo = new System.Windows.Forms.Label();
            this.txtProcsIgnorados = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtImgIgnoradas = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnRenomearOLD = new System.Windows.Forms.Label();
            this.lblAutomatizar = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ofdPDF
            // 
            this.ofdPDF.FileName = "openFileDialog1";
            // 
            // btnRelatorio
            // 
            this.btnRelatorio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRelatorio.FlatAppearance.BorderSize = 0;
            this.btnRelatorio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(147)))), ((int)(((byte)(200)))));
            this.btnRelatorio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(147)))), ((int)(((byte)(200)))));
            this.btnRelatorio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRelatorio.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.btnRelatorio.ForeColor = System.Drawing.Color.White;
            this.btnRelatorio.Location = new System.Drawing.Point(335, 115);
            this.btnRelatorio.Name = "btnRelatorio";
            this.btnRelatorio.Size = new System.Drawing.Size(325, 107);
            this.btnRelatorio.TabIndex = 6;
            this.btnRelatorio.Text = "Relatório de extração";
            this.btnRelatorio.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnRelatorio.UseVisualStyleBackColor = false;
            this.btnRelatorio.Click += new System.EventHandler(this.btnRelatorio_Click);
            // 
            // mskPaginaInicial
            // 
            this.mskPaginaInicial.Enabled = false;
            this.mskPaginaInicial.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.mskPaginaInicial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(139)))), ((int)(((byte)(186)))));
            this.mskPaginaInicial.Location = new System.Drawing.Point(267, 80);
            this.mskPaginaInicial.Mask = "00000";
            this.mskPaginaInicial.Name = "mskPaginaInicial";
            this.mskPaginaInicial.PromptChar = ' ';
            this.mskPaginaInicial.Size = new System.Drawing.Size(60, 26);
            this.mskPaginaInicial.TabIndex = 3;
            this.mskPaginaInicial.ValidatingType = typeof(int);
            // 
            // rdbAutomatico
            // 
            this.rdbAutomatico.AutoSize = true;
            this.rdbAutomatico.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.rdbAutomatico.FlatAppearance.BorderSize = 0;
            this.rdbAutomatico.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(147)))), ((int)(((byte)(200)))));
            this.rdbAutomatico.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(147)))), ((int)(((byte)(200)))));
            this.rdbAutomatico.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbAutomatico.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.rdbAutomatico.ForeColor = System.Drawing.Color.White;
            this.rdbAutomatico.Location = new System.Drawing.Point(92, 82);
            this.rdbAutomatico.Name = "rdbAutomatico";
            this.rdbAutomatico.Size = new System.Drawing.Size(97, 23);
            this.rdbAutomatico.TabIndex = 1;
            this.rdbAutomatico.TabStop = true;
            this.rdbAutomatico.Text = "Automático";
            this.rdbAutomatico.UseVisualStyleBackColor = false;
            this.rdbAutomatico.CheckedChanged += new System.EventHandler(this.rdbAutomatico_CheckedChanged);
            // 
            // rdbManual
            // 
            this.rdbManual.AutoSize = true;
            this.rdbManual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.rdbManual.FlatAppearance.BorderSize = 0;
            this.rdbManual.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(147)))), ((int)(((byte)(200)))));
            this.rdbManual.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(147)))), ((int)(((byte)(200)))));
            this.rdbManual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbManual.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.rdbManual.ForeColor = System.Drawing.Color.White;
            this.rdbManual.Location = new System.Drawing.Point(195, 82);
            this.rdbManual.Name = "rdbManual";
            this.rdbManual.Size = new System.Drawing.Size(72, 23);
            this.rdbManual.TabIndex = 2;
            this.rdbManual.TabStop = true;
            this.rdbManual.Text = "Manual";
            this.rdbManual.UseVisualStyleBackColor = false;
            this.rdbManual.CheckedChanged += new System.EventHandler(this.rdbManual_CheckedChanged);
            // 
            // lblProcessoAntigo
            // 
            this.lblProcessoAntigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblProcessoAntigo.Font = new System.Drawing.Font("Segoe UI", 12.25F);
            this.lblProcessoAntigo.ForeColor = System.Drawing.Color.White;
            this.lblProcessoAntigo.Location = new System.Drawing.Point(4, 4);
            this.lblProcessoAntigo.Name = "lblProcessoAntigo";
            this.lblProcessoAntigo.Size = new System.Drawing.Size(327, 107);
            this.lblProcessoAntigo.TabIndex = 1506;
            this.lblProcessoAntigo.Text = "Extrair processos [OLD}";
            this.lblProcessoAntigo.Click += new System.EventHandler(this.lblProcessoAntigo_Click);
            // 
            // lblImagensAntigo
            // 
            this.lblImagensAntigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblImagensAntigo.Font = new System.Drawing.Font("Segoe UI", 12.25F);
            this.lblImagensAntigo.ForeColor = System.Drawing.Color.White;
            this.lblImagensAntigo.Location = new System.Drawing.Point(335, 4);
            this.lblImagensAntigo.Name = "lblImagensAntigo";
            this.lblImagensAntigo.Size = new System.Drawing.Size(327, 107);
            this.lblImagensAntigo.TabIndex = 1522;
            this.lblImagensAntigo.Text = "Extrair imagens [OLD}";
            this.lblImagensAntigo.Click += new System.EventHandler(this.lblImagensAntigo_Click);
            // 
            // txtProcsIgnorados
            // 
            this.txtProcsIgnorados.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.txtProcsIgnorados.Location = new System.Drawing.Point(8, 254);
            this.txtProcsIgnorados.Name = "txtProcsIgnorados";
            this.txtProcsIgnorados.Size = new System.Drawing.Size(651, 26);
            this.txtProcsIgnorados.TabIndex = 1523;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label6.Location = new System.Drawing.Point(4, 232);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(322, 19);
            this.label6.TabIndex = 1524;
            this.label6.Text = "Ignorar processos - Utilizar | (pipe) como separador";
            // 
            // txtImgIgnoradas
            // 
            this.txtImgIgnoradas.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.txtImgIgnoradas.Location = new System.Drawing.Point(8, 305);
            this.txtImgIgnoradas.Name = "txtImgIgnoradas";
            this.txtImgIgnoradas.Size = new System.Drawing.Size(651, 26);
            this.txtImgIgnoradas.TabIndex = 1530;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.label9.Location = new System.Drawing.Point(7, 283);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(313, 19);
            this.label9.TabIndex = 1531;
            this.label9.Text = "Ignorar imagens - Utilizar | (pipe) como separador";
            // 
            // btnRenomearOLD
            // 
            this.btnRenomearOLD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRenomearOLD.Font = new System.Drawing.Font("Segoe UI", 12.25F);
            this.btnRenomearOLD.ForeColor = System.Drawing.Color.White;
            this.btnRenomearOLD.Location = new System.Drawing.Point(4, 115);
            this.btnRenomearOLD.Name = "btnRenomearOLD";
            this.btnRenomearOLD.Size = new System.Drawing.Size(327, 107);
            this.btnRenomearOLD.TabIndex = 1532;
            this.btnRenomearOLD.Text = "Finalizar extração [OLD}";
            this.btnRenomearOLD.Click += new System.EventHandler(this.btnRenomearOLD_Click);
            // 
            // lblAutomatizar
            // 
            this.lblAutomatizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lblAutomatizar.Font = new System.Drawing.Font("Segoe UI", 10.25F);
            this.lblAutomatizar.ForeColor = System.Drawing.Color.White;
            this.lblAutomatizar.Location = new System.Drawing.Point(337, 335);
            this.lblAutomatizar.Name = "lblAutomatizar";
            this.lblAutomatizar.Size = new System.Drawing.Size(325, 58);
            this.lblAutomatizar.TabIndex = 1533;
            this.lblAutomatizar.Text = "Automatizar extração";
            this.lblAutomatizar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAutomatizar.Click += new System.EventHandler(this.llAutomatizar_Click);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(666, 397);
            this.Controls.Add(this.lblAutomatizar);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtImgIgnoradas);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtProcsIgnorados);
            this.Controls.Add(this.lblImagensAntigo);
            this.Controls.Add(this.btnRelatorio);
            this.Controls.Add(this.rdbManual);
            this.Controls.Add(this.rdbAutomatico);
            this.Controls.Add(this.mskPaginaInicial);
            this.Controls.Add(this.lblProcessoAntigo);
            this.Controls.Add(this.btnRenomearOLD);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrincipal";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Extrator";
            this.Load += new System.EventHandler(this.frmOpen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdPDF;
        private System.Windows.Forms.Button btnRelatorio;
        private System.Windows.Forms.MaskedTextBox mskPaginaInicial;
        private System.Windows.Forms.RadioButton rdbAutomatico;
        private System.Windows.Forms.RadioButton rdbManual;
        private System.Windows.Forms.Label lblProcessoAntigo;
        private System.Windows.Forms.Label lblImagensAntigo;
        private System.Windows.Forms.TextBox txtProcsIgnorados;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtImgIgnoradas;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label btnRenomearOLD;
        private System.Windows.Forms.Label lblAutomatizar;
    }
}