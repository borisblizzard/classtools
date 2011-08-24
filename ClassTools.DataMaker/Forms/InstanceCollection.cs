using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Model;

namespace ClassTools.DataMaker.Forms
{
    public partial class InstanceCollection : Form, IRefreshable
    {
        #region Fields
        private ModelDatabase database;
        private MetaClass metaClass;
        private List<MetaInstance> metaInstances;
        private bool updating;
        #endregion

        #region Properties
        public List<MetaInstance> MetaInstances
        {
            get { return this.metaInstances; }
        }
        #endregion

        #region Constructors
        public InstanceCollection(ModelDatabase database, MetaClass metaClass, List<MetaInstance> metaInstances)
        {
            InitializeComponent();
            this.database = database;
            this.metaInstances = metaInstances;
            this.metaClass = metaClass;
            this.ivcInstanceVariables.SetData(this, this.database, this.metaClass);
            this.ivcInstanceVariables.MetaInstance = (this.metaInstances.Count > 0 ? this.metaInstances[0] : null);
            this.RefreshData();
        }
        #endregion

        #region Close
        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            DialogResult result = this.showSaveChangesDialog(savePromptExit);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
             * */
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Refresh
        public void RefreshData()
        {
            if (this.updating)
            {
                return;
            }
            this.updating = true;
            Utility.ApplyNewDataSource(this.lbInstances, new List<MetaInstance>(this.metaInstances), this.metaInstances.Count);
            this.lbInstances.Enabled = true;
            this.ivcInstanceVariables.MetaInstance = (MetaInstance)this.lbInstances.SelectedItem;
            this.updating = false;
        }
        #endregion

        #region Create / Delete
        private void bInstanceNew_Click(object sender, EventArgs e)
        {
            MetaInstance instance = new MetaInstance(this.database, this.metaClass);
            this.metaInstances.Insert(this.lbInstances.SelectedIndex + 1, instance);
            this.RefreshData();
            this.lbInstances.Focus();
        }

        private void bInstanceDelete_Click(object sender, EventArgs e)
        {
            if (this.lbInstances.SelectedIndex >= 0)
            {
                this.metaInstances.RemoveAt(this.lbInstances.SelectedIndex);
                this.RefreshData();
                this.lbInstances.Focus();
            }
        }

        private void lbClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }
        #endregion

        #region Tools
        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbInstances.Focused)
            {
                InternalClipboard.Instance = (MetaInstance)this.lbInstances.SelectedItem;
            }
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbInstances.Focused)
            {
                this.database.ReplaceInstanceAt(this.metaClass, this.lbInstances.SelectedIndex, InternalClipboard.Instance);
                this.RefreshData();
            }
        }

        private void addNewMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbInstances.Focused)
            {
                this.bInstanceNew_Click(sender, e);
            }
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbInstances.Focused)
            {
                this.bInstanceDelete_Click(sender, e);
            }
        }

        private void moveUpMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbInstances.Focused)
            {
                if (this.database.TryInstanceMoveUp(this.metaClass, this.lbInstances.SelectedIndex))
                {
                    this.lbInstances.SelectedIndex--;
                }
            }
        }

        private void moveDownMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbInstances.Focused)
            {
                if (this.database.TryInstanceMoveDown(this.metaClass, this.lbInstances.SelectedIndex))
                {
                    this.lbInstances.SelectedIndex++;
                }
            }
        }
        #endregion

        private void lbInstances_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }

    }
}
