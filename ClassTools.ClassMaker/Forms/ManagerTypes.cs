using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Data;
using ClassTools.Data.Hierarchy;

namespace ClassTools.ClassMaker.Forms
{
    public partial class ManagerTypes : Form
    {
        #region Constants
        const string errorClassDelete = "You cannot delete the class '{0}' using the Types Manager!";
        const string errorLastTypeDelete = "You cannot delete the last integral type '{0}'!";
        #endregion

        #region Fields
        private bool refreshing;
        private Model model;
        #endregion

        #region Construtors
        public ManagerTypes(Model model)
        {
            InitializeComponent();
            this.model = model;
            this.refreshing = false;
            this.cbCategoryType.Items.AddRange(Constants.NAMES_CATEGORY.ToArray());
            this.RefreshData();
        }
        #endregion

        #region Methods
        public void RefreshData()
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            MetaList<MetaType> types = new MetaList<MetaType>(this.model.Types);
            Utility.ApplyNewDataSource(this.lbTypes, types, types.Count);
            MetaList<MetaType> allTypes = new MetaList<MetaType>(this.model.AllTypes);
            MetaType metaType = allTypes[this.lbTypes.SelectedIndex];
            allTypes.Remove(metaType);
            Utility.ApplyNewDataSource(this.cbSubType1, new MetaList<MetaType>(allTypes), allTypes.Count);
            Utility.ApplyNewDataSource(this.cbSubType2, new MetaList<MetaType>(allTypes), allTypes.Count);
            bool enabled = (types.Count > 1);
            this.bTypeDelete.Enabled = enabled;
            this.RefreshType();
            this.refreshing = false;
        }

        public void RefreshType()
        {
            MetaList<MetaType> allTypes = new MetaList<MetaType>(this.model.AllTypes);
            MetaType metaType = allTypes[this.lbTypes.SelectedIndex];
            allTypes.Remove(metaType);
            Utility.ApplyNewDataSource(this.cbSubType1, new MetaList<MetaType>(allTypes), allTypes.Count);
            Utility.ApplyNewDataSource(this.cbSubType2, new MetaList<MetaType>(allTypes), allTypes.Count);
            this.tbTypeName.Text = metaType.Name;
            this.cbSubType1.SelectedItem = metaType.SubType1;
            this.cbSubType2.SelectedItem = metaType.SubType2;
            this.tbSuffix1.Text = metaType.Suffix1;
            this.tbSuffix2.Text = metaType.Suffix2;
			if (metaType.EnumValues == null)
			{
				this.tbEnumValues.Text = "";
			}
			else
			{
				this.tbEnumValues.Text = string.Join(" ", metaType.EnumValues.ToArray());
			}
            this.cbCategoryType.SelectedIndex = (int)metaType.CategoryType;
            switch (this.cbCategoryType.SelectedIndex)
            {
                case (int)ECategoryType.Integral:
                    lSubType1.Enabled = false;
                    cbSubType1.Enabled = false;
                    lSuffix1.Enabled = false;
                    tbSuffix1.Enabled = false;
                    lSubType2.Enabled = false;
                    cbSubType2.Enabled = false;
                    lSuffix2.Enabled = false;
                    tbSuffix2.Enabled = false;
					lEnumValues.Enabled = false;
					tbEnumValues.Enabled = false;
                    break;
                case (int)ECategoryType.List:
                    lSubType1.Enabled = true;
                    cbSubType1.Enabled = true;
                    lSuffix1.Enabled = true;
                    tbSuffix1.Enabled = true;
					lSubType2.Enabled = false;
					cbSubType2.Enabled = false;
					lSuffix2.Enabled = false;
					tbSuffix2.Enabled = false;
					lEnumValues.Enabled = false;
					tbEnumValues.Enabled = false;
					break;
				case (int)ECategoryType.Dictionary:
					lSubType1.Enabled = true;
					cbSubType1.Enabled = true;
					lSuffix1.Enabled = true;
					tbSuffix1.Enabled = true;
					lSubType2.Enabled = true;
					cbSubType2.Enabled = true;
					lSuffix2.Enabled = true;
					tbSuffix2.Enabled = true;
					lEnumValues.Enabled = false;
					tbEnumValues.Enabled = false;
					break;
				case (int)ECategoryType.Enum:
					lSubType1.Enabled = false;
					cbSubType1.Enabled = false;
					lSuffix1.Enabled = false;
					tbSuffix1.Enabled = false;
					lSubType2.Enabled = false;
					cbSubType2.Enabled = false;
					lSuffix2.Enabled = false;
					tbSuffix2.Enabled = false;
					lEnumValues.Enabled = true;
					tbEnumValues.Enabled = true;
					break;
			}
		}

