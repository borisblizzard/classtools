﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools;
using ClassTools.Common;
using ClassTools.Common.Forms;
using ClassTools.Data;
using ClassTools.Data.Hierarchy;

namespace ClassTools.ClassMaker.Forms
{
    public partial class Main : Form
    {
        #region Constants
        const string savePromptNew = "There are unsaved changes. Do you want to save before creating a new file?";
        const string savePromptOpen = "There are unsaved changes. Do you want to save before opening another file?";
        const string savePromptExit = "There are unsaved changes. Do you want to save before exiting?";

        const string validationSuccess = "Validation successful.\r\n";
        const string errorClassNameSpaces = "Class '{0}' has spaces in its name!\r\n";
        const string errorVariableNameSpaces = "In class '{0}' variable '{1}' has spaces in its name!\r\n";
        const string errorMethodNameSpaces = "In class '{0}' method '{1}' has spaces in its name!\r\n";
        const string errorClassNameDuplicate = "More than one class has name '{0}'!\r\n";
        const string errorVariableNameDuplicate = "In class '{0}' more than one variable has name '{1}'!\r\n";
        const string errorMethodNameDuplicate = "In class '{0}' more than one method has name '{1}'!\r\n";
        #endregion

        #region Fields
        private Model model;
        private Model lastModel;
        private string lastFilename;
        private bool refreshing;
        private string validationLog;
        private Log windowLog;
        #endregion

        #region Constructors
        public Main(string[] args)
        {
            InitializeComponent();
            this.refreshing = true;
            this.model = new Model();
            this.lastModel = Serializer.Clone(this.model);
            this.lastFilename = string.Empty;
            this.validationLog = string.Empty;
            this.cbVariableAccess.DataSource = new List<string>(ClassTools.Data.Constants.AccessNames);
            this.cbMethodAccess.DataSource = new List<string>(ClassTools.Data.Constants.AccessNames);
            this.windowLog = new Log();
            this.windowLog.Hide();
            if (args.Length > 0)
            {
                Stream stream = File.OpenRead(args[0]);
                this.model = Serializer.Deserialize(stream, this.model);
                this.lastFilename = args[0];
                stream.Close();
                this.lastModel = Serializer.Clone(this.model);
            }
            this.refreshing = false;
            this.refresh();
        }
        #endregion

        #region Save / Load
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.showSaveChangesDialog(savePromptOpen);
            if (result != DialogResult.Cancel)
            {
                ofd.FileName = string.Empty;
                result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Stream stream = ofd.OpenFile();
                    this.model = Serializer.Deserialize(stream, this.model);
                    this.lastFilename = ofd.FileName;
                    stream.Close();
                    this.lastModel = Serializer.Clone(this.model);
                    this.refresh();
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showSaveDialog();
            this.lastFilename = sfd.FileName;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lastFilename != string.Empty)
            {
                using (FileStream stream = new FileStream(this.lastFilename, FileMode.Truncate))
                {
                    this.save(stream);
                }
            }
            else
            {
                this.showSaveDialog();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.showSaveChangesDialog(savePromptNew);
            if (result != DialogResult.Cancel)
            {
                this.model = new Model();
                this.lastModel = Serializer.Clone(this.model);
                this.lastFilename = string.Empty;
                this.refresh();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = this.showSaveChangesDialog(savePromptExit);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private DialogResult showSaveChangesDialog(string text)
        {
            DialogResult result = DialogResult.OK;
            if (!this.model.Equals(this.lastModel))
            {
                result = MessageBox.Show(text, "Unsaved Changes", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    result = this.showSaveDialog();
                }
            }
            return result;
        }

        private DialogResult showSaveDialog()
        {
            if (this.lastFilename != string.Empty)
            {
                sfd.FileName = this.lastFilename;
            }
            DialogResult result = sfd.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (Stream stream = sfd.OpenFile())
                {
                    this.save(stream);
                }
            }
            return result;
        }

        private void save(Stream stream)
        {
            Serializer.Serialize(stream, this.model);
            this.lastModel = Serializer.Clone(this.model);
        }
        #endregion

        #region Refresh
        private void refresh()
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            Utility.ApplyNewDataSource(this.lbClasses, new List<MetaClass>(this.model.Classes), this.model.Classes.Count);
            Utility.ApplyNewDataSource(this.cbSuperClass, new List<MetaClass>(this.model.Classes), this.model.Classes.Count);
            Utility.ApplyNewDataSource(this.cbVariableType, new List<MetaType>(this.model.Types), this.model.Classes.Count);
            Utility.ApplyNewDataSource(this.cbMethodType, new List<MetaType>(this.model.Types), this.model.Classes.Count);
            MetaClass metaClass = (MetaClass)this.lbClasses.SelectedItem;
            bool enabled = (metaClass != null);
            this.gbClass.Enabled = enabled;
            this.gbVariables.Enabled = enabled;
            this.gbMethods.Enabled = enabled;
            this.bClassDelete.Enabled = enabled;
            if (enabled)
            {
                this.tbClassName.Text = metaClass.Name;
                this.tbClassModule.Text = metaClass.Module;
                this.cbxClassSerialize.Checked = metaClass.CanSerialize;
                this.cbInheritance.Checked = metaClass.HasSuperClass;
                this.cbSuperClass.Enabled = this.cbInheritance.Checked;
                if (metaClass.HasSuperClass)
                {
                    this.cbSuperClass.SelectedItem = metaClass.SuperClass;
                }
                else
                {
                    this.cbSuperClass.SelectedIndex = 0;
                }
                Utility.ApplyNewDataSource(this.lbVariables, new List<MetaVariable>(metaClass.Variables), metaClass.Variables.Count);
                Utility.ApplyNewDataSource(this.lbMethods, new List<MetaMethod>(metaClass.Methods), metaClass.Methods.Count);
                this.refreshVariable();
                this.refreshMethod();
            }
            else
            {
                this.lbVariables.DataSource = null;
                this.lbMethods.DataSource = null;
                this.tbClassName.Text = string.Empty;
                this.tbClassModule.Text = string.Empty;
            }
            this.refreshing = false;
        }

