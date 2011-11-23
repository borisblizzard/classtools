namespace ClassTools.DataMaker.Forms
{
    partial class TypeUpdate
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TypeUpdate));
            this.gbOldTypes = new System.Windows.Forms.GroupBox();
            this.bAutoReplace = new System.Windows.Forms.Button();
            this.lbOldTypes = new System.Windows.Forms.ListBox();
            this.bOk = new System.Windows.Forms.Button();
            this.gbNewTypes = new System.Windows.Forms.GroupBox();
            this.lbNewTypes = new System.Windows.Forms.ListBox();
            this.bReplace = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.gbOldTypes.SuspendLayout();
            this.gbNewTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOldTypes
            // 
            this.gbOldTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gbOldTypes.Controls.Add(this.bAutoReplace);
            this.gbOldTypes.Controls.Add(this.lbOldTypes);
            this.gbOldTypes.Location = new System.Drawing.Point(10, 12);
            this.gbOldTypes.Name = "gbOldTypes";
            this.gbOldTypes.Size = new System.Drawing.Size(296, 587);
            this.gbOldTypes.TabIndex = 4;
            this.gbOldTypes.TabStop = false;
            this.gbOldTypes.Text = "Old Types";
            // 
            // bAutoReplace
            // 
            this.bAutoReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bAutoReplace.Location = new System.Drawing.Point(7, 551);
            this.bAutoReplace.Name = "bAutoReplace";
            this.bAutoReplace.Size = new System.Drawing.Size(175, 23);
            this.bAutoReplace.TabIndex = 9;
            this.bAutoReplace.Text = "Auto Replace Matching Names";
            this.bAutoReplace.UseVisualStyleBackColor = true;
            this.bAutoReplace.Click += new System.EventHandler(this.bAutoReplace_Click);
            // 
            // lbOldTypes
            // 
            this.lbOldTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOldTypes.FormattingEnabled = true;
            this.lbOldTypes.HorizontalScrollbar = true;
            this.lbOldTypes.Location = new System.Drawing.Point(7, 20);
            this.lbOldTypes.Name = "lbOldTypes";
            this.lbOldTypes.Size = new System.Drawing.Size(283, 524);
            this.lbOldTypes.TabIndex = 0;
            // 
            // bOk
            // 
            this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOk.Location = new System.Drawing.Point(614, 605);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 6;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.closeMenuItem_Click);
            // 
            // gbNewTypes
            // 
            this.gbNewTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNewTypes.Controls.Add(this.lbNewTypes);
            this.gbNewTypes.Location = new System.Drawing.Point(393, 12);
            this.gbNewTypes.Name = "gbNewTypes";
            this.gbNewTypes.Size = new System.Drawing.Size(296, 587);
            this.gbNewTypes.TabIndex = 7;
            this.gbNewTypes.TabStop = false;
            this.gbNewTypes.Text = "New Types";
            // 
            // lbNewTypes
            // 
            this.lbNewTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNewTypes.FormattingEnabled = true;
            this.lbNewTypes.HorizontalScrollbar = true;
            this.lbNewTypes.Location = new System.Drawing.Point(7, 20);
            this.lbNewTypes.Name = "lbNewTypes";
            this.lbNewTypes.Size = new System.Drawing.Size(283, 524);
            this.lbNewTypes.TabIndex = 0;
            // 
            // bReplace
            // 
            this.bReplace.Location = new System.Drawing.Point(312, 269);
            this.bReplace.Name = "bReplace";
            this.bReplace.Size = new System.Drawing.Size(75, 23);
            this.bReplace.TabIndex = 1;
            this.bReplace.Text = "Replace";
            this.bReplace.UseVisualStyleBackColor = true;
            this.bReplace.Click += new System.EventHandler(this.bReplace_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(533, 605);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 8;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // TypeUpdate
            // 
            this.AcceptButton = this.bOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCancel;
            this.ClientSize = new System.Drawing.Size(704, 641);
            this.Controls.Add(this.bReplace);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.gbNewTypes);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.gbOldTypes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TypeUpdate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Types";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onFormClosing);
            this.gbOldTypes.ResumeLayout(false);
            this.gbNewTypes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOldTypes;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.GroupBox gbNewTypes;
        private System.Windows.Forms.ListBox lbOldTypes;
        private System.Windows.Forms.Button bReplace;
        private System.Windows.Forms.ListBox lbNewTypes;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bAutoReplace;
    }
}

