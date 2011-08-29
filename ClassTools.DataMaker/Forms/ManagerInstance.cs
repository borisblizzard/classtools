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
        private MetaValue metaValue;
        private bool refreshing;
        #endregion

        #region Properties
        public MetaValue MetaValue
        {
            get
            {
                if (!this.cbxExists.Checked)
                {
                    this.metaValue.Instance = null;
                }
                return this.metaValue;
            }
        }
        #endregion

        #region Construct
        public ManagerInstance(Repository repository, MetaClass metaClass, MetaValue metaValue, bool nullable)
        {
            InitializeComponent();
            this.respository = repository;
            this.metaValue = metaValue;
            this.cbxExists.Checked = (this.metaValue.Instance != null);
            if (this.metaValue.Instance == null)
            {
                this.metaValue.Instance = new MetaInstance(repository, metaClass);
            }
            this.metaClass = metaClass;
            this.ivbInstanceVariables.SetData(this, this.respository, this.metaClass);
            this.ivbInstanceVariables.SetMetaValue(this.metaValue);
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
            this.ivbInstanceVariables.Enabled = this.cbxExists.Checked;
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
