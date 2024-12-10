namespace DataJuggler.Regionizer.UI.Forms
{
    partial class XmlReservedCharacterHelperForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XmlReservedCharacterHelperForm));
            this.DoneButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.EncodedTextBox = new System.Windows.Forms.TextBox();
            this.EncodedLabel = new System.Windows.Forms.Label();
            this.PatternTextBox = new System.Windows.Forms.TextBox();
            this.PatternLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DoneButton
            // 
            this.DoneButton.BackColor = System.Drawing.Color.Transparent;
            this.DoneButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("DoneButton.BackgroundImage")));
            this.DoneButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DoneButton.FlatAppearance.BorderSize = 0;
            this.DoneButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.DoneButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.DoneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DoneButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DoneButton.ForeColor = System.Drawing.Color.White;
            this.DoneButton.Location = new System.Drawing.Point(814, 133);
            this.DoneButton.MaximumSize = new System.Drawing.Size(220, 42);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(103, 42);
            this.DoneButton.TabIndex = 71;
            this.DoneButton.Text = "Done";
            this.DoneButton.UseVisualStyleBackColor = false;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            this.DoneButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.DoneButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // CopyButton
            // 
            this.CopyButton.BackColor = System.Drawing.Color.Transparent;
            this.CopyButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CopyButton.BackgroundImage")));
            this.CopyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CopyButton.FlatAppearance.BorderSize = 0;
            this.CopyButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.CopyButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CopyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyButton.ForeColor = System.Drawing.Color.White;
            this.CopyButton.Location = new System.Drawing.Point(693, 133);
            this.CopyButton.MaximumSize = new System.Drawing.Size(220, 42);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(103, 42);
            this.CopyButton.TabIndex = 70;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = false;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            this.CopyButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.CopyButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // EncodedTextBox
            // 
            this.EncodedTextBox.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EncodedTextBox.Location = new System.Drawing.Point(126, 75);
            this.EncodedTextBox.Name = "EncodedTextBox";
            this.EncodedTextBox.Size = new System.Drawing.Size(789, 31);
            this.EncodedTextBox.TabIndex = 69;
            // 
            // EncodedLabel
            // 
            this.EncodedLabel.BackColor = System.Drawing.Color.Transparent;
            this.EncodedLabel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EncodedLabel.Location = new System.Drawing.Point(27, 82);
            this.EncodedLabel.Name = "EncodedLabel";
            this.EncodedLabel.Size = new System.Drawing.Size(100, 23);
            this.EncodedLabel.TabIndex = 68;
            this.EncodedLabel.Text = "Encoded:";
            this.EncodedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PatternTextBox
            // 
            this.PatternTextBox.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PatternTextBox.Location = new System.Drawing.Point(126, 25);
            this.PatternTextBox.Name = "PatternTextBox";
            this.PatternTextBox.Size = new System.Drawing.Size(789, 31);
            this.PatternTextBox.TabIndex = 67;
            this.PatternTextBox.TextChanged += new System.EventHandler(this.PatternTextBox_TextChanged);
            // 
            // PatternLabel
            // 
            this.PatternLabel.BackColor = System.Drawing.Color.Transparent;
            this.PatternLabel.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PatternLabel.Location = new System.Drawing.Point(27, 32);
            this.PatternLabel.Name = "PatternLabel";
            this.PatternLabel.Size = new System.Drawing.Size(100, 23);
            this.PatternLabel.TabIndex = 66;
            this.PatternLabel.Text = "Pattern:";
            this.PatternLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // XmlReservedWordHelperForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(944, 193);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.EncodedTextBox);
            this.Controls.Add(this.EncodedLabel);
            this.Controls.Add(this.PatternTextBox);
            this.Controls.Add(this.PatternLabel);
            this.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XmlReservedWordHelperForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xml Reserved Word Helper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.TextBox EncodedTextBox;
        private System.Windows.Forms.Label EncodedLabel;
        private System.Windows.Forms.TextBox PatternTextBox;
        private System.Windows.Forms.Label PatternLabel;
    }
}