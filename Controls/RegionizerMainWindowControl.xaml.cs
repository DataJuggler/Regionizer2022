

#region using statements

using DataJuggler.Core.UltimateHelper;
using DataJuggler.Regionizer.CodeModel.Objects;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using XmlMirror.Runtime.Util;

#endregion

namespace DataJuggler.Regionizer.Controls
{

    #region delegate HostEventHandlerCallBack
    /// <summary>
    /// Create a delegate that can be called be set by the client
    /// and called here when events happen.
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="arg1"></param>
    public delegate void HostEventHandlerCallBack(string eventName, object args);
    #endregion

    #region class RegionizerMainWindowControl
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class RegionizerMainWindowControl : UserControl
    {
        
        #region Private Variables
        private HostEventHandlerCallBack hostEventHandler;
        private bool autoComment;
        private const string RegionizerYouTubePlaylist = "https://www.youtube.com/watch?v=dtHtVAT_xW0&list=PLKrW5tXCPiX3exbvi16148c6K-57r3dUF";
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a RegionizerMainWindowControl object.
        /// </summary>
        public RegionizerMainWindowControl()
        {
            // Create Conrols
            InitializeComponent();
            
            // Perform Initializations for this object
            Init();
        }
        #endregion
        
        #region Events
            
            #region AddButton_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event adds an item to the solution
            /// </summary>
            private void AddButton_Click(object sender, RoutedEventArgs e)
            {
                // get the codeType
                string codeType = this.CodeTypeComboBox.SelectedItem.ToString();
                
                // local
                string lineText = "";
                
                // set the return Type for the item being inserted
                string returnType = this.ReturnTypeTextBox.Text;
                
                // if Private Variable is selected
                if (codeType == "Private Variable")
                {   
                    // set the lineText
                    lineText = "private " + returnType + " " + this.NameTextBox.Text + ";";
                    
                    // if the delegate is set
                    if (this.HasHostEventHandler)
                    {   
                        // Format the Active Document
                        this.HostEventHandler("InsertPrivateVariable", lineText);
                    }
                }
                else if (codeType == "Method")
                {
                    // if a Method
                    string methodName = this.NameTextBox.Text;
                    
                    IList<string> args = new List<string>();
                    args.Add(methodName);
                    args.Add(returnType);
                    
                    // if the delegate is set
                    if (this.HasHostEventHandler)
                    {   
                        // notify the host
                        this.HostEventHandler("InsertMethod", args);
                    }
                }
                else if (codeType == "Event")
                {
                    // if an Event
                    string eventName = this.NameTextBox.Text;
                    
                    // create the args
                    IList<string> args = new List<string>();
                    args.Add(eventName);
                    args.Add(returnType);
                    
                    // if the delegate is set
                    if (this.HasHostEventHandler)
                    {   
                        // notify the host
                        this.HostEventHandler("InsertEvent", args);
                    }
                }
            }
            #endregion
            
            #region Button_MouseLeave(object sender, MouseEventArgs e)
            /// <summary>
            /// event is fired when You Tube Button _ Mouse Leave
            /// </summary>
            private void Button_MouseLeave(object sender, MouseEventArgs e)
            {
                // Change the cursor back to default
                Cursor = null;
            }
            #endregion
            
            #region Button_OnMouseEnter(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when You Tube Button _ On Mouse Enter
            /// </summary>
            public void Button_OnMouseEnter(object sender, EventArgs e)
            {
                // Change the cursor to a Hand pointer
                Cursor = Cursors.Hand;
            }
            #endregion

            #region ButtonClicked(string eventName, string target)
            /// <summary>
            /// This event is called by child controls that a button was clicked.
            /// </summary>
            internal void ButtonClicked(string eventName, string target)
            {  
                // if the HostEventHandler exists
                if (this.HasHostEventHandler)
                {
                    // Call the HostEventHandler 
                    this.HostEventHandler(eventName, target);
                }
            }
            #endregion
            
            #region CodeTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            /// <summary>
            /// A selection was made
            /// </summary>
            private void CodeTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                // set the text
                string text = this.CodeTypeComboBox.SelectedItem.ToString();
                
                // if this is an Event
                if (text == "Event")
                {
                    // set to void
                    this.ReturnTypeTextBox.Text = "void";
                }
                
                // Set the text
                this.AddButton.Content = "Add " + text;
            }
            #endregion
            
