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
        private MetaType metaType;
        private MetaValue metaValue;
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
            this.metaType = null;
            this.metaValue = null;
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
                this.metaValue = null;
                this.Enabled = false;
            }
        }

        public void SetData(IRefreshable owner, Repository repository, MetaType metaType)
        {
            this.owner = owner;
            this.repository = repository;
            this.metaType = metaType;
            this.setupLayout();
        }

        private void setupLayout()
        {
            MetaVariable metaVariable;
            Label label;
            int maxWidth = 0;
            Control newControl;
            // TODO - if type is not class if ()
            if (metaType.CategoryType == ECategoryType.Class)
            {
                MetaList<MetaVariable> metaVariables = ((MetaClass)this.metaType).AllVariables;
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
            }
            else
            {
                // TODO
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
                    switch (metaType.Name)
                    {
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
                    break;
                case ECategoryType.Class:
                    control = this.createButton(index, new EventHandler(this.ivButtonInstance_Click));
                    break;
                case ECategoryType.List:
                    control = this.createButton(index);
                    break;
                case ECategoryType.Dictionary:
                    control = this.createButton(index);
                    break;
            }
            control.Name = name;
            control.TabIndex = 101 + index * 2;
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
            TextBox textBox;
            NumericUpDown numericUpDown;
            CheckBox checkBox;
            if (this.metaValue != null)
            {
                // TODO - what happens when metaValue is not a class
                MetaValue metaValue;
                MetaList<MetaInstanceVariable> metaInstanceVariables = this.metaValue.Instance.InstanceVariables;
                for (int i = 0; i < metaInstanceVariables.Count; i++)
                {
                    metaValue = metaInstanceVariables[i].Value;
                    if (metaValue.ValueType == EValueType.Integral)
                    {
                        if (Constants.TYPES_INT.Contains(metaValue.TypeName))
                        {
                            numericUpDown = (NumericUpDown)this.valueControls[i];
                            numericUpDown.Value = metaValue.Decimal;
                        }
                        else if (Constants.TYPES_BOOL.Contains(metaValue.TypeName))
                        {
                            checkBox = (CheckBox)this.valueControls[i];
                            checkBox.Checked = metaValue.Bool;
                        }
                        else if (!Constants.TYPES_VOID.Contains(metaValue.TypeName) && !Constants.TYPES_FLOAT.Contains(metaValue.TypeName))
                        {
                            textBox = (TextBox)this.valueControls[i];
                            textBox.Text = metaValue.String;
                        }
                    }
                }
            }
            else
            {
                if (this.metaType.CategoryType == ECategoryType.Class)
                {
                    MetaType metaType;
                    MetaList<MetaVariable> metaVariables = ((MetaClass)this.metaType).AllVariables;
                    for (int i = 0; i < metaVariables.Count; i++)
                    {
                        metaType = metaVariables[i].Type;
                        if (metaType.CategoryType == ECategoryType.Integral)
                        {
                            if (Constants.TYPES_INT.Contains(metaType.Name))
                            {
                                numericUpDown = (NumericUpDown)this.valueControls[i];
                                numericUpDown.Value = decimal.Zero;
                            }
                            else if (Constants.TYPES_BOOL.Contains(metaType.Name))
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
            }
            this.refreshing = false;
        }

        public void SetMetaValue(MetaValue metaValue)
        {
            this.metaValue = metaValue;
            this.Enabled = (this.metaValue != null);
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
            TextBox textBox = (TextBox)sender;
            foreach (MetaInstanceVariable variable in this.metaValue.Instance.InstanceVariables)
            {
                if (textBox.Name == variable.ToString())
                {
                    variable.Value.String = textBox.Text;
                    break;
                }
            }
            this.RefreshData();
        }

        private void ivNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            foreach (MetaInstanceVariable variable in this.metaValue.Instance.InstanceVariables)
            {
                if (numericUpDown.Name == variable.ToString())
                {
                    variable.Value.Decimal = numericUpDown.Value;
                    break;
                }
            }
            this.RefreshData();
        }

        private void ivCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.refreshing)
            {
                return;
            }
            CheckBox checkBox = (CheckBox)sender;
            foreach (MetaInstanceVariable variable in this.metaValue.Instance.InstanceVariables)
            {
                if (checkBox.Name == variable.ToString())
                {
                    variable.Value.Bool = checkBox.Checked;
                    break;
                }
            }
            this.RefreshData();
        }

        private void ivButtonInstance_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MetaInstanceVariable metaInstanceVariable;
            for (int i = 0; i < this.metaValue.Instance.InstanceVariables.Count; i++)
            {
                metaInstanceVariable = this.metaValue.Instance.InstanceVariables[i];
                if (button.Name == metaInstanceVariable.ToString())
                {
                    MetaVariable metaVariable = ((MetaClass)this.metaType).AllVariables[i];
                    ManagerInstance form = new ManagerInstance(this.repository, (MetaClass)metaVariable.Type, metaInstanceVariable.Value, metaVariable.Nullable);
                    form.Text = metaInstanceVariable.ToString();
                    form.ShowDialog();
                    metaInstanceVariable.Value = form.MetaValue;
                    this.RefreshData();
                    break;
                }
            }
        }

        private void ivButtonValueList_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MetaInstanceVariable metaInstanceVariable;
            for (int i = 0; i < this.metaValue.Instance.InstanceVariables.Count; i++)
            {
                metaInstanceVariable = this.metaValue.Instance.InstanceVariables[i];
                if (button.Name == metaInstanceVariable.ToString())
                {
                    /*
                    MetaVariable metaVariable = this.metaClass.AllVariables[i];
                    if (metaVariable.Type.IsClass)
                    {
                        MetaList<MetaInstance> metaInstances = new MetaList<MetaInstance>();
                        foreach (MetaValue value in metaInstanceVariable.Value.InstanceList)
                        {
                            metaInstances.Add(value.Instance);
                        }
                        
                        ManagerInstances form = new ManagerInstances(this.repository, (MetaClass)metaVariable.Type, metaInstances);
                        form.Text = metaInstanceVariable.ToString();
                        form.ShowDialog();
                        metaInstances = form.MetaInstances;
                        MetaList<MetaInstanceVariable> metaInstanceList = new MetaList<MetaInstanceVariable>();
                        foreach (MetaInstance mi in metaInstances)
                        {
                            //metaInstanceVariable = new MetaInstanceVariable(this.repository, );
                            metaInstanceVariable.Value.Instance = mi;
                            metaInstanceList.Add(metaInstanceVariable);
                        }
                        //metaInstanceVariable.ValueInstanceList = new 

                        metaInstances = new MetaList<MetaInstance>();
                        foreach (MetaInstanceVariable miv in metaInstanceVariable.Value.InstanceList)
                        {
                            metaInstances.Add(miv.ValueInstance);
                        }
                        //metaInstanceVariable.ValueInstanceList = form.MetaInstances;
                        
                    }
                    else
                    {
                    }
                    */
                    this.RefreshData();
                    break;
                }
            }
            /*
            Button button = (Button)sender;
            MetaInstanceVariable variable;
            for (int i = 0; i < this.metaInstance.InstanceVariables.Count; i++)
            {
                variable = this.metaInstance.InstanceVariables[i];
                if (button.Name == variable.ToString())
                {
                    if (this.metaClass.AllVariables[i].Type.IsClass)
                    {
                        // TODO
                        //Forms.InstanceList form = new Forms.InstanceList(this.database, (MetaClass)this.metaClass.AllVariables[i].Type, (List<MetaInstance>)variable.ValueObject);
                        //form.Text = variable.ToString();
                        //form.ShowDialog();
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
            */
        }
        #endregion

    }
}
