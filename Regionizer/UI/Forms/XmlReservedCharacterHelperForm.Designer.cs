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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XmlReservedCharacterHelperForm));
            this.DoneButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.EncodedTextBox = new System.Windows.Forms.TextBox();
            this.EncodedLabel = new System.Windows.Forms.Label();
            this.PatternTextBox = new System.Windows.Forms.TextBox();
            this.PatternLabel = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.FadeTimer = new System.Windows.Forms.Timer(this.components);
            this.HiddenButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DoneButton
            // 
            this.DoneButton.BackColor = System.Drawing.Color.Transparent;
            this.DoneButton.BackgroundImage = global::Regionizer.ProjectResources.BlackButton;
            this.DoneButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DoneButton.FlatAppearance.BorderSize = 0;
            this.DoneButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.DoneButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.DoneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DoneButton.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
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
            this.CopyButton.BackgroundImage = global::Regionizer.ProjectResources.BlackButton;
            this.CopyButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CopyButton.FlatAppearance.BorderSize = 0;
            this.CopyButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.CopyButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.CopyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyButton.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
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
            this.EncodedLabel.ForeColor = System.Drawing.Color.LemonChiffon;
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
            this.PatternLabel.ForeColor = System.Drawing.Color.LemonChiffon;
            this.PatternLabel.Location = new System.Drawing.Point(27, 32);
            this.PatternLabel.Name = "PatternLabel";
            this.PatternLabel.Size = new System.Drawing.Size(100, 23);
            this.PatternLabel.TabIndex = 66;
            this.PatternLabel.Text = "Pattern:";
            this.PatternLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.StatusLabel.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLabel.ForeColor = System.Drawing.Color.LemonChiffon;
            this.StatusLabel.Location = new System.Drawing.Point(506, 135);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(118, 39);
            this.StatusLabel.TabIndex = 72;
            this.StatusLabel.Text = "Copied!";
            this.StatusLabel.Visible = false;
            // 
            // FadeTimer
            // 
            this.FadeTimer.Interval = 2500;
            this.FadeTimer.Tick += new System.EventHandler(this.FadeTimer_Tick);
            // 
            // HiddenButton
            // 
            this.HiddenButton.BackColor = System.Drawing.Color.Transparent;
            this.HiddenButton.BackgroundImage = global::Regionizer.ProjectResources.BlackButton;
            this.HiddenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.HiddenButton.FlatAppearance.BorderSize = 0;
            this.HiddenButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.HiddenButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.HiddenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HiddenButton.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.HiddenButton.ForeColor = System.Drawing.Color.White;
            this.HiddenButton.Location = new System.Drawing.Point(-800, 137);
            this.HiddenButton.MaximumSize = new System.Drawing.Size(220, 42);
            this.HiddenButton.Name = "HiddenButton";
            this.HiddenButton.Size = new System.Drawing.Size(103, 42);
            this.HiddenButton.TabIndex = 73;
            this.HiddenButton.Text = "Hidden";
            this.HiddenButton.UseVisualStyleBackColor = false;
            // 
            // XmlReservedCharacterHelperForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Regionizer.ProjectResources.BlackImage;
            this.ClientSize = new System.Drawing.Size(944, 193);
            this.Controls.Add(this.HiddenButton);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.EncodedTextBox);
            this.Controls.Add(this.EncodedLabel);
            this.Controls.Add(this.PatternTextBox);
            this.Controls.Add(this.PatternLabel);
            this.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "XmlReservedCharacterHelperForm";
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
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Timer FadeTimer;
        private System.Windows.Forms.Button HiddenButton;
    }
}