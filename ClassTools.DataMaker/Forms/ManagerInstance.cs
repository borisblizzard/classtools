using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;

namespace ClassTools.DataMaker.Forms
{
    public partial class ManagerInstance : Form, IRefreshable
    {
        #region Fields
        private Repository repository;
        private MetaClass classe;
        private MetaValue value;
        private bool refreshing;
        #endregion

        #region Properties
        public MetaValue Value
        {
            get
            {
                if (!this.cbxExists.Checked)
                {
                    this.value.Instance = null;
                }
                return this.value;
            }
        }
        #endregion

        #region Construct
        public ManagerInstance(Repository repository, MetaClass metaClass, MetaValue metaValue, bool nullable)
        {
            InitializeComponent();
            this.repository = repository;
            this.value = metaValue;
            this.cbxExists.Checked = (this.value.Instance != null);
            if (this.value.Instance == null)
            {
                this.value.Instance = new MetaInstance(metaClass);
            }
            this.classe = metaClass;
            this.vlVariables.SetData(this, this.repository, this.classe);
            this.vlVariables.SetValue(this.value);
            if (!nullable)
            {
                this.cbxExists.Enabled = false;
                this.cbxExists.Checked = true;
            }
            this.RefreshData();
        }
        #endregion

        #region Refresh
        public void RefreshData()
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            this.vlVariables.Enabled = this.cbxExists.Checked;
            this.refreshing = false;
        }
        #endregion

        #region Events
        private void cbxExists_CheckedChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }
        
        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
