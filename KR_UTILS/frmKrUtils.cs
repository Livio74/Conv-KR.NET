using KRLib.NET;
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
using MessageBox = System.Windows.MessageBox;
using MessageBoxButtons = System.Windows.MessageBoxButton;

namespace KR_UTILS
{
    public partial class frmKrUtils : Form
    {
        public frmKrUtils()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strLog; DateTime dateM; string strS;
            strLog = txtPath.Text;
            if (strLog[strLog.Length - 1] != '\\') strLog += '\\';
            strLog += "klog.txt";
            dateM = File.GetLastWriteTime(strLog);
            if (MOD_UTILS_SO.ExistsFile(strLog))
            {
                Encoding iso88591 = Encoding.GetEncoding("ISO-8859-1");
                StreamReader streamFileLog = new StreamReader(strLog, iso88591, false);
                strS = streamFileLog.ReadLine();
                strS = STATICUTILS.EventuallyRemoveDoubleQuotes(strS);
                streamFileLog.Close();
                int first = dateM.Year;
                int second = dateM.Day * dateM.Month;
                string strS1 = dateM.ToString("yyyyMMdd") + first.ToString("X") + dateM.ToString("ddyyyyMM") + second.ToString("X");
                txtPWD.Text = MOD_INVKEY.InvKript(strS1, strS, false);
            }
            else
            {
                MessageBox.Show("file log KR non trovato", "Kr Utils", MessageBoxButtons.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnEsci_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