        private void refreshVariable()
        {
            this.bVariableDelete.Enabled = (this.lbVariables.Items.Count > 0);
            MetaVariable variable = (MetaVariable)this.lbVariables.SelectedItem;
            bool enabled = (variable != null);
            this.lVariableName.Enabled = enabled;
            this.tbVariableName.Enabled = enabled;
            this.lVariableType.Enabled = enabled;
            this.cbVariableType.Enabled = enabled;
            this.lVariableAccess.Enabled = enabled;
            this.cbVariableAccess.Enabled = enabled;
            this.lVariableDefault.Enabled = enabled;
            this.tbVariableDefault.Enabled = enabled;
            this.cbxVariableGetter.Enabled = enabled;
            this.cbxVariableSetter.Enabled = enabled;
            this.cbxVariableSerialize.Enabled = enabled;
            this.lVariablePrefix.Enabled = enabled;
            this.tbVariablePrefix.Enabled = enabled;
            if (enabled)
            {
                this.tbVariableName.Text = variable.Name;
                this.cbVariableType.SelectedItem = variable.Type;
                this.cbVariableAccess.SelectedIndex = (int)variable.Access;
                this.tbVariableDefault.Text = variable.DefaultValue;
                this.cbxVariableGetter.Checked = variable.Getter;
                this.cbxVariableSetter.Checked = variable.Setter;
                this.cbxVariableSerialize.Checked = variable.CanSerialize;
                this.tbVariablePrefix.Text = variable.Prefix;
            }
            else
            {
                this.tbVariableName.Text = string.Empty;
                this.cbVariableType.SelectedIndex = (this.cbVariableType.Items.Count > 0 ? 0 : -1);
                this.cbVariableAccess.SelectedIndex = (int)EAccess.Public;
                this.tbVariableDefault.Text = string.Empty;
                this.cbxVariableGetter.Checked = false;
                this.cbxVariableSetter.Checked = false;
                this.cbxVariableSerialize.Checked = true;
                this.tbVariablePrefix.Text = string.Empty;
            }
        }

        private void refreshMethod()
        {
            this.bMethodDelete.Enabled = (this.lbMethods.Items.Count > 0);
            MetaMethod metaMethod = (MetaMethod)this.lbMethods.SelectedItem;
            bool enabled = (metaMethod != null);
            this.lMethodName.Enabled = enabled;
            this.tbMethodName.Enabled = enabled;
            this.lMethodType.Enabled = enabled;
            this.cbMethodType.Enabled = enabled;
            this.lMethodAccess.Enabled = enabled;
            this.cbMethodAccess.Enabled = enabled;
            this.lMethodPrefix.Enabled = enabled;
            this.tbMethodPrefix.Enabled = enabled;
            this.bMethodParameters.Enabled = enabled;
            this.bMethodImplementation.Enabled = enabled;
            if (enabled)
            {
                this.tbMethodName.Text = metaMethod.Name;
                this.cbMethodType.SelectedItem = metaMethod.Type;
                this.cbMethodAccess.SelectedIndex = (int)metaMethod.Access;
                this.tbMethodPrefix.Text = metaMethod.Prefix;
            }
            else
            {
                this.tbMethodName.Text = string.Empty;
                this.cbMethodType.SelectedIndex = (this.cbMethodType.Items.Count > 0 ? 0 : -1);
                this.cbMethodAccess.SelectedIndex = (int)EAccess.Public;
                this.tbMethodPrefix.Text = string.Empty;
            }
        }

