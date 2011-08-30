using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using ClassTools.Common;
using ClassTools.Data;
using ClassTools.Data.Database;
using ClassTools.Data.Hierarchy;

namespace ClassTools.DataMaker.Forms.Controls
{
    public partial class InstanceVariables : UserControl, IRefreshable
    {
        #region Constants
        private const int OFFSET = 5;
        private const string MANAGE_TEXT = "Manage";
        #endregion

        #region Fields
        private Repository repository;
        private MetaType type;
        private MetaValue value;
        private List<Control> valueControls;
        private IRefreshable owner;
        bool refreshing;
        #endregion

        #region Construct
        public InstanceVariables()
        {
            InitializeComponent();
            this.owner = null;
            this.repository = null;
            this.type = null;
            this.value = null;
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
                this.value = null;
                this.Enabled = false;
            }
        }

        public void SetData(IRefreshable owner, Repository repository, MetaType metaType)
        {
            this.owner = owner;
            this.repository = repository;
            this.type = metaType;
            this.setupLayout();
        }

        private void setupLayout()
        {
            MetaVariable metaVariable;
            Label label;
            int maxWidth = 0;
            Control newControl;
            // TODO - if type is not class if ()
            switch (type.CategoryType)
            {
                case ECategoryType.Integral:
                    label = this.createLabel("Value", 0);
                    maxWidth = label.Width;
                    newControl = this.createIntegralControl(type.Name, 0);
                    this.Controls.Add(newControl);
                    this.valueControls.Add(newControl);
                    break;
                case ECategoryType.Class:
                    MetaList<MetaVariable> metaVariables = ((MetaClass)this.type).AllVariables;
                    for (int i = 0; i < metaVariables.Count; i++)
                    {
                        metaVariable = metaVariables[i];
                        label = this.createLabel(metaVariable.ToString(), i);
                        this.Controls.Add(label);
                        if (maxWidth < label.Width)
                        {
                            maxWidth = label.Width;
                        }
                        newControl = this.createControl(metaVariable.Type, metaVariable.ToString(), i);
                        this.Controls.Add(newControl);
                        this.valueControls.Add(newControl);
                    }
                    break;
            }
            foreach (Control control in this.valueControls)
            {
                control.Left += maxWidth + 10;
            }
        }

        private Label createLabel(string name, int index)
        {
            Label label = new Label();
            label.Location = new Point(5, OFFSET + 3 + index * 26);
            label.Text = name;
            label.Name = "name " + name;
            label.TabIndex = 10 + index * 2;
            label.AutoSize = true;
            return label;
        }

        private Control createControl(MetaType metaType, string name, int index)
        {
            Control control = null;
            switch (metaType.CategoryType)
            {
                case ECategoryType.Integral:
                    control = this.createIntegralControl(metaType.Name, index);
                    break;
                case ECategoryType.Class:
                    control = this.createButton(index, new EventHandler(this.ivButtonManage_Click));
                    break;
                case ECategoryType.List:
                    control = this.createButton(index, new EventHandler(this.ivButtonManage_Click));
                    break;
                case ECategoryType.Dictionary:
                    control = this.createButton(index);
                    break;
            }
            control.Name = name;
            control.TabIndex = 101 + index * 2;
            return control;
        }

        private Control createIntegralControl(string typeName, int index)
        {
            Control control = null;
            switch (typeName)
            {
                case Constants.TYPE_VOID:
                    control = this.createTextBox(index);
                    control.Enabled = false;
                    break;
                case Constants.TYPE_INT:
                    control = this.createNumericUpDown(index, int.MinValue, int.MaxValue);
                    break;
                case Constants.TYPE_UINT:
                    control = this.createNumericUpDown(index, uint.MinValue, uint.MaxValue);
                    break;
                case Constants.TYPE_SHORT:
                    control = this.createNumericUpDown(index, short.MinValue, short.MaxValue);
                    break;
                case Constants.TYPE_USHORT:
                    control = this.createNumericUpDown(index, ushort.MinValue, ushort.MaxValue);
                    break;
                case Constants.TYPE_LONG:
                    control = this.createNumericUpDown(index, long.MinValue, long.MaxValue);
                    break;
                case Constants.TYPE_ULONG:
                    control = this.createNumericUpDown(index, ulong.MinValue, ulong.MaxValue);
                    break;
                case Constants.TYPE_CHAR:
                    control = this.createTextBox(index, "", 1);
                    break;
                case Constants.TYPE_UCHAR:
                    control = this.createNumericUpDown(index, 0, 255);
                    break;
                case Constants.TYPE_FLOAT:
                    control = this.createTextBox(index, "0.0");
                    break;
                case Constants.TYPE_DOUBLE:
                    control = this.createTextBox(index, "0.0");
                    break;
                case Constants.TYPE_BOOL:
                    control = this.createCheckBox(index);
                    break;
                default:
                    control = this.createTextBox(index);
                    break;
            }
            return control;
        }

