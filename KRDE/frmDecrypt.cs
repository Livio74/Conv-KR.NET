using KRLib.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KRDE
{
    public partial class frmDecrypt : Form
    {
        public frmDecrypt()
        {
            InitializeComponent();
        }

        private void btnEsci_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            Text2.Text = MOD_INVKEY.reverseKey(Text1.Text, true);
            Text3.Text = MOD_INVKEY.reverseKey(Text1.Text, false);
        }
    }
}
