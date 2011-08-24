using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClassTools.ClassMaker.Forms
{
    public partial class Log : Form
    {
        public Log()
        {
            InitializeComponent();
        }

        public void SetText(string text)
        {
            this.tbLog.Text = text;
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
