namespace ClassTools.DataMaker.Forms
{
    partial class ManagerInstance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerInstance));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbInstanceVariables = new System.Windows.Forms.GroupBox();
            this.cbxExists = new System.Windows.Forms.CheckBox();
            this.bOk = new System.Windows.Forms.Button();
            this.ivbInstanceVariables = new ClassTools.DataMaker.Forms.Controls.InstanceVariables();
            this.menuStrip.SuspendLayout();
            this.gbInstanceVariables.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(438, 24);
            this.menuStrip.TabIndex = 1;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // closeMenuItem
            // 
            this.closeMenuItem.Name = "closeMenuItem";
            this.closeMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.closeMenuItem.Size = new System.Drawing.Size(146, 22);
            this.closeMenuItem.Text = "&Close";
            this.closeMenuItem.Click += new System.EventHandler(this.closeMenuItem_Click);
            // 
            // gbInstanceVariables
            // 
            this.gbInstanceVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInstanceVariables.Controls.Add(this.ivbInstanceVariables);
            this.gbInstanceVariables.Location = new System.Drawing.Point(12, 51);
            this.gbInstanceVariables.Name = "gbInstanceVariables";
            this.gbInstanceVariables.Size = new System.Drawing.Size(414, 430);
            this.gbInstanceVariables.TabIndex = 4;
            this.gbInstanceVariables.TabStop = false;
            this.gbInstanceVariables.Text = "Instance Variables";
            // 
            // cbxExists
            // 
            this.cbxExists.AutoSize = true;
            this.cbxExists.Location = new System.Drawing.Point(15, 28);
            this.cbxExists.Name = "cbxExists";
            this.cbxExists.Size = new System.Drawing.Size(59, 17);
            this.cbxExists.TabIndex = 5;
            this.cbxExists.Text = "Exists?";
            this.cbxExists.UseVisualStyleBackColor = true;
            this.cbxExists.CheckedChanged += new System.EventHandler(this.cbxExists_CheckedChanged);
            // 
            // bOk
            // 
            this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bOk.Location = new System.Drawing.Point(351, 487);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 6;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.closeMenuItem_Click);
            // 
            // ivbInstanceVariables
            // 
            this.ivbInstanceVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ivbInstanceVariables.Enabled = false;
            this.ivbInstanceVariables.Location = new System.Drawing.Point(6, 20);
            this.ivbInstanceVariables.Name = "ivbInstanceVariables";
            this.ivbInstanceVariables.Size = new System.Drawing.Size(400, 400);
            this.ivbInstanceVariables.TabIndex = 1;
            // 
            // ManagerInstance
            // 
            this.AcceptButton = this.bOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bOk;
            this.ClientSize = new System.Drawing.Size(438, 521);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.cbxExists);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.gbInstanceVariables);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManagerInstance";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Instance";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.gbInstanceVariables.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.GroupBox gbInstanceVariables;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeMenuItem;
        private Controls.InstanceVariables ivbInstanceVariables;
        private System.Windows.Forms.CheckBox cbxExists;
        private System.Windows.Forms.Button bOk;
    }
}

