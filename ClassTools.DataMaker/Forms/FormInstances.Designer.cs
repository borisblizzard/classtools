namespace ClassTools.DataMaker.Forms
{
    partial class FormInstances
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInstances));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.newStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.upStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.addNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.moveUpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdDatabase = new System.Windows.Forms.OpenFileDialog();
            this.sfdDatabase = new System.Windows.Forms.SaveFileDialog();
            this.gbInstances = new System.Windows.Forms.GroupBox();
            this.bInstanceNew = new System.Windows.Forms.Button();
            this.bInstanceDelete = new System.Windows.Forms.Button();
            this.lbInstances = new System.Windows.Forms.ListBox();
            this.ofdModel = new System.Windows.Forms.OpenFileDialog();
            this.gbInstanceVariables = new System.Windows.Forms.GroupBox();
            this.instanceVariablesBox1 = new ClassTools.DataMaker.Forms.InstanceVariablesBox();
            this.ivbInstanceVariables = new ClassTools.DataMaker.Forms.InstanceVariablesBox();
            this.contextMenuStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.gbInstances.SuspendLayout();
            this.gbInstanceVariables.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyStripMenuItem,
            this.pasteStripMenuItem,
            this.toolStripSeparator2,
            this.newStripMenuItem,
            this.deleteStripMenuItem,
            this.toolStripSeparator3,
            this.upStripMenuItem,
            this.downStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(138, 148);
            // 
            // copyStripMenuItem
            // 
            this.copyStripMenuItem.Name = "copyStripMenuItem";
            this.copyStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.copyStripMenuItem.Text = "Copy";
            this.copyStripMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
            // 
            // pasteStripMenuItem
            // 
            this.pasteStripMenuItem.Name = "pasteStripMenuItem";
            this.pasteStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.pasteStripMenuItem.Text = "Paste";
            this.pasteStripMenuItem.Click += new System.EventHandler(this.pasteMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(134, 6);
            // 
            // newStripMenuItem
            // 
            this.newStripMenuItem.Name = "newStripMenuItem";
            this.newStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.newStripMenuItem.Text = "Add new";
            this.newStripMenuItem.Click += new System.EventHandler(this.addNewMenuItem_Click);
            // 
            // deleteStripMenuItem
            // 
            this.deleteStripMenuItem.Name = "deleteStripMenuItem";
            this.deleteStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.deleteStripMenuItem.Text = "Delete";
            this.deleteStripMenuItem.Click += new System.EventHandler(this.deleteMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(134, 6);
            // 
            // upStripMenuItem
            // 
            this.upStripMenuItem.Name = "upStripMenuItem";
            this.upStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.upStripMenuItem.Text = "Move up";
            this.upStripMenuItem.Click += new System.EventHandler(this.moveUpMenuItem_Click);
            // 
            // downStripMenuItem
            // 
            this.downStripMenuItem.Name = "downStripMenuItem";
            this.downStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.downStripMenuItem.Text = "Move down";
            this.downStripMenuItem.Click += new System.EventHandler(this.moveDownMenuItem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(736, 24);
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
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyMenuItem,
            this.pasteMenuItem,
            this.toolStripSeparator7,
            this.addNewMenuItem,
            this.deleteMenuItem,
            this.toolStripSeparator6,
            this.moveUpMenuItem,
            this.moveDownMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyMenuItem.Image")));
            this.copyMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyMenuItem.Name = "copyMenuItem";
            this.copyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyMenuItem.Size = new System.Drawing.Size(202, 22);
            this.copyMenuItem.Text = "&Copy";
            this.copyMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
            // 
            // pasteMenuItem
            // 
            this.pasteMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteMenuItem.Image")));
            this.pasteMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteMenuItem.Name = "pasteMenuItem";
            this.pasteMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteMenuItem.Size = new System.Drawing.Size(202, 22);
            this.pasteMenuItem.Text = "&Paste";
            this.pasteMenuItem.Click += new System.EventHandler(this.pasteMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(199, 6);
            // 
            // addNewMenuItem
            // 
            this.addNewMenuItem.Name = "addNewMenuItem";
            this.addNewMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.addNewMenuItem.Size = new System.Drawing.Size(202, 22);
            this.addNewMenuItem.Text = "Add new";
            this.addNewMenuItem.Click += new System.EventHandler(this.addNewMenuItem_Click);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Name = "deleteMenuItem";
            this.deleteMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteMenuItem.Size = new System.Drawing.Size(202, 22);
            this.deleteMenuItem.Text = "Delete";
            this.deleteMenuItem.Click += new System.EventHandler(this.deleteMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(199, 6);
            // 
            // moveUpMenuItem
            // 
            this.moveUpMenuItem.Name = "moveUpMenuItem";
            this.moveUpMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.moveUpMenuItem.Size = new System.Drawing.Size(202, 22);
            this.moveUpMenuItem.Text = "Move up";
            this.moveUpMenuItem.Click += new System.EventHandler(this.moveUpMenuItem_Click);
            // 
            // moveDownMenuItem
            // 
            this.moveDownMenuItem.Name = "moveDownMenuItem";
            this.moveDownMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.moveDownMenuItem.Size = new System.Drawing.Size(202, 22);
            this.moveDownMenuItem.Text = "Move down";
            this.moveDownMenuItem.Click += new System.EventHandler(this.moveDownMenuItem_Click);
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
            // gbInstances
            // 
            this.gbInstances.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gbInstances.Controls.Add(this.bInstanceNew);
            this.gbInstances.Controls.Add(this.bInstanceDelete);
            this.gbInstances.Controls.Add(this.lbInstances);
            this.gbInstances.Location = new System.Drawing.Point(12, 38);
            this.gbInstances.Name = "gbInstances";
            this.gbInstances.Size = new System.Drawing.Size(193, 438);
            this.gbInstances.TabIndex = 3;
            this.gbInstances.TabStop = false;
            this.gbInstances.Text = "Instances";
            // 
            // bInstanceNew
            // 
            this.bInstanceNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bInstanceNew.Location = new System.Drawing.Point(112, 407);
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
            // lbInstances
            // 
            this.lbInstances.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInstances.ContextMenuStrip = this.contextMenuStrip;
            this.lbInstances.FormattingEnabled = true;
            this.lbInstances.Location = new System.Drawing.Point(7, 20);
            this.lbInstances.Name = "lbInstances";
            this.lbInstances.Size = new System.Drawing.Size(180, 381);
            this.lbInstances.TabIndex = 0;
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
            this.gbInstanceVariables.Location = new System.Drawing.Point(211, 38);
            this.gbInstanceVariables.Name = "gbInstanceVariables";
            this.gbInstanceVariables.Size = new System.Drawing.Size(513, 438);
            this.gbInstanceVariables.TabIndex = 4;
            this.gbInstanceVariables.TabStop = false;
            this.gbInstanceVariables.Text = "Instance Variables";
            // 
            // instanceVariablesBox1
            // 
            this.instanceVariablesBox1.Location = new System.Drawing.Point(3, 16);
            this.instanceVariablesBox1.Name = "instanceVariablesBox1";
            this.instanceVariablesBox1.Size = new System.Drawing.Size(500, 400);
            this.instanceVariablesBox1.TabIndex = 0;
            // 
            // ivbInstanceVariables
            // 
            this.ivbInstanceVariables.Location = new System.Drawing.Point(6, 20);
            this.ivbInstanceVariables.Name = "ivbInstanceVariables";
            this.ivbInstanceVariables.Size = new System.Drawing.Size(500, 400);
            this.ivbInstanceVariables.TabIndex = 1;
            // 
            // FormInstances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 486);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.gbInstanceVariables);
            this.Controls.Add(this.gbInstances);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormInstances";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Instances";
            this.contextMenuStrip.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.gbInstances.ResumeLayout(false);
            this.gbInstanceVariables.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.OpenFileDialog ofdDatabase;
        private System.Windows.Forms.SaveFileDialog sfdDatabase;
        private System.Windows.Forms.GroupBox gbInstances;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem newStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem upStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveUpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.OpenFileDialog ofdModel;
        private System.Windows.Forms.ListBox lbInstances;
        private System.Windows.Forms.Button bInstanceNew;
        private System.Windows.Forms.Button bInstanceDelete;
        private System.Windows.Forms.GroupBox gbInstanceVariables;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeMenuItem;
        private InstanceVariablesBox ivbInstanceVariables;
        private InstanceVariablesBox instanceVariablesBox1;
    }
}

