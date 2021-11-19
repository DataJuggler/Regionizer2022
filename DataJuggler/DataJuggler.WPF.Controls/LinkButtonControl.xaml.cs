

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

#endregion

namespace DataJuggler.WPF.Controls
{

    #region class LinkButtonControl
    /// <summary>
    /// Interaction logic for LinkButtonControl.xaml
    /// </summary>
    public partial class LinkButtonControl : UserControl
    {
        
        #region Private Variables
        private ButtonClickHandler clickHandler;
        private int buttonNumber;
        private string buttonText;
        private Brush textColor;
        private HorizontalAlignment textAlign;
        private FontFamily fontFamily;
        private FontWeight fontWeight;
        private double fontSize;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a LinkButtonControl
        /// </summary>
        public LinkButtonControl()
        {
            // Create controls
            InitializeComponent();
        }
        #endregion

        #region Events
            
            #region LinkButton_Click(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when the 'LinkButton' is clicked.
            /// </summary>
            public void LinkButton_Click(object sender, EventArgs e)
            {
                // if the Clickhandler exists
                if (this.HasClickHandler)
                {
                    // call the delegate
                    this.ClickHandler(this.ButtonNumber, this.ButtonText);
                }
            }
            #endregion
            
        #endregion

        #region Properties

            #region ButtonNumber
            /// <summary>
            /// This property gets or sets the value for 'ButtonNumber'.
            /// </summary>
            public int ButtonNumber
            {
                get { return buttonNumber; }
                set { buttonNumber = value; }
            }
            #endregion
            
            #region ButtonText
            /// <summary>
            /// This property gets or sets the value for 'ButtonText'.
            /// </summary>
            public string ButtonText
            {
                get { return buttonText; }
                set 
                { 
                    // set the value
                    buttonText = value;

                    // if the LinkButton exists (should always be true)
                    if (this.LinkButton != null)
                    {
                        // display the text
                        this.LinkButton.Content = value;
                    }
                }
            }
            #endregion
            
            #region ClickHandler
            /// <summary>
            /// This property gets or sets the value for 'ClickHandler'.
            /// </summary>
            public ButtonClickHandler ClickHandler
            {
                get { return clickHandler; }
                set { clickHandler = value; }
            }
            #endregion

            #region FontFamily
            /// <summary>
            /// This property gets or sets the value for 'FontFamily'.
            /// </summary>
            public new FontFamily FontFamily
            {
                get { return fontFamily; }
                set
                {
                    // set the value
                    fontFamily = value;

                    // if the Control exists
                    if (this.LinkButton != null)
                    {
                        // set the value in the control
                        this.LinkButton.FontFamily = value;
                    }
                }
            }
            #endregion

            #region FontSize
            /// <summary>
            /// This property gets or sets the value for 'FontSize'.
            /// </summary>
            public new double FontSize
            {
                get { return fontSize; }
                set
                {
                    // set the value
                    fontSize = value;

                    // if the  exists
                    if (this.LinkButton != null)
                    {
                        // set the fontsize on the 
                        this.LinkButton.FontSize = value;
                    }
                }
            }
            #endregion

            #region FontWeight
            /// <summary>
            /// This property gets or sets the value for 'FontWeight'.
            /// </summary>
            public new FontWeight FontWeight
            {
                get { return fontWeight; }
                set
                {
                    // set the value
                    fontWeight = value;

                    // if the Control exists
                    if (this.LinkButton != null)
                    {
                        // set the value on the control
                        this.LinkButton.FontWeight = value;
                    }
                }
            }
            #endregion
            
            #region HasClickHandler
            /// <summary>
            /// This property returns true if this object has a 'ClickHandler'.
            /// </summary>
            public bool HasClickHandler
            {
                get
                {
                    // initial value
                    bool hasClickHandler = (this.ClickHandler != null);
                    
                    // return value
                    return hasClickHandler;
                }
            }
            #endregion
            
            #region TextAlign
            /// <summary>
            /// This property gets or sets the value for 'TextAlign'.
            /// </summary>
            public HorizontalAlignment TextAlign
            {
                get { return textAlign; }
                set 
                { 
                    // text align
                    textAlign = value;

                    // if the LinkButton exists
                    if (this.LinkButton != null)
                    {
                        // set the value
                        this.LinkButton.HorizontalContentAlignment = value;
                        this.LinkButton.HorizontalAlignment = value;
                    }
                }
            }
            #endregion
            
            #region TextColor
            /// <summary>
            /// This property gets or sets the value for 'TextColor'.
            /// </summary>
            public Brush TextColor
            {
                get { return textColor; }
                set 
                { 
                    // set the value
                    textColor = value;

                    // if the LinkButton exists
                    if (this.LinkButton != null)
                    {
                        // set the value in the control 
                        this.LinkButton.Foreground = value;
                    }
                }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}

