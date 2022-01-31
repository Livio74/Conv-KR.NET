using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.MessageBox;
using MessageBoxButtons = System.Windows.MessageBoxButton;
using KRLib.NET;

namespace KR.NET
{
    public partial class minKR : Form
    {
        public minKR()
        {
            InitializeComponent();
        }

        private void ManageControls(bool boolInvert)
        {
            lblStato.Visible = boolInvert;
            btnVai.Visible = !boolInvert; txtChiave.Visible = !boolInvert;
            btnEsci.Visible = !boolInvert;
        }

        private void btnEsci_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minKR_Load(object sender, EventArgs e)
        {
            ManageControls(false);
            if (! "".Equals(MOD_MAIN.G_strChiave))
                txtChiave.Text = MOD_MAIN.G_strChiave;
        }

        private void btnVai_Click(object sender, EventArgs e)
        {
            EseguiKLog();
        }

        private void minKR_Activated(object sender, EventArgs e)
        {
            if (!"".Equals(txtChiave.Text))
                Esegui();
        }

        private void txtChiave_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strS = "";
            if (e.KeyChar == ((char)13))
                Esegui();
        }

        private void Esegui()
        {
            string strS = txtChiave.Text;
            ///strS = MOD_KLOG.CaricaLogFile(strS, MOD_MAIN.G_strFileLog); Non so come fa a funzionare nell'originale ma lo correggo
            string strS1 = MOD_KLOG.CaricaLogFile(strS, MOD_MAIN.G_strFileLog);
            if ("".Equals(strS1))
            {
                MessageBox.Show("Chiave inserita non corretta", "Esegui crypt dei file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
            else
            {
                MOD_KLOG.RigeneraLog(strS, MOD_MAIN.G_strDirRoot, MOD_MAIN.G_strFileLog, MOD_MAIN.G_strDirRoot);
                EseguiKLog();
            }
        }

        private void EseguiKLog()
        {
            string strNomeFile = "", strS = "", strIn = "";
            string strErr = ""; long lngCount = 0, i = 0;
            ManageControls(true);
            //2. CaricaLogFile
            strS = txtChiave.Text;
            strS = MOD_KLOG.CaricaLogFile(strS, MOD_MAIN.G_strFileLog);
            if ("".Equals(strS))
            {
                MessageBox.Show("Chiave inserita non valida", "Esegui crypt dei file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
            if (MOD_MAIN.G_bolErrLog)
            {
                MessageBox.Show("ORMATO LOG NON VALIDO", "Esegui crypt dei file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
            //3. Generazione della lista di tutti i file
            lblStato.Text = "Stato: Generazione della lista dei file da criptare....";
            lblStato.Refresh();
            strErr = MOD_FILE_LIST.Genera(1, MOD_MAIN.G_strDirRoot, MOD_MAIN.G_strFileLog);
            if (!"".Equals(strErr))
            {
                MessageBox.Show(strErr, "Esegui crypt dei file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
            //Criptatura di ogni file della lista
            lngCount = MOD_FILE_LIST.GetSize();
            if (lngCount <=0)
            {
                MessageBox.Show(strErr, "Non sono stati trovati files all'interno della directory", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                this.Close();
            }
            for (i = 0; i < lngCount; i++)
            {
                //3.1 Crpt del file(che cambia anche nome)
                strNomeFile = MOD_FILE_LIST.GetFile(i);
                //3.2 Conversione chiave da testo ad array di byte
                if (i % 1000 == 0) Application.DoEvents();
                lblStato.Text = "Stato: kritp di " + i.ToString() + "/" + lngCount.ToString();
                lblStato.Refresh();
                MOD_PRG_UTILS.Kriptp(strNomeFile, txtChiave.Text);
            }
            //4. SalvaLogFile
            MOD_KLOG.SalvaLogFile(txtChiave.Text, MOD_MAIN.G_strFileLog);
            //5. Operazione Completata
            MessageBox.Show("Operazione completata", "Esegui crypt dei file", MessageBoxButtons.OK, MessageBoxImage.Information);
            lblStato.Text = "Stato: PRONTO!";
            lblStato.Refresh();
            if (!"".Equals(MOD_MAIN.G_strErr))
            {
                MOD_MAIN.G_strErr = "<?xml version=\"1.01\" encoding=\"UTF-8\"?><EXCEPTIONS DATETIME=\"" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "\">" + MOD_MAIN.G_strErr + "</EXCEPTIONS>";
                MOD_UTILS_SO.ErrorLog(MOD_MAIN.G_strErr, Application.StartupPath);
            }
            Application.Exit();
        }
    }
}
