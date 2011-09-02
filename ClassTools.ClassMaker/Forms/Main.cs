using System;
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
        const string SAVE_PROMPT_NEW = "There are unsaved changes. Do you want to save before creating a new file?";
        const string SAVE_PROMPT_OPEN = "There are unsaved changes. Do you want to save before opening another file?";
        const string SAVE_PROMPT_EXIT = "There are unsaved changes. Do you want to save before exiting?";

        const string NOTIFICATION_VALIDATION_SUCCESS = "Validation successful.\r\n";
        const string ERROR_CLASS_NAME_SPACES = "Class '{0}' has spaces in its name!\r\n";
        const string ERROR_VARIABLE_NAME_SPACES = "In class '{0}' variable '{1}' has spaces in its name!\r\n";
        const string ERROR_METHOD_NAME_SPACES = "In class '{0}' method '{1}' has spaces in its name!\r\n";
        const string ERROR_CLASS_NAME_DUPLICATE = "More than one class has name '{0}'!\r\n";
        const string ERROR_VARIABLE_NAME_DUPLICATE = "In class '{0}' more than one variable has name '{1}'!\r\n";
        const string ERROR_METHOD_NAME_DUPLICATE = "In class '{0}' more than one method has name '{1}'!\r\n";
        #endregion

        #region Fields
        private Model model;
        private Model lastModel;
        private string lastFilename;
        private bool refreshing;
        private string validationLog;
        private Log windowLog;
        #endregion

        #region Construct
        public Main(string[] args)
        {
            InitializeComponent();
            this.refreshing = true;
            this.model = new Model();
            this.lastModel = Serializer.Clone(this.model);
            this.lastFilename = string.Empty;
            this.validationLog = string.Empty;
            this.cbVariableAccessType.DataSource = new List<string>(Constants.NAMES_ACCESS);
            this.cbVariableAccessType.SelectedIndex = -1;
            this.cbMethodAccessType.DataSource = new List<string>(Constants.NAMES_ACCESS);
            this.cbMethodAccessType.SelectedIndex = -1;
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
            this.RefreshData();
        }
        #endregion

        #region Save / Load
        private void openMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.showSaveChangesDialog(SAVE_PROMPT_OPEN);
            if (result != DialogResult.Cancel)
            {
                this.ofd.FileName = string.Empty;
                result = this.ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Stream stream = this.ofd.OpenFile();
                    this.model = Serializer.Deserialize(stream, this.model);
                    this.lastFilename = this.ofd.FileName;
                    stream.Close();
                    this.lastModel = Serializer.Clone(this.model);
                    this.RefreshData();
                }
            }
        }

        private void saveAsMenuItem_Click(object sender, EventArgs e)
        {
            this.showSaveDialog();
            this.lastFilename = this.sfd.FileName;
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
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

        private void newMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = this.showSaveChangesDialog(SAVE_PROMPT_NEW);
            if (result != DialogResult.Cancel)
            {
                this.model = new Model();
                this.lastModel = Serializer.Clone(this.model);
                this.lastFilename = string.Empty;
                this.RefreshData();
            }
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = this.showSaveChangesDialog(SAVE_PROMPT_EXIT);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            About form = new About();
            form.ShowDialog();
        }

        private void manageTypesMenuItem_Click(object sender, EventArgs e)
        {
            ManagerTypes form = new ManagerTypes(this.model);
            form.ShowDialog();
            this.RefreshData();
        }

        private void generateMenuItem_Click(object sender, EventArgs e)
        {
            Generator form = new Generator("ClassMaker");
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                form.Execute(this.model);
            }
        }

        private DialogResult showSaveChangesDialog(string text)
        {
            DialogResult result = DialogResult.OK;
            if (!this.model.Equals(this.lastModel))
            {
                result = MessageBox.Show(text, "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
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
        public void RefreshData()
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            Utility.ApplyNewDataSource(this.lbClasses, new List<MetaClass>(this.model.Classes), this.model.Classes.Count);
            MetaClass metaClass = (MetaClass)this.lbClasses.SelectedItem;
            bool enabled = (metaClass != null);
            List<MetaClass> metaClasses = new List<MetaClass>(this.model.Classes);
            if (enabled)
            {
                List<MetaClass> subClasses = metaClass.FindSubClasses(metaClasses, true);
                foreach (MetaClass classe in subClasses)
                {
                    for (int i = 0; i < metaClasses.Count; i++)
                    {
                        if (metaClasses[i] == classe)
                        {
                            metaClasses.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
            Utility.ApplyNewDataSource(this.cbSuperClass, metaClasses, metaClasses.Count);
            Utility.ApplyNewDataSource(this.cbVariableType, new List<MetaType>(this.model.AllTypes), this.model.Classes.Count);
            Utility.ApplyNewDataSource(this.cbMethodType, new List<MetaType>(this.model.AllTypes), this.model.Classes.Count);
            this.gbClass.Enabled = enabled;
            this.gbVariables.Enabled = enabled;
            this.gbMethods.Enabled = enabled;
            this.bClassDelete.Enabled = enabled;
            if (enabled)
            {
                this.tbClassName.Text = metaClass.Name;
                this.tbClassModule.Text = metaClass.Module;
                this.cbxClassSerialize.Checked = metaClass.CanSerialize;
                this.cbInheritance.Enabled = (this.cbSuperClass.Items.Count > 0);
                this.cbInheritance.Checked = metaClass.HasSuperClass;
                this.cbSuperClass.Enabled = (this.cbInheritance.Checked && this.cbInheritance.Enabled);
                if (metaClass.HasSuperClass)
                {
                    this.cbSuperClass.SelectedItem = metaClass.SuperClass;
                }
                else if (this.cbSuperClass.Items.Count > 0)
                {
                    this.cbSuperClass.SelectedIndex = 0;
                }
                Utility.ApplyNewDataSource(this.lbVariables, new MetaList<MetaVariable>(metaClass.Variables), metaClass.Variables.Count);
                Utility.ApplyNewDataSource(this.lbMethods, new MetaList<MetaMethod>(metaClass.Methods), metaClass.Methods.Count);
                this.RefreshVariable();
                this.RefreshMethod();
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

        public void RefreshVariable()
        {
            this.bVariableDelete.Enabled = (this.lbVariables.Items.Count > 0);
            MetaVariable metaVariable = (MetaVariable)this.lbVariables.SelectedItem;
            if (metaVariable != null)
            {
                this.pVariables.Enabled = true;
                this.cbxVariableNullable.Enabled = (metaVariable.Type.CategoryType == ECategoryType.Class);
                this.tbVariableName.Text = metaVariable.Name;
                this.cbVariableType.SelectedItem = metaVariable.Type;
                this.cbVariableAccessType.SelectedIndex = (int)metaVariable.AccessType;
                this.tbVariableDefault.Text = metaVariable.DefaultValue;
                this.cbxVariableGetter.Checked = metaVariable.Getter;
                this.cbxVariableSetter.Checked = metaVariable.Setter;
                this.cbxVariableNullable.Checked = metaVariable.Nullable;
                this.cbxVariableSerialize.Checked = metaVariable.CanSerialize;
                this.tbVariablePrefix.Text = metaVariable.Prefix;
            }
            else
            {
                this.pVariables.Enabled = false;
                this.tbVariableName.Text = string.Empty;
                this.cbVariableType.SelectedIndex = -1;
                this.cbVariableAccessType.SelectedIndex = -1;
                this.tbVariableDefault.Text = string.Empty;
                this.cbxVariableGetter.Checked = false;
                this.cbxVariableSetter.Checked = false;
                this.cbxVariableNullable.Checked = false;
                this.cbxVariableSerialize.Checked = true;
                this.tbVariablePrefix.Text = string.Empty;
            }
        }

        public void RefreshMethod()
        {
            this.bMethodDelete.Enabled = (this.lbMethods.Items.Count > 0);
            MetaMethod metaMethod = (MetaMethod)this.lbMethods.SelectedItem;
            if (metaMethod != null)
            {
                this.pMethods.Enabled = true;
                this.tbMethodName.Text = metaMethod.Name;
                this.cbMethodType.SelectedItem = metaMethod.Type;
                this.cbMethodAccessType.SelectedIndex = (int)metaMethod.AccessType;
                this.tbMethodPrefix.Text = metaMethod.Prefix;
            }
            else
            {
                this.pMethods.Enabled = false;
                this.tbMethodName.Text = string.Empty;
                this.cbMethodType.SelectedIndex = -1;
                this.cbMethodAccessType.SelectedIndex = -1;
                this.tbMethodPrefix.Text = string.Empty;
            }
        }

        #endregion

        #region Create / Delete
        private void bClassNew_Click(object sender, EventArgs e)
        {
            this.model.CreateNewClass(this.lbClasses.SelectedIndex + 1);
            this.RefreshData();
            if (this.lbClasses.SelectedIndex < this.lbClasses.Items.Count - 1)
            {
                this.lbClasses.SelectedIndex++;
            }
            this.lbClasses.Focus();
        }

        private void bClassDelete_Click(object sender, EventArgs e)
        {
            //2DO - add check for usage of class
            this.model.DeleteClassAt(this.lbClasses.SelectedIndex);
            this.RefreshData();
            this.lbClasses.Focus();
        }

        private void bVariableNew_Click(object sender, EventArgs e)
        {
            this.model.Classes[this.lbClasses.SelectedIndex].CreateNewVariable(this.lbVariables.SelectedIndex + 1);
            this.RefreshData();
            if (this.lbVariables.SelectedIndex < this.lbVariables.Items.Count - 1)
            {
                this.lbVariables.SelectedIndex++;
            }
            this.lbVariables.Focus();
        }

        private void bVariableDelete_Click(object sender, EventArgs e)
        {
            this.model.Classes[this.lbClasses.SelectedIndex].DeleteVariableAt(this.lbVariables.SelectedIndex);
            this.RefreshData();
            this.lbVariables.Focus();
        }

        private void bMethodNew_Click(object sender, EventArgs e)
        {
            this.model.Classes[this.lbClasses.SelectedIndex].CreateNewMethod(this.lbMethods.SelectedIndex + 1);
            this.RefreshData();
            if (this.lbMethods.SelectedIndex < this.lbMethods.Items.Count - 1)
            {
                this.lbMethods.SelectedIndex++;
            }
            this.lbMethods.Focus();
        }

        private void bMethodDelete_Click(object sender, EventArgs e)
        {
            this.model.Classes[this.lbClasses.SelectedIndex].DeleteMethodAt(this.lbMethods.SelectedIndex);
            this.RefreshData();
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
                this.windowLog.SetText(NOTIFICATION_VALIDATION_SUCCESS);
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
                this.validationLog += string.Format(ERROR_CLASS_NAME_SPACES, metaClass.Name);
            }
            else
            {
                for (int j = index + 1; j < this.model.Classes.Count; j++)
                {
                    if (metaClass.Name == this.model.Classes[j].Name)
                    {
                        this.validationLog += string.Format(ERROR_CLASS_NAME_DUPLICATE, metaClass.Name);
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
                this.validationLog += string.Format(ERROR_VARIABLE_NAME_SPACES, metaClass.Name, metaVariable.Name);
            }
            else
            {
                for (int i = index + 1; i < metaClass.Variables.Count; i++)
                {
                    if (metaVariable.Name == metaClass.Variables[i].Name)
                    {
                        this.validationLog += string.Format(ERROR_VARIABLE_NAME_DUPLICATE, metaClass.Name, metaVariable.Name);
                    }
                }
            }
        }

        private void validateMethod(MetaClass metaClass, MetaMethod metaMethod, int index)
        {
            if (metaMethod.Name.Contains(" "))
            {
                this.validationLog += string.Format(ERROR_METHOD_NAME_SPACES, metaClass.Name, metaMethod.Name);
            }
            else
            {
                for (int i = index + 1; i < metaClass.Methods.Count; i++)
                {
                    if (metaMethod.Name == metaClass.Methods[i].Name)
                    {
                        this.validationLog += string.Format(ERROR_METHOD_NAME_DUPLICATE, metaClass.Name, metaMethod.Name);
                    }
                }
            }
        }

        private void bMethodParameters_Click(object sender, EventArgs e)
        {
            MessageBox.Show("IMPLEMENT ME");
            // TODO
            /*
            ManagerParameters form = new ManagerParameters((MetaMethod)this.lbMethods.SelectedItem);
            form.ShowDialog();
            */
        }

        private void bMethodImplementation_Click(object sender, EventArgs e)
        {
            Implementation form = new Implementation((MetaMethod)this.lbMethods.SelectedItem);
            DialogResult result = form.ShowDialog();
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
                this.model.Classes[this.lbClasses.SelectedIndex] = InternalClipboard.Class;
                this.model.Classes[this.lbClasses.SelectedIndex].Update(this.model);
                this.RefreshData();
            }
            else if (this.lbVariables.Focused && InternalClipboard.ContainsVariable)
            {
                MetaClass metaClass = this.model.Classes[this.lbClasses.SelectedIndex];
                metaClass.Variables[this.lbVariables.SelectedIndex] = InternalClipboard.Variable;
                metaClass.Variables[this.lbVariables.SelectedIndex].Update(this.model);
                this.RefreshData();
            }
            else if (this.lbMethods.Focused && InternalClipboard.ContainsMethod)
            {
                MetaClass metaClass = this.model.Classes[this.lbClasses.SelectedIndex];
                metaClass.Methods[this.lbMethods.SelectedIndex] = InternalClipboard.Method;
                metaClass.Methods[this.lbMethods.SelectedIndex].Update(this.model);
                this.RefreshData();
            }
        }

        private void sortMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbClasses.Focused)
            {
                this.model.SortClasses();
                this.RefreshData();
            }
            else if (this.lbVariables.Focused)
            {
                this.model.Classes[this.lbClasses.SelectedIndex].SortVariables();
                this.RefreshData();
            }
            else if (this.lbMethods.Focused)
            {
                this.model.Classes[this.lbClasses.SelectedIndex].SortMethods();
                this.RefreshData();
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
                if (this.model.Classes.TryMoveUp(this.lbClasses.SelectedIndex))
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
                    this.RefreshData();
                }
            }
            else if (this.lbMethods.Focused)
            {
                if (this.model.Classes[this.lbClasses.SelectedIndex].TryMethodMoveUp(this.lbMethods.SelectedIndex))
                {
                    this.refreshing = true;
                    this.lbMethods.SelectedIndex--;
                    this.refreshing = false;
                    this.RefreshData();
                }
            }
        }

        private void moveDownMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbClasses.Focused)
            {
                if (this.model.Classes.TryMoveDown(this.lbClasses.SelectedIndex))
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
                    this.RefreshData();
                }
            }
            else if (this.lbMethods.Focused)
            {
                if (this.model.Classes[this.lbClasses.SelectedIndex].TryMethodMoveDown(this.lbMethods.SelectedIndex))
                {
                    this.refreshing = true;
                    this.lbMethods.SelectedIndex++;
                    this.refreshing = false;
                    this.RefreshData();
                }
            }
        }
        #endregion

        #region Class Sync
        private void lbClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshData();
        }

        private void tbClassName_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaClass metaClass = (MetaClass)this.lbClasses.SelectedItem;
            metaClass.Name = this.tbClassName.Text;
            this.RefreshData();
        }

        private void tbClassModule_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaClass metaClass = (MetaClass)this.lbClasses.SelectedItem;
            metaClass.Module = this.tbClassModule.Text;
            this.RefreshData();
        }

        private void cbInheritance_CheckedChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaClass metaClass = (MetaClass)this.lbClasses.SelectedItem;
            metaClass.SuperClass = (this.cbInheritance.Checked ? (MetaClass)this.cbSuperClass.Items[0] : null);
            this.RefreshData();
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
            this.RefreshVariable();
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
            this.RefreshData();
        }

        private void cbVariableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaVariable variable = (MetaVariable)this.lbVariables.SelectedItem;
            variable.Type = (MetaType)this.cbVariableType.SelectedItem;
            this.RefreshData();
        }

        private void cbVariableAccessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaVariable variable = (MetaVariable)this.lbVariables.SelectedItem;
            variable.AccessType = (EAccessType)this.cbVariableAccessType.SelectedIndex;
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

        private void cbxVariableNullable_CheckedChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaVariable variable = (MetaVariable)this.lbVariables.SelectedItem;
            variable.Nullable = this.cbxVariableNullable.Checked;
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
            this.RefreshData();
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
            this.RefreshMethod();
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
            this.RefreshData();
        }

        private void cbMethodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaMethod metaMethod = (MetaMethod)this.lbMethods.SelectedItem;
            metaMethod.Type = (MetaType)this.cbMethodType.SelectedItem;
            this.RefreshData();
        }

        private void cbMethodAccessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaMethod metaMethod = (MetaMethod)this.lbMethods.SelectedItem;
            metaMethod.AccessType = (EAccessType)this.cbMethodAccessType.SelectedIndex;
        }

        private void tbMethodPrefix_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaMethod metaMethod = (MetaMethod)this.lbMethods.SelectedItem;
            metaMethod.Prefix = this.tbMethodPrefix.Text;
            this.RefreshData();
        }
        #endregion

    }
}
