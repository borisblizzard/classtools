using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools.Data;
using ClassTools.Data.Hierarchy;

namespace ClassTools.ClassMaker.Forms
{
    public partial class Implementation : Form
    {
        private MetaMethod metaMethod;

        public Implementation(MetaMethod method)
        {
            InitializeComponent();
            this.metaMethod = method;
            this.tbImplementation.Text = this.metaMethod.Implementation;
        }

        private void bOk_Click(object sender, System.EventArgs e)
        {
            this.metaMethod.Implementation = this.tbImplementation.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}
