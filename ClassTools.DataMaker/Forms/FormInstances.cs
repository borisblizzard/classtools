using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Model;

namespace ClassTools.DataMaker.Forms
{
    public partial class FormInstances : Form
    {
        #region Constants
        #endregion

        #region Fields
        private ModelDatabase database;
        private MetaClass metaClass;
        private MetaType metaType;
        private List<MetaInstance> instances;
        private bool updating;
        #endregion

        #region Constructors
        public FormInstances(ModelDatabase database, MetaClass metaClass, MetaType metaType, List<MetaInstance> instances)
        {
            InitializeComponent();
            this.database = database;
            this.instances = instances;
            this.metaClass = metaClass;
            this.metaType = metaType;
            this.ivbInstanceVariables.MetaClass = metaClass;
            this.ivbInstanceVariables.DataContainer = this.lbInstances;
            this.refresh();
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
        private void refresh()
        {
            if (this.updating)
            {
                return;
            }
            this.updating = true;
            Utility.ApplyNewDataSource(this.lbInstances, new List<MetaInstance>(this.instances), this.instances.Count);
            this.lbInstances.Enabled = true;
            this.ivbInstanceVariables.MetaInstance = (MetaInstance)this.lbInstances.SelectedItem;
            this.updating = false;
        }
        #endregion

        #region Create / Delete
        private void bInstanceNew_Click(object sender, EventArgs e)
        {
            MetaInstance instance = new MetaInstance(this.database, this.metaClass);
            this.instances.Insert(this.lbInstances.SelectedIndex + 1, instance);
            this.refresh();
            this.lbInstances.Focus();
        }

        private void bInstanceDelete_Click(object sender, EventArgs e)
        {
            if (this.lbInstances.SelectedIndex >= 0)
            {
                this.instances.RemoveAt(this.lbInstances.SelectedIndex);
                this.refresh();
                this.lbInstances.Focus();
            }
        }

        private void lbClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.refresh();
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
                this.refresh();
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
            this.refresh();
        }

    }
}
