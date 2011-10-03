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
    public partial class VariableUpdate : Form, IRefreshable
    {
        #region Constants
        private const string ERROR_VARIABLE_UPDATE_INCOMPLETE = "There are still mismatched variables that have not been replaced.";
        #endregion

        #region Fields
        private Repository repository;
        private MetaClass oldClass;
        private MetaClass newClass;
        private MetaList<MetaVariable> mismatchedVariables;
        private bool refreshing;
        #endregion

        #region Construct
        public VariableUpdate(Repository repository, MetaClass oldClass, MetaClass newClass)
        {
            InitializeComponent();
            this.repository = repository;
            this.oldClass = oldClass;
            this.newClass = newClass;
            this.mismatchedVariables = new MetaList<MetaVariable>();
            this.lbNewVariables.DataSource = new MetaList<MetaVariable>(newClass.Variables);
            this.Text = string.Format("{0} : {1}", this.oldClass.GetNameWithModule(), this.newClass.GetNameWithModule());
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
            this.mismatchedVariables = this.oldClass.FindVariableMismatches(this.newClass);
            Utility.ApplyNewDataSource(this.lbOldVariables, new MetaList<MetaVariable>(this.mismatchedVariables), this.mismatchedVariables.Count);
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
                if (this.mismatchedVariables.Count > 0)
                {
                    e.Cancel = true;
                    MessageBox.Show(ERROR_VARIABLE_UPDATE_INCOMPLETE, "Variable Update Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void bReplace_Click(object sender, EventArgs e)
        {
            MetaVariable oldVariable = (MetaVariable)this.lbOldVariables.SelectedItem;
            if (oldVariable == null)
            {
                return;
            }
            MetaVariable newVariable = (MetaVariable)this.lbNewVariables.SelectedItem;
            if (newVariable == null)
            {
                return;
            }
            this.repository.UpdateVariable(this.oldClass, oldVariable, newVariable);
            this.RefreshData();
        }

        private void bRemove_Click(object sender, EventArgs e)
        {
            MetaVariable metaVariable = (MetaVariable)this.lbOldVariables.SelectedItem;
            if (metaVariable == null)
            {
                return;
            }
            this.repository.RemoveVariable(this.oldClass, metaVariable);
            this.RefreshData();
        }

        private void bAutoReplace_Click(object sender, EventArgs e)
        {
            MetaVariable newVariable;
            foreach (MetaVariable oldVariable in this.mismatchedVariables)
            {
                newVariable = this.newClass.Variables.Find(v => v.Name.Equals(oldVariable.Name));
                if (newVariable != null)
                {
                    this.repository.UpdateVariable(this.oldClass, oldVariable, newVariable);
                }
            }
            this.RefreshData();
        }
        #endregion

    }
}
