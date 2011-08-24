using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Model;

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
        private ClassModel model;
        #endregion

        #region Construtors
        public Types(ClassModel model)
        {
            InitializeComponent();
            this.model = model;
            this.refreshing = false;
            // same order as ETypeCategory indexes!
            this.cbTypeCategory.Items.Add("Normal");
            this.cbTypeCategory.Items.Add("Collection");
            this.cbTypeCategory.Items.Add("Dictionary");
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
            MetaType type = this.model.Types[this.lbTypes.SelectedIndex];
            List<MetaType> types = new List<MetaType>(this.model.Types);
            types.Remove(type);
            Utility.ApplyNewDataSource(this.cbSubType1, new List<MetaType>(types), types.Count);
            Utility.ApplyNewDataSource(this.cbSubType2, new List<MetaType>(types), types.Count);
            bool enabled = (typesOnly.Count > 1);
            this.bTypeDelete.Enabled = enabled;
            this.tbTypeName.Text = type.Name;
            this.cbSubType1.SelectedItem = type.SubType1;
            this.cbSubType2.SelectedItem = type.SubType2;
            this.tbSuffix1.Text = type.Suffix1;
            this.tbSuffix2.Text = type.Suffix2;
            if (enabled)
            {
                this.cbTypeCategory.SelectedIndex = (int)type.TypeCategory;
                switch (this.cbTypeCategory.SelectedIndex)
                {
                    case (int)ETypeCategory.Normal:
                        lSubType1.Enabled = false;
                        cbSubType1.Enabled = false;
                        lSuffix1.Enabled = false;
                        tbSuffix1.Enabled = false;
                        lSubType2.Enabled = false;
                        cbSubType2.Enabled = false;
                        lSuffix2.Enabled = false;
                        tbSuffix2.Enabled = false;
                        break;
                    case (int)ETypeCategory.Collection:
                        lSubType1.Enabled = true;
                        cbSubType1.Enabled = true;
                        lSuffix1.Enabled = true;
                        tbSuffix1.Enabled = true;
                        lSubType2.Enabled = false;
                        cbSubType2.Enabled = false;
                        lSuffix2.Enabled = false;
                        tbSuffix2.Enabled = false;
                        break;
                    case (int)ETypeCategory.Dictionary:
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
            MetaType type = (MetaType)this.lbTypes.SelectedItem;
            type.Name = this.tbTypeName.Text;
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
                    MetaType type = (MetaType)this.lbTypes.SelectedItem;
                    MessageBox.Show(string.Format(errorLastTypeDelete, type.Name), "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MetaType type = (MetaType)this.lbTypes.SelectedItem;
                    if (!types.Contains(type))
                    {
                        MessageBox.Show(string.Format(errorClassDelete, type.Name), "Error!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            MetaType type = (MetaType)this.lbTypes.SelectedItem;
            switch (index)
            {
                case (int)ETypeCategory.Normal:
                    type.SubType1 = null;
                    type.SubType2 = null;
                    break;
                case (int)ETypeCategory.Collection:
                    if (type.SubType1 == null)
                    {
                        type.SubType1 = this.model.Types[0];
                    }
                    type.SubType2 = null;
                    break;
                case (int)ETypeCategory.Dictionary:
                    if (type.SubType1 == null)
                    {
                        type.SubType1 = this.model.Types[0];
                    }
                    if (type.SubType2 == null)
                    {
                        type.SubType2 = this.model.Types[0];
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
            MetaType type = (MetaType)this.lbTypes.SelectedItem;
            type.SubType1 = (MetaType)this.cbSubType1.SelectedItem;
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
            MetaType type = (MetaType)this.lbTypes.SelectedItem;
            type.SubType2 = (MetaType)this.cbSubType2.SelectedItem;
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
            MetaType type = (MetaType)this.lbTypes.SelectedItem;
            type.Suffix1 = this.tbSuffix1.Text;
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
            MetaType type = (MetaType)this.lbTypes.SelectedItem;
            type.Suffix2 = this.tbSuffix2.Text;
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
