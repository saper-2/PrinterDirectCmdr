using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrinterDirectCmdr
{
    public partial class FormPrinterList : Form
    {
        public FormPrinterList()
        {
            InitializeComponent();
        }

        private string selPrinter = "";
        public string SelectedPrinter {  get { return selPrinter; } }


        private void FormPrinterList_Shown(object sender, EventArgs e)
        {
            List<string> plist = new List<string>(PrinterHelpers.EnumeratePrinterDriverNames.Enumerate());
            lbList.Items.Clear();
            foreach (string str in plist)
            {
                lbList.Items.Add(str);
            }
        }

        private void lbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbList.SelectedIndex != -1)
            {
                lblSelected.Text = "Selected: " + lbList.Items[lbList.SelectedIndex].ToString();
                selPrinter = lbList.Items[lbList.SelectedIndex].ToString();
            } else
            {
                lblSelected.Text = "Selected: -none-";
                selPrinter = "";
            }
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK; 
        }

        private void lbList_DoubleClick(object sender, EventArgs e)
        {
            if (lbList.SelectedIndex != -1)
            {
                bOK.PerformClick();
            }
        }
    }
}
