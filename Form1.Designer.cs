namespace Monitor_Confere_Estoque
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCaminhoTxt = new System.Windows.Forms.TextBox();
            this.textBoxChavePublica = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.btnSalvarChave = new System.Windows.Forms.Button();
            this.btnHabilitaAlterar = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnReiniciarAplicacao = new System.Windows.Forms.Button();
            this.btnExcluirLinhaDatagrid = new System.Windows.Forms.Button();
            this.brnSalvarCaminhoTxt = new System.Windows.Forms.Button();
            this.chavePublicaDataGrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewChaves = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChaves)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Chave Pública:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(12, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "Salvar .txt em:";
            // 
            // textBoxCaminhoTxt
            // 
            this.textBoxCaminhoTxt.Location = new System.Drawing.Point(140, 81);
            this.textBoxCaminhoTxt.Name = "textBoxCaminhoTxt";
            this.textBoxCaminhoTxt.ReadOnly = true;
            this.textBoxCaminhoTxt.Size = new System.Drawing.Size(269, 23);
            this.textBoxCaminhoTxt.TabIndex = 1;
            // 
            // textBoxChavePublica
            // 
            this.textBoxChavePublica.Location = new System.Drawing.Point(140, 53);
            this.textBoxChavePublica.Name = "textBoxChavePublica";
            this.textBoxChavePublica.ReadOnly = true;
            this.textBoxChavePublica.Size = new System.Drawing.Size(269, 23);
            this.textBoxChavePublica.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Panfleta Stencil ExtBd", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label3.Location = new System.Drawing.Point(35, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(431, 47);
            this.label3.TabIndex = 4;
            this.label3.Text = "MONITOR CONFERE ESTOQUE";
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.HorizontalScrollbar = true;
            this.listBoxLog.ItemHeight = 15;
            this.listBoxLog.Location = new System.Drawing.Point(14, 353);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(480, 139);
            this.listBoxLog.TabIndex = 5;
            // 
            // btnSalvarChave
            // 
            this.btnSalvarChave.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnSalvarChave.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSalvarChave.Location = new System.Drawing.Point(415, 51);
            this.btnSalvarChave.Name = "btnSalvarChave";
            this.btnSalvarChave.Size = new System.Drawing.Size(80, 25);
            this.btnSalvarChave.TabIndex = 6;
            this.btnSalvarChave.Text = "Salvar";
            this.btnSalvarChave.UseVisualStyleBackColor = false;
            this.btnSalvarChave.Click += new System.EventHandler(this.btnSalvarConfig_Click);
            // 
            // btnHabilitaAlterar
            // 
            this.btnHabilitaAlterar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnHabilitaAlterar.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnHabilitaAlterar.Location = new System.Drawing.Point(12, 110);
            this.btnHabilitaAlterar.Name = "btnHabilitaAlterar";
            this.btnHabilitaAlterar.Size = new System.Drawing.Size(236, 40);
            this.btnHabilitaAlterar.TabIndex = 7;
            this.btnHabilitaAlterar.Text = "Habilitar edição";
            this.btnHabilitaAlterar.UseVisualStyleBackColor = false;
            this.btnHabilitaAlterar.Click += new System.EventHandler(this.btnHabilitaAlterar_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Monitor Confere Estoque";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // btnReiniciarAplicacao
            // 
            this.btnReiniciarAplicacao.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnReiniciarAplicacao.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnReiniciarAplicacao.Location = new System.Drawing.Point(259, 110);
            this.btnReiniciarAplicacao.Name = "btnReiniciarAplicacao";
            this.btnReiniciarAplicacao.Size = new System.Drawing.Size(236, 40);
            this.btnReiniciarAplicacao.TabIndex = 9;
            this.btnReiniciarAplicacao.Text = "Reiniciar";
            this.btnReiniciarAplicacao.UseVisualStyleBackColor = false;
            this.btnReiniciarAplicacao.Click += new System.EventHandler(this.btnReiniciarAplicacao_Click);
            // 
            // btnExcluirLinhaDatagrid
            // 
            this.btnExcluirLinhaDatagrid.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnExcluirLinhaDatagrid.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnExcluirLinhaDatagrid.Location = new System.Drawing.Point(15, 309);
            this.btnExcluirLinhaDatagrid.Name = "btnExcluirLinhaDatagrid";
            this.btnExcluirLinhaDatagrid.Size = new System.Drawing.Size(479, 40);
            this.btnExcluirLinhaDatagrid.TabIndex = 13;
            this.btnExcluirLinhaDatagrid.Text = "Excluir linha selecionada";
            this.btnExcluirLinhaDatagrid.UseVisualStyleBackColor = false;
            this.btnExcluirLinhaDatagrid.Click += new System.EventHandler(this.btnExcluirLinhaDatagrid_Click);
            // 
            // brnSalvarCaminhoTxt
            // 
            this.brnSalvarCaminhoTxt.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.brnSalvarCaminhoTxt.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.brnSalvarCaminhoTxt.Location = new System.Drawing.Point(414, 79);
            this.brnSalvarCaminhoTxt.Name = "brnSalvarCaminhoTxt";
            this.brnSalvarCaminhoTxt.Size = new System.Drawing.Size(80, 25);
            this.brnSalvarCaminhoTxt.TabIndex = 14;
            this.brnSalvarCaminhoTxt.Text = "Salvar";
            this.brnSalvarCaminhoTxt.UseVisualStyleBackColor = false;
            this.brnSalvarCaminhoTxt.Click += new System.EventHandler(this.brnSalvarCaminhoTxt_Click);
            // 
            // chavePublicaDataGrid
            // 
            this.chavePublicaDataGrid.HeaderText = "Chave pública";
            this.chavePublicaDataGrid.Name = "chavePublicaDataGrid";
            this.chavePublicaDataGrid.ReadOnly = true;
            this.chavePublicaDataGrid.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.chavePublicaDataGrid.Width = 287;
            // 
            // usuario
            // 
            this.usuario.HeaderText = "Nome usuário";
            this.usuario.Name = "usuario";
            this.usuario.ReadOnly = true;
            this.usuario.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.usuario.Width = 150;
            // 
            // dataGridViewChaves
            // 
            this.dataGridViewChaves.AllowUserToAddRows = false;
            this.dataGridViewChaves.AllowUserToDeleteRows = false;
            this.dataGridViewChaves.AllowUserToResizeColumns = false;
            this.dataGridViewChaves.AllowUserToResizeRows = false;
            this.dataGridViewChaves.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dataGridViewChaves.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewChaves.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chavePublicaDataGrid,
            this.usuario});
            this.dataGridViewChaves.Location = new System.Drawing.Point(15, 161);
            this.dataGridViewChaves.Name = "dataGridViewChaves";
            this.dataGridViewChaves.ReadOnly = true;
            this.dataGridViewChaves.Size = new System.Drawing.Size(480, 143);
            this.dataGridViewChaves.TabIndex = 12;
            this.dataGridViewChaves.Text = "dataGridView1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(321, 495);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(173, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "Developed by Waldir Tiago Dias";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(506, 519);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.brnSalvarCaminhoTxt);
            this.Controls.Add(this.btnExcluirLinhaDatagrid);
            this.Controls.Add(this.dataGridViewChaves);
            this.Controls.Add(this.btnReiniciarAplicacao);
            this.Controls.Add(this.btnHabilitaAlterar);
            this.Controls.Add(this.btnSalvarChave);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxChavePublica);
            this.Controls.Add(this.textBoxCaminhoTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Monitor Confere Estoque";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChaves)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCaminhoTxt;
        private System.Windows.Forms.TextBox textBoxChavePublica;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Button btnSalvarChave;
        private System.Windows.Forms.Button btnHabilitaAlterar;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button btnReiniciarAplicacao;
        private System.Windows.Forms.DataGridView dataGridViewChaves;
        private System.Windows.Forms.Button btnExcluirLinhaDatagrid;
        private System.Windows.Forms.Button brnSalvarCaminhoTxt;
        private System.Windows.Forms.DataGridViewTextBoxColumn chavePublicaDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuario;
        private System.Windows.Forms.Label label5;
    }
}