            #region CodeWatchTimer_Elapsed(object sender, ElapsedEventArgs e)
            /// <summary>
            /// This event is fired when Code Watch Timer _ Elapsed
            /// </summary>
            private void CodeWatchTimer_Elapsed(object sender, ElapsedEventArgs e)
            {
                
            }
            #endregion
            
            #region CollapseAllRegionsButton_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event is not implemented at this tiem.
            /// </summary>
            private void CollapseAllRegionsButton_Click(object sender, RoutedEventArgs e)
            {
                // if the delegate is set
                if (this.HasHostEventHandler)
                {
                    // Format the Active Document
                    this.HostEventHandler("CollapseAllRegions", null);
                }
            }
            #endregion
            
            #region CreatePropertie(s)_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event creates the properties from the Selected Text
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void CreateProperties_Click(object sender, RoutedEventArgs e)
            {
                // if the delegate is set
                if (this.HasHostEventHandler)
                {   
                    // Format the Active Document
                    this.HostEventHandler("CreatePropertiesFromSelection", null);
                }
            }
            #endregion
            
            #region DataJugglerButton_Click(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when the 'DataJugglerButton' is clicked.
            /// </summary>
            public void DataJugglerButton_Click(object sender, EventArgs e)
            {
                // Send the user to Data Juggler home page
                System.Diagnostics.Process.Start("http://www.datajuggler.com");
            }
            #endregion
            
            #region DataJugglerButton_MouseEnter(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when Data Juggler Button _ Mouse Enter
            /// </summary>
            public void DataJugglerButton_MouseEnter(object sender, EventArgs e)
            {
                // restore the cursor
                this.Cursor = System.Windows.Input.Cursors.Hand;
            }
            #endregion
            
            #region DataJugglerButton_MouseLeave(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when Data Juggler Button _ Mouse Leave
            /// </summary>
            public void DataJugglerButton_MouseLeave(object sender, EventArgs e)
            {
                // restore the cursor
                this.Cursor = System.Windows.Input.Cursors.Arrow;
            }
            #endregion
            
            #region ExpandAllRegionsButton_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event is not implemented at this time.
            /// </summary>
            private void ExpandAllRegionsButton_Click(object sender, RoutedEventArgs e)
            {
                //// if the delegate is set
                //if (this.HasHostEventHandler)
                //{
                //    // Format the Active Document
                //    this.HostEventHandler("ExpandAllRegions", null);
                //}
            }
            #endregion
            
            #region FormatDocumentButton_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// The FormatDocument button was clicked.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void FormatDocumentButton_Click(object sender, RoutedEventArgs e)
            {
                // if the delegate is set
                if (this.HasHostEventHandler)
                {  
                    // Format the Active Document
                    this.HostEventHandler("Format Document", null);
                }
            }
            #endregion
            
            #region FormatSelectionButton_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event format the selected text and iunserts it into the proper region
            /// </summary>
            private void FormatSelectionButton_Click(object sender, RoutedEventArgs e)
            {
                // if the delegate is set
                if (this.HasHostEventHandler)
                {  
                    // Format the Active Document
                    this.HostEventHandler("Format Selection", null);
                }
            }
            #endregion
            
