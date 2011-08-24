using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using ClassTools;
using ClassTools.Common;
using ClassTools.Common.Forms;
using ClassTools.Model;

namespace ClassTools.ClassMaker.Forms
{
    public partial class Main : Form
    {
        #region Constants
        const string savePromptNew = "There are unsaved changes. Do you want to save be save before creating a new model?";
        const string savePromptOpen = "There are unsaved changes. Do you want to save be save before opening another file?";
        const string savePromptExit = "There are unsaved changes. Do you want to save be save before existing?";

        const string validationSuccess = "Validation successful.\r\n";
        const string errorClassNameSpaces = "Class '{0}' has spaces in its name!\r\n";
        const string errorVariableNameSpaces = "In class '{0}' variable '{1}' has spaces in its name!\r\n";
        const string errorMethodNameSpaces = "In class '{0}' method '{1}' has spaces in its name!\r\n";
        const string errorClassNameDuplicate = "More than one class has name '{0}'!\r\n";
        const string errorVariableNameDuplicate = "In class '{0}' more than one variable has name '{1}'!\r\n";
        const string errorMethodNameDuplicate = "In class '{0}' more than one method has name '{1}'!\r\n";
        #endregion

        #region Fields
        private ClassModel model;
        private ClassModel lastModel;
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
            this.model = new ClassModel();
            this.lastModel = (ClassModel)Serializer.Clone(this.model);
            this.lastFilename = string.Empty;
            this.validationLog = string.Empty;
            this.cbVariableAccess.DataSource = new List<string>(ClassModel.AccessorNames);
            this.cbMethodAccess.DataSource = new List<string>(ClassModel.AccessorNames);
            this.windowLog = new Log();
            this.windowLog.Hide();
            if (args.Length > 0)
            {
                Stream stream = File.OpenRead(args[0]);
                this.model = Serializer.Deserialize(stream, this.model);
                this.lastFilename = args[0];
                stream.Close();
                this.lastModel = (ClassModel)Serializer.Clone(this.model);
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
                    this.lastModel = (ClassModel)Serializer.Clone(this.model);
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
                this.model = new ClassModel();
                this.lastModel = (ClassModel)Serializer.Clone(this.model);
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
            this.lastModel = (ClassModel)Serializer.Clone(this.model);
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
            MetaClass classe = (MetaClass)this.lbClasses.SelectedItem;
            bool enabled = (classe != null);
            this.gbClass.Enabled = enabled;
            this.gbVariables.Enabled = enabled;
            this.gbMethods.Enabled = enabled;
            this.bClassDelete.Enabled = enabled;
            if (enabled)
            {
                this.tbClassName.Text = classe.Name;
                this.tbClassModule.Text = classe.Module;
                this.cbxClassSerialize.Checked = classe.CanSerialize;
                this.cbInheritance.Checked = classe.HasSuperClass;
                this.cbSuperClass.Enabled = this.cbInheritance.Checked;
                if (classe.HasSuperClass)
                {
                    this.cbSuperClass.SelectedItem = classe.SuperClass;
                }
                else
                {
                    this.cbSuperClass.SelectedIndex = 0;
                }
                Utility.ApplyNewDataSource(this.lbVariables, new List<MetaVariable>(classe.Variables), classe.Variables.Count);
                Utility.ApplyNewDataSource(this.lbMethods, new List<MetaMethod>(classe.Methods), classe.Methods.Count);
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
                this.cbVariableAccess.SelectedIndex = (int)variable.AccessType;
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
                this.cbVariableAccess.SelectedIndex = (int)EAccessType.Public;
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
            MetaMethod method = (MetaMethod)this.lbMethods.SelectedItem;
            bool enabled = (method != null);
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
                this.tbMethodName.Text = method.Name;
                this.cbMethodType.SelectedItem = method.Type;
                this.cbMethodAccess.SelectedIndex = (int)method.AccessType;
                this.tbMethodPrefix.Text = method.Prefix;
            }
            else
            {
                this.tbMethodName.Text = string.Empty;
                this.cbMethodType.SelectedIndex = (this.cbMethodType.Items.Count > 0 ? 0 : -1);
                this.cbMethodAccess.SelectedIndex = (int)EAccessType.Public;
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

        private void validateClass(MetaClass classe, int index)
        {
            if (classe.Name.Contains(" "))
            {
                this.validationLog += string.Format(errorClassNameSpaces, classe.Name);
            }
            else
            {
                for (int j = index + 1; j < this.model.Classes.Count; j++)
                {
                    if (classe.Name == this.model.Classes[j].Name)
                    {
                        this.validationLog += string.Format(errorClassNameDuplicate, classe.Name);
                    }
                }
            }
            for (int j = 0; j < classe.Variables.Count; j++)
            {
                this.validateVariable(classe, classe.Variables[j], j);
            }
            for (int j = 0; j < classe.Methods.Count; j++)
            {
                this.validateMethod(classe, classe.Methods[j], j);
            }
        }

        private void validateVariable(MetaClass classe, MetaVariable variable, int index)
        {
            if (variable.Name.Contains(" "))
            {
                this.validationLog += string.Format(errorVariableNameSpaces, classe.Name, variable.Name);
            }
            else
            {
                for (int i = index + 1; i < classe.Variables.Count; i++)
                {
                    if (variable.Name == classe.Variables[i].Name)
                    {
                        this.validationLog += string.Format(errorVariableNameDuplicate, classe.Name, variable.Name);
                    }
                }
            }
        }

        private void validateMethod(MetaClass classe, MetaMethod method, int index)
        {
            if (method.Name.Contains(" "))
            {
                this.validationLog += string.Format(errorMethodNameSpaces, classe.Name, method.Name);
            }
            else
            {
                for (int i = index + 1; i < classe.Methods.Count; i++)
                {
                    if (method.Name == classe.Methods[i].Name)
                    {
                        this.validationLog += string.Format(errorMethodNameDuplicate, classe.Name, method.Name);
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
            FormAbout f = new FormAbout();
            f.ShowDialog();
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGenerator f = new FormGenerator();
            DialogResult result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                IPlugin generator = f.Generator;
                if (generator != null)
                {
                    result = MessageBox.Show("Run generator in safe-mode?", generator.ToString() + " ready",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    //2DO - implement folder selection dialog
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            string message = generator.Execute(this.model, null, "gen");
                            MessageBox.Show(message, generator.ToString() + " is done",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, generator.ToString() + " encountered an exception",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (result == DialogResult.No)
                    {
                        string message = generator.Execute(this.model, null, "gen");
                        MessageBox.Show(message, generator.ToString() + " is done",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
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
                MetaClass classe = this.model.Classes[this.lbClasses.SelectedIndex];
                classe.ReplaceVariableAt(this.lbVariables.SelectedIndex, InternalClipboard.Variable);
                this.refresh();
            }
            else if (this.lbMethods.Focused && InternalClipboard.ContainsMethod)
            {
                MetaClass classe = this.model.Classes[this.lbClasses.SelectedIndex];
                classe.ReplaceMethodAt(this.lbMethods.SelectedIndex, InternalClipboard.Method);
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
            MetaClass classe = (MetaClass)this.lbClasses.SelectedItem;
            classe.Name = this.tbClassName.Text;
            this.refresh();
        }

        private void tbClassModule_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaClass classe = (MetaClass)this.lbClasses.SelectedItem;
            classe.Module = this.tbClassModule.Text;
            this.refresh();
        }

        private void cbInheritance_CheckedChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaClass classe = (MetaClass)this.lbClasses.SelectedItem;
            classe.SuperClass = (this.cbInheritance.Checked ? (MetaClass)this.cbSuperClass.Items[0] : null);
            this.refresh();
        }

        private void cbxClassSerialize_CheckedChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaClass classe = (MetaClass)this.lbClasses.SelectedItem;
            classe.CanSerialize = this.cbxClassSerialize.Checked;
        }

        private void cbSuperClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaClass classe = (MetaClass)this.lbClasses.SelectedItem;
            classe.SuperClass = (this.cbInheritance.Checked ? (MetaClass)this.cbSuperClass.SelectedItem : null);
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
            variable.AccessType = (EAccessType)this.cbVariableAccess.SelectedIndex;
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
            MetaMethod Method = (MetaMethod)this.lbMethods.SelectedItem;
            Method.Name = this.tbMethodName.Text;
            this.refresh();
        }

        private void cbMethodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaMethod Method = (MetaMethod)this.lbMethods.SelectedItem;
            Method.Type = (MetaType)this.cbMethodType.SelectedItem;
            this.refresh();
        }

        private void cbMethodAccess_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaMethod method = (MetaMethod)this.lbMethods.SelectedItem;
            method.AccessType = (EAccessType)this.cbMethodAccess.SelectedIndex;
        }

        private void tbMethodPrefix_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            MetaMethod method = (MetaMethod)this.lbMethods.SelectedItem;
            method.Prefix = this.tbMethodPrefix.Text;
            this.refresh();
        }
        #endregion

    }
}
