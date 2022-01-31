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
using MessageBox = System.Windows.MessageBox;
using MessageBoxButtons = System.Windows.MessageBoxButton;

namespace KR.NET
{
    public partial class LogFile : Form
    {
        private string m_StatoE = "";
        private string m_StatoK = "";
        private kr mainForm = null;

        public LogFile(kr mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void btnEsci_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LogFile_Load(object sender, EventArgs e)
        {
            string strKey = "", strS = "";
            m_StatoE = ""; m_StatoK = "";
            strS = this.mainForm.txtChiave.Text;
            if ("".Equals(strS))
            {
                MessageBox.Show("Inserire la chiave", "Log file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                return;
            }
            strS = MOD_KLOG.CaricaLogFile(strS, MOD_MAIN.G_strFileLog);
            if ("".Equals(strS))
            {
                MessageBox.Show("Chiave inserita non valida", "Log file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                this.mainForm.txtChiave.Text = "";
                return;
            }
            MOD_KLOG.LoadIntoList(lstDir, m_StatoE, m_StatoK);
        }

        private void btnStato_Click(object sender, EventArgs e)
        {
            string StatoNuovo = "";
            if (OptStato0.Checked)
                StatoNuovo = "E";
            else if (OptStato1.Checked)
                StatoNuovo = "D";
            else if (OptStato2.Checked)
                StatoNuovo = "";
            if (chkSubDirs.Checked)
            {
                if (lstDir.SelectedIndex >= 0)
                {
                    string selectedItem = (string)lstDir.SelectedItem;
                    MOD_KLOG.CambiaStato(m_StatoE, m_StatoK, StatoNuovo, selectedItem.Substring(0, selectedItem.Length - 3));
                }
                else
                {
                    MessageBox.Show("Per avere le sottodirectory è necessario\r\nspecificare una sola directory selezionandola", "Log file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }
            else
            {
                MessageBoxResult justSelectedDir = MessageBox.Show("Vuoi solo la directory selezionata?", "Creazione lista file", MessageBoxButtons.YesNo, MessageBoxImage.Question);
                if (justSelectedDir.Equals(MessageBoxResult.No))
                {
                    MOD_KLOG.CambiaStato(m_StatoE, m_StatoK, StatoNuovo);
                }
                else
                {
                    if (lstDir.SelectedIndex < 0)
                    {
                        MessageBox.Show("Per applicare il cambio di stato alla sottodirectory\r\nbisogna selezionare la sottodirectory!", "Log file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    else
                    {
                        string selectedItem = (string)lstDir.SelectedItem;
                        MOD_KLOG.SetNewStato(selectedItem.Substring(0, selectedItem.Length - 3), "", StatoNuovo);
                    }
                }
            }
            MOD_KLOG.LoadIntoList(lstDir, m_StatoE, m_StatoK);
        }

        private void btnConferma_Click(object sender, EventArgs e)
        {
            string strS = this.mainForm.txtChiave.Text;
            MOD_KLOG.SalvaLogFile(strS, MOD_MAIN.G_strFileLog);
            MessageBox.Show("Salvato file Log", "Log file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
            this.Close();
        }

        private void chkVisualNE_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkVisualE.Checked && !chkVisualNE.Checked)
                m_StatoE = "";
            else if (!chkVisualE.Checked && chkVisualNE.Checked)
                m_StatoE = "D";
            else if (chkVisualE.Checked && !chkVisualNE.Checked)
                m_StatoE = "E";
            else if (chkVisualE.Checked && chkVisualNE.Checked)
                m_StatoE = "";
            MOD_KLOG.LoadIntoList(lstDir, m_StatoE, m_StatoK);
        }

        private void chkVisualE_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkVisualE.Checked && !chkVisualNE.Checked)
                m_StatoE = "";
            else if (!chkVisualE.Checked && chkVisualNE.Checked)
                m_StatoE = "D";
            else if (chkVisualE.Checked && !chkVisualNE.Checked)
                m_StatoE = "E";
            else if (chkVisualE.Checked && chkVisualNE.Checked)
                m_StatoE = "";
            MOD_KLOG.LoadIntoList(lstDir, m_StatoE, m_StatoK);
        }

        private void chkVisualNK_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkVisualK.Checked && !chkVisualNK.Checked)
                m_StatoK = "";
            else if (!chkVisualK.Checked && chkVisualNK.Checked)
                m_StatoK = "_";
            else if (chkVisualK.Checked && !chkVisualNK.Checked)
                m_StatoK = "K";
            else if (chkVisualK.Checked && chkVisualNK.Checked)
                m_StatoK = "";
            MOD_KLOG.LoadIntoList(lstDir, m_StatoE, m_StatoK);
        }

        private void chkVisualK_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkVisualK.Checked && !chkVisualNK.Checked)
                m_StatoK = "";
            else if (!chkVisualK.Checked && chkVisualNK.Checked)
                m_StatoK = "_";
            else if (chkVisualK.Checked && !chkVisualNK.Checked)
                m_StatoK = "K";
            else if (chkVisualK.Checked && chkVisualNK.Checked)
                m_StatoK = "";
            MOD_KLOG.LoadIntoList(lstDir, m_StatoE, m_StatoK);
        }

        private void btnVisualizzaFiles_Click(object sender, EventArgs e)
        {
            if (lstDir.SelectedIndex >= 0)
            {
                try
                {
                    string selectedDir = ((string)lstDir.SelectedItem);
                    lstFiles.Path = selectedDir.Substring(0, selectedDir.Length - 3);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Log file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void lstDir_DoubleClick(object sender, EventArgs e)
        {
            btnVisualizzaFiles_Click(sender, e);
            string Message = "Osserva la lista dei file:\r\n";
            Message += "Scegli Sì se vuoi impostare lo stato directory a K\r\n";
            Message += "No per lo stato a _\r\n";
            Message += "Annulla se non lo vuoi impostare";
            MessageBoxResult cryptStatus = MessageBox.Show(Message, "Log file", MessageBoxButtons.YesNoCancel, MessageBoxImage.Exclamation);
            string selectedItem = (string)lstDir.SelectedItem;
            if (MessageBoxResult.Yes.Equals(cryptStatus))
            {
                MOD_KLOG.SetNewStato(selectedItem.Substring(0, selectedItem.Length - 3), "K", "");
            }
            else if (MessageBoxResult.No.Equals(cryptStatus))
            {
                MOD_KLOG.SetNewStato(selectedItem.Substring(0, selectedItem.Length - 3), "_", "");
            }
            MOD_KLOG.LoadIntoList(lstDir, m_StatoE, m_StatoK);
        }

        private void Rigenera_Click(object sender, EventArgs e)
        {
            string strS = this.mainForm.txtChiave.Text;
            if ("".Equals(strS))
            {
                MessageBox.Show("Chiave non valida", "Log file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
                return;
            }
            MOD_KLOG.RigeneraLog(strS, MOD_MAIN.G_strDirRoot, MOD_MAIN.G_strFileLog, MOD_MAIN.G_strDirRoot);
            MessageBox.Show("Log rigenerato", "Log file", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
            MOD_KLOG.LoadIntoList(lstDir, m_StatoE, m_StatoK);
        }
    }
}
