namespace ClassTools.DataMaker.Forms
{
	partial class ManagerClasses
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerClasses));
			this.gbClasses = new System.Windows.Forms.GroupBox();
			this.bOk = new System.Windows.Forms.Button();
			this.clbClasses = new System.Windows.Forms.CheckedListBox();
			this.gbClasses.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbClasses
			// 
			this.gbClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gbClasses.Controls.Add(this.clbClasses);
			this.gbClasses.Location = new System.Drawing.Point(13, 13);
			this.gbClasses.Name = "gbClasses";
			this.gbClasses.Size = new System.Drawing.Size(335, 634);
			this.gbClasses.TabIndex = 0;
			this.gbClasses.TabStop = false;
			this.gbClasses.Text = "Visible Classes";
			// 
			// bOk
			// 
			this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.bOk.Location = new System.Drawing.Point(273, 653);
			this.bOk.Name = "bOk";
			this.bOk.Size = new System.Drawing.Size(75, 23);
			this.bOk.TabIndex = 1;
			this.bOk.Text = "OK";
			this.bOk.UseVisualStyleBackColor = true;
			// 
			// clbClasses
			// 
			this.clbClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.clbClasses.FormattingEnabled = true;
			this.clbClasses.Location = new System.Drawing.Point(7, 19);
			this.clbClasses.Name = "clbClasses";
			this.clbClasses.Size = new System.Drawing.Size(322, 604);
			this.clbClasses.TabIndex = 3;
			// 
			// ManagerClasses
			// 
			this.AcceptButton = this.bOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(360, 688);
			this.Controls.Add(this.bOk);
			this.Controls.Add(this.gbClasses);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ManagerClasses";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.gbClasses.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox gbClasses;
		private System.Windows.Forms.Button bOk;
		private System.Windows.Forms.CheckedListBox clbClasses;
	}
}