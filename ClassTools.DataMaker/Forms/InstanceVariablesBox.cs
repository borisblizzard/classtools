using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using ClassTools.Model;

namespace ClassTools.DataMaker.Forms
{
    public partial class InstanceVariablesBox : UserControl
    {
        #region Constants
        int OFFSET = 5;
        #endregion

        #region Fields
        private MetaClass metaClass;
        private MetaInstance metaInstance;
        private List<Control> valueControls;
        bool updating;
        #endregion

        #region Properties
        public MetaClass MetaClass
        {
            set
            {
                this.metaClass = value;
                this.setupLayout();
            }
        }

        public MetaInstance MetaInstance
        {
            set
            {
                this.metaInstance = value;
                this.Enabled = (this.metaInstance != null);
                this.refresh();
            }
        }
        #endregion

        #region Construct
        public InstanceVariablesBox()
        {
            InitializeComponent();
            this.valueControls = new List<Control>();
            this.updating = false;
        }
        #endregion

        #region Setup
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
                label.Location = new System.Drawing.Point(5, OFFSET + 3 + i * 26);
                label.Text = variable.ToString();
                label.Name = "name " + variable.ToString();
                label.TabIndex = 10 + i * 2;
                //label.TextAlign = ContentAlignment.MiddleRight;
                label.AutoSize = true;
                if (maxWidth < label.Width)
                {
                    maxWidth = label.Width;
                }
                //label.AutoSize = false;
                //label.Size = new System.Drawing.Size(300, 13);
                //label.UseVisualStyleBackColor = true;
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
                                    numericUpDown.Size = new System.Drawing.Size(200, 20);
                                    numericUpDown.Location = new System.Drawing.Point(5, OFFSET + i * 26);
                                    numericUpDown.Maximum = int.MaxValue;
                                    numericUpDown.Minimum = int.MinValue;
                                    numericUpDown.TextAlign = HorizontalAlignment.Right;
                                    numericUpDown.Name = "value " + variable.ToString();
                                    numericUpDown.TabIndex = 11 + i * 2;
                                    break;
                                case "unsigned int":
                                    numericUpDown = new NumericUpDown();
                                    this.Controls.Add(numericUpDown);
                                    this.valueControls.Add(numericUpDown);
                                    numericUpDown.Size = new System.Drawing.Size(200, 20);
                                    numericUpDown.Location = new System.Drawing.Point(5, OFFSET + i * 26);
                                    numericUpDown.Maximum = uint.MaxValue;
                                    numericUpDown.Minimum = uint.MinValue;
                                    numericUpDown.TextAlign = HorizontalAlignment.Right;
                                    numericUpDown.Name = "value " + variable.ToString();
                                    numericUpDown.TabIndex = 11 + i * 2;
                                    break;
                                case "short":
                                    numericUpDown = new NumericUpDown();
                                    this.Controls.Add(numericUpDown);
                                    this.valueControls.Add(numericUpDown);
                                    numericUpDown.Size = new System.Drawing.Size(200, 20);
                                    numericUpDown.Location = new System.Drawing.Point(5, OFFSET + i * 26);
                                    numericUpDown.Maximum = short.MaxValue;
                                    numericUpDown.Minimum = short.MinValue;
                                    numericUpDown.TextAlign = HorizontalAlignment.Right;
                                    numericUpDown.Name = "value " + variable.ToString();
                                    numericUpDown.TabIndex = 11 + i * 2;
                                    break;
                                case "unsigned short":
                                    numericUpDown = new NumericUpDown();
                                    this.Controls.Add(numericUpDown);
                                    this.valueControls.Add(numericUpDown);
                                    numericUpDown.Size = new System.Drawing.Size(200, 20);
                                    numericUpDown.Location = new System.Drawing.Point(5, OFFSET + i * 26);
                                    numericUpDown.Maximum = ushort.MaxValue;
                                    numericUpDown.Minimum = ushort.MinValue;
                                    numericUpDown.TextAlign = HorizontalAlignment.Right;
                                    numericUpDown.Name = "value " + variable.ToString();
                                    numericUpDown.TabIndex = 11 + i * 2;
                                    break;
                                case "long":
                                    numericUpDown = new NumericUpDown();
                                    this.Controls.Add(numericUpDown);
                                    this.valueControls.Add(numericUpDown);
                                    numericUpDown.Size = new System.Drawing.Size(200, 20);
                                    numericUpDown.Location = new System.Drawing.Point(5, OFFSET + i * 26);
                                    /*
                                    numericUpDown.Maximum = long.MaxValue;
                                    numericUpDown.Minimum = long.MinValue;
                                    */
                                    numericUpDown.Maximum = int.MaxValue;
                                    numericUpDown.Minimum = int.MinValue;
                                    numericUpDown.TextAlign = HorizontalAlignment.Right;
                                    numericUpDown.Name = "value " + variable.ToString();
                                    numericUpDown.TabIndex = 11 + i * 2;
                                    break;
                                case "unsigned long":
                                    numericUpDown = new NumericUpDown();
                                    this.Controls.Add(numericUpDown);
                                    this.valueControls.Add(numericUpDown);
                                    numericUpDown.Size = new System.Drawing.Size(200, 20);
                                    numericUpDown.Location = new System.Drawing.Point(5, OFFSET + i * 26);
                                    /*
                                    numericUpDown.Maximum = ulong.MaxValue;
                                    numericUpDown.Minimum = ulong.MinValue;
                                    */
                                    numericUpDown.Maximum = uint.MaxValue;
                                    numericUpDown.Minimum = uint.MinValue;
                                    numericUpDown.TextAlign = HorizontalAlignment.Right;
                                    numericUpDown.Name = "value " + variable.ToString();
                                    numericUpDown.TabIndex = 11 + i * 2;
                                    break;
                                case "float":
                                    textBox = new TextBox();
                                    this.Controls.Add(textBox);
                                    this.valueControls.Add(textBox);
                                    textBox.Size = new System.Drawing.Size(200, 20);
                                    textBox.Location = new System.Drawing.Point(5, OFFSET + i * 26);
                                    textBox.Text = "0";
                                    textBox.Name = "value " + variable.ToString();
                                    textBox.TabIndex = 11 + i * 2;
                                    break;
                                case "double":
                                    textBox = new TextBox();
                                    this.Controls.Add(textBox);
                                    this.valueControls.Add(textBox);
                                    textBox.Size = new System.Drawing.Size(200, 20);
                                    textBox.Location = new System.Drawing.Point(5, OFFSET + i * 26);
                                    textBox.Text = "0";
                                    textBox.Name = "value " + variable.ToString();
                                    textBox.TabIndex = 11 + i * 2;
                                    break;
                                case "bool":
                                    checkBox = new CheckBox();
                                    this.Controls.Add(checkBox);
                                    this.valueControls.Add(checkBox);
                                    checkBox.Location = new System.Drawing.Point(5, OFFSET + 2 + i * 26);
                                    checkBox.Checked = false;
                                    checkBox.AutoSize = true;
                                    checkBox.TabIndex = 11 + i * 2;
                                    break;
                                default:
                                    textBox = new TextBox();
                                    this.Controls.Add(textBox);
                                    this.valueControls.Add(textBox);
                                    textBox.Size = new System.Drawing.Size(200, 20);
                                    textBox.Location = new System.Drawing.Point(5, OFFSET + i * 26);
                                    //textBox.Text = variable.ToString();
                                    textBox.Name = "value " + variable.ToString();
                                    textBox.TabIndex = 11 + i * 2;
                                    break;
                            }
                            break;
                        case ETypeCategory.Collection:
                            button = new Button();
                            this.Controls.Add(button);
                            this.valueControls.Add(button);
                            button.Location = new System.Drawing.Point(5, OFFSET - 2 + i * 26);
                            button.Text = "Manage";
                            button.Name = "value " + variable.ToString();
                            button.TabIndex = 11 + i * 2;
                            break;
                        case ETypeCategory.Dictionary:
                            button = new Button();
                            this.Controls.Add(button);
                            this.valueControls.Add(button);
                            button.Location = new System.Drawing.Point(5, OFFSET - 2 + i * 26);
                            button.Text = "Manage";
                            button.Name = "value " + variable.ToString();
                            button.TabIndex = 11 + i * 2;
                            break;
                    }
                }
                else
                {
                    button = new Button();
                    this.Controls.Add(button);
                    this.valueControls.Add(button);
                    button.Location = new System.Drawing.Point(5, OFFSET - 2 + i * 26);
                    button.Text = "Manage";
                    button.Name = "value " + variable.ToString();
                    button.TabIndex = 11 + i * 2;
                }
            }
            foreach (Control control in this.valueControls)
            {
                control.Left += maxWidth + 10;
            }
            this.ResumeLayout(false);
            this.PerformLayout();
            /*
            this.bEdit.Location = new System.Drawing.Point(229, 433);
            this.bEdit.Name = "bEdit";
            this.bEdit.Size = new System.Drawing.Size(75, 23);
            this.bEdit.TabIndex = 2;
            this.bEdit.Text = "Edit";
            this.bEdit.UseVisualStyleBackColor = true;
            this.bEdit.Click += new System.EventHandler(this.bEdit_Click);
            */

            //6; 23
            //69; 20
            //69; 46
            //194; 44
        }
        #endregion

        #region Refresh
        public void refresh()
        {
            if (this.updating)
            {
                return;
            }
            this.updating = true;
            this.updating = false;
        }
        #endregion

    }
}
