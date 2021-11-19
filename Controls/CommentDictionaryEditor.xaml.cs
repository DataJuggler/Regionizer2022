

#region using statements

using DataJuggler.Core.UltimateHelper;
using DataJuggler.Regionizer.CodeModel.Objects;
using DataJuggler.WPF.Controls;
using DataJuggler.WPF.Controls.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using XmlMirror.Runtime.Util;

#endregion

namespace DataJuggler.Regionizer.Controls
{

    #region delegate CloseParentForm()
    public delegate void CloseParentForm();
    #endregion

    #region delegate UpdateRegistryCallBack(DictionaryInfo dictionaryInfo);
    public delegate void UpdateRegistryCallBack(DictionaryInfo dictionaryInfo);
    #endregion

    #region class CommentDictionaryEditor
    /// <summary>
    /// Interaction logic for CommentDictionaryEditor.xaml
    /// </summary>
    public partial class CommentDictionaryEditor : System.Windows.Controls.UserControl, ITextChanged
    {
        
        #region Private Variables
        private CloseParentForm closeParentMethod;
        private UpdateRegistryCallBack updateRegistryMethod;
        private string downloadUrl;
        private DictionaryInfo storedDictionaryInfo;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'CommentDictionaryEditor' object.
        /// </summary>
        public CommentDictionaryEditor()
        {
            // Create controls
            InitializeComponent();

            // Perform initializations for this object
            Init();
        }
        #endregion
        
        #region Events
            
            #region BrowseForFileButton_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event is fired when the 'BrowseForFileButton' is clicked.
            /// </summary>
            private void BrowseForFileButton_Click(object sender, RoutedEventArgs e)
            {
                // create an instance of an openFileDialog
                OpenFileDialog dialog = new OpenFileDialog();

                // set the Filter
                dialog.Filter = "Xml files (*.xml)|*.xml";

                // show the dialog
                dialog.ShowDialog();

                // if the fileName exists
                if (TextHelper.Exists(dialog.FileName))
                {
                    // set the value for CustomDictionaryPathControl.Text
                    this.CustomDictionaryPathControl.Text = dialog.FileName;
                }
            }
            #endregion
            
            #region BrowseForFolderButton_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event is fired when the 'BrowseForFolderButton' is clicked.
            /// </summary>
            private void BrowseForFolderButton_Click(object sender, RoutedEventArgs e)
            {
                // Create an instance of a FolderBrowserDialog
                FolderBrowserDialog dialog = new FolderBrowserDialog();

                // show the dialog
                dialog.ShowDialog();

                // if a path was selected
                if (TextHelper.Exists(dialog.SelectedPath))
                {
                    // set the text of the TextBox
                    this.DictionaryPathControl.Text = dialog.SelectedPath;
                }
            }
            #endregion
            
            #region Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
            /// <summary>
            /// This event is fired when Button _ Mouse Enter
            /// </summary>
            private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
            {
                // Change the cursor to a hand
                this.Cursor = System.Windows.Input.Cursors.Hand;
            }
            #endregion
            
            #region Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
            /// <summary>
            /// This event is fired when Button _ Mouse Leave
            /// </summary>
            private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
            {
                // Change the cursor to a hand
                this.Cursor = System.Windows.Input.Cursors.Arrow;
            }
            #endregion
            
            #region DoneButton_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event is fired when the 'DoneButton' is clicked.
            /// </summary>
            private void DoneButton_Click(object sender, RoutedEventArgs e)
            {
                // if the CloseParentMethod exists
               if (this.HasCloseParentMethod)
               {
                    // call the close parent method
                    this.CloseParentMethod();
               }
            }
            #endregion
            
            #region DownloadDictionaryButton_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event is fired when the 'DownloadDictionaryButton' is clicked.
            /// </summary>
            private void DownloadDictionaryButton_Click(object sender, RoutedEventArgs e)
            {
                // Download the CommentDictionary
                DownloadCommentDictionary();
            }
            #endregion
            
            #region OnTextChanged(LabelTextBoxControl labelTextBoxControl, string text)
            /// <summary>
            /// This event is called by the LabelTextBox controls when a value changes in a textbox.
            /// </summary>
            /// <param name="labelTextBoxControl"></param>
            /// <param name="text"></param>
            public void OnTextChanged(LabelTextBoxControl labelTextBoxControl, string text)
            {
                // if this is the DictionaryPathTextBox
                if (labelTextBoxControl.Name == "DictionaryPathControl")
                {
                    // if the text exists
                    if (TextHelper.Exists(text))
                    {
                        // if the string does not end with the fileName
                        if (!text.EndsWith(@"\CommentDictionary.xml"))
                        {
                            // append the name of the file
                            labelTextBoxControl.Text = text + @"\CommentDictionary.xml";
                        }
                    }
                }

                // enable the controls based upon the value of DictionaryPath or the InstalledVersion
                UIEnable();
            }
            #endregion
            
