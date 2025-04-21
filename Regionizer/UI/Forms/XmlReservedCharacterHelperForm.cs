

#region using statements

using System;
using System.Windows.Forms;

#endregion

namespace DataJuggler.Regionizer.UI.Forms
{

    #region class XmlReservedCharacterHelperForm
    /// <summary>
    /// This simple tool is used to replace out reserved characters in an Xml file.
    /// '<   >   &    %'
    /// </summary>
    public partial class XmlReservedCharacterHelperForm : Form
    {
        
        #region Private Variables
        // Source Values
        private const string GreaterThanSymbol = ">";
        private const string LessThanSymbol = "<";
        private const string AmpersandSymbol = "&";
        private const string PercentSymbol = "%";

        // EncodedValues
        private const string GreaterThanCode = "&gt;";
        private const string LessThanCode = "&lt;";
        private const string AmpersandCode = "&amp;";
        private const string PercentCode = "&#37;";
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of an XmlPatternHelperForm object
        /// </summary>
        public XmlReservedCharacterHelperForm()
        {
            // Create Controls
            InitializeComponent();
        }
        #endregion
        
        #region Events
            
            #region Button_MouseEnter(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when Button _ Mouse Enter
            /// </summary>
            private void Button_MouseEnter(object sender, EventArgs e)
            {
                // Change the cursor to a hand
                this.Cursor = Cursors.Hand;
            }
            #endregion
            
            #region Button_MouseLeave(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when Button _ Mouse Leave
            /// </summary>
            private void Button_MouseLeave(object sender, EventArgs e)
            {
                // Change the cursor back to the default pointer
                this.Cursor = Cursors.Default;
            }
            #endregion
            
            #region CopyButton_Click(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when the 'CopyButton' is clicked.
            /// </summary>
            private void CopyButton_Click(object sender, EventArgs e)
            {
                // Remove focus from this button
                HiddenButton.Focus();

                // Set the text on the clipboard
                Clipboard.SetText(this.EncodedTextBox.Text);

                // Show the user a message
                StatusLabel.Visible = true;

                // Start the Timer
                FadeTimer.Start();

                // Force Manual Refresh
                Refresh();
                Application.DoEvents();
            }
            #endregion
            
            #region DoneButton_Click(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when the 'DoneButton' is clicked.
            /// </summary>
            private void DoneButton_Click(object sender, EventArgs e)
            {
                // close this app
                this.Close();    
            }
            #endregion
            
            #region FadeTimer_Tick(object sender, EventArgs e)
            /// <summary>
            /// event is fired when Fade Timer _ Tick
            /// </summary>
            private void FadeTimer_Tick(object sender, EventArgs e)
            {
                // Hide the label
                FadeTimer.Stop();
                StatusLabel.Visible = false;
            }
            #endregion
            
            #region PatternTextBox_TextChanged(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when Pattern Text Box _ Text Changed
            /// </summary>
            private void PatternTextBox_TextChanged(object sender, EventArgs e)
            {
                // get the sourceText
                string pattern = this.PatternTextBox.Text;

                // replace out each of the values that need to be replaced
                pattern = pattern.Replace(GreaterThanSymbol, GreaterThanCode);
                pattern = pattern.Replace(LessThanSymbol, LessThanCode);
                pattern = pattern.Replace(AmpersandSymbol, AmpersandCode);
                string encoded = pattern.Replace(PercentSymbol, PercentCode);

                // display the value
                this.EncodedTextBox.Text = encoded;
            }
        #endregion

        #endregion
    }
    #endregion

}
