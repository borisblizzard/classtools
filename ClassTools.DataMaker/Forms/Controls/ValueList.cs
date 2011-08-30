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
                this.ivVariables.ClearData();
            }
        }

        public void SetData(IRefreshable owner, Repository repository, MetaType metaType, MetaList<MetaValue> metaValues)
        {
            this.owner = owner;
            this.repository = repository;
            this.type = metaType;
            this.listValues = metaValues;
            this.ivVariables.ClearData();
            this.ivVariables.SetData(this, this.repository, this.type);
            this.ivVariables.SetValue(this.listValues.Count > 0 ? this.listValues[0] : null);
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
            Utility.ApplyNewDataSource(this.lbInstances, new MetaList<MetaValue>(this.listValues), this.listValues.Count);
            this.lbInstances.Enabled = true;
            this.ivVariables.SetValue((MetaValue)this.lbInstances.SelectedItem);
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
                this.listValues[this.lbInstances.SelectedIndex] = InternalClipboard.Value;
                this.listValues[this.lbInstances.SelectedIndex].Update(this.repository.Model);
                this.RefreshData();
            }
        }

        public void AddNewValue()
        {
            if (this.lbInstances.Focused)
            {
                MetaValue metaValue = null;
                switch (this.type.CategoryType)
                {
                    case ECategoryType.Integral:
                        metaValue = new MetaValue(this.type);
                        break;
                    case ECategoryType.Class:
                        metaValue = new MetaValue((MetaClass)this.type, new MetaInstance((MetaClass)this.type));
                        break;
                    case ECategoryType.List:
                        metaValue = new MetaValue(this.type, new MetaList<MetaValue>());
                        break;
                    case ECategoryType.Dictionary:
                        metaValue = new MetaValue(this.type, new MetaDictionary<MetaValue, MetaValue>());
                        break;
                }
                this.listValues.Insert(this.lbInstances.SelectedIndex + 1, metaValue);
                this.RefreshData();
            }
        }

        public void DeleteValue()
        {
            if (this.lbInstances.Focused && this.lbInstances.SelectedIndex >= 0)
            {
                this.listValues.RemoveAt(this.lbInstances.SelectedIndex);
                this.RefreshData();
            }
        }

        public void MoveUpValue()
        {
            if (this.lbInstances.Focused && this.listValues.TryMoveUp(this.lbInstances.SelectedIndex))
            {
                this.lbInstances.SelectedIndex--;
            }
        }

        public void MoveDownValue()
        {
            if (this.lbInstances.Focused && this.listValues.TryMoveDown(this.lbInstances.SelectedIndex))
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