        private NumericUpDown createNumericUpDown(int index, decimal min, decimal max)
        {
            NumericUpDown numericUpDown = new NumericUpDown();
            numericUpDown.Size = new Size(80, 20);
            numericUpDown.Location = new Point(5, OFFSET + index * 26);
            numericUpDown.Minimum = min;
            numericUpDown.Maximum = max;
            numericUpDown.TextAlign = HorizontalAlignment.Right;
            numericUpDown.ValueChanged += new EventHandler(this.ivNumericUpDown_ValueChanged);
            return numericUpDown;
        }

        private TextBox createTextBox(int index, string text = "", int maxLength = 0)
        {
            TextBox textBox = new TextBox();
            textBox.Size = new Size(160, 20);
            textBox.Location = new Point(5, OFFSET + index * 26);
            textBox.Text = text;
            if (maxLength > 0)
            {
                textBox.MaxLength = maxLength;
            }
            textBox.TextChanged += new EventHandler(this.ivTextBox_TextChanged);
            return textBox;
        }

        private CheckBox createCheckBox(int index)
        {
            CheckBox checkBox = new CheckBox();
            checkBox.Location = new Point(5, OFFSET + 2 + index * 26);
            checkBox.Checked = false;
            checkBox.AutoSize = true;
            checkBox.CheckedChanged += new EventHandler(this.ivCheckBox_CheckedChanged);
            return checkBox;
        }

        private Button createButton(int index, EventHandler eventHandler = null)
        {
            Button button = new Button();
            button.Size = new Size(80, 23);
            button.Location = new Point(5, OFFSET - 2 + index * 26);
            button.Text = MANAGE_TEXT;
            if (eventHandler != null)
            {
                button.Click += eventHandler;
            }
            return button;
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
            if (this.value != null)
            {
                switch (this.value.ValueType)
                {
                    case EValueType.Integral:
                        this.refreshEntry(this.value, 0);
                        break;
                    case EValueType.Object:
                        MetaList<MetaInstanceVariable> metaInstanceVariables = this.value.Instance.InstanceVariables;
                        for (int i = 0; i < metaInstanceVariables.Count; i++)
                        {
                            this.refreshEntry(metaInstanceVariables[i].Value, i);
                        }
                        break;
                }
            }
            else
            {
                switch (this.type.CategoryType)
                {
                    case ECategoryType.Integral:
                        this.refreshEntry(this.type, 0);
                        break;
                    case ECategoryType.Class:
                        MetaList<MetaVariable> metaVariables = ((MetaClass)this.type).AllVariables;
                        for (int i = 0; i < metaVariables.Count; i++)
                        {
                            this.refreshEntry(metaVariables[i].Type, i);
                        }
                        break;
                }
            }
            this.refreshing = false;
        }

        private void refreshEntry(MetaValue metaValue, int index)
        {
            if (metaValue.ValueType == EValueType.Integral)
            {
                if (Constants.TYPES_INT.Contains(metaValue.Type.Name))
                {
                    NumericUpDown numericUpDown = (NumericUpDown)this.valueControls[index];
                    numericUpDown.Value = metaValue.Decimal;
                }
                else if (Constants.TYPES_BOOL.Contains(metaValue.Type.Name))
                {
                    CheckBox checkBox = (CheckBox)this.valueControls[index];
                    checkBox.Checked = metaValue.Bool;
                }
                else if (!Constants.TYPES_VOID.Contains(metaValue.Type.Name))// && !Constants.TYPES_FLOAT.Contains(metaValue.TypeName))
                {
                    TextBox textBox = (TextBox)this.valueControls[index];
                    textBox.Text = metaValue.String;
                }
            }
        }

