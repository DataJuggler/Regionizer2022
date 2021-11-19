

#region using statements

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

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
        private DispatcherTimer showGraphTimer;
        private DispatcherTimer hideGraphTimer;
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
            
            #region CreateProperties_Click(object sender, RoutedEventArgs e)
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
            #region HasHideGraphTimer
            /// <summary>
            /// This property returns true if this object has a 'HideGraphTimer'.
            /// </summary>
            public bool HasHideGraphTimer
            {
                get
                {
                    // initial value
                    bool hideGraphTimer = (this.HideGraphTimer != null);
                    
                    // return value
                    return hideGraphTimer;
                }
            }
            #endregion
            
            #region HideGraphTimer
            /// <summary>
            /// This property gets or sets the value for 'HideGraphTimer'.
            /// </summary>
            public DispatcherTimer HideGraphTimer
            {
                get { return hideGraphTimer; }
                set { hideGraphTimer = value; }
            }
            #endregion
            
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
                    // Start the timer
                    this.ShowGraphTimer.Start();

                    // Format the Active Document
                    this.HostEventHandler("Format Document", null);

                    // Start the timer to hide the graph
                    this.HideGraphTimer.Start();
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
                    // Start the timer
                    this.ShowGraphTimer.Start();

                    // Format the Active Document
                    this.HostEventHandler("Format Selection", null);

                    // Start the timer
                    this.ShowGraphTimer.Stop();
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
            
            #region HideGraphTimer_Tick(object sender, EventArgs e)
            /// <summary>
            /// This event [enter description here].
            /// </summary>
            private void HideGraphTimer_Tick(object sender, EventArgs e)
            {
                // stop this timer
                this.HideGraphTimer.Stop();

                // Hide the graph
                this.Graph.Visibility = System.Windows.Visibility.Hidden;
            }
            #endregion
            
            #region ShowGraphTimer_Tick(object sender, EventArgs e)
            /// <summary>
            /// This event [enter description here].
            /// </summary>
            private void ShowGraphTimer_Tick(object sender, EventArgs e)
            {
                // only fire one
                this.ShowGraphTimer.Stop();

                // if the Graph is not visible
                if (!this.Graph.IsVisible)
                {
                    // Show the timer
                    this.Graph.Visibility = System.Windows.Visibility.Visible;
                }
            }
            #endregion
            
        #endregion
        
        #region Methods
            
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

                // Create the timer
                this.ShowGraphTimer = new DispatcherTimer();

                // Create the events for the Timer
                this.ShowGraphTimer.Interval = new TimeSpan(500);
                this.ShowGraphTimer.Tick += new EventHandler(ShowGraphTimer_Tick);
                this.HideGraphTimer = new DispatcherTimer();
                this.HideGraphTimer.Interval = new TimeSpan(500);
                this.HideGraphTimer.Tick += new EventHandler(HideGraphTimer_Tick);
            }
            #endregion
            
        #endregion
        
        #region Properties
            
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
            
            #region ShowGraphTimer
            /// <summary>
            /// This property gets or sets the value for 'ShowGraphTimer'.
            /// </summary>
            public DispatcherTimer ShowGraphTimer
            {
                get { return showGraphTimer; }
                set { showGraphTimer = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