            #region TryCustomFirstCheckBox_CheckedChange(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event is fired when Try Custom First Check Box _ Checked Change
            /// </summary>
            private void TryCustomFirstCheckBox_CheckedChange(object sender, RoutedEventArgs e)
            {
                // enable controls
                UIEnable();
            }
            #endregion
            
            #region UpdateRegistryButton_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event is fired when the 'UpdateRegistryButton' is clicked.
            /// </summary>
            private void UpdateRegistryButton_Click(object sender, RoutedEventArgs e)
            {
                // set the local values
                DictionaryInfo dictionaryInfo = this.CaptureDictionaryInfo();

                // if the UpdateRegistryMethod exists
                if (this.HasUpdateRegistryMethod)
                {
                    // update the registry
                    this.UpdateRegistryMethod(dictionaryInfo);
                }
            }
            #endregion
            
            #region UseCustomDictionaryCheckBox_CheckedChange(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event is fired when Use Custom Dictionary Check Box _ Checked Change
            /// </summary>
            private void UseCustomDictionaryCheckBox_CheckedChange(object sender, RoutedEventArgs e)
            {
                // set the value
                bool useCustomDictionary = (bool) this.UseCustomDictionaryCheckBox.IsChecked;
                
                // if UseCustomDictionary
                if (useCustomDictionary)
                {
                    // enable the controls for the Custom Dictionary
                    this.CustomDictionaryPathControl.IsEnabled = true;
                    this.TryCustomFirstCheckBox.IsEnabled = true;
                    this.BrowseForFileButton.IsEnabled = true;
                    this.TryCustomFirstCheckBox.IsChecked = true;
                }
                else
                {
                    // disable the controls for the 
                    this.CustomDictionaryPathControl.IsEnabled = false;
                    this.CustomDictionaryPathControl.Text = "";
                    this.TryCustomFirstCheckBox.IsEnabled = false;
                    this.BrowseForFileButton.IsEnabled = false;
                }

                // enable controls
                UIEnable();
            }
            #endregion
            
        #endregion
        
        #region Methods

            #region CaptureDictionaryInfo()
            /// <summary>
            /// This method returns the Dictionary Info
            /// </summary>
            private DictionaryInfo CaptureDictionaryInfo()
            {
                // initial value
                DictionaryInfo dictionaryInfo = new DictionaryInfo();

                // Set the value for DictionaryPath
                dictionaryInfo.DictionaryPath = this.DictionaryPathControl.Text;
                dictionaryInfo.InstalledVersion = NumericHelper.ParseDouble(this.InstalledVersionTextBox.Text, 0, -1);
                dictionaryInfo.UseCustomDictionary = (bool) this.UseCustomDictionaryCheckBox.IsChecked;
                dictionaryInfo.CustomDictionaryPath = this.CustomDictionaryPathControl.Text;
                dictionaryInfo.TryCustomDictionaryFirst = (bool) this.TryCustomFirstCheckBox.IsChecked;

                // return value
                return dictionaryInfo;
            }
            #endregion
            
