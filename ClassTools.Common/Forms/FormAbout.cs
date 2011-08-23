using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClassTools.Common.Forms
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            lAppName.Text = "Title: " + Utility.AssemblyTitle;
            lVersion.Text = "Version: " + Utility.AssemblyVersion;
            lCopyright.Text = "Copyright: " + Utility.AssemblyCopyright;
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
