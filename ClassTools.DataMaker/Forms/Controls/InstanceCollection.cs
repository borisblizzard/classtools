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
    public partial class InstanceCollection : UserControl, IRefreshable
    {
        #region Fields
        private Repository repository;
        private MetaClass metaClass;
        private MetaList<MetaInstance> metaInstances;
        private IRefreshable owner;
        private bool refreshing;
        #endregion

        #region Properties
        public MetaList<MetaInstance> MetaInstances
        {
            get { return this.metaInstances; }
        }
        #endregion

        #region Construct
        public InstanceCollection()
        {
            InitializeComponent();
            this.owner = null;
            this.repository = null;
            this.metaClass = null;
            this.metaInstances = null;
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

        public void SetData(IRefreshable owner, Repository repository, MetaClass metaClass, MetaList<MetaInstance> metaInstances)
        {
            this.owner = owner;
            this.repository = repository;
            this.metaClass = metaClass;
            this.metaInstances = metaInstances;
            this.ivVariables.ClearData();
            this.ivVariables.SetData(this, this.repository, this.metaClass);
            this.ivVariables.SetMetaInstance(this.metaInstances.Count > 0 ? this.metaInstances[0] : null);
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
            Utility.ApplyNewDataSource(this.lbInstances, new MetaList<MetaInstance>(this.metaInstances), this.metaInstances.Count);
            this.lbInstances.Enabled = true;
            this.ivVariables.SetMetaInstance((MetaInstance)this.lbInstances.SelectedItem);
            this.refreshing = false;
        }

        public void SetMetaInstances(MetaList<MetaInstance> metaInstances)
        {
            this.metaInstances = metaInstances;
            this.Enabled = (this.metaInstances.Count > 0);
            this.RefreshData();
        }
        #endregion

        #region Create / Delete
        private void bInstanceNew_Click(object sender, EventArgs e)
        {
            this.lbInstances.Focus();
            this.AddNewInstance();
        }

        private void bInstanceDelete_Click(object sender, EventArgs e)
        {
            this.lbInstances.Focus();
            this.DeleteInstance();
        }
        #endregion

        #region Tools
        public void CopyInstance()
        {
            if (this.lbInstances.Focused)
            {
                InternalClipboard.Instance = (MetaInstance)this.lbInstances.SelectedItem;
            }
        }

        public void PasteInstance()
        {
            if (this.lbInstances.Focused)
            {
                this.repository.ReplaceInstanceAt(this.metaClass, this.lbInstances.SelectedIndex, InternalClipboard.Instance);
                this.RefreshData();
            }
        }

        public void AddNewInstance()
        {
            if (this.lbInstances.Focused)
            {
                MetaInstance metaInstance = new MetaInstance(this.repository, this.metaClass);
                this.metaInstances.Insert(this.lbInstances.SelectedIndex + 1, metaInstance);
                this.RefreshData();
            }
        }

        public void DeleteInstance()
        {
            if (this.lbInstances.Focused && this.lbInstances.SelectedIndex >= 0)
            {
                this.metaInstances.RemoveAt(this.lbInstances.SelectedIndex);
                this.RefreshData();
            }
        }

        public void MoveUpInstance()
        {
            if (this.lbInstances.Focused && this.metaInstances.TryMoveUp(this.lbInstances.SelectedIndex))
            {
                this.lbInstances.SelectedIndex--;
            }
        }

        public void MoveDownInstance()
        {
            if (this.lbInstances.Focused && this.metaInstances.TryMoveDown(this.lbInstances.SelectedIndex))
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
