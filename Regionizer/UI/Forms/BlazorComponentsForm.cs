

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
using DataJuggler.Regionizer.CodeModel.Util;
using DataJuggler.Regionizer.CodeModel.Objects;
using System.Threading;
using DataJuggler.Core.UltimateHelper.Objects;

#endregion

namespace DataJuggler.Regionizer.UI.Forms
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
        private const string InstallRowBuilder = "dotnet new install DataJuggler.RowBuilder.Template@9.20.3";
        private const string CreateRowBuilder = "dotnet new RowBuilder -n [OutputFileName] --force";
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

            // Set the LabelColors
            RazorFilePicker.LabelColor = Color.White;
            OutputFolderSelector.LabelColor = Color.White;
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
            string templateText = "";

            // if checked
            if (SlimCheckBox.Checked)
            {
                // Get the slim text
                templateText = LoadTemplate("CalendarSlim.txt");
            }
            else
            {
                // Get the component text
                templateText = LoadTemplate("CalendarComponent.txt");
            }

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
            string templateText = "";

            // if checked
            if (SlimCheckBox.Checked)
            {
                // Get the slim text
                templateText = LoadTemplate("CheckBoxSlim.txt");
            }
            else
            {
                // Get the component text
                templateText = LoadTemplate("CheckBoxComponent.txt");
            }

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
            string templateText = "";

            // if checked
            if (SlimCheckBox.Checked)
            {
                // Get the slim text
                templateText = LoadTemplate("CheckedListBoxSlim.txt");
            }
            else
            {
                // Get the component text
                templateText = LoadTemplate("CheckedListBoxComponent.txt");
            }

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
            string templateText = "";

            if (CheckedListModeCheckBox.Checked)
            {
                // if checked
                if (SlimCheckBox.Checked)
                {
                    // Get the slim text
                    templateText = LoadTemplate("ComboBoxCheckListSlim.txt");
                }
                else
                {
                    // Get the component text
                    templateText = LoadTemplate("ComboBoxCheckListComponent.txt");
                }
            }
            else
            {
                // if checked
                if (SlimCheckBox.Checked)
                {
                    // Get the slim text
                    templateText = LoadTemplate("ComboBoxSlim.txt");
                }
                else
                {
                    // Get the component text
                    templateText = LoadTemplate("ComboBoxComponent.txt");
                }
            }

            // if the value for HasMainWindowCallback is true
            if ((HasMainWindowCallback) && (TextHelper.Exists(templateText)))
            {
                // Insert a TextBox.
                MainWindowCallback.AddBlazorComponent(templateText);
            }
        }
        #endregion
        
        #region AddImage_Click(object sender, EventArgs e)
        /// <summary>
        /// event is fired when the 'AddImage' is clicked.
        /// </summary>
        private void AddImage_Click(object sender, EventArgs e)
        {
            // Remove focus from the button just clicked to a hidden button            
            RemoveFocus();

            // Get the tempateText
            string templateText = "";

            // if Slm version is selected
            if (SlimCheckBox.Checked)
            {
                // set the value
                templateText = LoadTemplate("ImageComponentSlim.txt");
            }
            else
            {
                // set the value
                templateText = LoadTemplate("ImageComponent.txt");
            }

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
            string templateText = "";

            // if Slm version is selected
            if (SlimCheckBox.Checked)
            {
                // set the value
                templateText = LoadTemplate("ImageButtonSlim.txt");
            }
            else
            {
                // set the value
                templateText = LoadTemplate("ImageButtonComponent.txt");
            }

            // if the value for HasMainWindowCallback is true
            if ((HasMainWindowCallback) && (TextHelper.Exists(templateText)))
            {
                // Insert a TextBox.
                MainWindowCallback.AddBlazorComponent(templateText);
            }
        }
        #endregion
        
        #region AddLabelButton_Click(object sender, EventArgs e)
        /// <summary>
        /// event is fired when the 'AddLabelButton' is clicked.
        /// </summary>
        private void AddLabelButton_Click(object sender, EventArgs e)
        {
            // Remove focus from the button just clicked to a hidden button            
            RemoveFocus();

            // Get the tempateText
            string templateText = "";

            // if Slm version is selected
            if (SlimCheckBox.Checked)
            {
                // set the value
                templateText = LoadTemplate("LabelSlim.txt");
            }
            else
            {
                // set the value
                templateText = LoadTemplate("LabelComponent.txt");
            }

            // if the value for HasMainWindowCallback is true
            if ((HasMainWindowCallback) && (TextHelper.Exists(templateText)))
            {
                // Insert a TextBox.
                MainWindowCallback.AddBlazorComponent(templateText);
            }
        }
        #endregion
        
        #region AddListBoxButton_Click(object sender, EventArgs e)
        /// <summary>
        /// event is fired when the 'AddListBoxButton' is clicked.
        /// </summary>
        private void AddListBoxButton_Click(object sender, EventArgs e)
        {
            // Remove focus from the button just clicked to a hidden button            
            RemoveFocus();

            // Get the tempateText
            string templateText = "";

            // if Slm version is selected
            if (SlimCheckBox.Checked)
            {
                // set the value
                templateText = LoadTemplate("ListBoxSlim.txt");
            }
            else
            {
                // set the value
                templateText = LoadTemplate("ListBoxComponent.txt");
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
            string templateText = "";

            // if Slm version is selected
            if (SlimCheckBox.Checked)
            {
                // set the value
                templateText = LoadTemplate("TextBoxSlim.txt");
            }
            else
            {
                // set the value
                templateText = LoadTemplate("TextBoxComponent.txt");
            }

            // if the value for HasMainWindowCallback is true
            if ((HasMainWindowCallback) && (TextHelper.Exists(templateText)))
            {
                // Insert a TextBox.
                MainWindowCallback.AddBlazorComponent(templateText);
            }
        }
        #endregion
        
        #region AddTimeComponent_Click(object sender, EventArgs e)
        /// <summary>
        /// event is fired when the 'AddTimeComponent' is clicked.
        /// </summary>
        private void AddTimeComponent_Click(object sender, EventArgs e)
        {
            // Remove focus from the button just clicked to a hidden button            
            RemoveFocus();

            // Get the tempateText
            string templateText = "";

            // if Slm version is selected
            if (SlimCheckBox.Checked)
            {
                // set the value
                templateText = LoadTemplate("TimeSlim.txt");
            }
            else
            {
                // set the value
                templateText = LoadTemplate("TimeComponent.txt");
            }

            // if the value for HasMainWindowCallback is true
            if ((HasMainWindowCallback) && (TextHelper.Exists(templateText)))
            {
                // Insert a TextBox.
                MainWindowCallback.AddBlazorComponent(templateText);
            }
        }
        #endregion
        
        #region AddToggleButton_Click(object sender, EventArgs e)
        /// <summary>
        /// event is fired when the 'AddToggleButton' is clicked.
        /// </summary>
        private void AddToggleButton_Click(object sender, EventArgs e)
        {
            // Remove focus from the button just clicked to a hidden button            
            RemoveFocus();

            // Get the tempateText
            string templateText = "";

            // if Slm version is selected
            if (SlimCheckBox.Checked)
            {
                // set the value
                templateText = LoadTemplate("ToggleSlim.txt");
            }
            else
            {
                // set the value
                templateText = LoadTemplate("ToggleComponent.txt");
            }

            // if the value for HasMainWindowCallback is true
            if ((HasMainWindowCallback) && (TextHelper.Exists(templateText)))
            {
                // Insert a TextBox.
                MainWindowCallback.AddBlazorComponent(templateText);
            }
        }
        #endregion
        
        #region Button_MouseEnter(object sender, EventArgs e)
        /// <summary>
        /// event is fired when Button _ Mouse Enter
        /// </summary>
        private void Button_MouseEnter(object sender, EventArgs e)
        {
            // Change the cursor to a hand
            Cursor = Cursors.Hand;
        }
        #endregion
        
        #region Button_MouseLeave(object sender, EventArgs e)
        /// <summary>
        /// event is fired when Button _ Mouse Leave
        /// </summary>
        private void Button_MouseLeave(object sender, EventArgs e)
        {
            // Change the cursor back to the default pointer
            Cursor = Cursors.Default;
        }
        #endregion
        
        #region CreateRowBuildButton_Click(object sender, EventArgs e)
        /// <summary>
        /// event is fired when the 'CreateRowBuildButton' is clicked.
        /// </summary>
        private void CreateRowBuildButton_Click(object sender, EventArgs e)
        {
            // locals
            int attempts = 0;
            bool fileCreated = false;

            // Create a new instance of a 'RowBuilderInfo' object.
            RowBuilderInfo info = new RowBuilderInfo();

            // set the properites
            info.GridRazorPath = RazorFilePicker.Text;
            info.NamespaceName = NamespaceControl.Text;
            info.ObjectName = ObjectNameControl.Text;
            info.OutputFolder = OutputFolderSelector.Text;
            info.OutputFileName = OutputFileControl.Text;
            info.ListName = ListNameControl.Text;
            info.VariableName = VariableNameControl.Text;
            info.ClassName = ClassNameControl.Text;

            // First Install
            CommandRunner.InstallTemplate(InstallRowBuilder);

            // local
            string templateName = Path.Combine(info.OutputFolder, "RowBuilderItemTemplate.cs");

            // if the file exists
            if (File.Exists(templateName))
            {
                // delete
                File.Delete(templateName);
            }

            // Get the fulloutput path
            string outputFullPath = Path.Combine(info.OutputFolder, info.OutputFileName);

            // if the file exists
            if (File.Exists(outputFullPath))
            {
                // delete
                File.Delete(outputFullPath);
            }

            // the args must include the output file name
            DirectoryInfo directoryInfo = new DirectoryInfo(outputFullPath);

            // Set the workingDirectory
            string workingDirectory = directoryInfo.Parent.FullName;

            // Set the instance name
            string createInstanceCommand = CreateRowBuilder.Replace("[OutputFileName]", info.OutputFileName);

            // Create the instance
            CommandRunner.CreateInstance(createInstanceCommand, workingDirectory);

            do
            {
                // Increment the value for attempts
                attempts++;

                // wait 0.5s before trying again
                Thread.Sleep(500);

                // if the file exists
                if (File.Exists(templateName))
                {
                    // set to true
                    fileCreated = true;

                    // break out of loop
                    break;
                }

                // delay half a second
            } while(attempts < 5);

            // if the value for fileCreated is true
            if (fileCreated)
            {
                // Rename the file
                File.Move(templateName, outputFullPath);

                // we can do the replacements now
                List<TextLine> lines = TextHelper.GetTextLinesFromFile(outputFullPath);

                // If the lines collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(lines))
                {
                    // Iterate the collection of TextLine objects
                    foreach (TextLine line in lines)
                    {
                        // now do the replacements
                        line.Text = line.Text.Replace("[NamespaceName]", info.NamespaceName);                        
                        line.Text = line.Text.Replace("[ClassName]", info.ClassName);
                        line.Text = line.Text.Replace("[ListVariableName]", info.ListName);
                        line.Text = line.Text.Replace("[ObjectName]", info.ObjectName);
                        line.Text = line.Text.Replace("[VariableName]", info.VariableName);
                    }

                    // Delete the file so it can be saved
                    File.Delete(outputFullPath);

                    // Get the updated values for the text
                    string fileText = TextHelper.ExportTextLines(lines);

                    // Write out the new text
                    File.WriteAllText(outputFullPath, fileText);

                    // Show the StatusLabel
                    StatusLabel.Visible = true;

                    // Start the timer
                    StatusTimer.Enabled = true;
                }
            }
        }
        #endregion
        
        #region StatusTimer_Tick(object sender, EventArgs e)
        /// <summary>
        /// event is fired when Status Timer _ Tick
        /// </summary>
        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            // Only run once
            StatusTimer.Enabled = false;

            // Hide
            StatusLabel.Visible = false;
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
