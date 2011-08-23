namespace ClassTools.ClassMaker.Forms
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.gbClasses = new System.Windows.Forms.GroupBox();
            this.bClassDelete = new System.Windows.Forms.Button();
            this.bClassNew = new System.Windows.Forms.Button();
            this.lbClasses = new System.Windows.Forms.ListBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.newStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.upStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.sortStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.gbClass = new System.Windows.Forms.GroupBox();
            this.cbxClassSerialize = new System.Windows.Forms.CheckBox();
            this.tbClassModule = new System.Windows.Forms.TextBox();
            this.cbInheritance = new System.Windows.Forms.CheckBox();
            this.tbClassName = new System.Windows.Forms.TextBox();
            this.cbSuperClass = new System.Windows.Forms.ComboBox();
            this.lModule = new System.Windows.Forms.Label();
            this.lClassName = new System.Windows.Forms.Label();
            this.gbMethods = new System.Windows.Forms.GroupBox();
            this.tbMethodPrefix = new System.Windows.Forms.TextBox();
            this.bMethodImplementation = new System.Windows.Forms.Button();
            this.bMethodParameters = new System.Windows.Forms.Button();
            this.cbMethodAccess = new System.Windows.Forms.ComboBox();
            this.cbMethodType = new System.Windows.Forms.ComboBox();
            this.lMethodAccess = new System.Windows.Forms.Label();
            this.lMethodPrefix = new System.Windows.Forms.Label();
            this.lMethodType = new System.Windows.Forms.Label();
            this.lMethodName = new System.Windows.Forms.Label();
            this.lbMethods = new System.Windows.Forms.ListBox();
            this.tbMethodName = new System.Windows.Forms.TextBox();
            this.bMethodNew = new System.Windows.Forms.Button();
            this.bMethodDelete = new System.Windows.Forms.Button();
            this.gbVariables = new System.Windows.Forms.GroupBox();
            this.cbxVariableSerialize = new System.Windows.Forms.CheckBox();
            this.tbVariablePrefix = new System.Windows.Forms.TextBox();
            this.cbxVariableSetter = new System.Windows.Forms.CheckBox();
            this.cbxVariableGetter = new System.Windows.Forms.CheckBox();
            this.tbVariableDefault = new System.Windows.Forms.TextBox();
            this.cbVariableAccess = new System.Windows.Forms.ComboBox();
            this.cbVariableType = new System.Windows.Forms.ComboBox();
            this.lVariablePrefix = new System.Windows.Forms.Label();
            this.lVariableDefault = new System.Windows.Forms.Label();
            this.lVariableAccess = new System.Windows.Forms.Label();
            this.lVariableType = new System.Windows.Forms.Label();
            this.lVariableName = new System.Windows.Forms.Label();
            this.tbVariableName = new System.Windows.Forms.TextBox();
            this.lbVariables = new System.Windows.Forms.ListBox();
            this.bVariableNew = new System.Windows.Forms.Button();
            this.bVariableDelete = new System.Windows.Forms.Button();
            this.gbClasses.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.gbClass.SuspendLayout();
            this.gbMethods.SuspendLayout();
            this.gbVariables.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbClasses
            // 
            this.gbClasses.Controls.Add(this.bClassDelete);
            this.gbClasses.Controls.Add(this.bClassNew);
            this.gbClasses.Controls.Add(this.lbClasses);
            this.gbClasses.Location = new System.Drawing.Point(12, 38);
            this.gbClasses.Name = "gbClasses";
            this.gbClasses.Size = new System.Drawing.Size(193, 476);
            this.gbClasses.TabIndex = 0;
            this.gbClasses.TabStop = false;
            this.gbClasses.Text = "Classes";
            // 
            // bClassDelete
            // 
            this.bClassDelete.Location = new System.Drawing.Point(6, 445);
            this.bClassDelete.Name = "bClassDelete";
            this.bClassDelete.Size = new System.Drawing.Size(75, 23);
            this.bClassDelete.TabIndex = 1;
            this.bClassDelete.Text = "Delete";
            this.bClassDelete.UseVisualStyleBackColor = true;
            this.bClassDelete.Click += new System.EventHandler(this.bClassDelete_Click);
            // 
            // bClassNew
            // 
            this.bClassNew.Location = new System.Drawing.Point(112, 445);
            this.bClassNew.Name = "bClassNew";
            this.bClassNew.Size = new System.Drawing.Size(75, 23);
            this.bClassNew.TabIndex = 1;
            this.bClassNew.Text = "Add new";
            this.bClassNew.UseVisualStyleBackColor = true;
            this.bClassNew.Click += new System.EventHandler(this.bClassNew_Click);
            // 
            // lbClasses
            // 
            this.lbClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbClasses.ContextMenuStrip = this.contextMenuStrip;
            this.lbClasses.FormattingEnabled = true;
            this.lbClasses.Location = new System.Drawing.Point(6, 19);
            this.lbClasses.Name = "lbClasses";
            this.lbClasses.Size = new System.Drawing.Size(181, 420);
            this.lbClasses.TabIndex = 0;
            this.lbClasses.SelectedIndexChanged += new System.EventHandler(this.lbClasses_SelectedIndexChanged);
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
            this.downStripMenuItem,
            this.toolStripSeparator4,
            this.sortStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(138, 176);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(134, 6);
            // 
            // sortStripMenuItem
            // 
            this.sortStripMenuItem.Name = "sortStripMenuItem";
            this.sortStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.sortStripMenuItem.Text = "Sort";
            this.sortStripMenuItem.Click += new System.EventHandler(this.sortMenuItem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(661, 24);
            this.menuStrip.TabIndex = 1;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(183, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
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
            this.moveDownToolStripMenuItem,
            this.toolStripSeparator5,
            this.sortToolStripMenuItem});
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
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(199, 6);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.sortToolStripMenuItem.Text = "Sort";
            this.sortToolStripMenuItem.Click += new System.EventHandler(this.sortMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageTypesToolStripMenuItem,
            this.validateToolStripMenuItem,
            this.generateToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // manageTypesToolStripMenuItem
            // 
            this.manageTypesToolStripMenuItem.Name = "manageTypesToolStripMenuItem";
            this.manageTypesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.T)));
            this.manageTypesToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.manageTypesToolStripMenuItem.Text = "Manage Types";
            this.manageTypesToolStripMenuItem.Click += new System.EventHandler(this.manageTypesToolStripMenuItem_Click);
            // 
            // validateToolStripMenuItem
            // 
            this.validateToolStripMenuItem.Name = "validateToolStripMenuItem";
            this.validateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.V)));
            this.validateToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.validateToolStripMenuItem.Text = "Validate";
            this.validateToolStripMenuItem.Click += new System.EventHandler(this.validateToolStripMenuItem_Click);
            // 
            // generateToolStripMenuItem
            // 
            this.generateToolStripMenuItem.Name = "generateToolStripMenuItem";
            this.generateToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.G)));
            this.generateToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.generateToolStripMenuItem.Text = "Generate";
            this.generateToolStripMenuItem.Click += new System.EventHandler(this.generateToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // ofd
            // 
            this.ofd.DefaultExt = "cmm";
            this.ofd.Filter = "Class Maker Model files (*.cmm)|*.cmm";
            this.ofd.Title = "Open Class Model";
            // 
            // sfd
            // 
            this.sfd.DefaultExt = "cmm";
            this.sfd.Filter = "Class Maker Model files (*.cmm)|*.cmm";
            this.sfd.Title = "Save Class Model";
            // 
            // gbClass
            // 
            this.gbClass.Controls.Add(this.cbxClassSerialize);
            this.gbClass.Controls.Add(this.tbClassModule);
            this.gbClass.Controls.Add(this.cbInheritance);
            this.gbClass.Controls.Add(this.tbClassName);
            this.gbClass.Controls.Add(this.cbSuperClass);
            this.gbClass.Controls.Add(this.lModule);
            this.gbClass.Controls.Add(this.lClassName);
            this.gbClass.Location = new System.Drawing.Point(211, 38);
            this.gbClass.Name = "gbClass";
            this.gbClass.Size = new System.Drawing.Size(443, 76);
            this.gbClass.TabIndex = 3;
            this.gbClass.TabStop = false;
            this.gbClass.Text = "Class";
            // 
            // cbxClassSerialize
            // 
            this.cbxClassSerialize.AutoSize = true;
            this.cbxClassSerialize.Location = new System.Drawing.Point(347, 23);
            this.cbxClassSerialize.Name = "cbxClassSerialize";
            this.cbxClassSerialize.Size = new System.Drawing.Size(65, 17);
            this.cbxClassSerialize.TabIndex = 6;
            this.cbxClassSerialize.Text = "Serialize";
            this.cbxClassSerialize.UseVisualStyleBackColor = true;
            this.cbxClassSerialize.CheckedChanged += new System.EventHandler(this.cbxClassSerialize_CheckedChanged);
            // 
            // tbClassModule
            // 
            this.tbClassModule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbClassModule.Location = new System.Drawing.Point(76, 46);
            this.tbClassModule.Name = "tbClassModule";
            this.tbClassModule.Size = new System.Drawing.Size(154, 20);
            this.tbClassModule.TabIndex = 2;
            this.tbClassModule.TextChanged += new System.EventHandler(this.tbClassModule_TextChanged);
            // 
            // cbInheritance
            // 
            this.cbInheritance.AutoSize = true;
            this.cbInheritance.Location = new System.Drawing.Point(258, 22);
            this.cbInheritance.Name = "cbInheritance";
            this.cbInheritance.Size = new System.Drawing.Size(79, 17);
            this.cbInheritance.TabIndex = 5;
            this.cbInheritance.Text = "Inheritance";
            this.cbInheritance.UseVisualStyleBackColor = true;
            this.cbInheritance.CheckedChanged += new System.EventHandler(this.cbInheritance_CheckedChanged);
            // 
            // tbClassName
            // 
            this.tbClassName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbClassName.Location = new System.Drawing.Point(76, 20);
            this.tbClassName.Name = "tbClassName";
            this.tbClassName.Size = new System.Drawing.Size(154, 20);
            this.tbClassName.TabIndex = 2;
            this.tbClassName.TextChanged += new System.EventHandler(this.tbClassName_TextChanged);
            // 
            // cbSuperClass
            // 
            this.cbSuperClass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSuperClass.FormattingEnabled = true;
            this.cbSuperClass.Location = new System.Drawing.Point(258, 45);
            this.cbSuperClass.Name = "cbSuperClass";
            this.cbSuperClass.Size = new System.Drawing.Size(169, 21);
            this.cbSuperClass.TabIndex = 4;
            this.cbSuperClass.SelectedIndexChanged += new System.EventHandler(this.cbSuperClass_SelectedIndexChanged);
            this.cbSuperClass.SelectedValueChanged += new System.EventHandler(this.cbSuperClass_SelectedIndexChanged);
            // 
            // lModule
            // 
            this.lModule.AutoSize = true;
            this.lModule.Location = new System.Drawing.Point(6, 49);
            this.lModule.Name = "lModule";
            this.lModule.Size = new System.Drawing.Size(42, 13);
            this.lModule.TabIndex = 1;
            this.lModule.Text = "Module";
            // 
            // lClassName
            // 
            this.lClassName.AutoSize = true;
            this.lClassName.Location = new System.Drawing.Point(6, 23);
            this.lClassName.Name = "lClassName";
            this.lClassName.Size = new System.Drawing.Size(35, 13);
            this.lClassName.TabIndex = 0;
            this.lClassName.Text = "Name";
            // 
            // gbMethods
            // 
            this.gbMethods.Controls.Add(this.tbMethodPrefix);
            this.gbMethods.Controls.Add(this.bMethodImplementation);
            this.gbMethods.Controls.Add(this.bMethodParameters);
            this.gbMethods.Controls.Add(this.cbMethodAccess);
            this.gbMethods.Controls.Add(this.cbMethodType);
            this.gbMethods.Controls.Add(this.lMethodAccess);
            this.gbMethods.Controls.Add(this.lMethodPrefix);
            this.gbMethods.Controls.Add(this.lMethodType);
            this.gbMethods.Controls.Add(this.lMethodName);
            this.gbMethods.Controls.Add(this.lbMethods);
            this.gbMethods.Controls.Add(this.tbMethodName);
            this.gbMethods.Controls.Add(this.bMethodNew);
            this.gbMethods.Controls.Add(this.bMethodDelete);
            this.gbMethods.Location = new System.Drawing.Point(211, 324);
            this.gbMethods.Name = "gbMethods";
            this.gbMethods.Size = new System.Drawing.Size(443, 190);
            this.gbMethods.TabIndex = 2;
            this.gbMethods.TabStop = false;
            this.gbMethods.Text = "Method";
            // 
            // tbMethodPrefix
            // 
            this.tbMethodPrefix.Location = new System.Drawing.Point(258, 99);
            this.tbMethodPrefix.Name = "tbMethodPrefix";
            this.tbMethodPrefix.Size = new System.Drawing.Size(169, 20);
            this.tbMethodPrefix.TabIndex = 9;
            this.tbMethodPrefix.TextChanged += new System.EventHandler(this.tbMethodPrefix_TextChanged);
            // 
            // bMethodImplementation
            // 
            this.bMethodImplementation.Location = new System.Drawing.Point(258, 154);
            this.bMethodImplementation.Name = "bMethodImplementation";
            this.bMethodImplementation.Size = new System.Drawing.Size(107, 23);
            this.bMethodImplementation.TabIndex = 6;
            this.bMethodImplementation.Text = "Implementation";
            this.bMethodImplementation.UseVisualStyleBackColor = true;
            this.bMethodImplementation.Click += new System.EventHandler(this.bMethodImplementation_Click);
            // 
            // bMethodParameters
            // 
            this.bMethodParameters.Location = new System.Drawing.Point(258, 125);
            this.bMethodParameters.Name = "bMethodParameters";
            this.bMethodParameters.Size = new System.Drawing.Size(107, 23);
            this.bMethodParameters.TabIndex = 6;
            this.bMethodParameters.Text = "Parameters";
            this.bMethodParameters.UseVisualStyleBackColor = true;
            this.bMethodParameters.Click += new System.EventHandler(this.bMethodParameters_Click);
            // 
            // cbMethodAccess
            // 
            this.cbMethodAccess.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbMethodAccess.Enabled = false;
            this.cbMethodAccess.FormattingEnabled = true;
            this.cbMethodAccess.Location = new System.Drawing.Point(258, 72);
            this.cbMethodAccess.Name = "cbMethodAccess";
            this.cbMethodAccess.Size = new System.Drawing.Size(169, 21);
            this.cbMethodAccess.TabIndex = 5;
            this.cbMethodAccess.SelectedIndexChanged += new System.EventHandler(this.cbMethodAccess_SelectedIndexChanged);
            // 
            // cbMethodType
            // 
            this.cbMethodType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbMethodType.Enabled = false;
            this.cbMethodType.FormattingEnabled = true;
            this.cbMethodType.Location = new System.Drawing.Point(258, 45);
            this.cbMethodType.Name = "cbMethodType";
            this.cbMethodType.Size = new System.Drawing.Size(169, 21);
            this.cbMethodType.TabIndex = 4;
            this.cbMethodType.SelectedIndexChanged += new System.EventHandler(this.cbMethodType_SelectedIndexChanged);
            this.cbMethodType.SelectedValueChanged += new System.EventHandler(this.cbMethodType_SelectedIndexChanged);
            // 
            // lMethodAccess
            // 
            this.lMethodAccess.AutoSize = true;
            this.lMethodAccess.Enabled = false;
            this.lMethodAccess.Location = new System.Drawing.Point(188, 75);
            this.lMethodAccess.Name = "lMethodAccess";
            this.lMethodAccess.Size = new System.Drawing.Size(42, 13);
            this.lMethodAccess.TabIndex = 3;
            this.lMethodAccess.Text = "Access";
            // 
            // lMethodPrefix
            // 
            this.lMethodPrefix.AutoSize = true;
            this.lMethodPrefix.Enabled = false;
            this.lMethodPrefix.Location = new System.Drawing.Point(189, 102);
            this.lMethodPrefix.Name = "lMethodPrefix";
            this.lMethodPrefix.Size = new System.Drawing.Size(33, 13);
            this.lMethodPrefix.TabIndex = 3;
            this.lMethodPrefix.Text = "Prefix";
            // 
            // lMethodType
            // 
            this.lMethodType.AutoSize = true;
            this.lMethodType.Enabled = false;
            this.lMethodType.Location = new System.Drawing.Point(188, 48);
            this.lMethodType.Name = "lMethodType";
            this.lMethodType.Size = new System.Drawing.Size(31, 13);
            this.lMethodType.TabIndex = 3;
            this.lMethodType.Text = "Type";
            // 
            // lMethodName
            // 
            this.lMethodName.AutoSize = true;
            this.lMethodName.Enabled = false;
            this.lMethodName.Location = new System.Drawing.Point(188, 22);
            this.lMethodName.Name = "lMethodName";
            this.lMethodName.Size = new System.Drawing.Size(35, 13);
            this.lMethodName.TabIndex = 3;
            this.lMethodName.Text = "Name";
            // 
            // lbMethods
            // 
            this.lbMethods.ContextMenuStrip = this.contextMenuStrip;
            this.lbMethods.FormattingEnabled = true;
            this.lbMethods.Location = new System.Drawing.Point(6, 19);
            this.lbMethods.Name = "lbMethods";
            this.lbMethods.Size = new System.Drawing.Size(176, 134);
            this.lbMethods.TabIndex = 0;
            this.lbMethods.SelectedIndexChanged += new System.EventHandler(this.lbMethods_SelectedIndexChanged);
            // 
            // tbMethodName
            // 
            this.tbMethodName.Enabled = false;
            this.tbMethodName.Location = new System.Drawing.Point(258, 19);
            this.tbMethodName.Name = "tbMethodName";
            this.tbMethodName.Size = new System.Drawing.Size(169, 20);
            this.tbMethodName.TabIndex = 2;
            this.tbMethodName.TextChanged += new System.EventHandler(this.tbMethodName_TextChanged);
            // 
            // bMethodNew
            // 
            this.bMethodNew.Location = new System.Drawing.Point(107, 159);
            this.bMethodNew.Name = "bMethodNew";
            this.bMethodNew.Size = new System.Drawing.Size(75, 23);
            this.bMethodNew.TabIndex = 1;
            this.bMethodNew.Text = "Add new";
            this.bMethodNew.UseVisualStyleBackColor = true;
            this.bMethodNew.Click += new System.EventHandler(this.bMethodNew_Click);
            // 
            // bMethodDelete
            // 
            this.bMethodDelete.Enabled = false;
            this.bMethodDelete.Location = new System.Drawing.Point(6, 159);
            this.bMethodDelete.Name = "bMethodDelete";
            this.bMethodDelete.Size = new System.Drawing.Size(75, 23);
            this.bMethodDelete.TabIndex = 1;
            this.bMethodDelete.Text = "Delete";
            this.bMethodDelete.UseVisualStyleBackColor = true;
            this.bMethodDelete.Click += new System.EventHandler(this.bMethodDelete_Click);
            // 
            // gbVariables
            // 
            this.gbVariables.Controls.Add(this.cbxVariableSerialize);
            this.gbVariables.Controls.Add(this.tbVariablePrefix);
            this.gbVariables.Controls.Add(this.cbxVariableSetter);
            this.gbVariables.Controls.Add(this.cbxVariableGetter);
            this.gbVariables.Controls.Add(this.tbVariableDefault);
            this.gbVariables.Controls.Add(this.cbVariableAccess);
            this.gbVariables.Controls.Add(this.cbVariableType);
            this.gbVariables.Controls.Add(this.lVariablePrefix);
            this.gbVariables.Controls.Add(this.lVariableDefault);
            this.gbVariables.Controls.Add(this.lVariableAccess);
            this.gbVariables.Controls.Add(this.lVariableType);
            this.gbVariables.Controls.Add(this.lVariableName);
            this.gbVariables.Controls.Add(this.tbVariableName);
            this.gbVariables.Controls.Add(this.lbVariables);
            this.gbVariables.Controls.Add(this.bVariableNew);
            this.gbVariables.Controls.Add(this.bVariableDelete);
            this.gbVariables.Location = new System.Drawing.Point(211, 133);
            this.gbVariables.Name = "gbVariables";
            this.gbVariables.Size = new System.Drawing.Size(443, 188);
            this.gbVariables.TabIndex = 2;
            this.gbVariables.TabStop = false;
            this.gbVariables.Text = "Variables";
            // 
            // cbxVariableSerialize
            // 
            this.cbxVariableSerialize.AutoSize = true;
            this.cbxVariableSerialize.Location = new System.Drawing.Point(347, 163);
            this.cbxVariableSerialize.Name = "cbxVariableSerialize";
            this.cbxVariableSerialize.Size = new System.Drawing.Size(65, 17);
            this.cbxVariableSerialize.TabIndex = 10;
            this.cbxVariableSerialize.Text = "Serialize";
            this.cbxVariableSerialize.UseVisualStyleBackColor = true;
            this.cbxVariableSerialize.CheckedChanged += new System.EventHandler(this.cbxVariableSerialize_CheckedChanged);
            // 
            // tbVariablePrefix
            // 
            this.tbVariablePrefix.Location = new System.Drawing.Point(258, 128);
            this.tbVariablePrefix.Name = "tbVariablePrefix";
            this.tbVariablePrefix.Size = new System.Drawing.Size(169, 20);
            this.tbVariablePrefix.TabIndex = 9;
            this.tbVariablePrefix.TextChanged += new System.EventHandler(this.tbVariablePrefix_TextChanged);
            // 
            // cbxVariableSetter
            // 
            this.cbxVariableSetter.AutoSize = true;
            this.cbxVariableSetter.Enabled = false;
            this.cbxVariableSetter.Location = new System.Drawing.Point(258, 163);
            this.cbxVariableSetter.Name = "cbxVariableSetter";
            this.cbxVariableSetter.Size = new System.Drawing.Size(54, 17);
            this.cbxVariableSetter.TabIndex = 8;
            this.cbxVariableSetter.Text = "Setter";
            this.cbxVariableSetter.UseVisualStyleBackColor = true;
            this.cbxVariableSetter.CheckedChanged += new System.EventHandler(this.cbxVariableSetter_CheckedChanged);
            // 
            // cbxVariableGetter
            // 
            this.cbxVariableGetter.AutoSize = true;
            this.cbxVariableGetter.Enabled = false;
            this.cbxVariableGetter.Location = new System.Drawing.Point(191, 163);
            this.cbxVariableGetter.Name = "cbxVariableGetter";
            this.cbxVariableGetter.Size = new System.Drawing.Size(55, 17);
            this.cbxVariableGetter.TabIndex = 7;
            this.cbxVariableGetter.Text = "Getter";
            this.cbxVariableGetter.UseVisualStyleBackColor = true;
            this.cbxVariableGetter.CheckedChanged += new System.EventHandler(this.cbxVariableGetter_CheckedChanged);
            // 
            // tbVariableDefault
            // 
            this.tbVariableDefault.Enabled = false;
            this.tbVariableDefault.Location = new System.Drawing.Point(258, 101);
            this.tbVariableDefault.Name = "tbVariableDefault";
            this.tbVariableDefault.Size = new System.Drawing.Size(169, 20);
            this.tbVariableDefault.TabIndex = 6;
            this.tbVariableDefault.TextChanged += new System.EventHandler(this.tbVariableDefault_TextChanged);
            // 
            // cbVariableAccess
            // 
            this.cbVariableAccess.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbVariableAccess.Enabled = false;
            this.cbVariableAccess.FormattingEnabled = true;
            this.cbVariableAccess.Location = new System.Drawing.Point(258, 73);
            this.cbVariableAccess.Name = "cbVariableAccess";
            this.cbVariableAccess.Size = new System.Drawing.Size(169, 21);
            this.cbVariableAccess.TabIndex = 5;
            this.cbVariableAccess.SelectedIndexChanged += new System.EventHandler(this.cbVariableAccess_SelectedIndexChanged);
            // 
            // cbVariableType
            // 
            this.cbVariableType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbVariableType.Enabled = false;
            this.cbVariableType.FormattingEnabled = true;
            this.cbVariableType.Location = new System.Drawing.Point(258, 46);
            this.cbVariableType.Name = "cbVariableType";
            this.cbVariableType.Size = new System.Drawing.Size(169, 21);
            this.cbVariableType.TabIndex = 4;
            this.cbVariableType.SelectedIndexChanged += new System.EventHandler(this.cbVariableType_SelectedIndexChanged);
            this.cbVariableType.SelectedValueChanged += new System.EventHandler(this.cbVariableType_SelectedIndexChanged);
            // 
            // lVariablePrefix
            // 
            this.lVariablePrefix.AutoSize = true;
            this.lVariablePrefix.Enabled = false;
            this.lVariablePrefix.Location = new System.Drawing.Point(188, 131);
            this.lVariablePrefix.Name = "lVariablePrefix";
            this.lVariablePrefix.Size = new System.Drawing.Size(33, 13);
            this.lVariablePrefix.TabIndex = 3;
            this.lVariablePrefix.Text = "Prefix";
            // 
            // lVariableDefault
            // 
            this.lVariableDefault.AutoSize = true;
            this.lVariableDefault.Enabled = false;
            this.lVariableDefault.Location = new System.Drawing.Point(188, 104);
            this.lVariableDefault.Name = "lVariableDefault";
            this.lVariableDefault.Size = new System.Drawing.Size(41, 13);
            this.lVariableDefault.TabIndex = 3;
            this.lVariableDefault.Text = "Default";
            // 
            // lVariableAccess
            // 
            this.lVariableAccess.AutoSize = true;
            this.lVariableAccess.Enabled = false;
            this.lVariableAccess.Location = new System.Drawing.Point(188, 76);
            this.lVariableAccess.Name = "lVariableAccess";
            this.lVariableAccess.Size = new System.Drawing.Size(42, 13);
            this.lVariableAccess.TabIndex = 3;
            this.lVariableAccess.Text = "Access";
            // 
            // lVariableType
            // 
            this.lVariableType.AutoSize = true;
            this.lVariableType.Enabled = false;
            this.lVariableType.Location = new System.Drawing.Point(188, 49);
            this.lVariableType.Name = "lVariableType";
            this.lVariableType.Size = new System.Drawing.Size(31, 13);
            this.lVariableType.TabIndex = 3;
            this.lVariableType.Text = "Type";
            // 
            // lVariableName
            // 
            this.lVariableName.AutoSize = true;
            this.lVariableName.Enabled = false;
            this.lVariableName.Location = new System.Drawing.Point(188, 22);
            this.lVariableName.Name = "lVariableName";
            this.lVariableName.Size = new System.Drawing.Size(35, 13);
            this.lVariableName.TabIndex = 3;
            this.lVariableName.Text = "Name";
            // 
            // tbVariableName
            // 
            this.tbVariableName.Enabled = false;
            this.tbVariableName.Location = new System.Drawing.Point(258, 19);
            this.tbVariableName.Name = "tbVariableName";
            this.tbVariableName.Size = new System.Drawing.Size(169, 20);
            this.tbVariableName.TabIndex = 2;
            this.tbVariableName.TextChanged += new System.EventHandler(this.tbVariableName_TextChanged);
            // 
            // lbVariables
            // 
            this.lbVariables.ContextMenuStrip = this.contextMenuStrip;
            this.lbVariables.FormattingEnabled = true;
            this.lbVariables.Location = new System.Drawing.Point(6, 19);
            this.lbVariables.Name = "lbVariables";
            this.lbVariables.Size = new System.Drawing.Size(176, 134);
            this.lbVariables.TabIndex = 0;
            this.lbVariables.SelectedIndexChanged += new System.EventHandler(this.lbVariables_SelectedIndexChanged);
            // 
            // bVariableNew
            // 
            this.bVariableNew.Location = new System.Drawing.Point(107, 159);
            this.bVariableNew.Name = "bVariableNew";
            this.bVariableNew.Size = new System.Drawing.Size(75, 23);
            this.bVariableNew.TabIndex = 1;
            this.bVariableNew.Text = "Add new";
            this.bVariableNew.UseVisualStyleBackColor = true;
            this.bVariableNew.Click += new System.EventHandler(this.bVariableNew_Click);
            // 
            // bVariableDelete
            // 
            this.bVariableDelete.Enabled = false;
            this.bVariableDelete.Location = new System.Drawing.Point(6, 159);
            this.bVariableDelete.Name = "bVariableDelete";
            this.bVariableDelete.Size = new System.Drawing.Size(75, 23);
            this.bVariableDelete.TabIndex = 1;
            this.bVariableDelete.Text = "Delete";
            this.bVariableDelete.UseVisualStyleBackColor = true;
            this.bVariableDelete.Click += new System.EventHandler(this.bVariableDelete_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 520);
            this.Controls.Add(this.gbClass);
            this.Controls.Add(this.gbVariables);
            this.Controls.Add(this.gbClasses);
            this.Controls.Add(this.gbMethods);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Class Maker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onFormClosing);
            this.gbClasses.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.gbClass.ResumeLayout(false);
            this.gbClass.PerformLayout();
            this.gbMethods.ResumeLayout(false);
            this.gbMethods.PerformLayout();
            this.gbVariables.ResumeLayout(false);
            this.gbVariables.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbClasses;
        private System.Windows.Forms.ListBox lbClasses;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.GroupBox gbClass;
        private System.Windows.Forms.TextBox tbClassModule;
        private System.Windows.Forms.TextBox tbClassName;
        private System.Windows.Forms.Label lModule;
        private System.Windows.Forms.Label lClassName;
        private System.Windows.Forms.GroupBox gbMethods;
        private System.Windows.Forms.ListBox lbMethods;
        private System.Windows.Forms.GroupBox gbVariables;
        private System.Windows.Forms.ListBox lbVariables;
        private System.Windows.Forms.Button bClassDelete;
        private System.Windows.Forms.Button bClassNew;
        private System.Windows.Forms.Button bMethodNew;
        private System.Windows.Forms.Button bMethodDelete;
        private System.Windows.Forms.Button bVariableNew;
        private System.Windows.Forms.Button bVariableDelete;
        private System.Windows.Forms.ToolStripMenuItem validateToolStripMenuItem;
        private System.Windows.Forms.Label lVariableName;
        private System.Windows.Forms.TextBox tbVariableName;
        private System.Windows.Forms.Label lMethodName;
        private System.Windows.Forms.TextBox tbMethodName;
        private System.Windows.Forms.ComboBox cbMethodType;
        private System.Windows.Forms.Label lMethodType;
        private System.Windows.Forms.ComboBox cbVariableType;
        private System.Windows.Forms.Label lVariableType;
        private System.Windows.Forms.ToolStripMenuItem manageTypesToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbSuperClass;
        private System.Windows.Forms.CheckBox cbInheritance;
        private System.Windows.Forms.ToolStripMenuItem generateToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbVariableAccess;
        private System.Windows.Forms.ComboBox cbMethodAccess;
        private System.Windows.Forms.Label lMethodAccess;
        private System.Windows.Forms.Label lVariableAccess;
        private System.Windows.Forms.TextBox tbVariableDefault;
        private System.Windows.Forms.Label lVariableDefault;
        private System.Windows.Forms.CheckBox cbxVariableSetter;
        private System.Windows.Forms.CheckBox cbxVariableGetter;
        private System.Windows.Forms.Button bMethodImplementation;
        private System.Windows.Forms.Button bMethodParameters;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copyStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem newStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem upStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.TextBox tbMethodPrefix;
        private System.Windows.Forms.Label lMethodPrefix;
        private System.Windows.Forms.TextBox tbVariablePrefix;
        private System.Windows.Forms.Label lVariablePrefix;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.CheckBox cbxClassSerialize;
        private System.Windows.Forms.CheckBox cbxVariableSerialize;
    }
}