        #endregion

        #region Create / Delete
        private void bClassNew_Click(object sender, EventArgs e)
        {
            this.model.CreateNewClass(this.lbClasses.SelectedIndex + 1);
            this.refresh();
            this.lbClasses.Focus();
        }

        private void bClassDelete_Click(object sender, EventArgs e)
        {
            //2DO - add check for usage of class
            this.model.DeleteClassAt(this.lbClasses.SelectedIndex);
            this.refresh();
            this.lbClasses.Focus();
        }

        private void bVariableNew_Click(object sender, EventArgs e)
        {
            this.model.Classes[this.lbClasses.SelectedIndex].CreateNewVariable(this.lbVariables.SelectedIndex + 1);
            this.refresh();
            this.lbVariables.Focus();
        }

        private void bVariableDelete_Click(object sender, EventArgs e)
        {
            this.model.Classes[this.lbClasses.SelectedIndex].DeleteVariableAt(this.lbVariables.SelectedIndex);
            this.refresh();
            this.lbVariables.Focus();
        }

        private void bMethodNew_Click(object sender, EventArgs e)
        {
            this.model.Classes[this.lbClasses.SelectedIndex].CreateNewMethod(this.lbMethods.SelectedIndex + 1);
            this.refresh();
            this.lbMethods.Focus();
        }

        private void bMethodDelete_Click(object sender, EventArgs e)
        {
            this.model.Classes[this.lbClasses.SelectedIndex].DeleteMethodAt(this.lbMethods.SelectedIndex);
            this.refresh();
            this.lbMethods.Focus();
        }
        #endregion

        #region Tools
        private void validateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.validationLog = string.Empty;
            for (int i = 0; i < this.model.Classes.Count; i++)
            {
                this.validateClass(this.model.Classes[i], i);
            }
            if (this.validationLog == string.Empty)
            {
                this.windowLog.SetText(validationSuccess);
            }
            else
            {
                this.windowLog.SetText(this.validationLog);
            }
            this.windowLog.Hide();
            this.windowLog.Show();
        }

        private void validateClass(MetaClass metaClass, int index)
        {
            if (metaClass.Name.Contains(" "))
            {
                this.validationLog += string.Format(errorClassNameSpaces, metaClass.Name);
            }
            else
            {
                for (int j = index + 1; j < this.model.Classes.Count; j++)
                {
                    if (metaClass.Name == this.model.Classes[j].Name)
                    {
                        this.validationLog += string.Format(errorClassNameDuplicate, metaClass.Name);
                    }
                }
            }
            for (int j = 0; j < metaClass.Variables.Count; j++)
            {
                this.validateVariable(metaClass, metaClass.Variables[j], j);
            }
            for (int j = 0; j < metaClass.Methods.Count; j++)
            {
                this.validateMethod(metaClass, metaClass.Methods[j], j);
            }
        }

        private void validateVariable(MetaClass metaClass, MetaVariable metaVariable, int index)
        {
            if (metaVariable.Name.Contains(" "))
            {
                this.validationLog += string.Format(errorVariableNameSpaces, metaClass.Name, metaVariable.Name);
            }
            else
            {
                for (int i = index + 1; i < metaClass.Variables.Count; i++)
                {
                    if (metaVariable.Name == metaClass.Variables[i].Name)
                    {
                        this.validationLog += string.Format(errorVariableNameDuplicate, metaClass.Name, metaVariable.Name);
                    }
                }
            }
        }

        private void validateMethod(MetaClass metaClass, MetaMethod metaMethod, int index)
        {
            if (metaMethod.Name.Contains(" "))
            {
                this.validationLog += string.Format(errorMethodNameSpaces, metaClass.Name, metaMethod.Name);
            }
            else
            {
                for (int i = index + 1; i < metaClass.Methods.Count; i++)
                {
                    if (metaMethod.Name == metaClass.Methods[i].Name)
                    {
                        this.validationLog += string.Format(errorMethodNameDuplicate, metaClass.Name, metaMethod.Name);
                    }
                }
            }
        }

