namespace Regionizer.Controls
{
    partial class RegionizerToolWindowControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegionizerToolWindowControl));
            this.TitleLabel = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.ActiveDocumentLabel = new System.Windows.Forms.Label();
            this.FormatDocumentButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleLabel.Font = new System.Drawing.Font("Corbel", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.ForeColor = System.Drawing.Color.White;
            this.TitleLabel.Location = new System.Drawing.Point(15, 13);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(218, 23);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Regionizer Main Window";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(19, 71);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(321, 380);
            this.treeView1.TabIndex = 1;
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.FileNameLabel.ForeColor = System.Drawing.Color.White;
            this.FileNameLabel.Location = new System.Drawing.Point(19, 52);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(92, 13);
            this.FileNameLabel.TabIndex = 2;
            this.FileNameLabel.Text = "Active Document:";
            // 
            // ActiveDocumentLabel
            // 
            this.ActiveDocumentLabel.AutoSize = true;
            this.ActiveDocumentLabel.BackColor = System.Drawing.Color.Transparent;
            this.ActiveDocumentLabel.ForeColor = System.Drawing.Color.White;
            this.ActiveDocumentLabel.Location = new System.Drawing.Point(108, 52);
            this.ActiveDocumentLabel.Name = "ActiveDocumentLabel";
            this.ActiveDocumentLabel.Size = new System.Drawing.Size(124, 13);
            this.ActiveDocumentLabel.TabIndex = 3;
            this.ActiveDocumentLabel.Text = "[No Document Selected]";
            // 
            // FormatDocumentButton
            // 
            this.FormatDocumentButton.BackColor = System.Drawing.SystemColors.Control;
            this.FormatDocumentButton.Location = new System.Drawing.Point(19, 458);
            this.FormatDocumentButton.Name = "FormatDocumentButton";
            this.FormatDocumentButton.Size = new System.Drawing.Size(110, 23);
            this.FormatDocumentButton.TabIndex = 4;
            this.FormatDocumentButton.Text = "Format Document";
            this.FormatDocumentButton.UseVisualStyleBackColor = false;
            this.FormatDocumentButton.Click += new System.EventHandler(this.FormatDocumentButton_Click);
            // 
            // RegionizerToolWindowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.FormatDocumentButton);
            this.Controls.Add(this.FileNameLabel);
            this.Controls.Add(this.ActiveDocumentLabel);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.TitleLabel);
            this.Name = "RegionizerToolWindowControl";
            this.Size = new System.Drawing.Size(433, 494);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.Label ActiveDocumentLabel;
        private System.Windows.Forms.Button FormatDocumentButton;
    }
}
