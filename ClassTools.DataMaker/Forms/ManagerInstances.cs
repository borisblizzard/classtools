using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;

namespace ClassTools.DataMaker.Forms
{
    public partial class ManagerInstances : Form, IRefreshable
    {
        #region Fields
        private Repository repository;
        private MetaClass metaClass;
        private MetaList<MetaInstance> metaInstances;
        private bool refreshing;
        #endregion

        #region Properties
        public MetaList<MetaInstance> Instances
        {
            get { return this.metaInstances; }
        }
        #endregion

        #region Construct
        public ManagerInstances(Repository repository, MetaClass metaClass, MetaList<MetaInstance> metaInstances)
        {
            InitializeComponent();
            this.repository = repository;
            this.metaInstances = metaInstances;
            this.metaClass = metaClass;
            this.icInstances.SetData(this, this.repository, this.metaClass, this.metaInstances);
            this.RefreshData();
        }
        #endregion

        #region Close
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
            this.refreshing = false;
        }
        #endregion

        #region Tools
        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            this.icInstances.CopyInstance();
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            this.icInstances.PasteInstance();
        }

        private void addNewMenuItem_Click(object sender, EventArgs e)
        {
            this.icInstances.AddNewInstance();
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            this.icInstances.DeleteInstance();
        }

        private void moveUpMenuItem_Click(object sender, EventArgs e)
        {
            this.icInstances.MoveUpInstance();
        }

        private void moveDownMenuItem_Click(object sender, EventArgs e)
        {
            this.icInstances.MoveDownInstance();
        }
        #endregion

        private void lbInstances_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }

    }
}
