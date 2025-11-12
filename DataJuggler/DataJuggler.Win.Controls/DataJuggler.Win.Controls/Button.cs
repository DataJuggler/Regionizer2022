

#region using statements

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataJuggler.Win.Controls.Enumerations;

#endregion

namespace DataJuggler.Win.Controls
{

    #region class Button
    /// <summary>
    /// This class creates a button that is easy to style with the Theme property.
    /// </summary>
    public partial class Button : UserControl
    {
        
        #region Private Variables
        private ThemeEnum theme;
        private string buttonText;
        private FlatStyle flatStyle;
        private ContentAlignment textAlign;        
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'Button' object.
        /// </summary>
        public Button()
        {
            // Create Controls
            InitializeComponent();
            
            // Default to Wood theme
            Theme = ThemeEnum.Wood;
            ButtonText = "Click Me";
            BackColor = Color.Transparent;
            InnerButton.BackColor = Color.Transparent;
            FlatStyle = FlatStyle.Flat;
            UseMnemonic = true;
        }
        #endregion
        
        #region Events
            
            #region Button_EnabledChanged(object sender, EventArgs e)
            /// <summary>
            /// event is fired when Button _ Enabled Changed
            /// </summary>
            private void Button_EnabledChanged(object sender, EventArgs e)
            {
                // Setup the button
                SetButtonBackgroundImage();

                // Set the ForeColor
                SetForeColor();
            }
            #endregion
            
            #region Button_Enter(object sender, EventArgs e)
            /// <summary>
            /// event is fired when Button _ Enter
            /// </summary>
            private void Button_Enter(object sender, EventArgs e)
            {
                // Change the cursor to a hand
                Cursor = Cursors.Hand;
            }
            #endregion
            
            #region Button_FontChanged(object sender, EventArgs e)
            /// <summary>
            /// event is fired when Button _ Font Changed
            /// </summary>
            private void Button_FontChanged(object sender, EventArgs e)
            {
                // Set the Font on the Inner Button
                this.InnerButton.Font = this.Font;
            }
            #endregion
            
            #region Button_ForeColorChanged(object sender, EventArgs e)
            /// <summary>
            /// event is fired when Button _ Fore Color Changed
            /// </summary>
            private void Button_ForeColorChanged(object sender, EventArgs e)
            {
                // Set the property on the child
                InnerButton.ForeColor = this.ForeColor;
            }
            #endregion
            
            #region Button_Leave(object sender, EventArgs e)
            /// <summary>
            /// event is fired when Button _ Leave
            /// </summary>
            private void Button_Leave(object sender, EventArgs e)
            {   
                // Change the cursor back to the default pointer
                Cursor = Cursors.Default;
            }
        #endregion

            #region InnerButton_Click(object sender, EventArgs e)
            /// <summary>
            /// event is fired when the 'InnerButton' is clicked.
            /// </summary>
            private void InnerButton_Click(object sender, EventArgs e)
            {
                // Simulate the Click event
                this.OnClick(EventArgs.Empty);
            }
            #endregion
            
        #endregion

        #region Methods

            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Default to dark
                Theme = ThemeEnum.Dark;   
            }
            #endregion
            
            #region SetButtonBackgroundImage()
            /// <summary>
            /// This method Set Button Background Image
            /// </summary>
            public void SetButtonBackgroundImage()
            {
                // if the Dark Theme
                if (this.Theme == ThemeEnum.Dark)
                {
                    // Set to WoodButton
                    InnerButton.BackgroundImage = Properties.Resources.BlackButton;                    
                }
                else if (Theme == ThemeEnum.Glass)
                {
                    InnerButton.BackgroundImage = Properties.Resources.GlassButtonBlack;                    
                }
                else
                { 
                    // Wood Theme

                    // if enabled
                    if (this.Enabled)
                    {
                        // Set to WoodButton
                        InnerButton.BackgroundImage = Properties.Resources.WoodButtonWidth640;
                    }
                    else
                    {
                        // Set to WoodButton Gray
                        InnerButton.BackgroundImage = Properties.Resources.WoodButtonWidth640Disabled;
                    }
                }

                // Ensure Transparent BackColor
                this.InnerButton.BackColor = Color.Transparent;
                this.BackColor = Color.Transparent;
            }
            #endregion
            
            #region SetForeColor()
            /// <summary>
            /// This method Set Fore Color
            /// </summary>
            public void SetForeColor()
            {
                // if Dark
                if ((theme == ThemeEnum.Dark)|| (theme == ThemeEnum.Glass))
                {
                    // if enabled
                    if (this.Enabled)
                    {
                        // Set to light yellow
                        this.ForeColor = Color.LemonChiffon;
                    }
                    else
                    {
                        // Set to Gray
                        this.ForeColor = Color.Gray;
                    }
                }                
                else
                {
                    // if enabled
                    if (this.Enabled)
                    {
                         // Set to black
                        this.ForeColor = Color.Black;
                    }
                    else
                    {
                        // Set to Gray
                        this.ForeColor = Color.Gray;
                    }
                }
            }
            #endregion
            
        #endregion

        #region Properties
    
            #region ButtonText
            /// <summary>
            /// This property gets or sets the value for 'ButtonText'.
            /// </summary>
            public string ButtonText
            {
                get { return buttonText; }
                set 
                { 
                    // Set the value
                    buttonText = value;

                    // Set the value
                    InnerButton.Text = buttonText;
                }
            }
            #endregion
            
            #region FlatStyle
            /// <summary>
            /// This property gets or sets the value for 'FlatStyle'.
            /// </summary>
            public FlatStyle FlatStyle
            {
                get 
                { 
                    // initial value
                    FlatStyle flat = FlatStyle.Flat;

                    // if the InnerButton exists
                    if (InnerButton != null)
                    {
                        // set the return value
                        flat = InnerButton.FlatStyle;
                    }

                    // return value
                    return flat;
                }
                set 
                { 
                    flatStyle = value;

                    // Set the value
                    this.InnerButton.FlatStyle = value;
                }
            }
            #endregion
            
            #region TextAlign
            /// <summary>
            /// This property gets or sets the value for 'TextAlign'.
            /// </summary>
            public ContentAlignment TextAlign
            {
                get { return textAlign; }
                set 
                { 
                    textAlign = value;

                    // Set the value
                    InnerButton.TextAlign = value;
                }
            }
            #endregion
            
            #region Theme
            /// <summary>
            /// This property gets or sets the value for 'Theme'.
            /// </summary>
            public ThemeEnum Theme
            {
                get { return theme; }
                set 
                { 
                    // set the value
                    theme = value;
                    
                    // Set the button
                    SetButtonBackgroundImage();

                    // Set the ForeColor
                    SetForeColor();
                }
            }
        #endregion

            #region UseMnemonic
            /// <summary>
            /// This property gets or sets the value for 'UseMnemonic'.
            /// </summary>
            public bool UseMnemonic
            {
                get 
                { 
                    // Initial value
                    bool useMnemonic = false;

                    // Check if InnerButton is not null
                    if (InnerButton != null) 
                    {
                        useMnemonic = InnerButton.UseMnemonic;
                    }

                    // Return value
                    return useMnemonic;
                }
                set 
                { 
                    // Check if InnerButton is not null
                    if (InnerButton != null) 
                    {
                        InnerButton.UseMnemonic = value;
                    }
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
