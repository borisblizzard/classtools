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
    public partial class ValueDictionary : UserControl, IRefreshable
    {
        #region Fields
        private Repository repository;
        private MetaType type1;
        private MetaType type2;
        private MetaList<MetaValue> dictionaryKeys;
        private MetaList<MetaValue> dictionaryValues;
        private IRefreshable owner;
        private bool refreshing;
        #endregion

        #region Properties
        public MetaList<MetaValue> Keys
        {
            get { return this.dictionaryKeys; }
        }

        public MetaList<MetaValue> Values
        {
            get { return this.dictionaryValues; }
        }

        public MetaDictionary<MetaValue, MetaValue> DictionaryValues
        {
            get
            {
                MetaDictionary<MetaValue, MetaValue> result = new MetaDictionary<MetaValue, MetaValue>();
                for (int i = 0; i < this.dictionaryKeys.Count; i++)
                {
                    result[this.dictionaryKeys[i]] = this.dictionaryValues[i];
                }
                return result;
            }
        }
        #endregion

        #region Construct
        public ValueDictionary()
        {
            InitializeComponent();
            this.owner = null;
            this.repository = null;
            this.type1 = null;
            this.type2 = null;
            this.dictionaryKeys = null;
            this.dictionaryValues = null;
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

        public void SetData(IRefreshable owner, Repository repository, MetaType metaType1, MetaType metaType2, MetaDictionary<MetaValue, MetaValue> metaValues)
        {
            this.owner = owner;
            this.repository = repository;
            this.type1 = metaType1;
            this.type2 = metaType2;
            this.dictionaryKeys = metaValues.GetKeys();
            this.dictionaryValues = metaValues.GetValues(this.dictionaryKeys);
            this.vlVariables.ClearData();
            this.vlVariables.SetData(this, this.repository, this.type2);
            this.vlVariables.SetValue(this.dictionaryValues.Count > 0 ? this.dictionaryValues[0] : null);
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
            this.owner.RefreshData();
            this.Enabled = (this.type1 != null && this.type2 != null);
            Utility.ApplyNewDataSource(this.lbKeys, new MetaList<MetaValue>(this.dictionaryKeys), this.dictionaryKeys.Count);
            Utility.ApplyNewDataSource(this.lbValues, new MetaList<MetaValue>(this.dictionaryValues), this.dictionaryValues.Count);
            this.lbKeys.Enabled = true;
            this.lbValues.Enabled = true;
            this.vlVariables.SetValue((MetaValue)this.lbValues.SelectedItem);
            this.refreshing = false;
        }

        public void SetDictionaryValues(MetaDictionary<MetaValue, MetaValue> metaValues)
        {
            this.dictionaryKeys = metaValues.GetKeys();
            this.dictionaryValues = metaValues.GetValues(this.dictionaryKeys);
            this.Enabled = (this.dictionaryValues.Count > 0);
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
                this.dictionaryValues[this.lbValues.SelectedIndex] = InternalClipboard.Value;
                this.dictionaryValues[this.lbValues.SelectedIndex].Update(this.repository.Model);
                this.RefreshData();
            }
        }

        public void AddNewValue()
        {
            if (this.lbKeys.Focused || this.lbValues.Focused)
            {
                this.dictionaryKeys.Insert(this.lbValues.SelectedIndex + 1, new MetaValue(this.type1));
                this.dictionaryValues.Insert(this.lbValues.SelectedIndex + 1, new MetaValue(this.type2));
                this.RefreshData();
            }
        }

        public void DeleteValue()
        {
            if ((this.lbKeys.Focused || this.lbValues.Focused) && this.lbValues.SelectedIndex >= 0)
            {
                this.dictionaryKeys.RemoveAt(this.lbValues.SelectedIndex);
                this.dictionaryValues.RemoveAt(this.lbValues.SelectedIndex);
                this.RefreshData();
            }
        }

        public void MoveUpValue()
        {
            if ((this.lbKeys.Focused || this.lbValues.Focused) && this.dictionaryValues.TryMoveUp(this.lbValues.SelectedIndex))
            {
                this.dictionaryKeys.TryMoveUp(this.lbValues.SelectedIndex);
                this.lbValues.SelectedIndex--;
                this.lbKeys.SelectedIndex--;
            }
        }

        public void MoveDownValue()
        {
            if ((this.lbKeys.Focused || this.lbValues.Focused) && this.dictionaryValues.TryMoveDown(this.lbValues.SelectedIndex))
            {
                this.dictionaryKeys.TryMoveDown(this.lbValues.SelectedIndex);
                this.lbValues.SelectedIndex++;
                this.lbKeys.SelectedIndex++;
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
            this.lbKeys.SelectedIndex = this.lbValues.SelectedIndex;
            this.vlVariables.SetValue((MetaValue)this.lbValues.SelectedItem);
            this.refreshing = false;
        }

        private void lbKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            this.lbValues.SelectedIndex = this.lbKeys.SelectedIndex;
            this.vlVariables.SetValue((MetaValue)this.lbValues.SelectedItem);
            this.refreshing = false;
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            if (this.lbKeys.SelectedIndex >= 0)
            {
                MetaValue value = this.dictionaryKeys[this.lbKeys.SelectedIndex];
                ManagerInstance formInstance = new ManagerInstance(this.repository, value.Type, value);
                formInstance.Text = value.Type.GetNameWithModule();
                formInstance.ShowDialog();
                this.dictionaryKeys[this.lbKeys.SelectedIndex] = formInstance.Value;
                this.RefreshData();
            }
        }
        #endregion

    }
}
