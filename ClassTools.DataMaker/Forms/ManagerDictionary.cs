using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;

namespace ClassTools.DataMaker.Forms
{
    public partial class ManagerDictionary : Form, IRefreshable
    {
        #region Constants
        private const string ERROR_DUPLICATE_KEYS = "There are duplicate key entries.";
        #endregion

        #region Properties
        public MetaDictionary<MetaValue, MetaValue> DictionaryValues
        {
            get { return this.vdValues.DictionaryValues; }
        }
        #endregion

        #region Construct
        public ManagerDictionary(Repository repository, MetaType metaType1, MetaType metaType2, MetaDictionary<MetaValue, MetaValue> metaDictionary)
        {
            InitializeComponent();
            this.vdValues.SetData(this, repository, metaType1, metaType2, metaDictionary);
        }
        #endregion

        #region Refresh
        public void RefreshData()
        {
        }
        #endregion

        #region Tools
        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            this.vdValues.CopyValue();
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            this.vdValues.PasteValue();
        }

        private void addNewMenuItem_Click(object sender, EventArgs e)
        {
            this.vdValues.AddNewValue();
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            this.vdValues.DeleteValue();
        }

        private void moveUpMenuItem_Click(object sender, EventArgs e)
        {
            this.vdValues.MoveUpValue();
        }

        private void moveDownMenuItem_Click(object sender, EventArgs e)
        {
            this.vdValues.MoveDownValue();
        }
        #endregion

        #region Events
        private void closeMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            MetaList<MetaValue> keys = new MetaList<MetaValue>();
            foreach (MetaValue key in this.vdValues.Keys)
            {
                if (keys.Contains(key))
                {
                    e.Cancel = true;
                    MessageBox.Show(ERROR_DUPLICATE_KEYS, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
                keys.Add(key);
            }
        }
        #endregion

    }
}
