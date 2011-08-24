﻿namespace ClassTools.DataMaker.Forms
{
    partial class Instance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Instance));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdDatabase = new System.Windows.Forms.OpenFileDialog();
            this.sfdDatabase = new System.Windows.Forms.SaveFileDialog();
            this.ofdModel = new System.Windows.Forms.OpenFileDialog();
            this.gbInstanceVariables = new System.Windows.Forms.GroupBox();
            this.ivbInstanceVariables = new ClassTools.DataMaker.Forms.Controls.InstanceVariables();
            this.instanceVariablesBox1 = new ClassTools.DataMaker.Forms.Controls.InstanceVariables();
            this.cbExists = new System.Windows.Forms.CheckBox();
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
            this.menuStrip.Size = new System.Drawing.Size(538, 24);
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
            // ofdDatabase
            // 
            this.ofdDatabase.DefaultExt = "cmm";
            this.ofdDatabase.Filter = "Model Database files (*.mdb)|*.mdb";
            this.ofdDatabase.Title = "Open Class Model";
            // 
            // sfdDatabase
            // 
            this.sfdDatabase.DefaultExt = "cmm";
            this.sfdDatabase.Filter = "Model Database files (*.mdb)|*.mdb";
            this.sfdDatabase.Title = "Save Class Model";
            // 
            // ofdModel
            // 
            this.ofdModel.DefaultExt = "cmm";
            this.ofdModel.Filter = "Class Maker Model files (*.cmm)|*.cmm";
            this.ofdModel.Title = "Open Class Model";
            // 
            // gbInstanceVariables
            // 
            this.gbInstanceVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInstanceVariables.Controls.Add(this.ivbInstanceVariables);
            this.gbInstanceVariables.Controls.Add(this.instanceVariablesBox1);
            this.gbInstanceVariables.Location = new System.Drawing.Point(12, 51);
            this.gbInstanceVariables.Name = "gbInstanceVariables";
            this.gbInstanceVariables.Size = new System.Drawing.Size(514, 430);
            this.gbInstanceVariables.TabIndex = 4;
            this.gbInstanceVariables.TabStop = false;
            this.gbInstanceVariables.Text = "Instance Variables";
            // 
            // ivbInstanceVariables
            // 
            this.ivbInstanceVariables.Location = new System.Drawing.Point(6, 20);
            this.ivbInstanceVariables.Name = "ivbInstanceVariables";
            this.ivbInstanceVariables.Size = new System.Drawing.Size(500, 400);
            this.ivbInstanceVariables.TabIndex = 1;
            // 
            // instanceVariablesBox1
            // 
            this.instanceVariablesBox1.Location = new System.Drawing.Point(3, 16);
            this.instanceVariablesBox1.Name = "instanceVariablesBox1";
            this.instanceVariablesBox1.Size = new System.Drawing.Size(500, 400);
            this.instanceVariablesBox1.TabIndex = 0;
            // 
            // cbExists
            // 
            this.cbExists.AutoSize = true;
            this.cbExists.Location = new System.Drawing.Point(15, 28);
            this.cbExists.Name = "cbExists";
            this.cbExists.Size = new System.Drawing.Size(59, 17);
            this.cbExists.TabIndex = 5;
            this.cbExists.Text = "Exists?";
            this.cbExists.UseVisualStyleBackColor = true;
            this.cbExists.CheckedChanged += new System.EventHandler(this.cbExists_CheckedChanged);
            // 
            // FormObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 493);
            this.Controls.Add(this.cbExists);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.gbInstanceVariables);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormObject";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Instances";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.gbInstanceVariables.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.OpenFileDialog ofdDatabase;
        private System.Windows.Forms.SaveFileDialog sfdDatabase;
        private System.Windows.Forms.OpenFileDialog ofdModel;
        private System.Windows.Forms.GroupBox gbInstanceVariables;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeMenuItem;
        private Controls.InstanceVariables ivbInstanceVariables;
        private Controls.InstanceVariables instanceVariablesBox1;
        private System.Windows.Forms.CheckBox cbExists;
    }
}

