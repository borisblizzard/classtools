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
			this.gbVariables = new System.Windows.Forms.GroupBox();
			this.vlVariables = new ClassTools.DataMaker.Forms.Controls.VariableList();
			this.cbxExists = new System.Windows.Forms.CheckBox();
			this.bOk = new System.Windows.Forms.Button();
			this.gbVariables.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbVariables
			// 
			this.gbVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbVariables.Controls.Add(this.vlVariables);
			this.gbVariables.Location = new System.Drawing.Point(9, 35);
			this.gbVariables.Name = "gbVariables";
			this.gbVariables.Size = new System.Drawing.Size(414, 430);
			this.gbVariables.TabIndex = 4;
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
			this.vlVariables.Size = new System.Drawing.Size(400, 400);
			this.vlVariables.TabIndex = 1;
			// 
			// cbxExists
			// 
			this.cbxExists.AutoSize = true;
			this.cbxExists.Location = new System.Drawing.Point(12, 12);
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
			this.bOk.Location = new System.Drawing.Point(348, 471);
			this.bOk.Name = "bOk";
			this.bOk.Size = new System.Drawing.Size(75, 23);
			this.bOk.TabIndex = 6;
			this.bOk.Text = "OK";
			this.bOk.UseVisualStyleBackColor = true;
			this.bOk.Click += new System.EventHandler(this.closeMenuItem_Click);
			// 
			// ManagerInstance
			// 
			this.AcceptButton = this.bOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(438, 507);
			this.Controls.Add(this.bOk);
			this.Controls.Add(this.cbxExists);
			this.Controls.Add(this.gbVariables);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ManagerInstance";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.gbVariables.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbVariables;
        private Controls.VariableList vlVariables;
        private System.Windows.Forms.CheckBox cbxExists;
        private System.Windows.Forms.Button bOk;
    }
}

