using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Data;
using ClassTools.Data.Hierarchy;

namespace ClassTools.ClassMaker.Forms
{
    public partial class Types : Form
    {
        #region Constants
        const string errorClassDelete = "You cannot delete the class '{0}' using the Types Manager!";
        const string errorLastTypeDelete = "You cannot delete the last simple type '{0}'!";
        #endregion

        #region Fields
        private bool refreshing;
        private Model model;
        #endregion

        #region Construtors
        public Types(Model model)
        {
            InitializeComponent();
            this.model = model;
            this.refreshing = false;
            this.cbTypeCategory.Items.AddRange(Constants.CategoryNames);
            this.refresh();
        }
        #endregion

        #region Methods
        private void refresh()
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            List<MetaType> typesOnly = this.model.TypesOnly;
            Utility.ApplyNewDataSource(this.lbTypes, typesOnly, typesOnly.Count);
            MetaType metaType = this.model.Types[this.lbTypes.SelectedIndex];
            List<MetaType> types = new List<MetaType>(this.model.Types);
            types.Remove(metaType);
            Utility.ApplyNewDataSource(this.cbSubType1, new List<MetaType>(types), types.Count);
            Utility.ApplyNewDataSource(this.cbSubType2, new List<MetaType>(types), types.Count);
            bool enabled = (typesOnly.Count > 1);
            this.bTypeDelete.Enabled = enabled;
            this.tbTypeName.Text = metaType.Name;
            this.cbSubType1.SelectedItem = metaType.SubType1;
            this.cbSubType2.SelectedItem = metaType.SubType2;
            this.tbSuffix1.Text = metaType.Suffix1;
            this.tbSuffix2.Text = metaType.Suffix2;
            if (enabled)
            {
                this.cbTypeCategory.SelectedIndex = (int)metaType.Category;
                switch (this.cbTypeCategory.SelectedIndex)
                {
                    case (int)ECategory.Normal:
                        lSubType1.Enabled = false;
                        cbSubType1.Enabled = false;
                        lSuffix1.Enabled = false;
                        tbSuffix1.Enabled = false;
                        lSubType2.Enabled = false;
                        cbSubType2.Enabled = false;
                        lSuffix2.Enabled = false;
                        tbSuffix2.Enabled = false;
                        break;
                    case (int)ECategory.Collection:
                        lSubType1.Enabled = true;
                        cbSubType1.Enabled = true;
                        lSuffix1.Enabled = true;
                        tbSuffix1.Enabled = true;
                        lSubType2.Enabled = false;
                        cbSubType2.Enabled = false;
                        lSuffix2.Enabled = false;
                        tbSuffix2.Enabled = false;
                        break;
                    case (int)ECategory.Dictionary:
                        lSubType1.Enabled = true;
                        cbSubType1.Enabled = true;
                        lSuffix1.Enabled = true;
                        tbSuffix1.Enabled = true;
                        lSubType2.Enabled = true;
                        cbSubType2.Enabled = true;
                        lSuffix2.Enabled = true;
                        tbSuffix2.Enabled = true;
                        break;
                }
            }
            else
            {
                lSubType1.Enabled = false;
                cbSubType1.Enabled = false;
                lSuffix1.Enabled = false;
                tbSuffix1.Enabled = false;
                lSubType2.Enabled = false;
                cbSubType2.Enabled = false;
                lSuffix2.Enabled = false;
                tbSuffix2.Enabled = false;
            }
            this.refreshing = false;
        }

        private void lbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.refresh();
        }

        private void tbTypeName_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            MetaType metaType = (MetaType)this.lbTypes.SelectedItem;
            metaType.Name = this.tbTypeName.Text;
            this.refreshing = false;
            this.refresh();
        }

        private void bTypeNew_Click(object sender, EventArgs e)
        {
            this.model.CreateNewType(this.lbTypes.SelectedIndex + 1);
            this.refresh();
        }

        private void bTypeDelete_Click(object sender, EventArgs e)
        {
            if (this.lbTypes.SelectedIndex >= 0)
            {
                List<MetaType> types = this.model.TypesOnly;
                if (types.Count == 1)
                {
                    MetaType metaType = (MetaType)this.lbTypes.SelectedItem;
                    MessageBox.Show(string.Format(errorLastTypeDelete, metaType.Name), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MetaType metaType = (MetaType)this.lbTypes.SelectedItem;
                    if (!types.Contains(metaType))
                    {
                        MessageBox.Show(string.Format(errorClassDelete, metaType.Name), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        this.model.DeleteTypeAt(this.lbTypes.SelectedIndex);
                        this.refresh();
                    }
                }
            }
        }

        private void cbTypeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            int index = this.cbTypeCategory.SelectedIndex;
            MetaType metaType = (MetaType)this.lbTypes.SelectedItem;
            switch (index)
            {
                case (int)ECategory.Normal:
                    metaType.SubType1 = null;
                    metaType.SubType2 = null;
                    break;
                case (int)ECategory.Collection:
                    if (metaType.SubType1 == null)
                    {
                        metaType.SubType1 = this.model.Types[0];
                    }
                    metaType.SubType2 = null;
                    break;
                case (int)ECategory.Dictionary:
                    if (metaType.SubType1 == null)
                    {
                        metaType.SubType1 = this.model.Types[0];
                    }
                    if (metaType.SubType2 == null)
                    {
                        metaType.SubType2 = this.model.Types[0];
                    }
                    break;
            }
            this.refreshing = false;
            this.refresh();
        }

        private void cbSubType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            MetaType metaType = (MetaType)this.lbTypes.SelectedItem;
            metaType.SubType1 = (MetaType)this.cbSubType1.SelectedItem;
            this.refreshing = false;
            this.refresh();
        }

        private void cbSubType2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            MetaType metaType = (MetaType)this.lbTypes.SelectedItem;
            metaType.SubType2 = (MetaType)this.cbSubType2.SelectedItem;
            this.refreshing = false;
            this.refresh();
        }

        private void tbSuffix1_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            MetaType metaType = (MetaType)this.lbTypes.SelectedItem;
            metaType.Suffix1 = this.tbSuffix1.Text;
            this.refreshing = false;
            this.refresh();
        }

        private void tbSuffix2_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            MetaType metaType = (MetaType)this.lbTypes.SelectedItem;
            metaType.Suffix2 = this.tbSuffix2.Text;
            this.refreshing = false;
            this.refresh();
        }

        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbTypes.Focused)
            {
                InternalClipboard.Type = (MetaType)this.lbTypes.SelectedItem;
            }
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbTypes.Focused)
            {
                this.model.ReplaceTypeAt(this.lbTypes.SelectedIndex, InternalClipboard.Type);
                this.refresh();
            }
        }

        private void addNewMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbTypes.Focused)
            {
                this.bTypeNew_Click(sender, e);
            }
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbTypes.Focused)
            {
                this.bTypeDelete_Click(sender, e);
            }
        }

        private void moveUpMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbTypes.Focused)
            {
                if (this.model.TryTypeMoveUp(this.lbTypes.SelectedIndex))
                {
                    this.lbTypes.SelectedIndex--;
                }
            }
        }

        private void moveDownMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbTypes.Focused)
            {
                if (this.model.TryTypeMoveDown(this.lbTypes.SelectedIndex))
                {
                    this.lbTypes.SelectedIndex++;
                }
            }
        }
        #endregion

    }
}
