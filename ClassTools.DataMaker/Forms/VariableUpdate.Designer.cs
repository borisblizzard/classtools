namespace ClassTools.DataMaker.Forms
{
    partial class VariableUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VariableUpdate));
            this.gbOldVariables = new System.Windows.Forms.GroupBox();
            this.bAutoReplace = new System.Windows.Forms.Button();
            this.lbOldVariables = new System.Windows.Forms.ListBox();
            this.bOk = new System.Windows.Forms.Button();
            this.gbNewVariables = new System.Windows.Forms.GroupBox();
            this.lbNewVariables = new System.Windows.Forms.ListBox();
            this.bReplace = new System.Windows.Forms.Button();
            this.bRemove = new System.Windows.Forms.Button();
            this.gbOldVariables.SuspendLayout();
            this.gbNewVariables.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOldVariables
            // 
            this.gbOldVariables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gbOldVariables.Controls.Add(this.bAutoReplace);
            this.gbOldVariables.Controls.Add(this.lbOldVariables);
            this.gbOldVariables.Location = new System.Drawing.Point(10, 12);
            this.gbOldVariables.Name = "gbOldVariables";
            this.gbOldVariables.Size = new System.Drawing.Size(296, 430);
            this.gbOldVariables.TabIndex = 4;
            this.gbOldVariables.TabStop = false;
            this.gbOldVariables.Text = "Old Variables";
            // 
            // bAutoReplace
            // 
            this.bAutoReplace.Location = new System.Drawing.Point(7, 394);
            this.bAutoReplace.Name = "bAutoReplace";
            this.bAutoReplace.Size = new System.Drawing.Size(175, 23);
            this.bAutoReplace.TabIndex = 9;
            this.bAutoReplace.Text = "Auto Replace Matching Names";
            this.bAutoReplace.UseVisualStyleBackColor = true;
            this.bAutoReplace.Click += new System.EventHandler(this.bAutoReplace_Click);
            // 
            // lbOldVariables
            // 
            this.lbOldVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOldVariables.FormattingEnabled = true;
            this.lbOldVariables.Location = new System.Drawing.Point(7, 20);
            this.lbOldVariables.Name = "lbOldVariables";
            this.lbOldVariables.Size = new System.Drawing.Size(283, 368);
            this.lbOldVariables.TabIndex = 0;
            // 
            // bOk
            // 
            this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bOk.Location = new System.Drawing.Point(614, 448);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 6;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.closeMenuItem_Click);
            // 
            // gbNewVariables
            // 
            this.gbNewVariables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNewVariables.Controls.Add(this.lbNewVariables);
            this.gbNewVariables.Location = new System.Drawing.Point(393, 12);
            this.gbNewVariables.Name = "gbNewVariables";
            this.gbNewVariables.Size = new System.Drawing.Size(296, 430);
            this.gbNewVariables.TabIndex = 7;
            this.gbNewVariables.TabStop = false;
            this.gbNewVariables.Text = "New Variables";
            // 
            // lbNewVariables
            // 
            this.lbNewVariables.FormattingEnabled = true;
            this.lbNewVariables.Location = new System.Drawing.Point(7, 20);
            this.lbNewVariables.Name = "lbNewVariables";
            this.lbNewVariables.Size = new System.Drawing.Size(283, 368);
            this.lbNewVariables.TabIndex = 0;
            // 
            // bReplace
            // 
            this.bReplace.Location = new System.Drawing.Point(312, 186);
            this.bReplace.Name = "bReplace";
            this.bReplace.Size = new System.Drawing.Size(75, 23);
            this.bReplace.TabIndex = 1;
            this.bReplace.Text = "Replace";
            this.bReplace.UseVisualStyleBackColor = true;
            this.bReplace.Click += new System.EventHandler(this.bReplace_Click);
            // 
            // bRemove
            // 
            this.bRemove.Location = new System.Drawing.Point(312, 215);
            this.bRemove.Name = "bRemove";
            this.bRemove.Size = new System.Drawing.Size(75, 23);
            this.bRemove.TabIndex = 8;
            this.bRemove.Text = "Remove";
            this.bRemove.UseVisualStyleBackColor = true;
            this.bRemove.Click += new System.EventHandler(this.bRemove_Click);
            // 
            // VariableUpdate
            // 
            this.AcceptButton = this.bOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 484);
            this.Controls.Add(this.bRemove);
            this.Controls.Add(this.bReplace);
            this.Controls.Add(this.gbNewVariables);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.gbOldVariables);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VariableUpdate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Variables";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onFormClosing);
            this.gbOldVariables.ResumeLayout(false);
            this.gbNewVariables.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOldVariables;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.GroupBox gbNewVariables;
        private System.Windows.Forms.ListBox lbOldVariables;
        private System.Windows.Forms.Button bReplace;
        private System.Windows.Forms.ListBox lbNewVariables;
        private System.Windows.Forms.Button bAutoReplace;
        private System.Windows.Forms.Button bRemove;
    }
}

