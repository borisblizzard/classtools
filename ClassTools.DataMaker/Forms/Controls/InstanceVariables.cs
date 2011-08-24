using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Model;

namespace ClassTools.DataMaker.Forms.Controls
{
    public partial class InstanceVariables : UserControl, IRefreshable
    {
        #region Constants
        private const int OFFSET = 5;
        private const string MANAGE_TEXT = "Manage";

        private List<string> INT_TYPES = new List<string>(new string[] { "int", "unsigned int", "short", "unsigned short", "long", "unsigned long", "unsigned char" });
        private List<string> CHAR_TYPES = new List<string>(new string[] { "char" });
        private List<string> FIXED_POINT_TYPES = new List<string>(new string[] { "float", "double" });
        #endregion

        #region Fields
        private ModelDatabase database;
        private MetaClass metaClass;
        private MetaInstance metaInstance;
        private List<Control> valueControls;
        private IRefreshable owner;
        bool refreshing;
        #endregion

        #region Properties
        public MetaInstance MetaInstance
        {
            set
            {
                this.metaInstance = value;
                this.Enabled = (this.metaInstance != null);
                this.RefreshData();
            }
        }
        #endregion

        #region Construct
        public InstanceVariables()
        {
            InitializeComponent();
            this.owner = null;
            this.database = null;
            this.metaClass = null;
            this.metaInstance = null;
            this.valueControls = new List<Control>();
            this.refreshing = false;
            this.Enabled = false;
        }

        public void ClearData()
        {
            if (this.owner != null)
            {
                this.Controls.Clear();
                this.valueControls.Clear();
                this.metaInstance = null;
                this.Enabled = false;
            }
        }

        public void SetData(IRefreshable owner, ModelDatabase database, MetaClass metaClass)
        {
            this.owner = owner;
            this.database = database;
            this.metaClass = metaClass;
            this.setupLayout();
        }

