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
    public partial class ManagerList : Form, IRefreshable
    {
        #region Fields
        private Repository repository;
        private MetaType type;
        private MetaList<MetaValue> listValues;
        private bool refreshing;
        #endregion

        #region Properties
        public MetaList<MetaValue> ListValues
        {
            get { return this.listValues; }
        }
        #endregion

        #region Construct
        public ManagerList(Repository repository, MetaType metaType, MetaList<MetaValue> metaValues)
        {
            InitializeComponent();
            this.repository = repository;
            this.type = metaType;
            this.listValues = metaValues;
            this.vlValues.SetData(this, this.repository, this.type, this.listValues);
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
            this.refreshing = false;
        }
        #endregion

        #region Tools
        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            this.vlValues.CopyValue();
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            this.vlValues.PasteValue();
        }

        private void addNewMenuItem_Click(object sender, EventArgs e)
        {
            this.vlValues.AddNewValue();
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            this.vlValues.DeleteValue();
        }

        private void moveUpMenuItem_Click(object sender, EventArgs e)
        {
            this.vlValues.MoveUpValue();
        }

        private void moveDownMenuItem_Click(object sender, EventArgs e)
        {
            this.vlValues.MoveDownValue();
        }
        #endregion

        #region Events
        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbInstances_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }
        #endregion

    }
}
