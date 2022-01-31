
namespace KR.NET
{
    partial class minKR
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
            this.txtChiave = new System.Windows.Forms.TextBox();
            this.btnVai = new System.Windows.Forms.Button();
            this.btnEsci = new System.Windows.Forms.Button();
            this.lblStato = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtChiave
            // 
            this.txtChiave.Location = new System.Drawing.Point(12, 11);
            this.txtChiave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtChiave.Name = "txtChiave";
            this.txtChiave.PasswordChar = '*';
            this.txtChiave.Size = new System.Drawing.Size(693, 22);
            this.txtChiave.TabIndex = 5;
            this.txtChiave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChiave_KeyPress);
            // 
            // btnVai
            // 
            this.btnVai.Location = new System.Drawing.Point(711, 10);
            this.btnVai.Name = "btnVai";
            this.btnVai.Size = new System.Drawing.Size(75, 23);
            this.btnVai.TabIndex = 6;
            this.btnVai.Text = "VAI";
            this.btnVai.UseVisualStyleBackColor = true;
            this.btnVai.Click += new System.EventHandler(this.btnVai_Click);
            // 
            // btnEsci
            // 
            this.btnEsci.Location = new System.Drawing.Point(792, 10);
            this.btnEsci.Name = "btnEsci";
            this.btnEsci.Size = new System.Drawing.Size(75, 23);
            this.btnEsci.TabIndex = 7;
            this.btnEsci.Text = "Esci";
            this.btnEsci.UseVisualStyleBackColor = true;
            this.btnEsci.Click += new System.EventHandler(this.btnEsci_Click);
            // 
            // lblStato
            // 
            this.lblStato.AutoEllipsis = true;
            this.lblStato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStato.Location = new System.Drawing.Point(12, 8);
            this.lblStato.Name = "lblStato";
            this.lblStato.Size = new System.Drawing.Size(858, 25);
            this.lblStato.TabIndex = 10;
            this.lblStato.Text = "Stato: PRONTO";
            // 
            // minKR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 40);
            this.Controls.Add(this.btnEsci);
            this.Controls.Add(this.btnVai);
            this.Controls.Add(this.txtChiave);
            this.Controls.Add(this.lblStato);
            this.Name = "minKR";
            this.Text = "minKR";
            this.Activated += new System.EventHandler(this.minKR_Activated);
            this.Load += new System.EventHandler(this.minKR_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtChiave;
        private System.Windows.Forms.Button btnVai;
        private System.Windows.Forms.Button btnEsci;
        private System.Windows.Forms.Label lblStato;
    }
}