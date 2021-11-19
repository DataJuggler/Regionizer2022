

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

namespace DataJuggler.WPF.Controls.TabControls
{

    #region class TabButton
    /// <summary>
    /// Interaction logic for TabButton.xaml
    /// </summary>
    public partial class TabButton : UserControl
    {
        
        #region Private Variables
        private bool selected;
        private int buttonNumber;
        private string buttonText;
        #endregion
        
        #region Constructor
        /// <summary>
        /// This control represents a Tab on the TabHostControl.
        /// </summary>
        public TabButton()
        {
            // Create Controls
            InitializeComponent();
        }
        #endregion

        #region Events

            #region LabelButton_Click(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when the 'LabelButton' is clicked.
            /// </summary>
            public void LabelButton_Click(object sender, EventArgs e)
            {
                // if the ParentTabHost exists
                if (this.HasParentTabHost)
                {
                    // set selected to true
                    this.Selected = true;

                    // a button was selected
                    this.ParentTabHost.TabSelected(this);
                }
            }
            #endregion
            
        #endregion

        #region Methods

            #region DisplayTabImage()
            /// <summary>
            /// This method Display Tab Image
            /// </summary>
            public void DisplayTabImage()
            {
                // create an image brush
                ImageBrush brush = new ImageBrush();

                // if this Tab is currently selected
                if (this.Selected)
                {
                    // Use the blue tab for selected
                    brush.ImageSource = new BitmapImage(new Uri("..Images/BlueTab.png"));
                }
                else
                {
                    // Use the blue tab for selected
                    brush.ImageSource = new BitmapImage(new Uri("..Images/DisabledTab2.png"));
                }

                // set the background
                this.Background = brush;
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
                get 
                {
                    // if the buttonText is not set
                    if (String.IsNullOrEmpty(buttonText))
                    {
                        // set the buttonText
                        buttonText = this.LabelButton.Content.ToString();
                    }

                    // return value
                    return buttonText;
                }
                set
                {
                    // set the value
                    buttonText = value;

                    // set the value
                    this.LabelButton.Content = value;
                }
            }
            #endregion

            #region HasButtonNumber
            /// <summary>
            /// This property returns true if the 'ButtonNumber' is set.
            /// </summary>
            public bool HasButtonNumber
            {
                get
                {
                    // initial value
                    bool hasButtonNumber = (this.ButtonNumber > 0);

                    // return value
                    return hasButtonNumber;
                }
            }
            #endregion

            #region HasButtonText
            /// <summary>
            /// This property returns true if the 'ButtonText' exists.
            /// </summary>
            public bool HasButtonText
            {
                get
                {
                    // initial value
                    bool hasButtonText = (!String.IsNullOrEmpty(this.ButtonText));

                    // return value
                    return hasButtonText;
                }
            }
            #endregion

            #region HasParentTabHost
            /// <summary>
            /// This property returns true if this object has a 'ParentTabHost'.
            /// </summary>
            public bool HasParentTabHost
            {
                get
                {
                    // initial value
                    bool hasParentTabHost = (this.ParentTabHost != null);

                    // return value
                    return hasParentTabHost;
                }
            }
            #endregion

            #region ParentTabHost
            /// <summary>
            /// This read only property returns the value for 'ParentTabHost'.
            /// </summary>
            public TabHostControl ParentTabHost
            {
                get
                {
                    // initial value
                    TabHostControl parenTabHost = this.Parent as TabHostControl;

                    // return value
                    return parenTabHost;
                }
            }
            #endregion

            #region Selected
            /// <summary>
            /// This property gets or sets the value for 'Selected'.
            /// </summary>
            public bool Selected
            {
                get { return selected; }
                set
                {
                    // set the value
                    selected = value;

                    // display the TabImage
                    DisplayTabImage();
                }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
