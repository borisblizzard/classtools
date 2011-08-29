using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;

namespace ClassTools.DataMaker.Forms.Controls
{
    public partial class InstanceList : UserControl, IRefreshable
    {
        #region Fields
        private Repository repository;
        private MetaClass metaClass;
        private MetaList<MetaValue> metaValues;
        private IRefreshable owner;
        private bool refreshing;
        #endregion

        #region Properties
        public MetaList<MetaValue> MetaValues
        {
            get { return this.metaValues; }
        }
        #endregion

        #region Construct
        public InstanceList()
        {
            InitializeComponent();
            this.owner = null;
            this.repository = null;
            this.metaClass = null;
            this.metaValues = null;
            this.Enabled = false;
            this.refreshing = false;
        }

        public void ClearData()
        {
            if (this.owner != null)
            {
                this.ivVariables.ClearData();
            }
        }

        public void SetData(IRefreshable owner, Repository repository, MetaClass metaClass, MetaList<MetaValue> metaValues)
        {
            this.owner = owner;
            this.repository = repository;
            this.metaClass = metaClass;
            this.metaValues = metaValues;
            this.ivVariables.ClearData();
            this.ivVariables.SetData(this, this.repository, this.metaClass);
            this.ivVariables.SetMetaValue(this.metaValues.Count > 0 ? this.metaValues[0] : null);
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
            this.Enabled = (this.metaClass != null);
            Utility.ApplyNewDataSource(this.lbInstances, new MetaList<MetaValue>(this.metaValues), this.metaValues.Count);
            this.lbInstances.Enabled = true;
            this.ivVariables.SetMetaValue((MetaValue)this.lbInstances.SelectedItem);
            this.refreshing = false;
        }

        public void SetMetaValues(MetaList<MetaValue> metaValues)
        {
            this.metaValues = metaValues;
            this.Enabled = (this.metaValues.Count > 0);
            this.RefreshData();
        }
        #endregion

        #region Create / Delete
        private void bInstanceNew_Click(object sender, EventArgs e)
        {
            this.lbInstances.Focus();
            this.AddNewValue();
        }

        private void bInstanceDelete_Click(object sender, EventArgs e)
        {
            this.lbInstances.Focus();
            this.DeleteValue();
        }
        #endregion

        #region Tools
        public void CopyValue()
        {
            if (this.lbInstances.Focused)
            {
                InternalClipboard.Value = (MetaValue)this.lbInstances.SelectedItem;
            }
        }

        public void PasteValue()
        {
            if (this.lbInstances.Focused)
            {
                this.repository.ReplaceValueAt(this.metaClass, this.lbInstances.SelectedIndex, InternalClipboard.Value);
                this.RefreshData();
            }
        }

        public void AddNewValue()
        {
            if (this.lbInstances.Focused)
            {
                MetaValue metaValue = new MetaValue(this.repository, this.metaClass, new MetaInstance(this.repository, this.metaClass));
                this.metaValues.Insert(this.lbInstances.SelectedIndex + 1, metaValue);
                this.RefreshData();
            }
        }

        public void DeleteValue()
        {
            if (this.lbInstances.Focused && this.lbInstances.SelectedIndex >= 0)
            {
                this.metaValues.RemoveAt(this.lbInstances.SelectedIndex);
                this.RefreshData();
            }
        }

        public void MoveUpValue()
        {
            if (this.lbInstances.Focused && this.metaValues.TryMoveUp(this.lbInstances.SelectedIndex))
            {
                this.lbInstances.SelectedIndex--;
            }
        }

        public void MoveDownValue()
        {
            if (this.lbInstances.Focused && this.metaValues.TryMoveDown(this.lbInstances.SelectedIndex))
            {
                this.lbInstances.SelectedIndex++;
            }
        }
        #endregion

        #region Events
        private void lbInstances_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }
        #endregion

    }
}
