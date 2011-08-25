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
        private Repository respository;
        private MetaClass metaClass;
        private MetaInstance metaInstance;
        private bool refreshing;
        #endregion

        #region Properties
        public MetaInstance MetaInstance
        {
            get { return this.metaInstance; }
        }
        #endregion

        #region Constructors
        public ManagerInstance(Repository repository, MetaClass metaClass, MetaInstance metaInstance)
        {
            InitializeComponent();
            this.respository = repository;
            this.metaInstance = (metaInstance != null ? metaInstance : new MetaInstance(repository, metaClass));
            this.metaClass = metaClass;
            this.ivbInstanceVariables.SetData(this, this.respository, this.metaClass);
            this.ivbInstanceVariables.MetaInstance = this.metaInstance;
            this.cbExists.Checked = (this.metaInstance != null);
            this.RefreshData();
        }
        #endregion

        #region Close
        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.cbExists.Checked)
            {
                this.metaInstance = null;
            }
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
            this.ivbInstanceVariables.Enabled = this.cbExists.Checked;
            this.refreshing = false;
        }
        #endregion

        #region Tools
        private void cbExists_CheckedChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }
        #endregion

    }
}
