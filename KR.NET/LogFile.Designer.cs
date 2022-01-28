
namespace KR.NET
{
    partial class LogFile
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
            this.btnEsci = new System.Windows.Forms.Button();
            this.lstDir = new System.Windows.Forms.ListBox();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.btnConferma = new System.Windows.Forms.Button();
            this.btnVisualizzaFiles = new System.Windows.Forms.Button();
            this.btnStato = new System.Windows.Forms.Button();
            this.Rigenera = new System.Windows.Forms.Button();
            this.chkVisualNE = new System.Windows.Forms.CheckBox();
            this.chkVisualE = new System.Windows.Forms.CheckBox();
            this.chkVisualNK = new System.Windows.Forms.CheckBox();
            this.chkVisualK = new System.Windows.Forms.CheckBox();
            this.OptStato0 = new System.Windows.Forms.RadioButton();
            this.OptStato1 = new System.Windows.Forms.RadioButton();
            this.OptStato2 = new System.Windows.Forms.RadioButton();
            this.chkSubDirs = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnEsci
            // 
            this.btnEsci.Location = new System.Drawing.Point(668, 557);
            this.btnEsci.Name = "btnEsci";
            this.btnEsci.Size = new System.Drawing.Size(146, 43);
            this.btnEsci.TabIndex = 0;
            this.btnEsci.Text = "Esci";
            this.btnEsci.UseVisualStyleBackColor = true;
            this.btnEsci.Click += new System.EventHandler(this.btnEsci_Click);
            // 
            // lstDir
            // 
            this.lstDir.FormattingEnabled = true;
            this.lstDir.ItemHeight = 16;
            this.lstDir.Location = new System.Drawing.Point(12, 12);
            this.lstDir.Name = "lstDir";
            this.lstDir.Size = new System.Drawing.Size(800, 324);
            this.lstDir.TabIndex = 1;
            // 
            // lstFiles
            // 
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.ItemHeight = 16;
            this.lstFiles.Location = new System.Drawing.Point(12, 352);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(343, 180);
            this.lstFiles.TabIndex = 2;
            // 
            // btnConferma
            // 
            this.btnConferma.Location = new System.Drawing.Point(508, 557);
            this.btnConferma.Name = "btnConferma";
            this.btnConferma.Size = new System.Drawing.Size(150, 43);
            this.btnConferma.TabIndex = 3;
            this.btnConferma.Text = "CONFERMA";
            this.btnConferma.UseVisualStyleBackColor = true;
            this.btnConferma.Click += new System.EventHandler(this.btnConferma_Click);
            // 
            // btnVisualizzaFiles
            // 
            this.btnVisualizzaFiles.Location = new System.Drawing.Point(12, 557);
            this.btnVisualizzaFiles.Name = "btnVisualizzaFiles";
            this.btnVisualizzaFiles.Size = new System.Drawing.Size(150, 43);
            this.btnVisualizzaFiles.TabIndex = 4;
            this.btnVisualizzaFiles.Text = "Visualizza File";
            this.btnVisualizzaFiles.UseVisualStyleBackColor = true;
            // 
            // btnStato
            // 
            this.btnStato.Location = new System.Drawing.Point(168, 557);
            this.btnStato.Name = "btnStato";
            this.btnStato.Size = new System.Drawing.Size(150, 43);
            this.btnStato.TabIndex = 5;
            this.btnStato.Text = "Cambia stato";
            this.btnStato.UseVisualStyleBackColor = true;
            this.btnStato.Click += new System.EventHandler(this.btnStato_Click);
            // 
            // Rigenera
            // 
            this.Rigenera.Location = new System.Drawing.Point(324, 557);
            this.Rigenera.Name = "Rigenera";
            this.Rigenera.Size = new System.Drawing.Size(150, 43);
            this.Rigenera.TabIndex = 6;
            this.Rigenera.Text = "Rigenera log";
            this.Rigenera.UseVisualStyleBackColor = true;
            // 
            // chkVisualNE
            // 
            this.chkVisualNE.AutoSize = true;
            this.chkVisualNE.Checked = true;
            this.chkVisualNE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVisualNE.Location = new System.Drawing.Point(376, 363);
            this.chkVisualNE.Name = "chkVisualNE";
            this.chkVisualNE.Size = new System.Drawing.Size(138, 21);
            this.chkVisualNE.TabIndex = 7;
            this.chkVisualNE.Text = "Visualizza file NE";
            this.chkVisualNE.UseVisualStyleBackColor = true;
            this.chkVisualNE.CheckedChanged += new System.EventHandler(this.chkVisualNE_CheckedChanged);
            // 
            // chkVisualE
            // 
            this.chkVisualE.AutoSize = true;
            this.chkVisualE.Checked = true;
            this.chkVisualE.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVisualE.Location = new System.Drawing.Point(376, 399);
            this.chkVisualE.Name = "chkVisualE";
            this.chkVisualE.Size = new System.Drawing.Size(128, 21);
            this.chkVisualE.TabIndex = 8;
            this.chkVisualE.Text = "Visualizza file E";
            this.chkVisualE.UseVisualStyleBackColor = true;
            this.chkVisualE.CheckedChanged += new System.EventHandler(this.chkVisualE_CheckedChanged);
            // 
            // chkVisualNK
            // 
            this.chkVisualNK.AutoSize = true;
            this.chkVisualNK.Checked = true;
            this.chkVisualNK.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVisualNK.Location = new System.Drawing.Point(376, 438);
            this.chkVisualNK.Name = "chkVisualNK";
            this.chkVisualNK.Size = new System.Drawing.Size(138, 21);
            this.chkVisualNK.TabIndex = 9;
            this.chkVisualNK.Text = "Visualizza file NK";
            this.chkVisualNK.UseVisualStyleBackColor = true;
            this.chkVisualNK.CheckedChanged += new System.EventHandler(this.chkVisualNK_CheckedChanged);
            // 
            // chkVisualK
            // 
            this.chkVisualK.AutoSize = true;
            this.chkVisualK.Checked = true;
            this.chkVisualK.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVisualK.Location = new System.Drawing.Point(376, 479);
            this.chkVisualK.Name = "chkVisualK";
            this.chkVisualK.Size = new System.Drawing.Size(128, 21);
            this.chkVisualK.TabIndex = 10;
            this.chkVisualK.Text = "Visualizza file K";
            this.chkVisualK.UseVisualStyleBackColor = true;
            this.chkVisualK.CheckedChanged += new System.EventHandler(this.chkVisualK_CheckedChanged);
            // 
            // OptStato0
            // 
            this.OptStato0.AutoSize = true;
            this.OptStato0.Location = new System.Drawing.Point(606, 362);
            this.OptStato0.Name = "OptStato0";
            this.OptStato0.Size = new System.Drawing.Size(75, 21);
            this.OptStato0.TabIndex = 11;
            this.OptStato0.Text = "Stato E";
            this.OptStato0.UseVisualStyleBackColor = true;
            // 
            // OptStato1
            // 
            this.OptStato1.AutoSize = true;
            this.OptStato1.Location = new System.Drawing.Point(605, 399);
            this.OptStato1.Name = "OptStato1";
            this.OptStato1.Size = new System.Drawing.Size(76, 21);
            this.OptStato1.TabIndex = 12;
            this.OptStato1.Text = "Stato D";
            this.OptStato1.UseVisualStyleBackColor = true;
            // 
            // OptStato2
            // 
            this.OptStato2.AutoSize = true;
            this.OptStato2.Checked = true;
            this.OptStato2.Location = new System.Drawing.Point(606, 438);
            this.OptStato2.Name = "OptStato2";
            this.OptStato2.Size = new System.Drawing.Size(125, 21);
            this.OptStato2.TabIndex = 13;
            this.OptStato2.TabStop = true;
            this.OptStato2.Text = "Stato Cambiato";
            this.OptStato2.UseVisualStyleBackColor = true;
            // 
            // chkSubDirs
            // 
            this.chkSubDirs.AutoSize = true;
            this.chkSubDirs.Location = new System.Drawing.Point(606, 479);
            this.chkSubDirs.Name = "chkSubDirs";
            this.chkSubDirs.Size = new System.Drawing.Size(205, 26);
            this.chkSubDirs.TabIndex = 14;
            this.chkSubDirs.Text = "Anche sotto directory";
            this.chkSubDirs.UseVisualStyleBackColor = true;
            // 
            // LogFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 606);
            this.Controls.Add(this.chkSubDirs);
            this.Controls.Add(this.OptStato2);
            this.Controls.Add(this.OptStato1);
            this.Controls.Add(this.OptStato0);
            this.Controls.Add(this.chkVisualK);
            this.Controls.Add(this.chkVisualNK);
            this.Controls.Add(this.chkVisualE);
            this.Controls.Add(this.chkVisualNE);
            this.Controls.Add(this.Rigenera);
            this.Controls.Add(this.btnStato);
            this.Controls.Add(this.btnVisualizzaFiles);
            this.Controls.Add(this.btnConferma);
            this.Controls.Add(this.lstFiles);
            this.Controls.Add(this.lstDir);
            this.Controls.Add(this.btnEsci);
            this.Name = "LogFile";
            this.Text = "LogFile";
            this.Load += new System.EventHandler(this.LogFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEsci;
        private System.Windows.Forms.ListBox lstDir;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Button btnConferma;
        private System.Windows.Forms.Button btnVisualizzaFiles;
        private System.Windows.Forms.Button btnStato;
        private System.Windows.Forms.Button Rigenera;
        private System.Windows.Forms.CheckBox chkVisualNE;
        private System.Windows.Forms.CheckBox chkVisualE;
        private System.Windows.Forms.CheckBox chkVisualNK;
        private System.Windows.Forms.CheckBox chkVisualK;
        private System.Windows.Forms.RadioButton OptStato0;
        private System.Windows.Forms.RadioButton OptStato1;
        private System.Windows.Forms.RadioButton OptStato2;
        private System.Windows.Forms.CheckBox chkSubDirs;
    }
}