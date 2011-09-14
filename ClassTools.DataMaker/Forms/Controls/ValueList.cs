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
    public partial class ValueList : UserControl, IRefreshable
    {
        #region Fields
        private Repository repository;
        private MetaType type;
        private MetaList<MetaValue> listValues;
        private IRefreshable owner;
        private bool refreshing;
        #endregion

        #region Properties
        public MetaList<MetaValue> ListValues
        {
            get { return this.listValues; }
        }
        #endregion

        #region Construct
        public ValueList()
        {
            InitializeComponent();
            this.owner = null;
            this.repository = null;
            this.type = null;
            this.listValues = null;
            this.Enabled = false;
            this.refreshing = false;
        }

        public void ClearData()
        {
            if (this.owner != null)
            {
                this.vlVariables.ClearData();
            }
        }

        public void SetData(IRefreshable owner, Repository repository, MetaType metaType, MetaList<MetaValue> metaValues)
        {
            this.owner = owner;
            this.repository = repository;
            this.type = metaType;
            this.listValues = metaValues;
            this.vlVariables.ClearData();
            this.vlVariables.SetData(this, this.repository, this.type);
            this.vlVariables.SetValue(this.listValues.Count > 0 ? this.listValues[0] : null);
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
            this.Enabled = (this.type != null);
            Utility.ApplyNewDataSource(this.lbValues, new MetaList<MetaValue>(this.listValues), this.listValues.Count);
            this.lbValues.Enabled = true;
            this.vlVariables.SetValue((MetaValue)this.lbValues.SelectedItem);
            this.refreshing = false;
        }

        public void SetListValues(MetaList<MetaValue> metaValues)
        {
            this.listValues = metaValues;
            this.Enabled = (this.listValues.Count > 0);
            this.RefreshData();
        }
        #endregion

        #region Create / Delete
        private void bInstanceNew_Click(object sender, EventArgs e)
        {
            this.lbValues.Focus();
            this.AddNewValue();
        }

        private void bInstanceDelete_Click(object sender, EventArgs e)
        {
            this.lbValues.Focus();
            this.DeleteValue();
        }
        #endregion

        #region Tools
        public void CopyValue()
        {
            if (this.lbValues.Focused)
            {
                InternalClipboard.Value = (MetaValue)this.lbValues.SelectedItem;
            }
        }

        public void PasteValue()
        {
            if (this.lbValues.Focused)
            {
                this.listValues[this.lbValues.SelectedIndex] = InternalClipboard.Value;
                this.listValues[this.lbValues.SelectedIndex].Update(this.repository.Model);
                this.RefreshData();
            }
        }

        public void AddNewValue()
        {
            if (this.lbValues.Focused)
            {
                this.listValues.Insert(this.lbValues.SelectedIndex + 1, new MetaValue(this.type));
                this.RefreshData();
            }
        }

        public void DeleteValue()
        {
            if (this.lbValues.Focused && this.lbValues.SelectedIndex >= 0)
            {
                this.listValues.RemoveAt(this.lbValues.SelectedIndex);
                this.RefreshData();
            }
        }

        public void MoveUpValue()
        {
            if (this.lbValues.Focused && this.listValues.TryMoveUp(this.lbValues.SelectedIndex))
            {
                this.lbValues.SelectedIndex--;
                this.RefreshData();
            }
        }

        public void MoveDownValue()
        {
            if (this.lbValues.Focused && this.listValues.TryMoveDown(this.lbValues.SelectedIndex))
            {
                this.lbValues.SelectedIndex++;
                this.RefreshData();
            }
        }
        #endregion

        #region Events
        private void lbInstances_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            this.vlVariables.SetValue((MetaValue)this.lbValues.SelectedItem);
            this.refreshing = false;
        }
        #endregion

    }
}
