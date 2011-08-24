namespace ClassTools.DataMaker.Forms.Controls
{
    partial class InstanceCollection
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
            this.gbInstanceVariables = new System.Windows.Forms.GroupBox();
            this.ivcInstanceVariables = new ClassTools.DataMaker.Forms.Controls.InstanceVariables();
            this.instanceVariablesBox1 = new ClassTools.DataMaker.Forms.Controls.InstanceVariables();
            this.gbInstances = new System.Windows.Forms.GroupBox();
            this.bInstanceNew = new System.Windows.Forms.Button();
            this.bInstanceDelete = new System.Windows.Forms.Button();
            this.lbInstances = new System.Windows.Forms.ListBox();
            this.gbInstanceVariables.SuspendLayout();
            this.gbInstances.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbInstanceVariables
            // 
            this.gbInstanceVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInstanceVariables.Controls.Add(this.ivcInstanceVariables);
            this.gbInstanceVariables.Controls.Add(this.instanceVariablesBox1);
            this.gbInstanceVariables.Location = new System.Drawing.Point(199, 0);
            this.gbInstanceVariables.Name = "gbInstanceVariables";
            this.gbInstanceVariables.Size = new System.Drawing.Size(513, 438);
            this.gbInstanceVariables.TabIndex = 6;
            this.gbInstanceVariables.TabStop = false;
            this.gbInstanceVariables.Text = "Instance Variables";
            // 
            // ivcInstanceVariables
            // 
            this.ivcInstanceVariables.Location = new System.Drawing.Point(6, 20);
            this.ivcInstanceVariables.Name = "ivcInstanceVariables";
            this.ivcInstanceVariables.Size = new System.Drawing.Size(500, 400);
            this.ivcInstanceVariables.TabIndex = 1;
            // 
            // instanceVariablesBox1
            // 
            this.instanceVariablesBox1.Location = new System.Drawing.Point(3, 16);
            this.instanceVariablesBox1.Name = "instanceVariablesBox1";
            this.instanceVariablesBox1.Size = new System.Drawing.Size(500, 400);
            this.instanceVariablesBox1.TabIndex = 0;
            // 
            // gbInstances
            // 
            this.gbInstances.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gbInstances.Controls.Add(this.bInstanceNew);
            this.gbInstances.Controls.Add(this.bInstanceDelete);
            this.gbInstances.Controls.Add(this.lbInstances);
            this.gbInstances.Location = new System.Drawing.Point(0, 0);
            this.gbInstances.Name = "gbInstances";
            this.gbInstances.Size = new System.Drawing.Size(193, 438);
            this.gbInstances.TabIndex = 5;
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
            // 
            // lbInstances
            // 
            this.lbInstances.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInstances.FormattingEnabled = true;
            this.lbInstances.Location = new System.Drawing.Point(7, 20);
            this.lbInstances.Name = "lbInstances";
            this.lbInstances.Size = new System.Drawing.Size(180, 381);
            this.lbInstances.TabIndex = 0;
            // 
            // InstanceCollectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbInstanceVariables);
            this.Controls.Add(this.gbInstances);
            this.Name = "InstanceCollectionControl";
            this.Size = new System.Drawing.Size(713, 439);
            this.gbInstanceVariables.ResumeLayout(false);
            this.gbInstances.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbInstanceVariables;
        private InstanceVariables ivcInstanceVariables;
        private InstanceVariables instanceVariablesBox1;
        private System.Windows.Forms.GroupBox gbInstances;
        private System.Windows.Forms.Button bInstanceNew;
        private System.Windows.Forms.Button bInstanceDelete;
        private System.Windows.Forms.ListBox lbInstances;
    }
}
