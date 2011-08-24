namespace ClassTools.Common.Forms
{
    partial class About
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
            this.lVersion = new System.Windows.Forms.Label();
            this.gbAbout = new System.Windows.Forms.GroupBox();
            this.lCopyright = new System.Windows.Forms.Label();
            this.lAppName = new System.Windows.Forms.Label();
            this.bOk = new System.Windows.Forms.Button();
            this.gbAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // lVersion
            // 
            this.lVersion.AutoSize = true;
            this.lVersion.Location = new System.Drawing.Point(6, 48);
            this.lVersion.Name = "lVersion";
            this.lVersion.Size = new System.Drawing.Size(81, 13);
            this.lVersion.TabIndex = 0;
            this.lVersion.Text = "Version: 1.0.0.0";
            // 
            // gbAbout
            // 
            this.gbAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAbout.Controls.Add(this.lCopyright);
            this.gbAbout.Controls.Add(this.lAppName);
            this.gbAbout.Controls.Add(this.lVersion);
            this.gbAbout.Location = new System.Drawing.Point(12, 12);
            this.gbAbout.Name = "gbAbout";
            this.gbAbout.Size = new System.Drawing.Size(256, 97);
            this.gbAbout.TabIndex = 1;
            this.gbAbout.TabStop = false;
            this.gbAbout.Text = "About";
            // 
            // lCopyright
            // 
            this.lCopyright.AutoSize = true;
            this.lCopyright.Location = new System.Drawing.Point(6, 74);
            this.lCopyright.Name = "lCopyright";
            this.lCopyright.Size = new System.Drawing.Size(120, 13);
            this.lCopyright.TabIndex = 1;
            this.lCopyright.Text = "Copyright: © Boris Mikić";
            // 
            // lAppName
            // 
            this.lAppName.AutoSize = true;
            this.lAppName.Location = new System.Drawing.Point(6, 22);
            this.lAppName.Name = "lAppName";
            this.lAppName.Size = new System.Drawing.Size(70, 13);
            this.lAppName.TabIndex = 0;
            this.lAppName.Text = "Title: Generic";
            // 
            // bOk
            // 
            this.bOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOk.Location = new System.Drawing.Point(193, 115);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 2;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 150);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.gbAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.gbAbout.ResumeLayout(false);
            this.gbAbout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lVersion;
        private System.Windows.Forms.GroupBox gbAbout;
        private System.Windows.Forms.Label lCopyright;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Label lAppName;
    }
}