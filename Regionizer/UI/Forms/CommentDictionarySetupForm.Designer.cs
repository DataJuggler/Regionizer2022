

#region using statements


#endregion

namespace DataJuggler.Regionizer.UI.Forms
{

    #region class CommentDictionarySetupForm
    /// <summary>
    /// This class is used to setup the CommentDictionaryEditor
    /// </summary>
    partial class CommentDictionarySetupForm
    {
        
        #region Private Variables
        private System.ComponentModel.IContainer components = null;
        private Controls.ExtendedElementHost MainHost;
        private Regionizer.Controls.CommentDictionaryEditor DictionaryEditor;
        #endregion
        
        #region Methods
            
            #region Dispose(bool disposing)
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
            #endregion
            
            #region InitializeComponent()
            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommentDictionarySetupForm));
            this.MainHost = new DataJuggler.Regionizer.UI.Controls.ExtendedElementHost();
            this.DictionaryEditor = new DataJuggler.Regionizer.Controls.CommentDictionaryEditor();
            this.SuspendLayout();
            // 
            // MainHost
            // 
            this.MainHost.CloseFormMethod = null;
            this.MainHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainHost.Location = new System.Drawing.Point(0, 0);
            this.MainHost.Name = "MainHost";
            this.MainHost.Size = new System.Drawing.Size(956, 281);
            this.MainHost.TabIndex = 0;
            this.MainHost.UpdateRegistryMethod = null;
            this.MainHost.Child = this.DictionaryEditor;
            // 
            // CommentDictionarySetupForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(956, 281);
            this.Controls.Add(this.MainHost);
            this.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CommentDictionarySetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup Comment Dictionary";
            this.ResumeLayout(false);

            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