        private void manageTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Types f = new Types(this.model);
            f.ShowDialog();
            this.refresh();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About f = new About();
            f.ShowDialog();
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Generator f = new Generator();
            DialogResult result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                f.Execute(this.model, null);
            }
        }

        private void bMethodParameters_Click(object sender, EventArgs e)
        {
            MessageBox.Show("IMPLEMENT ME");
            /*
            FormParameters f = new FormParameters((MetaMethod)this.lbMethods.SelectedItem);
            f.ShowDialog();
            */
        }

        private void bMethodImplementation_Click(object sender, EventArgs e)
        {
            Implementation f = new Implementation((MetaMethod)this.lbMethods.SelectedItem);
            DialogResult result = f.ShowDialog();
        }

        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbClasses.Focused)
            {
                InternalClipboard.Class = (MetaClass)this.lbClasses.SelectedItem;
            }
            else if (this.lbVariables.Focused)
            {
                InternalClipboard.Variable = (MetaVariable)this.lbVariables.SelectedItem;
            }
            else if (this.lbMethods.Focused)
            {
                InternalClipboard.Method = (MetaMethod)this.lbMethods.SelectedItem;
            }
        }

        private void pasteMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbClasses.Focused && InternalClipboard.ContainsClass)
            {
                this.model.ReplaceClassAt(this.lbClasses.SelectedIndex, InternalClipboard.Class);
                this.refresh();
            }
            else if (this.lbVariables.Focused && InternalClipboard.ContainsVariable)
            {
                MetaClass metaClass = this.model.Classes[this.lbClasses.SelectedIndex];
                metaClass.ReplaceVariableAt(this.lbVariables.SelectedIndex, InternalClipboard.Variable);
                this.refresh();
            }
            else if (this.lbMethods.Focused && InternalClipboard.ContainsMethod)
            {
                MetaClass metaClass = this.model.Classes[this.lbClasses.SelectedIndex];
                metaClass.ReplaceMethodAt(this.lbMethods.SelectedIndex, InternalClipboard.Method);
                this.refresh();
            }
        }

        private void sortMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbClasses.Focused)
            {
                this.model.SortClasses();
                this.refresh();
            }
            else if (this.lbVariables.Focused)
            {
                this.model.Classes[this.lbClasses.SelectedIndex].SortVariables();
                this.refresh();
            }
            else if (this.lbMethods.Focused)
            {
                this.model.Classes[this.lbClasses.SelectedIndex].SortMethods();
                this.refresh();
            }
        }

        private void addNewMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbClasses.Focused)
            {
                this.bClassNew_Click(sender, e);
            }
            else if (this.lbVariables.Focused)
            {
                this.bVariableNew_Click(sender, e);
            }
            else if (this.lbMethods.Focused)
            {
                this.bMethodNew_Click(sender, e);
            }
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbClasses.Focused)
            {
                this.bClassDelete_Click(sender, e);
            }
            else if (this.lbVariables.Focused)
            {
                this.bVariableDelete_Click(sender, e);
            }
            else if (this.lbMethods.Focused)
            {
                this.bMethodDelete_Click(sender, e);
            }
        }

        private void moveUpMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbClasses.Focused)
            {
                if (this.model.TryClassMoveUp(this.lbClasses.SelectedIndex))
                {
                    this.lbClasses.SelectedIndex--;
                }
            }
            else if (this.lbVariables.Focused)
            {
                if (this.model.Classes[this.lbClasses.SelectedIndex].TryVariableMoveUp(this.lbVariables.SelectedIndex))
                {
                    this.refreshing = true;
                    this.lbVariables.SelectedIndex--;
                    this.refreshing = false;
                    this.refresh();
                }
            }
            else if (this.lbMethods.Focused)
            {
                if (this.model.Classes[this.lbClasses.SelectedIndex].TryMethodMoveUp(this.lbMethods.SelectedIndex))
                {
                    this.refreshing = true;
                    this.lbMethods.SelectedIndex--;
                    this.refreshing = false;
                    this.refresh();
                }
            }
        }

        private void moveDownMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbClasses.Focused)
            {
                if (this.model.TryClassMoveDown(this.lbClasses.SelectedIndex))
                {
                    this.lbClasses.SelectedIndex++;
                }
            }
            else if (this.lbVariables.Focused)
            {
                if (this.model.Classes[this.lbClasses.SelectedIndex].TryVariableMoveDown(this.lbVariables.SelectedIndex))
                {
                    this.refreshing = true;
                    this.lbVariables.SelectedIndex++;
                    this.refreshing = false;
                    this.refresh();
                }
            }
            else if (this.lbMethods.Focused)
            {
                if (this.model.Classes[this.lbClasses.SelectedIndex].TryMethodMoveDown(this.lbMethods.SelectedIndex))
                {
                    this.refreshing = true;
                    this.lbMethods.SelectedIndex++;
                    this.refreshing = false;
                    this.refresh();
                }
            }
        }
        #endregion

        #region Class Sync
        private void lbClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.refresh();
        }

        private void tbClassName_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaClass metaClass = (MetaClass)this.lbClasses.SelectedItem;
            metaClass.Name = this.tbClassName.Text;
            this.refresh();
        }

        private void tbClassModule_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaClass metaClass = (MetaClass)this.lbClasses.SelectedItem;
            metaClass.Module = this.tbClassModule.Text;
            this.refresh();
        }

        private void cbInheritance_CheckedChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaClass metaClass = (MetaClass)this.lbClasses.SelectedItem;
            metaClass.SuperClass = (this.cbInheritance.Checked ? (MetaClass)this.cbSuperClass.Items[0] : null);
            this.refresh();
        }

        private void cbxClassSerialize_CheckedChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaClass metaClass = (MetaClass)this.lbClasses.SelectedItem;
            metaClass.CanSerialize = this.cbxClassSerialize.Checked;
        }

        private void cbSuperClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaClass metaClass = (MetaClass)this.lbClasses.SelectedItem;
            metaClass.SuperClass = (this.cbInheritance.Checked ? (MetaClass)this.cbSuperClass.SelectedItem : null);
        }
        #endregion

        #region Variable Sync
        private void lbVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            this.refreshVariable();
            this.refreshing = false;
        }

        private void tbVariableName_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaVariable variable = (MetaVariable)this.lbVariables.SelectedItem;
            variable.Name = this.tbVariableName.Text;
            this.refresh();
        }

        private void cbVariableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaVariable variable = (MetaVariable)this.lbVariables.SelectedItem;
            variable.Type = (MetaType)this.cbVariableType.SelectedItem;
            this.refresh();
        }

        private void cbVariableAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaVariable variable = (MetaVariable)this.lbVariables.SelectedItem;
            variable.Access = (EAccess)this.cbVariableAccess.SelectedIndex;
        }

        private void tbVariableDefault_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaVariable variable = (MetaVariable)this.lbVariables.SelectedItem;
            variable.DefaultValue = this.tbVariableDefault.Text;
        }

        private void cbxVariableGetter_CheckedChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaVariable variable = (MetaVariable)this.lbVariables.SelectedItem;
            variable.Getter = this.cbxVariableGetter.Checked;
        }

        private void cbxVariableSetter_CheckedChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaVariable variable = (MetaVariable)this.lbVariables.SelectedItem;
            variable.Setter = this.cbxVariableSetter.Checked;
        }

        private void cbxVariableSerialize_CheckedChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaVariable variable = (MetaVariable)this.lbVariables.SelectedItem;
            variable.CanSerialize = this.cbxVariableSerialize.Checked;
        }

        private void tbVariablePrefix_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaVariable variable = (MetaVariable)this.lbVariables.SelectedItem;
            variable.Prefix = this.tbVariablePrefix.Text;
            this.refresh();
        }
        #endregion

        #region Method Sync
        private void lbMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            this.refreshMethod();
            this.refreshing = false;
        }

        private void tbMethodName_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaMethod metaMethod = (MetaMethod)this.lbMethods.SelectedItem;
            metaMethod.Name = this.tbMethodName.Text;
            this.refresh();
        }

        private void cbMethodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaMethod metaMethod = (MetaMethod)this.lbMethods.SelectedItem;
            metaMethod.Type = (MetaType)this.cbMethodType.SelectedItem;
            this.refresh();
        }

        private void cbMethodAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaMethod metaMethod = (MetaMethod)this.lbMethods.SelectedItem;
            metaMethod.Access = (EAccess)this.cbMethodAccess.SelectedIndex;
        }

        private void tbMethodPrefix_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaMethod metaMethod = (MetaMethod)this.lbMethods.SelectedItem;
            metaMethod.Prefix = this.tbMethodPrefix.Text;
            this.refresh();
        }
        #endregion

    }
}
