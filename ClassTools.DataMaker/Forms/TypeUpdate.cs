using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;

namespace ClassTools.DataMaker.Forms
{
    public partial class TypeUpdate : Form, IRefreshable
    {
        #region Constants
        private const string ERROR_TYPE_UPDATE_INCOMPLETE = "There are still mismatched types that have not been replaced.";

        private const string WARNING_TYPE_NOT_MATCHING = "The selected type's variables do not match.";
        #endregion

        #region Fields
        private Repository repository;
        private Model newModel;
        private MetaList<MetaType> mismatchedTypes;
        private bool refreshing;
        #endregion

        #region Construct
        public TypeUpdate(Repository repository, Model newModel)
        {
            InitializeComponent();
            this.repository = repository;
            this.newModel = newModel;
            this.mismatchedTypes = new MetaList<MetaType>();
            this.lbNewTypes.DataSource = new MetaList<MetaType>(newModel.AllTypes);
            this.refreshing = false;
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
            this.mismatchedTypes = this.repository.Model.FindTypeMismatches(newModel);
            Utility.ApplyNewDataSource(this.lbOldTypes, new MetaList<MetaType>(this.mismatchedTypes), this.mismatchedTypes.Count);
            this.refreshing = false;
        }
        #endregion

        #region Events
        private void cbxExists_CheckedChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }

        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (this.mismatchedTypes.Count > 0)
                {
                    e.Cancel = true;
                    MessageBox.Show(ERROR_TYPE_UPDATE_INCOMPLETE, "Type Update Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void bReplace_Click(object sender, EventArgs e)
        {
            MetaType oldType = (MetaType)this.lbOldTypes.SelectedItem;
            if (oldType == null)
            {
                return;
            }
            MetaType newType = (MetaType)this.lbNewTypes.SelectedItem;
            if (newType == null)
            {
                return;
            }
            this.updateVariables(oldType, newType);
            this.repository.UpdateType(oldType, newType);
            this.RefreshData();
        }

        private void bAutoReplace_Click(object sender, EventArgs e)
        {
            bool found = false;
            MetaType newType;
            foreach (MetaType oldType in this.mismatchedTypes)
            {
                newType = null;
                if (oldType.CategoryType == ECategoryType.Class)
                {
                    newType = this.newModel.Classes.Find(c => c.GetNameWithModule().Equals(oldType.GetNameWithModule()));
                }
                else
                {
                    newType = this.newModel.Types.Find(t => t.GetNameWithModule().Equals(oldType.GetNameWithModule()));
                }
                if (newType != null)
                {
                    this.updateVariables(oldType, newType);
                    this.repository.UpdateType(oldType, newType);
                    found = true;
                }
            }
            if (found)
            {
                this.RefreshData();
            }
        }

        private void updateVariables(MetaType oldType, MetaType newType)
        {
            if (oldType.FindVariableMismatches(newType).Count > 0)
            {
                MessageBox.Show(WARNING_TYPE_NOT_MATCHING, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                VariableUpdate form = new VariableUpdate(repository, (MetaClass)oldType, (MetaClass)newType);
                form.ShowDialog();
            }
        }
        #endregion

    }
}