        private void lbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            this.RefreshType();
            this.refreshing = false;
			this.RefreshData();
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
            this.RefreshData();
        }

        private void bTypeNew_Click(object sender, EventArgs e)
        {
            this.model.CreateNewType(this.lbTypes.SelectedIndex + 1);
            this.RefreshData();
            this.lbTypes.SelectedIndex++;
        }

        private void bTypeDelete_Click(object sender, EventArgs e)
        {
            this.tryDeleteType();
        }

        private void tryDeleteType()
        {
            if (this.lbTypes.SelectedIndex >= 0)
            {
                MetaList<MetaType> types = this.model.Types;
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
                        this.RefreshData();
                    }
                }
            }
        }

        private void cbCategoryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            ECategoryType categoryType = (ECategoryType)this.cbCategoryType.SelectedIndex;
            MetaType metaType = (MetaType)this.lbTypes.SelectedItem;
            switch (categoryType)
            {
                case ECategoryType.Integral:
                    metaType.SubType1 = null;
                    metaType.SubType2 = null;
					metaType.EnumValues = null;
                    break;
                case ECategoryType.List:
                    if (metaType.SubType1 == null)
                    {
                        metaType.SubType1 = this.model.AllTypes[0];
                    }
                    metaType.SubType2 = null;
					metaType.EnumValues = null;
					break;
                case ECategoryType.Dictionary:
                    if (metaType.SubType1 == null)
                    {
                        metaType.SubType1 = this.model.AllTypes[0];
                    }
                    if (metaType.SubType2 == null)
                    {
                        metaType.SubType2 = this.model.AllTypes[0];
                    }
					metaType.EnumValues = null;
					break;
				case ECategoryType.Enum:
					metaType.SubType1 = null;
					metaType.SubType2 = null;
					metaType.EnumValues = new List<string>();
					break;
			}
			this.refreshing = false;
            this.RefreshData();
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
            this.RefreshData();
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
            this.RefreshData();
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
            this.RefreshData();
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
            this.RefreshData();
        }

		private void tbEnumValues_TextChanged(object sender, EventArgs e)
		{
			if (this.refreshing)
			{
				return;
			}
			this.refreshing = true;
			MetaType metaType = (MetaType)this.lbTypes.SelectedItem;
			metaType.EnumValues = new List<string>(this.tbEnumValues.Text.Split(new char[] { ' ' }));
			this.refreshing = false;
			this.RefreshData();
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
                this.model.Types[this.lbTypes.SelectedIndex] = InternalClipboard.Type;
                this.model.Types[this.lbTypes.SelectedIndex].Update(this.model);
                this.RefreshData();
            }
        }

        private void addNewMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbTypes.Focused)
            {
                this.model.CreateNewType(this.lbTypes.SelectedIndex + 1);
                this.RefreshData();
                this.lbTypes.SelectedIndex++;
            }
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbTypes.Focused)
            {
                this.tryDeleteType();
            }
        }

        private void moveUpMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbTypes.Focused && this.model.Types.TryMoveUp(this.lbTypes.SelectedIndex))
            {
                this.lbTypes.SelectedIndex--;
            }
        }

        private void moveDownMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbTypes.Focused && this.model.Types.TryMoveDown(this.lbTypes.SelectedIndex))
            {
                this.lbTypes.SelectedIndex++;
            }
        }
		#endregion

	}
}
