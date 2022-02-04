using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
    public partial class kr : Form
    {

        string strFileLog = "";

        public kr()
        {
            InitializeComponent();
        }

        private void drv_SelectedIndexChanged(object sender, EventArgs e)
        {
            dirRadice.Path  = drv.Drive.Substring(0,2) + "\\";
            strFileLog = dirRadice.Text + "\\klog.txt";
        }

        private void drv_SelectedValueChanged(object sender, EventArgs e)
        {
            dirRadice.Path = drv.Drive.Substring(0, 2) + "\\";
            strFileLog = dirRadice.Text + "\\klog.txt";
        }

        private void kr_Load(object sender, EventArgs e)
        {
            if ("".Equals(MOD_MAIN.G_strDirRoot))
            {
                drv.Drive = "C:";
                dirRadice.Path = "C:\\";
                MOD_MAIN.G_lng_NumFiles = 0;
            }
            else
            {
                drv.Drive = MOD_MAIN.G_strDirRoot.Substring(0,2);
                dirRadice.Path = MOD_MAIN.G_strDirRoot;
                chkBlock.Checked = false;
                MOD_MAIN.G_lng_NumFiles = 0;
            }
            strFileLog = dirRadice.Path + "\\klog.txt";
            MOD_MAIN.G_bolEsisteLog = false;
        }

        private void kr_Activated(object sender, EventArgs e)
        {
            txtChiave.Focus();
        }

        private void btnCreaFileList_Click(object sender, EventArgs e)
        {
            string strS = "";
            if ("".Equals(txtFileList.Text))
            {
                MessageBox.Show("Non è possibile creare il file \"Lista file\"", "Creazione lista file", MessageBoxButtons.OK , MessageBoxImage.Exclamation);
                return;
            }
            if (MOD_UTILS_SO.ExistsFile(txtFileList.Text))
            {
                MessageBoxResult overwrite = MessageBox.Show("Vuoi sovrascrivere il file esistente?", "Creazione lista file", MessageBoxButtons.YesNo, MessageBoxImage.Question);
                if (overwrite.Equals(MessageBoxResult.No))
                {
                    return;
                }
            }
            lblStato.Text = "Stato: Generazione della lista di file ....";
            lblStato.Refresh();
            strS = MOD_UTILS_SO.SalvaListaFile(txtFileList.Text, dirRadice.Path, txtChiave.Text, strFileLog);
            if (!"".Equals(strS))
            {
                MessageBox.Show(strS, "Creazione lista file", MessageBoxButtons.OK , MessageBoxImage.Exclamation);
            }
            lblStato.Text = "Stato: PRONTO!";
            lblStato.Refresh();
        }

        private void chkEnableFileList_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableFileList.Checked)
                txtFileList.Text = MOD_MAIN.G_strFileList;
            else
                txtFileList.Text = "";
        }

        private void btnDir_Click(object sender, EventArgs e)
        {
            string clipboardPath = Clipboard.GetText();
            if (!"".Equals(clipboardPath)) {
                if (Directory.Exists(clipboardPath))
                {
                    if (STATICUTILS.CheckSystemOrCriticalFolder(clipboardPath))
                    {
                        chkBlock.Checked = true;
                    }
                    else
                    {
                        chkBlock.Checked = false;
                    }
                    drv.Drive = clipboardPath.Substring(0, 2); //Correzione BUG rispestto a originale
                    dirRadice.Path = clipboardPath;
                    MOD_MAIN.G_strFileList = clipboardPath + "\\FileList.txt";
                    strFileLog = clipboardPath + "\\klog.txt";
                    txtChiave.Focus();
                }
            }
        }

        private void btnLogFile_Click(object sender, EventArgs e)
        {
            MOD_MAIN.G_strDirRoot = dirRadice.Path;
            MOD_MAIN.G_strFileLog = dirRadice.Path + "\\klog.txt";
            LogFile logFileWnd = new LogFile(this);
            logFileWnd.Show();
        }

        private void EseguiConKLog()
        {
            string strNomeFile="", strS="", strIn="";
            string strErr=""; long lngCount=0, i=0;
            if ("".Equals(strFileLog))
            {
                MessageBox.Show("File log mancante", "Esegui crypt dei file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                return;
            }
            //2. CaricaLogFile
            strS = txtChiave.Text;
            strS = MOD_KLOG.CaricaLogFile(strS, strFileLog);
            if ("".Equals(strS))
            {
                MessageBox.Show("Chiave inserita non valida", "Esegui crypt dei file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (MOD_MAIN.G_bolErrLog)
            {
                MessageBox.Show("ORMATO LOG NON VALIDO", "Esegui crypt dei file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                return;
            }
            //3. Generazione della lista di tutti i file
            lblStato.Text = "Stato: Generazione della lista dei file da criptare....";
            lblStato.Refresh();
            lstFileK.Items.Clear();
            strErr = MOD_FILE_LIST.Genera(1, dirRadice.Path, strFileLog);
            if (! "".Equals(strErr))
            {
                MessageBox.Show(strErr, "Esegui crypt dei file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                return;
            }
            //Criptatura di ogni file della lista
            lngCount = MOD_FILE_LIST.GetSize();
            for (i = 0; i < lngCount; i++)
            {
                strNomeFile = MOD_FILE_LIST.GetFile(i);
                lstFileK.Items.Add(strNomeFile);
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
            MOD_KLOG.SalvaLogFile(txtChiave.Text, strFileLog);
            //5. Operazione Completata
            MessageBox.Show("Operazione completata", "Esegui crypt dei file", MessageBoxButtons.OK, MessageBoxImage.Information);
            lblStato.Text = "Stato: PRONTO!";
            lblStato.Refresh();
            lstFileK.Items.Clear();
            if (!"".Equals(MOD_MAIN.G_strErr))
            {
                MOD_MAIN.G_strErr = "<?xml version=\"1.01\" encoding=\"UTF-8\"?><EXCEPTIONS DATETIME=\"" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "\">" + MOD_MAIN.G_strErr + "</EXCEPTIONS>";
                MOD_UTILS_SO.ErrorLog(MOD_MAIN.G_strErr, Application.StartupPath);
            }
        }

        private void EseguiConListaKript()
        {
            //1. Inizio
            string strNomeFile = "", strS = "", strIn = "";
            string strErr = "", strDir = "";
            long i = 0, j = 0,  k = 0;
            if (!MOD_UTILS_SO.ExistsFile(txtFileList.Text))
            {
                MessageBox.Show("Lista file non trovato", "Esegui crypt dei file con lista file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                return;
            }
            i = 0; j = txtFileList.Text.LastIndexOf("\\");
            if (j >= 0) strDir = txtFileList.Text.Substring(0, (int) j - 1);
            //3: Crypt lista file
            Encoding iso88591 = Encoding.GetEncoding("ISO-8859-1");
            StreamReader streamFileLog = new StreamReader(txtFileList.Text, iso88591, false);
            while (streamFileLog.Peek() >= 0)
            {
                strIn = streamFileLog.ReadLine();
                //3.1 Crpt del file in un'altro
                j = strIn.IndexOf("\t");
                if (j < 0)
                {
                    strDir = strIn;
                    if (strDir[strDir.Length - 1] != '\\') strDir += '\\';
                } else
                {
                    //3.2 Conversione chiave da testo ad array di byte
                    k = strIn.IndexOf('\t', (int) j + 1);
                    if (k < 0)
                    {
                        //Aggiungo gestione assenza tab non gestita nel progetto originale
                        MOD_MAIN.G_strErr = "<EXCEPTION ID = \"0\" IDREF=\"0\" DESCRIPTION=\"secondo tab non trovato\" SOURCE=\"EseguiConListaKript\"";
                        MOD_MAIN.G_strErr += "\" DATETIME=\"" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "\">";
                        MOD_MAIN.G_strErr += "<DETAILS><LINE>" + strIn + "</LINE></DETAILS></EXCEPTION>\r\n";
                    }
                    else
                    {
                        strNomeFile = strIn.Substring(0 , (int) k).Replace('\t', '\\');
                        strS = strDir + strIn.Substring((int)k + 1);
                        lblStato.Text = "Stato: kritp di " + i.ToString() + " numero File";
                        lblStato.Refresh();
                        MOD_PRG_UTILS.Kriptp(strNomeFile, txtChiave.Text, strS);
                    }
                }
                i++;
            }
            streamFileLog.Close();
            lblStato.Text = "Stato: PRONTO!";
            lblStato.Refresh();
        }

        private void txtChiave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)13))
                Esegui();
        }

        private void btnVai_Click(object sender, EventArgs e)
        {
            Esegui();
        }

        private void Esegui()
        {
            //1. Controlli di correttezza
            string chiave = txtChiave.Text;
            if ("".Equals(chiave))
            {
                MessageBox.Show("E' necessario inserire una chiave", "Esegui crypt", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (chkBlock.Checked)
            {
                MessageBox.Show("E' necessario effettuare lo sblocco", "Esegui crypt", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (STATICUTILS.CheckSystemOrCriticalFolder(dirRadice.Path))
            {
                MessageBox.Show("Non è possibile criptare una cartella di sistema o critica", "Esegui crypt", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                return;
            }
            if ("".Equals(txtFileList.Text))
            {
                string strS = MOD_KLOG.CaricaLogFile(chiave, strFileLog);
                if ("".Equals(strS))
                {
                    MessageBox.Show("Chiave inserita non valida", "Esegui crypt", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    MOD_KLOG.RigeneraLog(txtChiave.Text, dirRadice.Path, strFileLog, dirRadice.Path);
                    EseguiConKLog();
                }
            }
            else
                EseguiConListaKript();
        }

        private void dirRadice_DoubleClick(object sender, EventArgs e)
        {
            chkBlock.Checked = STATICUTILS.CheckSystemOrCriticalFolder(dirRadice.Path);
        }
    }
}