        private void setupLayout()
        {
            MetaVariable variable;
            TextBox textBox;
            NumericUpDown numericUpDown;
            CheckBox checkBox;
            Button button;
            Label label;
            int maxWidth = 0;
            List<MetaVariable> variables = this.metaClass.AllVariables;
            for (int i = 0; i < variables.Count; i++)
            {
                variable = variables[i];
                label = new Label();
                this.Controls.Add(label);
                label.Location = new Point(5, OFFSET + 3 + i * 26);
                label.Text = variable.ToString();
                label.Name = "name " + variable.ToString();
                label.TabIndex = 10 + i * 2;
                label.AutoSize = true;
                if (maxWidth < label.Width)
                {
                    maxWidth = label.Width;
                }
                if (!variable.Type.IsClass)
                {
                    switch (variable.Type.TypeCategory)
                    {
                        case ETypeCategory.Normal:
                            switch (variable.Type.Name)
                            {
                                case "int":
                                    numericUpDown = new NumericUpDown();
                                    this.Controls.Add(numericUpDown);
                                    this.valueControls.Add(numericUpDown);
                                    numericUpDown.Size = new Size(80, 20);
                                    numericUpDown.Location = new Point(5, OFFSET + i * 26);
                                    numericUpDown.Maximum = int.MaxValue;
                                    numericUpDown.Minimum = int.MinValue;
                                    numericUpDown.TextAlign = HorizontalAlignment.Right;
                                    numericUpDown.Name = variable.ToString();
                                    numericUpDown.TabIndex = 11 + i * 2;
                                    numericUpDown.ValueChanged += new EventHandler(this.numericUpDown_ValueChanged);
                                    break;
                                case "unsigned int":
                                    numericUpDown = new NumericUpDown();
                                    this.Controls.Add(numericUpDown);
                                    this.valueControls.Add(numericUpDown);
                                    numericUpDown.Size = new Size(80, 20);
                                    numericUpDown.Location = new Point(5, OFFSET + i * 26);
                                    numericUpDown.Maximum = uint.MaxValue;
                                    numericUpDown.Minimum = uint.MinValue;
                                    numericUpDown.TextAlign = HorizontalAlignment.Right;
                                    numericUpDown.Name = variable.ToString();
                                    numericUpDown.TabIndex = 11 + i * 2;
                                    numericUpDown.ValueChanged += new EventHandler(this.numericUpDown_ValueChanged);
                                    break;
                                case "short":
                                    numericUpDown = new NumericUpDown();
                                    this.Controls.Add(numericUpDown);
                                    this.valueControls.Add(numericUpDown);
                                    numericUpDown.Size = new Size(80, 20);
                                    numericUpDown.Location = new Point(5, OFFSET + i * 26);
                                    numericUpDown.Maximum = short.MaxValue;
                                    numericUpDown.Minimum = short.MinValue;
                                    numericUpDown.TextAlign = HorizontalAlignment.Right;
                                    numericUpDown.Name = variable.ToString();
                                    numericUpDown.TabIndex = 11 + i * 2;
                                    numericUpDown.ValueChanged += new EventHandler(this.numericUpDown_ValueChanged);
                                    break;
                                case "unsigned short":
                                    numericUpDown = new NumericUpDown();
                                    this.Controls.Add(numericUpDown);
                                    this.valueControls.Add(numericUpDown);
                                    numericUpDown.Size = new Size(80, 20);
                                    numericUpDown.Location = new Point(5, OFFSET + i * 26);
                                    numericUpDown.Maximum = ushort.MaxValue;
                                    numericUpDown.Minimum = ushort.MinValue;
                                    numericUpDown.TextAlign = HorizontalAlignment.Right;
                                    numericUpDown.Name = variable.ToString();
                                    numericUpDown.TabIndex = 11 + i * 2;
                                    numericUpDown.ValueChanged += new EventHandler(this.numericUpDown_ValueChanged);
                                    break;
                                case "long":
                                    numericUpDown = new NumericUpDown();
                                    this.Controls.Add(numericUpDown);
                                    this.valueControls.Add(numericUpDown);
                                    numericUpDown.Size = new Size(80, 20);
                                    numericUpDown.Location = new Point(5, OFFSET + i * 26);
                                    numericUpDown.Maximum = long.MaxValue;
                                    numericUpDown.Minimum = long.MinValue;
                                    numericUpDown.TextAlign = HorizontalAlignment.Right;
                                    numericUpDown.Name = variable.ToString();
                                    numericUpDown.TabIndex = 11 + i * 2;
                                    numericUpDown.ValueChanged += new EventHandler(this.numericUpDown_ValueChanged);
                                    break;
                                case "unsigned long":
                                    numericUpDown = new NumericUpDown();
                                    this.Controls.Add(numericUpDown);
                                    this.valueControls.Add(numericUpDown);
                                    numericUpDown.Size = new Size(80, 20);
                                    numericUpDown.Location = new Point(5, OFFSET + i * 26);
                                    numericUpDown.Maximum = ulong.MaxValue;
                                    numericUpDown.Minimum = ulong.MinValue;
                                    numericUpDown.TextAlign = HorizontalAlignment.Right;
                                    numericUpDown.Name = variable.ToString();
                                    numericUpDown.TabIndex = 11 + i * 2;
                                    numericUpDown.ValueChanged += new EventHandler(this.numericUpDown_ValueChanged);
                                    break;
                                case "char":
                                    textBox = new TextBox();
                                    this.Controls.Add(textBox);
                                    this.valueControls.Add(textBox);
                                    textBox.Size = new Size(160, 20);
                                    textBox.Location = new Point(5, OFFSET + i * 26);
                                    textBox.MaxLength = 1;
                                    textBox.Name = variable.ToString();
                                    textBox.TabIndex = 11 + i * 2;
                                    textBox.TextChanged += new EventHandler(this.textBox_TextChanged);
                                    break;
                                case "unsigned char":
                                    numericUpDown = new NumericUpDown();
                                    this.Controls.Add(numericUpDown);
                                    this.valueControls.Add(numericUpDown);
                                    numericUpDown.Size = new Size(80, 20);
                                    numericUpDown.Location = new Point(5, OFFSET + i * 26);
                                    numericUpDown.Maximum = 255;
                                    numericUpDown.Minimum = 0;
                                    numericUpDown.TextAlign = HorizontalAlignment.Right;
                                    numericUpDown.Name = variable.ToString();
                                    numericUpDown.TabIndex = 11 + i * 2;
                                    numericUpDown.ValueChanged += new EventHandler(this.numericUpDown_ValueChanged);
                                    break;
                                case "float":
                                    textBox = new TextBox();
                                    this.Controls.Add(textBox);
                                    this.valueControls.Add(textBox);
                                    textBox.Size = new Size(160, 20);
                                    textBox.Location = new Point(5, OFFSET + i * 26);
                                    textBox.Text = "0";
                                    textBox.Name = variable.ToString();
                                    textBox.TabIndex = 11 + i * 2;
                                    textBox.TextChanged += new EventHandler(this.textBox_TextChanged);
                                    break;
                                case "double":
                                    textBox = new TextBox();
                                    this.Controls.Add(textBox);
                                    this.valueControls.Add(textBox);
                                    textBox.Size = new Size(160, 20);
                                    textBox.Location = new Point(5, OFFSET + i * 26);
                                    textBox.Text = "0";
                                    textBox.Name = variable.ToString();
                                    textBox.TabIndex = 11 + i * 2;
                                    textBox.TextChanged += new EventHandler(this.textBox_TextChanged);
                                    break;
                                case "bool":
                                    checkBox = new CheckBox();
                                    this.Controls.Add(checkBox);
                                    this.valueControls.Add(checkBox);
                                    checkBox.Location = new Point(5, OFFSET + 2 + i * 26);
                                    checkBox.Checked = false;
                                    checkBox.AutoSize = true;
                                    checkBox.TabIndex = 11 + i * 2;
                                    checkBox.CheckedChanged += new EventHandler(this.checkBox_CheckedChanged);
                                    break;
                                default:
                                    textBox = new TextBox();
                                    this.Controls.Add(textBox);
                                    this.valueControls.Add(textBox);
                                    textBox.Size = new Size(160, 20);
                                    textBox.Location = new Point(5, OFFSET + i * 26);
                                    textBox.Name = variable.ToString();
                                    textBox.TabIndex = 11 + i * 2;
                                    textBox.TextChanged += new EventHandler(this.textBox_TextChanged);
                                    break;
                            }
                            break;
                        case ETypeCategory.Collection:
                            button = new Button();
                            this.Controls.Add(button);
                            this.valueControls.Add(button);
                            button.Size = new Size(80, 23);
                            button.Location = new Point(5, OFFSET - 2 + i * 26);
                            button.Text = MANAGE_TEXT;
                            button.Name = variable.ToString();
                            button.TabIndex = 11 + i * 2;
                            break;
                        case ETypeCategory.Dictionary:
                            button = new Button();
                            this.Controls.Add(button);
                            this.valueControls.Add(button);
                            button.Size = new Size(80, 23);
                            button.Location = new Point(5, OFFSET - 2 + i * 26);
                            button.Text = MANAGE_TEXT;
                            button.Name = variable.ToString();
                            button.TabIndex = 11 + i * 2;
                            break;
                    }
                }
                else
                {
                    button = new Button();
                    this.Controls.Add(button);
                    this.valueControls.Add(button);
                    button.Size = new Size(80, 23);
                    button.Location = new Point(5, OFFSET - 2 + i * 26);
                    button.Text = MANAGE_TEXT;
                    button.Name = variable.ToString();
                    button.TabIndex = 11 + i * 2;
                    button.Click += new EventHandler(this.buttonObject_Click);
                }
            }
            foreach (Control control in this.valueControls)
            {
                control.Left += maxWidth + 10;
            }
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
            this.owner.RefreshData();
            MetaVariable variable;
            MetaInstanceVariable instanceVariable;
            TextBox textBox;
            NumericUpDown numericUpDown;
            CheckBox checkBox;
            List<MetaVariable> variables = this.metaClass.AllVariables;
            if (this.metaInstance != null)
            {
                for (int i = 0; i < variables.Count; i++)
                {
                    variable = variables[i];
                    instanceVariable = this.metaInstance.InstanceVariables[i];
                    if (!variable.Type.IsClass && variable.Type.TypeCategory == ETypeCategory.Normal)
                    {
                        if (INT_TYPES.Contains(variable.Type.Name) || CHAR_TYPES.Contains(variable.Type.Name))
                        {
                            numericUpDown = (NumericUpDown)this.valueControls[i];
                            numericUpDown.Value = instanceVariable.ValueDecimal;
                        }
                        else if (FIXED_POINT_TYPES.Contains(variable.Type.Name))
                        {
                            textBox = (TextBox)this.valueControls[i];
                            textBox.Text = instanceVariable.ValueString;
                        }
                        else if (variable.Type.Name == "bool")
                        {
                            checkBox = (CheckBox)this.valueControls[i];
                            checkBox.Checked = instanceVariable.ValueBool;
                        }
                        else
                        {
                            textBox = (TextBox)this.valueControls[i];
                            textBox.Text = instanceVariable.ValueString;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < variables.Count; i++)
                {
                    variable = variables[i];
                    if (!variable.Type.IsClass && variable.Type.TypeCategory == ETypeCategory.Normal)
                    {
                        if (INT_TYPES.Contains(variable.Type.Name) || CHAR_TYPES.Contains(variable.Type.Name))
                        {
                            numericUpDown = (NumericUpDown)this.valueControls[i];
                            numericUpDown.Value = decimal.Zero;
                        }
                        else if (variable.Type.Name == "bool")
                        {
                            checkBox = (CheckBox)this.valueControls[i];
                            checkBox.Checked = false;
                        }
                        else
                        {
                            textBox = (TextBox)this.valueControls[i];
                            textBox.Text = string.Empty;
                        }
                    }
                }
            }
            this.refreshing = false;
        }
        #endregion

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            TextBox textBox = (TextBox)sender;
            foreach (MetaInstanceVariable variable in this.metaInstance.InstanceVariables)
            {
                if (textBox.Name == variable.ToString())
                {
                    variable.ValueString = textBox.Text;
                    break;
                }
            }
            this.RefreshData();
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            foreach (MetaInstanceVariable variable in this.metaInstance.InstanceVariables)
            {
                if (numericUpDown.Name == variable.ToString())
                {
                    variable.ValueDecimal = numericUpDown.Value;
                    break;
                }
            }
            this.RefreshData();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            CheckBox checkBox = (CheckBox)sender;
            foreach (MetaInstanceVariable variable in this.metaInstance.InstanceVariables)
            {
                if (checkBox.Name == variable.ToString())
                {
                    variable.ValueBool = checkBox.Checked;
                    break;
                }
            }
            this.RefreshData();
        }

        private void buttonObject_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MetaInstanceVariable variable;
            for (int i = 0; i < this.metaInstance.InstanceVariables.Count; i++)
            {
                variable = this.metaInstance.InstanceVariables[i];
                if (button.Name == variable.ToString())
                {
                    Instance form = new Instance(this.database, (MetaClass)this.metaClass.AllVariables[i].Type, (MetaInstance)variable.ValueObject);
                    form.Text = variable.ToString();
                    form.ShowDialog();
                    variable.ValueObject = form.MetaInstance;
                    this.RefreshData();
                    break;
                }
            }
        }

        private void buttonCollection_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MetaInstanceVariable variable;
            for (int i = 0; i < this.metaInstance.InstanceVariables.Count; i++)
            {
                variable = this.metaInstance.InstanceVariables[i];
                if (button.Name == variable.ToString())
                {
                    if (this.metaClass.AllVariables[i].Type.IsClass)
                    {
                        Forms.InstanceCollection form = new Forms.InstanceCollection(this.database, (MetaClass)this.metaClass.AllVariables[i].Type, (List<MetaInstance>)variable.ValueObject);
                        form.Text = variable.ToString();
                        form.ShowDialog();
                    }
                    else
                    {
                        //FormInstanceVariableCollection form = new FormInstanceVariableCollection(this.database, (MetaClass)this.metaClass.AllVariables[i].Type, (List<MetaVariableInstance>)variable.ValueObject);
                        //form.ShowDialog();
                        // TODO
                    }
                    this.RefreshData();
                    break;
                }
            }
        }

    }
}
