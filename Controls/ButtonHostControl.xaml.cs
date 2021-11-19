    

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

namespace DataJuggler.Regionizer.Controls
{

    #region class ButtonHostControl
    /// <summary>
    /// Interaction logic for ButtonHostControl.xaml
    /// </summary>
    public partial class ButtonHostControl : UserControl
    {
        
        #region Private Variables
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a ButtonHostControl
        /// </summary>
        public ButtonHostControl()
        {
            // Create controls
            InitializeComponent();
        }
        #endregion

        #region Events

            #region ForEachButton_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event is used to write out a For Each loop
            /// </summary>
            private void ForEachButton_Click(object sender, RoutedEventArgs e)
            {
                // set the target
                string collectionName = this.TargetTextBox.Text;

                // if the parent MainWindowControl exists
                if (this.HasParentMainWindowControl)
                {
                    // get the return type
                    string dataType = this.ParentMainWindowControl.GetReturnTypeText();

                    // set the target
                    string target = dataType + "|" + collectionName;

                    // local
                    string eventName = "ForEachButton_Click";

                    // if the ParentMainWindowControl exists
                    if (this.HasParentMainWindowControl)
                    {
                        // Call the ButtonClicked method on the ParentMainWindowControl
                        this.ParentMainWindowControl.ButtonClicked(eventName, target);
                    }
                }
            }
            #endregion
            
            #region ForEachButton_Click(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when the ForEachButton is clicked.
            /// </summary>
            public void ForEachButton_Click(object sender, EventArgs e)
            {
                // set the target
                string target = "";
                
                // set the dataType
                string dataType = "";
                
                // local
                string eventName = "ForEachButton_Click";

                // if the ParentMainWindowControl exists
                if (this.HasParentMainWindowControl)
                {
                    // set the dataType
                    dataType = this.ParentMainWindowControl.GetReturnTypeText();

                    // set the collectionName
                    string collectionName = this.TargetTextBox.Text;

                    // set the target
                    target = dataType + "|" + collectionName;

                    // Call the ButtonClicked method on the ParentMainWindowControl
                    this.ParentMainWindowControl.ButtonClicked(eventName, target);
                }
            }
            #endregion
            
            #region IfExistsButton_Click(object sender, RoutedEventArgs e)
            /// <summary>
            /// This event is fired when the IfExistsButton is clicked.
            /// </summary>
            private void IfExistsButton_Click(object sender, RoutedEventArgs e)
            {
                // set the target
                string target = this.TargetTextBox.Text;

                // if the parent MainWindowControl exists
                if (this.HasParentMainWindowControl)
                {
                    // get the return type
                    string dataType = this.ParentMainWindowControl.GetReturnTypeText();

                    // Create the commentLine
                    string commentLine = "If the '" + target + "' exists";
                    string testLine = "if (" + target + " != null)";

                    // if the dataType is a string
                    if (dataType == "string")
                    {
                        // update the commentLine & testLine
                        commentLine = "If the '" + target + "' is set.";
                        testLine = "if (!String.IsNullOrEmpty(" + target + "))";
                    }
                    else if ((dataType == "int") || (dataType == "double"))
                    {
                        // update the commentLine & testLine
                        commentLine = "If the '" + target + "' is set.";
                        testLine = "if (" + target + " > 0)";
                    }
                    
                    // set the target
                    target = commentLine + "|" + testLine;
                    
                    // local
                    string eventName = "IfExistsButton_Click";

                    // if the ParentMainWindowControl exists
                    if (this.HasParentMainWindowControl)
                    {
                        // Call the ButtonClicked method on the ParentMainWindowControl
                        this.ParentMainWindowControl.ButtonClicked(eventName, target);
                    }
                }
            }
            #endregion
            
            #region IfExistsButton_Click(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when the IfExistsButton is clicked
            /// </summary>
            public void IfExistsButton_Click(object sender, EventArgs e)
            {
                // set the target
                string target = this.TargetTextBox.Text;

                // local
                string eventName = "IfExistsButton_Click";

                // if the ParentMainWindowControl exists
                if (this.HasParentMainWindowControl)
                {
                    // Call the ButtonClicked method on the ParentMainWindowControl
                    this.ParentMainWindowControl.ButtonClicked(eventName, target);
                }
            }
            #endregion
            
            #region InitialValueButton_Click(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when the InitialValueButton is clicked.
            /// </summary>
            public void InitialValueButton_Click(object sender, EventArgs e)
            {
                // set the target
                string target = "";
                
                // if the parent MainWindowControl exists
                if (this.HasParentMainWindowControl)
                {
                    // get the return type
                    string returnType = this.ParentMainWindowControl.GetReturnTypeText();

                    // get the target
                    target = returnType + "|" + this.TargetTextBox.Text;
                }

                // local
                string eventName = "InitialValueButton_Click";

                // if the ParentMainWindowControl exists
                if (this.HasParentMainWindowControl)
                {
                    // Call the ButtonClicked method on the ParentMainWindowControl
                    this.ParentMainWindowControl.ButtonClicked(eventName, target);
                }
            }
            #endregion
            
        #endregion

        #region Properties

            #region HasParentMainWindowControl
            /// <summary>
            /// This property returns true if this object has a 'ParentMainWindowControl'.
            /// </summary>
            public bool HasParentMainWindowControl
            {
                get
                {
                    // initial value
                    bool hasParentMainWindowControl = (this.ParentMainWindowControl != null);
                    
                    // return value
                    return hasParentMainWindowControl;
                }
            }
            #endregion
            
            #region ParentMainWindowControl
            /// <summary>
            /// This read only property returns the parent MainWindowControl if it exists.
            /// </summary>
            public RegionizerMainWindowControl ParentMainWindowControl
            {
                get
                {
                    // initial value
                    RegionizerMainWindowControl parentMainWindowControl = null;

                    // get the immediate parent
                    StackPanel stackPanel = this.Parent as StackPanel;

                    // if the stackPanel exists
                    if (stackPanel != null)
                    {
                        // set the return value
                        parentMainWindowControl = stackPanel.Parent as RegionizerMainWindowControl;
                    }

                    // return value
                    return parentMainWindowControl;
                }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
