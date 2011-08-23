using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools.Model;

namespace ClassTools.ClassMaker.Forms
{
    public partial class FormImplementation : Form
    {
        private MetaMethod method;

        public FormImplementation(MetaMethod method)
        {
            InitializeComponent();
            this.method = method;
            this.tbImplemtation.Text = this.method.Implementation;
        }

        private void bOk_Click(object sender, System.EventArgs e)
        {
            this.method.Implementation = this.tbImplemtation.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}
