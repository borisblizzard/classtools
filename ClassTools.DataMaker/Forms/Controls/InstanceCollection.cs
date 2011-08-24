using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Model;

namespace ClassTools.DataMaker.Forms.Controls
{
    public partial class InstanceCollection : UserControl, IRefreshable
    {
        #region Fields
        private ModelDatabase database;
        private MetaClass metaClass;
        private List<MetaInstance> metaInstances;
        private IRefreshable owner;
        private bool refreshing;
        #endregion

        #region Properties
        public List<MetaInstance> MetaInstances
        {
            get { return this.metaInstances; }
            set
            {
                this.metaInstances = value;
                this.Enabled = (this.metaInstances.Count > 0);
                this.RefreshData();
            }
        }
        #endregion

        #region Constructors
        public InstanceCollection()
        {
            InitializeComponent();
            this.owner = null;
            this.database = null;
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

        public void SetData(IRefreshable owner, ModelDatabase database, MetaClass metaClass, List<MetaInstance> metaInstances)
        {
            this.owner = owner;
            this.database = database;
            this.metaClass = metaClass;
            this.metaInstances = metaInstances;
            this.ivVariables.ClearData();
            this.ivVariables.SetData(this, this.database, this.metaClass);
            this.ivVariables.MetaInstance = (this.metaInstances.Count > 0 ? this.metaInstances[0] : null);
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
            Utility.ApplyNewDataSource(this.lbInstances, new List<MetaInstance>(this.metaInstances), this.metaInstances.Count);
            this.lbInstances.Enabled = true;
            this.ivVariables.MetaInstance = (MetaInstance)this.lbInstances.SelectedItem;
            this.refreshing = false;
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
                this.database.ReplaceInstanceAt(this.metaClass, this.lbInstances.SelectedIndex, InternalClipboard.Instance);
                this.RefreshData();
            }
        }

        public void AddNewInstance()
        {
            if (this.lbInstances.Focused)
            {
                MetaInstance instance = new MetaInstance(this.database, this.metaClass);
                this.metaInstances.Insert(this.lbInstances.SelectedIndex + 1, instance);
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
            if (this.lbInstances.Focused && Utility.TryMoveUp(ref this.metaInstances, this.lbInstances.SelectedIndex))
            {
                this.lbInstances.SelectedIndex--;
            }
        }

        public void MoveDownInstance()
        {
            if (this.lbInstances.Focused && Utility.TryMoveDown(ref this.metaInstances, this.lbInstances.SelectedIndex))
            {
                this.lbInstances.SelectedIndex++;
            }
        }
        #endregion

        private void lbInstances_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }

    }
}
