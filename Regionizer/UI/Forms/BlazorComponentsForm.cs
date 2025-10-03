

#region using statements

using DataJuggler.Core.UltimateHelper;
using DataJuggler.Regionizer;
using Regionizer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#endregion

namespace Regionizer.UI.Forms
{

    #region class BlazorComponentsForm
    /// <summary>
    /// This class is a form to add DataJuggler.Blazor.Components and buttons to a form.
    /// </summary>
    public partial class BlazorComponentsForm : Form
    {
        
        #region Private Variables
        private RegionizerMainWindow mainWindowCallback;
        private bool isActivatedOnce;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'BlazorComponentsForm' object.
        /// </summary>
        public BlazorComponentsForm()
        {
            // Create Controls
            InitializeComponent();

            // Load the ComboBox
            FontComboBox.LoadItems(typeof(KnownColor));

            // Select Black
            FontComboBox.SelectedIndex = FontComboBox.FindItemIndexByValue("Black");
        }
        #endregion
        
        #region Events
            
        #region AddCalendarButton_Click(object sender, EventArgs e)
        /// <summary>
        /// event is fired when the 'AddCalendarButton' is clicked.
        /// </summary>
        private void AddCalendarButton_Click(object sender, EventArgs e)
        {
            // Remove focus from the button just clicked to a hidden button            
            RemoveFocus();

             // Get the tempateText
            string templateText = LoadTemplate("CalendarComponent.txt");

            // if the value for HasMainWindowCallback is true
            if ((HasMainWindowCallback) && (TextHelper.Exists(templateText)))
            {
                // Insert a TextBox.
                MainWindowCallback.AddBlazorComponent(templateText);
            }
        }
        #endregion
        
        #region AddCheckBox_Click(object sender, EventArgs e)
        /// <summary>
        /// event is fired when the 'AddCheckBox' is clicked.
        /// </summary>
        private void AddCheckBox_Click(object sender, EventArgs e)
        {
            // Remove focus from the button just clicked to a hidden button            
            RemoveFocus();

            // Get the tempateText
            string templateText = LoadTemplate("CheckBoxComponent.txt");

            // if the value for HasMainWindowCallback is true
            if ((HasMainWindowCallback) && (TextHelper.Exists(templateText)))
            {
                // Insert a TextBox.
                MainWindowCallback.AddBlazorComponent(templateText);
            }
        }
        #endregion
        
        #region AddCheckedListBox_Click(object sender, EventArgs e)
        /// <summary>
        /// event is fired when the 'AddCheckedListBox' is clicked.
        /// </summary>
        private void AddCheckedListBox_Click(object sender, EventArgs e)
        {
            // Remove focus from the button just clicked to a hidden button            
            RemoveFocus();

            // Get the tempateText
            string templateText = LoadTemplate("CheckedListBoxComponent.txt");

            // if the value for HasMainWindowCallback is true
            if ((HasMainWindowCallback) && (TextHelper.Exists(templateText)))
            {
                // Insert a TextBox.
                MainWindowCallback.AddBlazorComponent(templateText);
            }
        }
        #endregion
        
        #region AddComboBoxbutton_Click(object sender, EventArgs e)
        /// <summary>
        /// event is fired when the 'AddComboBoxbutton' is clicked.
        /// </summary>
        private void AddComboBoxbutton_Click(object sender, EventArgs e)
        {
            // Remove focus from the button just clicked to a hidden button            
            RemoveFocus();

            // Get the tempateText
            string templateText = LoadTemplate("ComboBoxComponent.txt");

            // if the value for HasMainWindowCallback is true
            if ((HasMainWindowCallback) && (TextHelper.Exists(templateText)))
            {
                // Insert a TextBox.
                MainWindowCallback.AddBlazorComponent(templateText);
            }
        }
        #endregion
        
        #region AddImageButton_Click(object sender, EventArgs e)
        /// <summary>
        /// event is fired when the 'AddImageButton' is clicked.
        /// </summary>
        private void AddImageButton_Click(object sender, EventArgs e)
        {
            // Remove focus from the button just clicked to a hidden button            
            RemoveFocus();

            // Get the tempateText
            string templateText = LoadTemplate("ImageButtonComponent.txt");

            // if Slm version is selected
            if (SlimCheckBox.Checked)
            {
                LoadTemplate("ImageButtonSlim.txt");
            }

            // if the value for HasMainWindowCallback is true
            if ((HasMainWindowCallback) && (TextHelper.Exists(templateText)))
            {
                // Insert a TextBox.
                MainWindowCallback.AddBlazorComponent(templateText);
            }
        }
        #endregion
        
        #region AddTextBoxButton_Click(object sender, EventArgs e)
        /// <summary>
        /// event is fired when the 'AddTextBoxButton' is clicked.
        /// The user must have a Razor file open to where they want the text inserted.
        /// </summary>
        private void AddTextBoxButton_Click(object sender, EventArgs e)
        {
            // Remove focus from the button just clicked to a hidden button            
            RemoveFocus();

            // Get the tempateText
            string templateText = LoadTemplate("TextBoxComponent.txt");

            // if the value for HasMainWindowCallback is true
            if ((HasMainWindowCallback) && (TextHelper.Exists(templateText)))
            {
                // Insert a TextBox.
                MainWindowCallback.AddBlazorComponent(templateText);
            }
        }
        #endregion
        
        #endregion
        
        #region Methods

            #region LoadTemplate(string templateFileName)
            /// <summary>
            /// method returns the Template
            /// </summary>
            public static string LoadTemplate(string templateFileName)
            {
                // initial value
                string templateText = "";

                try
                {
                    var assembly = typeof(RegionizerPackage).Assembly;
                    
                    string resourceName = "Regionizer.Templates." + templateFileName;

                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        if (NullHelper.Exists(stream))
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                // set the return value
                                templateText = reader.ReadToEnd();
                            }                        
                        }
                    }
                }
                catch (Exception error)
                {
                    // For debugging only for now
                    DebugHelper.WriteDebugError("LoadTempalte", "BlazorComponentsForm", error);
                }

                // return value
                return templateText;
            }
            #endregion
            
            #region RemoveFocus()
            /// <summary>
            /// Remove Focus
            /// </summary>
            public void RemoveFocus()
            {
                // Set Focus to the Hidden button
                HiddenButton.Focus();

                // Update the UI
                Refresh();
                Application.DoEvents();
            }
            #endregion
            
        #endregion
        
        #region Properties
            
            #region HasMainWindowCallback
            /// <summary>
            /// This property returns true if this object has a 'MainWindowCallback'.
            /// </summary>
            public bool HasMainWindowCallback
            {
                get
                {
                    // initial value
                    bool hasMainWindowCallback = (MainWindowCallback != null);

                    // return value
                    return hasMainWindowCallback;
                }
            }
            #endregion
            
            #region IsActivatedOnce
            /// <summary>
            /// This property gets or sets the value for 'IsActivatedOnce'.
            /// </summary>
            public bool IsActivatedOnce
            {
                get { return isActivatedOnce; }
                set { isActivatedOnce = value; }
            }
            #endregion
            
            #region MainWindowCallback
            /// <summary>
            /// This property gets or sets the value for 'MainWindowCallback'.
            /// </summary>
            public RegionizerMainWindow MainWindowCallback
            {
                get { return mainWindowCallback; }
                set { mainWindowCallback = value; }
            }
        #endregion

        #endregion
    }
    #endregion

}
