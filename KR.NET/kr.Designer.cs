namespace KR.NET
{
    partial class kr
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(kr));
            this.drv = new Microsoft.VisualBasic.Compatibility.VB6.DriveListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dirRadice = new Microsoft.VisualBasic.Compatibility.VB6.DirListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtChiave = new System.Windows.Forms.TextBox();
            this.btnVai = new System.Windows.Forms.Button();
            this.btnLogFile = new System.Windows.Forms.Button();
            this.btnDir = new System.Windows.Forms.Button();
            this.btnCreaFileList = new System.Windows.Forms.Button();
            this.chkEnableFileList = new System.Windows.Forms.CheckBox();
            this.txtFileList = new System.Windows.Forms.TextBox();
            this.lblStato = new System.Windows.Forms.Label();
            this.lstFileK = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkBlock = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // drv
            // 
            this.drv.FormattingEnabled = true;
            this.drv.Location = new System.Drawing.Point(15, 33);
            this.drv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.drv.Name = "drv";
            this.drv.Size = new System.Drawing.Size(273, 23);
            this.drv.TabIndex = 0;
            this.drv.SelectedIndexChanged += new System.EventHandler(this.drv_SelectedIndexChanged);
            this.drv.SelectedValueChanged += new System.EventHandler(this.drv_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Drive";
            // 
            // dirRadice
            // 
            this.dirRadice.FormattingEnabled = true;
            this.dirRadice.IntegralHeight = false;
            this.dirRadice.Location = new System.Drawing.Point(15, 84);
            this.dirRadice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dirRadice.Name = "dirRadice";
            this.dirRadice.Size = new System.Drawing.Size(273, 229);
            this.dirRadice.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "Directory radice";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 316);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Chiave";
            // 
            // txtChiave
            // 
            this.txtChiave.Location = new System.Drawing.Point(15, 336);
            this.txtChiave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtChiave.Name = "txtChiave";
            this.txtChiave.PasswordChar = '*';
            this.txtChiave.Size = new System.Drawing.Size(273, 22);
            this.txtChiave.TabIndex = 4;
            this.txtChiave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChiave_KeyPress);
            // 
            // btnVai
            // 
            this.btnVai.Location = new System.Drawing.Point(15, 374);
            this.btnVai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnVai.Name = "btnVai";
            this.btnVai.Size = new System.Drawing.Size(273, 31);
            this.btnVai.TabIndex = 3;
            this.btnVai.Text = "VAI";
            this.btnVai.UseVisualStyleBackColor = true;
            this.btnVai.Click += new System.EventHandler(this.btnVai_Click);
            // 
            // btnLogFile
            // 
            this.btnLogFile.Location = new System.Drawing.Point(15, 411);
            this.btnLogFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogFile.Name = "btnLogFile";
            this.btnLogFile.Size = new System.Drawing.Size(273, 31);
            this.btnLogFile.TabIndex = 10;
            this.btnLogFile.Text = "Log file";
            this.btnLogFile.UseVisualStyleBackColor = true;
            this.btnLogFile.Click += new System.EventHandler(this.btnLogFile_Click);
            // 
            // btnDir
            // 
            this.btnDir.Location = new System.Drawing.Point(15, 448);
            this.btnDir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(273, 31);
            this.btnDir.TabIndex = 11;
            this.btnDir.Text = "&Importa dal Copia/Incolla";
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // btnCreaFileList
            // 
            this.btnCreaFileList.Location = new System.Drawing.Point(15, 485);
            this.btnCreaFileList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreaFileList.Name = "btnCreaFileList";
            this.btnCreaFileList.Size = new System.Drawing.Size(273, 31);
            this.btnCreaFileList.TabIndex = 13;
            this.btnCreaFileList.Text = "Crea File List";
            this.btnCreaFileList.UseVisualStyleBackColor = true;
            this.btnCreaFileList.Click += new System.EventHandler(this.btnCreaFileList_Click);
            // 
            // chkEnableFileList
            // 
            this.chkEnableFileList.AutoSize = true;
            this.chkEnableFileList.Location = new System.Drawing.Point(15, 522);
            this.chkEnableFileList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkEnableFileList.Name = "chkEnableFileList";
            this.chkEnableFileList.Size = new System.Drawing.Size(128, 21);
            this.chkEnableFileList.TabIndex = 15;
            this.chkEnableFileList.Text = "Abilita Lista File";
            this.chkEnableFileList.UseVisualStyleBackColor = true;
            this.chkEnableFileList.CheckedChanged += new System.EventHandler(this.chkEnableFileList_CheckedChanged);
            // 
            // txtFileList
            // 
            this.txtFileList.Location = new System.Drawing.Point(15, 549);
            this.txtFileList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFileList.Name = "txtFileList";
            this.txtFileList.Size = new System.Drawing.Size(975, 22);
            this.txtFileList.TabIndex = 14;
            // 
            // lblStato
            // 
            this.lblStato.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStato.AutoEllipsis = true;
            this.lblStato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStato.Location = new System.Drawing.Point(15, 581);
            this.lblStato.Name = "lblStato";
            this.lblStato.Size = new System.Drawing.Size(973, 25);
            this.lblStato.TabIndex = 9;
            this.lblStato.Text = "Stato: PRONTO";
            // 
            // lstFileK
            // 
            this.lstFileK.FormattingEnabled = true;
            this.lstFileK.ItemHeight = 16;
            this.lstFileK.Location = new System.Drawing.Point(305, 36);
            this.lstFileK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstFileK.Name = "lstFileK";
            this.lstFileK.Size = new System.Drawing.Size(684, 500);
            this.lstFileK.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(301, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "Lista Files k";
            // 
            // chkBlock
            // 
            this.chkBlock.AutoSize = true;
            this.chkBlock.Checked = true;
            this.chkBlock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBlock.Location = new System.Drawing.Point(605, 9);
            this.chkBlock.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkBlock.Name = "chkBlock";
            this.chkBlock.Size = new System.Drawing.Size(72, 21);
            this.chkBlock.TabIndex = 12;
            this.chkBlock.Text = "Blocco";
            this.chkBlock.UseVisualStyleBackColor = true;
            // 
            // kr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 614);
            this.Controls.Add(this.chkBlock);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lstFileK);
            this.Controls.Add(this.lblStato);
            this.Controls.Add(this.txtFileList);
            this.Controls.Add(this.chkEnableFileList);
            this.Controls.Add(this.btnCreaFileList);
            this.Controls.Add(this.btnDir);
            this.Controls.Add(this.btnLogFile);
            this.Controls.Add(this.btnVai);
            this.Controls.Add(this.txtChiave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dirRadice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.drv);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "kr";
            this.Text = "KR";
            this.Activated += new System.EventHandler(this.kr_Activated);
            this.Load += new System.EventHandler(this.kr_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.VisualBasic.Compatibility.VB6.DriveListBox drv;
        private System.Windows.Forms.Label label2;
        private Microsoft.VisualBasic.Compatibility.VB6.DirListBox dirRadice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtChiave;
        private System.Windows.Forms.Button btnVai;
        private System.Windows.Forms.Button btnLogFile;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.Button btnCreaFileList;
        private System.Windows.Forms.CheckBox chkEnableFileList;
        private System.Windows.Forms.TextBox txtFileList;
        private System.Windows.Forms.Label lblStato;
        private System.Windows.Forms.ListBox lstFileK;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkBlock;
    }
}

