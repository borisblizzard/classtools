namespace ClassTools.ClassMaker.Forms
{
    partial class Types
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Types));
            this.lbTypes = new System.Windows.Forms.ListBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.newStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.upStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbTypes = new System.Windows.Forms.GroupBox();
            this.tbSuffix2 = new System.Windows.Forms.TextBox();
            this.cbSubType2 = new System.Windows.Forms.ComboBox();
            this.cbTypeCategory = new System.Windows.Forms.ComboBox();
            this.lSubType2 = new System.Windows.Forms.Label();
            this.lCategory = new System.Windows.Forms.Label();
            this.lSubType1 = new System.Windows.Forms.Label();
            this.lSuffix2 = new System.Windows.Forms.Label();
            this.lSuffix1 = new System.Windows.Forms.Label();
            this.tbSuffix1 = new System.Windows.Forms.TextBox();
            this.cbSubType1 = new System.Windows.Forms.ComboBox();
            this.bTypeNew = new System.Windows.Forms.Button();
            this.bTypeDelete = new System.Windows.Forms.Button();
            this.tbTypeName = new System.Windows.Forms.TextBox();
            this.lTypeName = new System.Windows.Forms.Label();
            this.bOk = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.gbTypes.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTypes
            // 
            this.lbTypes.ContextMenuStrip = this.contextMenuStrip;
            this.lbTypes.FormattingEnabled = true;
            this.lbTypes.Location = new System.Drawing.Point(6, 19);
            this.lbTypes.Name = "lbTypes";
            this.lbTypes.Size = new System.Drawing.Size(176, 186);
            this.lbTypes.TabIndex = 0;
            this.lbTypes.SelectedIndexChanged += new System.EventHandler(this.lbTypes_SelectedIndexChanged);
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
            // gbTypes
            // 
            this.gbTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTypes.Controls.Add(this.tbSuffix2);
            this.gbTypes.Controls.Add(this.cbSubType2);
            this.gbTypes.Controls.Add(this.cbTypeCategory);
            this.gbTypes.Controls.Add(this.lSubType2);
            this.gbTypes.Controls.Add(this.lCategory);
            this.gbTypes.Controls.Add(this.lSubType1);
            this.gbTypes.Controls.Add(this.lSuffix2);
            this.gbTypes.Controls.Add(this.lSuffix1);
            this.gbTypes.Controls.Add(this.tbSuffix1);
            this.gbTypes.Controls.Add(this.cbSubType1);
            this.gbTypes.Controls.Add(this.bTypeNew);
            this.gbTypes.Controls.Add(this.bTypeDelete);
            this.gbTypes.Controls.Add(this.tbTypeName);
            this.gbTypes.Controls.Add(this.lTypeName);
            this.gbTypes.Controls.Add(this.lbTypes);
            this.gbTypes.Location = new System.Drawing.Point(12, 27);
            this.gbTypes.Name = "gbTypes";
            this.gbTypes.Size = new System.Drawing.Size(438, 238);
            this.gbTypes.TabIndex = 1;
            this.gbTypes.TabStop = false;
            this.gbTypes.Text = "Types";
            // 
            // tbSuffix2
            // 
            this.tbSuffix2.Location = new System.Drawing.Point(258, 154);
            this.tbSuffix2.Name = "tbSuffix2";
            this.tbSuffix2.Size = new System.Drawing.Size(169, 20);
            this.tbSuffix2.TabIndex = 13;
            this.tbSuffix2.TextChanged += new System.EventHandler(this.tbSuffix2_TextChanged);
            // 
            // cbSubType2
            // 
            this.cbSubType2.FormattingEnabled = true;
            this.cbSubType2.Location = new System.Drawing.Point(258, 126);
            this.cbSubType2.Name = "cbSubType2";
            this.cbSubType2.Size = new System.Drawing.Size(169, 21);
            this.cbSubType2.TabIndex = 12;
            this.cbSubType2.SelectedIndexChanged += new System.EventHandler(this.cbSubType2_SelectedIndexChanged);
            // 
            // cbTypeCategory
            // 
            this.cbTypeCategory.FormattingEnabled = true;
            this.cbTypeCategory.Location = new System.Drawing.Point(258, 46);
            this.cbTypeCategory.Name = "cbTypeCategory";
            this.cbTypeCategory.Size = new System.Drawing.Size(169, 21);
            this.cbTypeCategory.TabIndex = 11;
            this.cbTypeCategory.SelectedIndexChanged += new System.EventHandler(this.cbTypeCategory_SelectedIndexChanged);
            // 
            // lSubType2
            // 
            this.lSubType2.AutoSize = true;
            this.lSubType2.Location = new System.Drawing.Point(188, 129);
            this.lSubType2.Name = "lSubType2";
            this.lSubType2.Size = new System.Drawing.Size(62, 13);
            this.lSubType2.TabIndex = 10;
            this.lSubType2.Text = "Sub Type 2";
            // 
            // lCategory
            // 
            this.lCategory.AutoSize = true;
            this.lCategory.Location = new System.Drawing.Point(188, 49);
            this.lCategory.Name = "lCategory";
            this.lCategory.Size = new System.Drawing.Size(49, 13);
            this.lCategory.TabIndex = 10;
            this.lCategory.Text = "Category";
            // 
            // lSubType1
            // 
            this.lSubType1.AutoSize = true;
            this.lSubType1.Location = new System.Drawing.Point(188, 76);
            this.lSubType1.Name = "lSubType1";
            this.lSubType1.Size = new System.Drawing.Size(62, 13);
            this.lSubType1.TabIndex = 10;
            this.lSubType1.Text = "Sub Type 1";
            // 
            // lSuffix2
            // 
            this.lSuffix2.AutoSize = true;
            this.lSuffix2.Location = new System.Drawing.Point(188, 157);
            this.lSuffix2.Name = "lSuffix2";
            this.lSuffix2.Size = new System.Drawing.Size(42, 13);
            this.lSuffix2.TabIndex = 10;
            this.lSuffix2.Text = "Suffix 2";
            // 
            // lSuffix1
            // 
            this.lSuffix1.AutoSize = true;
            this.lSuffix1.Location = new System.Drawing.Point(188, 103);
            this.lSuffix1.Name = "lSuffix1";
            this.lSuffix1.Size = new System.Drawing.Size(42, 13);
            this.lSuffix1.TabIndex = 10;
            this.lSuffix1.Text = "Suffix 1";
            // 
            // tbSuffix1
            // 
            this.tbSuffix1.Location = new System.Drawing.Point(258, 100);
            this.tbSuffix1.Name = "tbSuffix1";
            this.tbSuffix1.Size = new System.Drawing.Size(169, 20);
            this.tbSuffix1.TabIndex = 9;
            this.tbSuffix1.TextChanged += new System.EventHandler(this.tbSuffix1_TextChanged);
            // 
            // cbSubType1
            // 
            this.cbSubType1.Enabled = false;
            this.cbSubType1.FormattingEnabled = true;
            this.cbSubType1.Location = new System.Drawing.Point(258, 73);
            this.cbSubType1.Name = "cbSubType1";
            this.cbSubType1.Size = new System.Drawing.Size(169, 21);
            this.cbSubType1.TabIndex = 8;
            this.cbSubType1.SelectedIndexChanged += new System.EventHandler(this.cbSubType1_SelectedIndexChanged);
            // 
            // bTypeNew
            // 
            this.bTypeNew.Location = new System.Drawing.Point(107, 208);
            this.bTypeNew.Name = "bTypeNew";
            this.bTypeNew.Size = new System.Drawing.Size(75, 23);
            this.bTypeNew.TabIndex = 5;
            this.bTypeNew.Text = "Add new";
            this.bTypeNew.UseVisualStyleBackColor = true;
            this.bTypeNew.Click += new System.EventHandler(this.bTypeNew_Click);
            // 
            // bTypeDelete
            // 
            this.bTypeDelete.Location = new System.Drawing.Point(6, 208);
            this.bTypeDelete.Name = "bTypeDelete";
            this.bTypeDelete.Size = new System.Drawing.Size(75, 23);
            this.bTypeDelete.TabIndex = 6;
            this.bTypeDelete.Text = "Delete";
            this.bTypeDelete.UseVisualStyleBackColor = true;
            this.bTypeDelete.Click += new System.EventHandler(this.bTypeDelete_Click);
            // 
            // tbTypeName
            // 
            this.tbTypeName.Location = new System.Drawing.Point(258, 20);
            this.tbTypeName.Name = "tbTypeName";
            this.tbTypeName.Size = new System.Drawing.Size(169, 20);
            this.tbTypeName.TabIndex = 2;
            this.tbTypeName.TextChanged += new System.EventHandler(this.tbTypeName_TextChanged);
            // 
            // lTypeName
            // 
            this.lTypeName.AutoSize = true;
            this.lTypeName.Location = new System.Drawing.Point(188, 23);
            this.lTypeName.Name = "lTypeName";
            this.lTypeName.Size = new System.Drawing.Size(35, 13);
            this.lTypeName.TabIndex = 1;
            this.lTypeName.Text = "Name";
            // 
            // bOk
            // 
            this.bOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bOk.Location = new System.Drawing.Point(375, 271);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 2;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(462, 24);
            this.menuStrip.TabIndex = 4;
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator7,
            this.addNewToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator6,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(199, 6);
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.addNewToolStripMenuItem.Text = "Add new";
            this.addNewToolStripMenuItem.Click += new System.EventHandler(this.addNewMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(199, 6);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.moveUpToolStripMenuItem.Text = "Move up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.moveDownToolStripMenuItem.Text = "Move down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownMenuItem_Click);
            // 
            // FormTypes
            // 
            this.AcceptButton = this.bOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bOk;
            this.ClientSize = new System.Drawing.Size(462, 304);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.gbTypes);
            this.Controls.Add(this.bOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTypes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Types";
            this.contextMenuStrip.ResumeLayout(false);
            this.gbTypes.ResumeLayout(false);
            this.gbTypes.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbTypes;
        private System.Windows.Forms.GroupBox gbTypes;
        private System.Windows.Forms.TextBox tbTypeName;
        private System.Windows.Forms.Label lTypeName;
        private System.Windows.Forms.Button bTypeNew;
        private System.Windows.Forms.Button bTypeDelete;
        private System.Windows.Forms.ComboBox cbSubType1;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Label lSuffix1;
        private System.Windows.Forms.TextBox tbSuffix1;
        private System.Windows.Forms.ComboBox cbTypeCategory;
        private System.Windows.Forms.ComboBox cbSubType2;
        private System.Windows.Forms.Label lSubType1;
        private System.Windows.Forms.Label lSubType2;
        private System.Windows.Forms.TextBox tbSuffix2;
        private System.Windows.Forms.Label lSuffix2;
        private System.Windows.Forms.Label lCategory;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem newStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem upStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
    }
}