        private void refreshEntry(MetaType metaType, int index)
        {
            if (metaType.CategoryType == ECategoryType.Integral)
            {
                if (Constants.TYPES_INT.Contains(metaType.Name))
                {
                    NumericUpDown numericUpDown = (NumericUpDown)this.valueControls[index];
                    numericUpDown.Value = decimal.Zero;
                }
                else if (Constants.TYPES_BOOL.Contains(metaType.Name))
                {
                    CheckBox checkBox = (CheckBox)this.valueControls[index];
                    checkBox.Checked = false;
                }
                else if (!Constants.TYPES_VOID.Contains(metaType.Name))
                {
                    TextBox textBox = (TextBox)this.valueControls[index];
                    textBox.Text = string.Empty;
                }
            }
        }

        public void SetValue(MetaValue metaValue)
        {
            this.value = metaValue;
            this.Enabled = (this.value != null);
            this.RefreshData();
        }
        #endregion

        #region Events
        private void ivTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            TextBox textBox = (TextBox)sender;
            foreach (MetaInstanceVariable variable in this.value.Instance.InstanceVariables)
            {
                if (textBox.Name == variable.ToString())
                {
                    variable.Value.String = textBox.Text;
                    break;
                }
            }
            this.refreshing = false;
            this.RefreshData();
        }

        private void ivNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            foreach (MetaInstanceVariable variable in this.value.Instance.InstanceVariables)
            {
                if (numericUpDown.Name == variable.ToString())
                {
                    variable.Value.Decimal = numericUpDown.Value;
                    break;
                }
            }
            this.refreshing = false;
            this.RefreshData();
        }

        private void ivCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            this.refreshing = true;
            CheckBox checkBox = (CheckBox)sender;
            foreach (MetaInstanceVariable variable in this.value.Instance.InstanceVariables)
            {
                if (checkBox.Name == variable.ToString())
                {
                    variable.Value.Bool = checkBox.Checked;
                    break;
                }
            }
            this.refreshing = false;
            this.RefreshData();
        }

        private void ivButtonManage_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MetaInstanceVariable metaInstanceVariable;
            MetaVariable metaVariable;
            MetaClass metaClass = (MetaClass)this.type;
            for (int i = 0; i < this.value.Instance.InstanceVariables.Count; i++)
            {
                metaInstanceVariable = this.value.Instance.InstanceVariables[i];
                if (button.Name == metaInstanceVariable.ToString())
                {
                    value = metaInstanceVariable.Value;
                    metaVariable = metaClass.AllVariables[i];
                    switch (metaVariable.Type.CategoryType)
                    {
                        case ECategoryType.Integral:
                            break;
                        case ECategoryType.Class:
                            ManagerInstance formInstance = new ManagerInstance(this.repository, (MetaClass)metaVariable.Type, metaInstanceVariable.Value, metaVariable.Nullable);
                            formInstance.Text = metaInstanceVariable.ToString();
                            formInstance.ShowDialog();
                            metaInstanceVariable.Value = formInstance.Value;
                            this.RefreshData();
                            break;
                        case ECategoryType.List:
                            ManagerList formList = new ManagerList(this.repository, metaVariable.Type.SubType1, metaInstanceVariable.Value.List);
                            formList.Text = metaInstanceVariable.ToString();
                            formList.ShowDialog();
                            metaInstanceVariable.Value.List = formList.ListValues;
                            break;
                        case ECategoryType.Dictionary:
                            ManagerDictionary formDictionary = new ManagerDictionary(this.repository, (MetaClass)metaVariable.Type.SubType1, metaInstanceVariable.Value.Dictionary);
                            formDictionary.Text = metaInstanceVariable.ToString();
                            formDictionary.ShowDialog();
                            metaInstanceVariable.Value.Dictionary = formDictionary.DictionaryValues;
                            break;
                    }
                    this.RefreshData();
                    break;
                }
            }
        }
        #endregion

    }
}