            #region CheckForUpdatedVersion()
            /// <summary>
            /// This method Check For Updated Version
            /// </summary>
            private void CheckForUpdatedVersion()
            {
                // locals
                double latestVersion = 0;
                string url = "";

                try
                {
                    // get the url for the Xml info
                    url = "http://www.datajuggler.com/Regionizer/CommentDictionaryInfo.xml";

                    // create an xml reader
                    XmlTextReader reader = new XmlTextReader(url);

                    // read the xml line per line
                    while (reader.Read())
                    {
                        // determine what to do by the text
                        string nodeName = reader.Name;

                        // determine the action by the nodeName
                        switch (nodeName)
                        {
                            case "LatestVersion":

                                // set the latestVersion
                                latestVersion = NumericHelper.ParseDouble(reader.ReadInnerXml(), 0, -1);

                                // required
                                break;

                            case "Url":

                                // set the latestVersion
                                url = reader.ReadInnerXml();

                                // required
                                break;
                        }

                        
                    }

                    // close the reader
                    reader.Close();

                    // if the latestVersion is set
                    if ((latestVersion > 0) && (TextHelper.Exists(url)))
                    {
                        // set the content
                        this.AvailableVersionValueLabel.Content = latestVersion.ToString();
                        this.AvailableVersionValueLabel.Visibility = System.Windows.Visibility.Visible;
                        this.CheckForUpdateButton.Visibility = System.Windows.Visibility.Hidden;
                        this.DownloadUrl = url;

                        // enable the Download button if a newer version is available
                        UIEnable();
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
            }
            #endregion
            
            #region ClickHandler(int buttonNumber, string buttonText)
            /// <summary>
            /// This method handles the button clicks.
            /// </summary>
            /// <param name="buttonNumber"></param>
            /// <param name="buttonText"></param>
            public void ClickHandler(int buttonNumber, string buttonText)
            {
                // deteremine the action by the buttonNumber
                switch (buttonNumber)
                {
                    case 1:

                        // Check for updated version
                        CheckForUpdatedVersion();

                        // required
                        break;
                }
            }
            #endregion

            #region DetermineIfTheDictionaryHasChanged()
            /// <summary>
            /// This event is fired when Determine If The Dictionary Has Changed
            /// </summary>
            private bool DetermineIfTheDictionaryHasChanged()
            {
                // initial value
                bool hasDictionaryChanged = false;

                // if there is a stored dictionaryInfo object
                if (this.HasStoredDictionaryInfo)
                {
                    // capture the current dictionaryInfo
                    DictionaryInfo dictionaryInfo = CaptureDictionaryInfo();

                    // if the DictionaryPath has changed
                    if (!TextHelper.IsEqual(this.StoredDictionaryInfo.DictionaryPath, dictionaryInfo.DictionaryPath))
                    {
                        // the dictionary has changed
                        hasDictionaryChanged = true;
                    }
                    else if (StoredDictionaryInfo.InstalledVersion != dictionaryInfo.InstalledVersion)
                    {
                        // the dictionary has changed
                        hasDictionaryChanged = true;
                    }
                    else if (StoredDictionaryInfo.UseCustomDictionary != dictionaryInfo.UseCustomDictionary)
                    {
                        // the dictionary has changed
                        hasDictionaryChanged = true;
                    }
                    else if (StoredDictionaryInfo.TryCustomDictionaryFirst != dictionaryInfo.TryCustomDictionaryFirst)
                    {
                        // the dictionary has changed
                        hasDictionaryChanged = true;
                    }
                    else 
                    {
                        // locals
                        string storedCustomDictionaryPath = "";
                        string customDictionaryPath = "";
                        
                        // if the CustomDictionaryPath exists
                        if (StoredDictionaryInfo.HasCustomDictionaryPath)
                        {
                            // trim the storedCustomDictionaryPath
                            storedCustomDictionaryPath = StoredDictionaryInfo.CustomDictionaryPath.Trim();

                            // trim the customDictionaryPath
                            customDictionaryPath = dictionaryInfo.CustomDictionaryPath.Trim();

                            // if the string are not equal
                            if (!TextHelper.IsEqual(storedCustomDictionaryPath, customDictionaryPath))
                            {
                                // the dictionary has changed
                                hasDictionaryChanged = true;
                            }
                        }
                    }
                }
                else
                {
                    // there are changes
                    hasDictionaryChanged = true;
                }

                // return value
                return hasDictionaryChanged;
            }
            #endregion

            #region DisplayDictionaryInfo(DictionaryInfo dictionaryInfo)
            /// <summary>
            /// This method returns the Dictionary Info
            /// </summary>
            private void DisplayDictionaryInfo(DictionaryInfo dictionaryInfo)
            {
                // locals
                string dictionaryPath = "";
                double installedVersion = 0;
                bool useCustomDictionary = false;
                string customDictionaryPath = "";
                bool tryCustomFirst = false;

                // if the dictionaryInfo object exists
                if (dictionaryInfo != null)
                {
                    // set the values
                    dictionaryPath = dictionaryInfo.DictionaryPath;
                    installedVersion = dictionaryInfo.InstalledVersion;
                    useCustomDictionary = dictionaryInfo.UseCustomDictionary;
                    customDictionaryPath = dictionaryInfo.CustomDictionaryPath;
                    tryCustomFirst = dictionaryInfo.TryCustomDictionaryFirst;
                }

                // display the values
                this.DictionaryPathControl.Text = dictionaryPath;
                this.InstalledVersionTextBox.Text = installedVersion.ToString();
                this.UseCustomDictionaryCheckBox.IsChecked = useCustomDictionary;
                this.CustomDictionaryPathControl.Text = customDictionaryPath;
                this.TryCustomFirstCheckBox.IsChecked = tryCustomFirst;
            }
            #endregion
            
            #region DownloadCommentDictionary()
            /// <summary>
            /// This method Download Comment Dictionary
            /// </summary>
            private void DownloadCommentDictionary()
            {
                 // if the Available version is greater than the installed version
                if ((this.AvailableVersion > this.InstalledVersion) && (this.HasDownloadUrl) && (this.HasDictionaryPath))
                {
                    try
                    { 
                        // if the file previously exists
                        if (File.Exists(this.DictionaryPath))
                        {
                            // delete the existing dictionary
                            File.Delete(DictionaryPath);
                        }

                        // using the client
                        using (WebClient Client = new WebClient())
                        {
                            // download the file
                            Client.DownloadFile(this.DownloadUrl, DictionaryPath);
                        }

                        // update the textbox
                        this.InstalledVersionTextBox.Text = this.AvailableVersion.ToString();

                        // enable controls
                        UIEnable();

                        // Show the user a success message
                        MessageBoxHelper.ShowMessage("The comment dictionary has been downloaded." + Environment.NewLine + "Now click the 'Update Registry' button to update your system.", "Download Complete");
                    }
                    catch (Exception error)
                    {
                        // for debugging only
                        string err = error.ToString();

                        // Show the user a message about the error
                        MessageBoxHelper.ShowMessage("Sorry we were not able to download the comment dictionary at this time.", "Download Failed");
                    }
                }
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Setup the ClickHandler
                this.CheckForUpdateButton.ClickHandler = this.ClickHandler;

                // Setup the TextChanged listeners
                this.DictionaryPathControl.OnTextChangedListener = this;
                this.CustomDictionaryPathControl.OnTextChangedListener = this;

                // enable the buttons for startup mode
                UIEnable();
            }
            #endregion

            #region Setup(string dictionaryPath, double version, bool useCustomDictionary, string customDictionaryPath, bool tryCustomDictionaryFirst)
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Setup(DictionaryInfo dictionaryInfo)
            {
                // store the values so we can compare against the current value so we can determine if there are any changes
                this.StoredDictionaryInfo = dictionaryInfo;

                // display the dictionaryInfo
                DisplayDictionaryInfo(dictionaryInfo);

                // Enable the buttons
                UIEnable();
            }
            #endregion
            
            #region UIEnable()
            /// <summary>
            /// This method UI Enable
            /// </summary>
            private void UIEnable()
            {
                // if the Available version is greater than the installed version
                if ((this.AvailableVersion > this.InstalledVersion) && (this.HasDownloadUrl) && (this.HasDictionaryPath))
                {
                    // enable the Download button
                    this.DownloadDictionaryButton.IsEnabled = true;

                    // Set the background to blue
                    this.DownloadDictionaryButton.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/DataJuggler.Regionizer.Controls;component/Images/DeepBlue.png")));
                }
                else
                {
                    // do not enable
                    this.DownloadDictionaryButton.IsEnabled = false;

                    // Set the background to gray
                    this.DownloadDictionaryButton.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/DataJuggler.Regionizer.Controls;component/Images/DeepGray.png")));
                }

                // has the dictionaryChagned
                bool hasDictionaryChanged = DetermineIfTheDictionaryHasChanged();

                // if the installed version is newer than the StoredVersion
                if (hasDictionaryChanged)
                {
                    // The UpdateRegistryButton is enabled
                    this.UpdateRegistryButton.IsEnabled = true;

                    // Set the background to blue
                    this.UpdateRegistryButton.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/DataJuggler.Regionizer.Controls;component/Images/DeepBlue.png")));
                }
                else
                {
                    // The UpdateRegistryButton is not enabled
                    this.UpdateRegistryButton.IsEnabled = false;

                    // Set the background to gray
                    this.UpdateRegistryButton.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/DataJuggler.Regionizer.Controls;component/Images/DeepGray.png")));
                }
            }
            #endregion
            
        #endregion
        
        #region Properties

            #region AvailableVersion
            /// <summary>
            /// This read only property returns the value from the AvailableVersionValueLabel
            /// </summary>
            public double AvailableVersion
            {
                get
                {
                    // initial value
                    double availableVersion = NumericHelper.ParseDouble(this.AvailableVersionValueLabel.Content.ToString(), 0, -1);

                    // return value
                    return availableVersion;
                }
            }
            #endregion

            #region CloseParentMethod
            /// <summary>
            /// This property gets or sets the value for 'CloseParentMethod'.
            /// </summary>
            public CloseParentForm CloseParentMethod
            {
                get { return closeParentMethod; }
                set { closeParentMethod = value; }
            }
            #endregion
            
            #region DictionaryPath
            /// <summary>
            /// This read only property returns the value for 'DictionaryPath'.
            /// </summary>
            public string DictionaryPath
            {
                get
                {
                    // initial value
                    string dictionaryPath = this.DictionaryPathControl.Text;
                    
                    // return value
                    return dictionaryPath;
                }
            }
            #endregion
            
            #region DownloadUrl
            /// <summary>
            /// This property gets or sets the value for 'DownloadUrl'.
            /// </summary>
            public string DownloadUrl
            {
                get { return downloadUrl; }
                set { downloadUrl = value; }
            }
            #endregion
            
            #region HasAvailableVersion
            /// <summary>
            /// This property returns true if the 'AvailableVersion' is set.
            /// </summary>
            public bool HasAvailableVersion
            {
                get
                {
                    // initial value
                    bool hasAvailableVersion = (this.AvailableVersion > 0);
                    
                    // return value
                    return hasAvailableVersion;
                }
            }
            #endregion
            
            #region HasCloseParentMethod
            /// <summary>
            /// This property returns true if this object has a 'CloseParentMethod'.
            /// </summary>
            public bool HasCloseParentMethod
            {
                get
                {
                    // initial value
                    bool hasCloseParentMethod = (this.CloseParentMethod != null);
                    
                    // return value
                    return hasCloseParentMethod;
                }
            }
            #endregion
            
            #region HasDictionaryPath
            /// <summary>
            /// This property returns true if the 'DictionaryPath' exists.
            /// </summary>
            public bool HasDictionaryPath
            {
                get
                {
                    // initial value
                    bool hasDictionaryPath = (!String.IsNullOrEmpty(this.DictionaryPath));
                    
                    // return value
                    return hasDictionaryPath;
                }
            }
            #endregion
            
            #region HasDownloadUrl
            /// <summary>
            /// This property returns true if the 'DownloadUrl' exists.
            /// </summary>
            public bool HasDownloadUrl
            {
                get
                {
                    // initial value
                    bool hasDownloadUrl = (!String.IsNullOrEmpty(this.DownloadUrl));
                    
                    // return value
                    return hasDownloadUrl;
                }
            }
            #endregion
            
            #region HasInstalledVersion
            /// <summary>
            /// This property returns true if the 'InstalledVersion' is set.
            /// </summary>
            public bool HasInstalledVersion
            {
                get
                {
                    // initial value
                    bool hasInstalledVersion = (this.InstalledVersion > 0);
                    
                    // return value
                    return hasInstalledVersion;
                }
            }
            #endregion
            
            #region HasStoredDictionaryInfo
            /// <summary>
            /// This property returns true if this object has a 'StoredDictionaryInfo'.
            /// </summary>
            public bool HasStoredDictionaryInfo
            {
                get
                {
                    // initial value
                    bool hasStoredDictionaryInfo = (this.StoredDictionaryInfo != null);
                    
                    // return value
                    return hasStoredDictionaryInfo;
                }
            }
            #endregion
            
            #region HasUpdateRegistryMethod
            /// <summary>
            /// This property returns true if this object has an 'UpdateRegistryMethod'.
            /// </summary>
            public bool HasUpdateRegistryMethod
            {
                get
                {
                    // initial value
                    bool hasUpdateRegistryMethod = (this.UpdateRegistryMethod != null);
                    
                    // return value
                    return hasUpdateRegistryMethod;
                }
            }
            #endregion
            
            #region InstalledVersion
            /// <summary>
            /// This read only property returns the value for 'InstalledVersion'.
            /// </summary>
            public double InstalledVersion
            {
                get
                {
                    // initial value
                    double installedVersion = NumericHelper.ParseDouble(this.InstalledVersionTextBox.Text, 0, -1);
                    
                    // return value
                    return installedVersion;
                }
            }
            #endregion
            
            #region StoredDictionaryInfo
            /// <summary>
            /// This property gets or sets the value for 'StoredDictionaryInfo'.
            /// </summary>
            public DictionaryInfo StoredDictionaryInfo
            {
                get { return storedDictionaryInfo; }
                set { storedDictionaryInfo = value; }
            }
            #endregion
            
            #region UpdateRegistryMethod
            /// <summary>
            /// This property gets or sets the value for 'UpdateRegistryMethod'.
            /// </summary>
            public UpdateRegistryCallBack UpdateRegistryMethod
            {
                get { return updateRegistryMethod; }
                set { updateRegistryMethod = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
