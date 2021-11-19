

#region using statements

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DataJuggler.WPF.Controls;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Navigation;

#endregion

namespace DataJuggler.Regionizer.Controls
{

    #region class ImageButton : UserControl
    /// <summary>
    /// Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageButton : UserControl
    {

        #region Private Variables
        private ButtonClickHandler clickHandler;
        private int buttonNumber;
        private string backgroundImageName;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of an ImageButton object
        /// </summary>
        public ImageButton()
        {
            // Create Controls
            InitializeComponent();
        }
        #endregion

        #region Events

            #region Button_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event is fired when the 'Button' is clicked.
            /// </summary>
            private void Button_Click(object sender, RoutedEventArgs e)
            {
                // If the ClickHandler exists
                if (this.HasClickHandler)
                {
                    // Notify the Clickhandler that the button was clicked
                    this.ClickHandler(this.ButtonNumber, this.Text);
                }
            }
            #endregion
            
            #region Button_MouseEnter(object sender, MouseEventArgs e)
            /// <summary>
            /// This event is fired when Button _ Mouse Enter
            /// </summary>
            private void Button_MouseEnter(object sender, MouseEventArgs e)
            {
                // restore the cursor
                this.Cursor = Cursors.Hand;
            }
            #endregion

            #region Button_MouseLeave(object sender, MouseEventArgs e)
            /// <summary>
            /// This event is fired when Button _ Mouse Leave
            /// </summary>
            private void Button_MouseLeave(object sender, MouseEventArgs e)
            {
                // restore the cursor
                this.Cursor = Cursors.Arrow;
            }
            #endregion

        #endregion

        #region Methods

            #region SetImageUrl(string imageName)
            /// <summary>
            /// This method sets the ImageUrl that is displayed by this button.
            /// The typical format for this is:
            /// [Component Name];component/[Path to image]
            /// /DataJuggler.Regionizer.Controls;component/Images/DarkBlueButton.png
            /// </summary>
            /// <param name="imageUrl">The image must be in your project that gets built with your component or an external url</param>
            public void SetImageUrl(string imageName)
            {
                // set the buttoNBackground
                ImageBrush buttonBackground = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/DataJuggler.Regionizer.Controls;component/Images" + imageName)));

                // set the background image
                this.Background = buttonBackground;
            }
            #endregion

        #endregion

        #region Properties

            #region BackgroundImageName
            /// <summary>
            /// This property gets or sets the value for 'BackgroundImageName'.
            /// </summary>
            public string BackgroundImageName
            {
                get { return backgroundImageName; }
                set 
                {
                    // set the value
                    backgroundImageName = value;

                    // Set the BackgroundImagename
                    this.SetImageUrl(backgroundImageName);
                }
            }
            #endregion
            
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
            
            #region Text
            /// <summary>
            /// This property gets or sets the value for 'Text'.
            /// </summary>
            public string Text
            {
                get
                {
                    // initial value
                    string text = "";

                    // if the control exists
                    if (this.Label != null)
                    {
                        // set the return value
                        text = (string)this.Label.Content;
                    }

                    // return value
                    return text;
                }
                set
                {
                    // if the control exists
                    if (this.Label != null)
                    {
                        // set the value
                        this.Label.Content = value;
                    }
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
