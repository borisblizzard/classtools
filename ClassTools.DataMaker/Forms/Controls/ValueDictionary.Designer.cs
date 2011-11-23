namespace ClassTools.DataMaker.Forms.Controls
{
    partial class ValueDictionary
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbVariables = new System.Windows.Forms.GroupBox();
            this.vlVariables = new ClassTools.DataMaker.Forms.Controls.VariableList();
            this.gbValues = new System.Windows.Forms.GroupBox();
            this.bInstanceNew = new System.Windows.Forms.Button();
            this.bInstanceDelete = new System.Windows.Forms.Button();
            this.lbValues = new System.Windows.Forms.ListBox();
            this.gbKeys = new System.Windows.Forms.GroupBox();
            this.bEdit = new System.Windows.Forms.Button();
            this.lbKeys = new System.Windows.Forms.ListBox();
            this.gbVariables.SuspendLayout();
            this.gbValues.SuspendLayout();
            this.gbKeys.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbVariables
            // 
            this.gbVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbVariables.Controls.Add(this.vlVariables);
            this.gbVariables.Location = new System.Drawing.Point(412, 0);
            this.gbVariables.Name = "gbVariables";
            this.gbVariables.Size = new System.Drawing.Size(412, 440);
            this.gbVariables.TabIndex = 6;
            this.gbVariables.TabStop = false;
            this.gbVariables.Text = "Variables";
            // 
            // vlVariables
            // 
            this.vlVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vlVariables.AutoScroll = true;
            this.vlVariables.Enabled = false;
            this.vlVariables.Location = new System.Drawing.Point(6, 20);
            this.vlVariables.Name = "vlVariables";
            this.vlVariables.Size = new System.Drawing.Size(399, 402);
            this.vlVariables.TabIndex = 1;
            // 
            // gbValues
            // 
            this.gbValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gbValues.Controls.Add(this.bInstanceNew);
            this.gbValues.Controls.Add(this.bInstanceDelete);
            this.gbValues.Controls.Add(this.lbValues);
            this.gbValues.Location = new System.Drawing.Point(206, 0);
            this.gbValues.Name = "gbValues";
            this.gbValues.Size = new System.Drawing.Size(200, 440);
            this.gbValues.TabIndex = 5;
            this.gbValues.TabStop = false;
            this.gbValues.Text = "Values";
            // 
            // bInstanceNew
            // 
            this.bInstanceNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bInstanceNew.Location = new System.Drawing.Point(119, 407);
            this.bInstanceNew.Name = "bInstanceNew";
            this.bInstanceNew.Size = new System.Drawing.Size(75, 23);
            this.bInstanceNew.TabIndex = 1;
            this.bInstanceNew.Text = "Add new";
            this.bInstanceNew.UseVisualStyleBackColor = true;
            this.bInstanceNew.Click += new System.EventHandler(this.bInstanceNew_Click);
            // 
            // bInstanceDelete
            // 
            this.bInstanceDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bInstanceDelete.Location = new System.Drawing.Point(7, 407);
            this.bInstanceDelete.Name = "bInstanceDelete";
            this.bInstanceDelete.Size = new System.Drawing.Size(75, 23);
            this.bInstanceDelete.TabIndex = 1;
            this.bInstanceDelete.Text = "Delete";
            this.bInstanceDelete.UseVisualStyleBackColor = true;
            this.bInstanceDelete.Click += new System.EventHandler(this.bInstanceDelete_Click);
            // 
            // lbValues
            // 
            this.lbValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbValues.FormattingEnabled = true;
            this.lbValues.HorizontalScrollbar = true;
            this.lbValues.Location = new System.Drawing.Point(7, 20);
            this.lbValues.Name = "lbValues";
            this.lbValues.Size = new System.Drawing.Size(187, 381);
            this.lbValues.TabIndex = 0;
            this.lbValues.SelectedIndexChanged += new System.EventHandler(this.lbInstances_SelectedIndexChanged);
            // 
            // gbKeys
            // 
            this.gbKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gbKeys.Controls.Add(this.bEdit);
            this.gbKeys.Controls.Add(this.lbKeys);
            this.gbKeys.Location = new System.Drawing.Point(0, 0);
            this.gbKeys.Name = "gbKeys";
            this.gbKeys.Size = new System.Drawing.Size(200, 440);
            this.gbKeys.TabIndex = 7;
            this.gbKeys.TabStop = false;
            this.gbKeys.Text = "Keys";
            // 
            // bEdit
            // 
            this.bEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bEdit.Location = new System.Drawing.Point(7, 408);
            this.bEdit.Name = "bEdit";
            this.bEdit.Size = new System.Drawing.Size(75, 23);
            this.bEdit.TabIndex = 1;
            this.bEdit.Text = "Edit Key";
            this.bEdit.UseVisualStyleBackColor = true;
            this.bEdit.Click += new System.EventHandler(this.bEdit_Click);
            // 
            // lbKeys
            // 
            this.lbKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbKeys.FormattingEnabled = true;
            this.lbKeys.HorizontalScrollbar = true;
            this.lbKeys.Location = new System.Drawing.Point(7, 20);
            this.lbKeys.Name = "lbKeys";
            this.lbKeys.Size = new System.Drawing.Size(187, 381);
            this.lbKeys.TabIndex = 0;
            this.lbKeys.SelectedIndexChanged += new System.EventHandler(this.lbKeys_SelectedIndexChanged);
            // 
            // ValueDictionary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbKeys);
            this.Controls.Add(this.gbVariables);
            this.Controls.Add(this.gbValues);
            this.Name = "ValueDictionary";
            this.Size = new System.Drawing.Size(824, 440);
            this.gbVariables.ResumeLayout(false);
            this.gbValues.ResumeLayout(false);
            this.gbKeys.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbVariables;
        private System.Windows.Forms.GroupBox gbValues;
        private System.Windows.Forms.Button bInstanceNew;
        private System.Windows.Forms.Button bInstanceDelete;
        private System.Windows.Forms.ListBox lbValues;
        private VariableList vlVariables;
        private System.Windows.Forms.GroupBox gbKeys;
        private System.Windows.Forms.Button bEdit;
        private System.Windows.Forms.ListBox lbKeys;
    }
}
