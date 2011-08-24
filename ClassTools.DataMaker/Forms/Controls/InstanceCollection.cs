using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            this.refreshing = false;
        }

        public void SetData(IRefreshable owner, ModelDatabase database, MetaClass metaClass, List<MetaInstance> metaInstances)
        {
            this.owner = owner;
            this.database = database;
            this.metaClass = metaClass;
            this.metaInstances = metaInstances;
            this.ivcInstanceVariables.SetData(this, this.database, this.metaClass);
            this.ivcInstanceVariables.MetaInstance = (this.metaInstances.Count > 0 ? this.metaInstances[0] : null);
            this.RefreshData();
        }
        #endregion

        public void RefreshData()
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            this.owner.RefreshData();
            this.refreshing = false;
        }

    }
}
