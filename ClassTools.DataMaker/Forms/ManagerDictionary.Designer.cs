namespace ClassTools.DataMaker.Forms
{
    partial class ManagerDictionary
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerDictionary));
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
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.addNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bOk = new System.Windows.Forms.Button();
			this.vdValues = new ClassTools.DataMaker.Forms.Controls.ValueDictionary();
			this.contextMenuStrip.SuspendLayout();
			this.menuStrip.SuspendLayout();
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
			// 
			// deleteStripMenuItem
			// 
			this.deleteStripMenuItem.Name = "deleteStripMenuItem";
			this.deleteStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.deleteStripMenuItem.Text = "Delete";
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
			// 
			// downStripMenuItem
			// 
			this.downStripMenuItem.Name = "downStripMenuItem";
			this.downStripMenuItem.Size = new System.Drawing.Size(137, 22);
			this.downStripMenuItem.Text = "Move down";
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(848, 24);
			this.menuStrip.TabIndex = 1;
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyMenuItem,
            this.pasteMenuItem,
            this.toolStripSeparator7,
            this.addNewMenuItem,
            this.deleteMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// copyMenuItem
			// 
			this.copyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyMenuItem.Image")));
			this.copyMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.copyMenuItem.Name = "copyMenuItem";
			this.copyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
			this.copyMenuItem.Size = new System.Drawing.Size(176, 22);
			this.copyMenuItem.Text = "&Copy";
			this.copyMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
			// 
			// pasteMenuItem
			// 
			this.pasteMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteMenuItem.Image")));
			this.pasteMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.pasteMenuItem.Name = "pasteMenuItem";
			this.pasteMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.V)));
			this.pasteMenuItem.Size = new System.Drawing.Size(176, 22);
			this.pasteMenuItem.Text = "&Paste";
			this.pasteMenuItem.Click += new System.EventHandler(this.pasteMenuItem_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(173, 6);
			// 
			// addNewMenuItem
			// 
			this.addNewMenuItem.Name = "addNewMenuItem";
			this.addNewMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.addNewMenuItem.Size = new System.Drawing.Size(176, 22);
			this.addNewMenuItem.Text = "Add new";
			this.addNewMenuItem.Click += new System.EventHandler(this.addNewMenuItem_Click);
			// 
			// deleteMenuItem
			// 
			this.deleteMenuItem.Name = "deleteMenuItem";
			this.deleteMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
			this.deleteMenuItem.Size = new System.Drawing.Size(176, 22);
			this.deleteMenuItem.Text = "Delete";
			this.deleteMenuItem.Click += new System.EventHandler(this.deleteMenuItem_Click);
			// 
			// bOk
			// 
			this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bOk.Location = new System.Drawing.Point(761, 473);
			this.bOk.Name = "bOk";
			this.bOk.Size = new System.Drawing.Size(75, 23);
			this.bOk.TabIndex = 2;
			this.bOk.Text = "OK";
			this.bOk.UseVisualStyleBackColor = true;
			this.bOk.Click += new System.EventHandler(this.closeMenuItem_Click);
			// 
			// vdValues
			// 
			this.vdValues.Enabled = false;
			this.vdValues.Location = new System.Drawing.Point(12, 27);
			this.vdValues.Name = "vdValues";
			this.vdValues.Size = new System.Drawing.Size(824, 440);
			this.vdValues.TabIndex = 3;
			// 
			// ManagerDictionary
			// 
			this.AcceptButton = this.bOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(848, 506);
			this.Controls.Add(this.vdValues);
			this.Controls.Add(this.bOk);
			this.Controls.Add(this.menuStrip);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ManagerDictionary";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onFormClosing);
			this.contextMenuStrip.ResumeLayout(false);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.Button bOk;
        private Controls.ValueDictionary vdValues;
    }
}