            #region HandleKeyPress(object sender, KeyEventArgs e)
            /// <summary>
            /// This event is fired when Handle Key Press
            /// </summary>
            private void HandleKeyPress(object sender, KeyEventArgs e)
            {
                // local
                bool updateComments = false;

                // if auto commentText is true
                if (this.AutoComment)
                {  
                    // if the Shift Up Arrow is pressed
                    if ((e.KeyboardDevice.IsKeyDown(Key.LeftShift)) && (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl)))
                    {
                        // set updateComments to true
                        updateComments = true;
                        
                        // handled is true
                        e.Handled = true;
                    }

                    // if the HostEventHandler exists
                    if ((updateComments) && (this.HasHostEventHandler))
                    {
                        // auto commentText the line below the cursor
                        this.hostEventHandler("AutoComment", null);

                        // e has been handled
                        e.Handled = true;
                    }    
                }
            }
            #endregion
            
            #region HasPropertyCreator_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event creates a HasProperty for the selected property
            /// </summary>
            private void HasPropertyCreator_Click(object sender, RoutedEventArgs e)
            {
                // if the delegate is set
                if (this.HasHostEventHandler)
                {
                    // Format the Active Document
                    this.HostEventHandler("CreateHasPropertyFromSelection", null);
                }
            }
            #endregion
            
            #region HelpLink_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event launches the web browser to view the User's Guide
            /// </summary>
            private void HelpLink_Click(object sender, RoutedEventArgs e)
            {
                // set the web url
                string webUsersGuideUrl = @"http://regionizer.codeplex.com/documentation";

                // launch the web browser
                System.Diagnostics.Process.Start(webUsersGuideUrl);
            }
            #endregion
            
            #region OnCheckChangedHandler(object sender, bool isChecked)
            /// <summary>
            /// This event is used to handle the On Check Changed Handler
            /// </summary>
            public void OnCheckChangedHandler(object sender, bool isChecked)
            {
                // turn AutoComment on or off
                this.AutoComment = isChecked;

                // if the AutoComment is true
                if (this.AutoComment)
                {
                    // Setup the AutoComment();
                    this.SetupAutoComment();
                }
            }
            #endregion
            
            #region UserControl_Loaded(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event [enter description here].
            /// </summary>
            private void UserControl_Loaded(object sender, RoutedEventArgs e)
            {
                try
                {
                    // Create the interop host control.
                    System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();
                    host.Height = 140;
                    host.Width = 160;
                    
                    host.Background = Brushes.White;
                    
                    // add this item
                    this.MainGrid.Children.Add(host);
                    Grid.SetRow(host, 5);                    
                }
                catch (Exception error)
                {
                    string err = error.ToString();
                }
            }
            #endregion
            
            #region YouTubeButton_Click(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when the 'YouTubeButton' is clicked.
            /// </summary>
            public void YouTubeButton_Click(object sender, EventArgs e)
            {
                // Send the user to the play list for Regionzer
                System.Diagnostics.Process.Start(RegionizerYouTubePlaylist);
            }
            #endregion
            
        #endregion
        
        #region Methods

            #region ClickHandler(int buttonNumber, string buttonText)
            /// <summary>
            /// This method handles the click events for the Edit Comment Dictionary button
            /// </summary>
            /// <param name="buttonNumber"></param>
            /// <param name="buttonText"></param>
            public void ClickHandler(int buttonNumber, string buttonText)
            {
                
                // determine which button was clicked
                switch (buttonNumber)
                {
                    case 1:

                        // Edit Comment Dictionary
                        EditCommentDictionary();
                       
                        // required
                        break;

                    case 2:

                        // Edit the custom dictionary
                        EditCustomDictionary();

                        // required
                        break;

                    case 3:

                        // Launch the Xml Reserved Word Helper Form
                        LaunchXmlReservedCharacterHelper();

                        // required
                        break;

                    case 4:

                        // Setup or Update the Comment Dictionary
                        SetupCommentDictionary();

                        // required
                        break;

                    case 5:

                        // Handle the Button click event for data juggler.com
                        DataJugglerButton_Click(this, null);

                        // required
                        break;
                }
            }
            #endregion

            #region EditCommentDictionary()
            /// <summary>
            /// This method Edits Comment Dictionary
            /// </summary>
            private void EditCommentDictionary()
            {
                // local
                string dictionaryPath = "";

                try
                {
                    // load the dictionaryInfo object from the Registry
                    DictionaryInfo dictionaryInfo = null; // RegistryHelper.GetDictionaryInfo();

                    // if the dictionaryInfo object exists
                    if (dictionaryInfo != null)
                    {
                        // set the dictionaryPath
                        dictionaryPath = dictionaryInfo.DictionaryPath;

                        // load the CommentDictionairy
                        if (this.HasHostEventHandler)
                        {
                            // auto commentText the line below the cursor
                            this.hostEventHandler("EditCommentDictionary", dictionaryPath);
                        }  
                    }
                    else
                    {
                        // Show the user a message they must set your dictionary path
                        MessageBoxHelper.ShowMessage("Before you can edit the comment dictoinary, you must setup the comment dictionary." + Environment.NewLine + "Click the 'Setup Comment Dictionary button.", "Setup Required");
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
            }
            #endregion

            #region EditCustomDictionary()
            /// <summary>
            /// This method Edit Comment Dictionary
            /// </summary>
            private void EditCustomDictionary()
            {
                // local
                string dictionaryPath = "";

                try
                {
                    // load the dictionaryInfo object from the Registry
                    DictionaryInfo dictionaryInfo = null; // RegistryHelper.GetDictionaryInfo();

                    // if the dictionaryInfo object exists
                    if (dictionaryInfo != null)
                    {
                        // set the dictionaryPath
                        dictionaryPath = dictionaryInfo.CustomDictionaryPath;

                        // load the CommentDictionairy
                        if (this.HasHostEventHandler)
                        {
                            // auto commentText the line below the cursor
                            this.hostEventHandler("EditCommentDictionary", dictionaryPath);
                        }
                    }
                    else
                    {
                        // Show the user a message they must set your dictionary path
                        MessageBoxHelper.ShowMessage("Before you can edit the custom dictoinary, you must setup the comment dictionary." + Environment.NewLine + "Click the 'Setup Comment Dictionary button.", "Setup Required");
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
            }
            #endregion
            
            #region GetReturnTypeText()
            /// <summary>
            /// This method returns the ReturnType from the ReturnTypeTextBox
            /// </summary>
            public string GetReturnTypeText()
            {
                // return the text
                return this.ReturnTypeTextBox.Text;
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            private void Init()
            {
                // Create the codeTypes
                IList<string> codeTypes = new List<string>();
                
                // Add the choices
                codeTypes.Add("Event");
                codeTypes.Add("Method");
                codeTypes.Add("Private Variable");
                
                // add each codeType
                foreach (string codeType in codeTypes)
                {
                    // add this item
                    this.CodeTypeComboBox.Items.Add(codeType);
                }

                // Set the CheckChangedHandler
                this.AutoCommentCheckBox.CheckChangedHandler = OnCheckChangedHandler;

                // Setup the LinkButton ClickHandlers
                this.EditCommentDictionaryButton.ClickHandler = this.ClickHandler;
                this.EditCustomDictionaryButton.ClickHandler = this.ClickHandler;
                this.XmlReservedCharacterHelperButton.ClickHandler = this.ClickHandler;
                this.SetupCommentDictionaryButton.ClickHandler = this.ClickHandler;
            }
            #endregion
            
            #region LaunchXmlReservedCharacterHelper()
            /// <summary>
            /// This method Launch Xml Reserved Word Helper
            /// </summary>
            private void LaunchXmlReservedCharacterHelper()
            {
                // load the CommentDictionairy
                if (this.HasHostEventHandler)
                {
                    // auto commentText the line below the cursor
                    this.hostEventHandler("LaunchXmlReservedCharacterHelperForm", null);
                }
            }
            #endregion
            
            #region SetupAutoComment()
            /// <summary>
            /// This method loads the Auto Comment Hot Key
            /// </summary>
            public void SetupAutoComment()
            {
                // load the CommentDictionairy
                if (this.HasHostEventHandler)
                {
                    // auto commentText the line below the cursor
                    this.hostEventHandler("LoadCommentDictionary", null);
                }

                // setup the HandleKeyPress
                var window = Window.GetWindow(this);
                window.PreviewKeyDown += HandleKeyPress;
            }
            #endregion
            
            #region SetupCommentDictionary()
            /// <summary>
            /// This method Setup Comment Dictionary
            /// </summary>
            private void SetupCommentDictionary()
            {
                // load the CommentDictionairy
                if (this.HasHostEventHandler)
                {
                    // auto commentText the line below the cursor
                    this.hostEventHandler("SetupCommentDictionary", null);
                }  
            }
            #endregion
            
        #endregion
        
        #region Properties

            #region AutoComment
            /// <summary>
            /// This property gets or sets the value for 'AutoComment'.
            /// </summary>
            public bool AutoComment
            {
                get { return autoComment; }
                set { autoComment = value; }
            }
            #endregion

            #region HasHostEventHandler
            /// <summary>
            /// This property returns true if this object has the call back delegate set.
            /// </summary>
            public bool HasHostEventHandler
            {
                get
                {
                    bool hasHostEventHandler = (this.HostEventHandler != null);
                    
                    return hasHostEventHandler;
                }
            }
            #endregion
            
            #region HostEventHandler
            /// <summary>
            /// Does this object have a HostEventHandler 
            /// </summary>
            public HostEventHandlerCallBack HostEventHandler
            {  
                get { return hostEventHandler; }
                set { hostEventHandler = value; }
            }
        #endregion

        #endregion
        
    }
    #endregion

}
