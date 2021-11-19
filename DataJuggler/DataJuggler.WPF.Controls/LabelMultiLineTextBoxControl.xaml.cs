

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
using System.Media;
using DataJuggler.WPF.Controls.Interfaces;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using DataJuggler.WPF.Controls.Util;
using x = DataJuggler.WPF.Controls.Objects;

#endregion

namespace DataJuggler.WPF.Controls
{

    #region class LabelMultiLineTextBoxControl
    /// <summary>
    /// Interaction logic for LabelMultiLineTextBoxControl.xaml
    /// </summary>
    public partial class LabelMultiLineTextBoxControl : UserControl
    {
        
        #region Private Variables
        private int labelWidth;
        private string labelText;
        private string text;
        private HorizontalAlignment textAlign;
        private bool editable;
        private bool enabled;
        private bool encrypted;
        private HorizontalAlignment labelTextAlign;
        private FontFamily labelFont;
        private FontWeight labelFontWeight;
        private double labelFontSize;
        private FontFamily textBoxFontFamily;
        private FontWeight textBoxFontWeight;
        private double textBoxFontSize;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a LabelComboBoxControl object.
        /// </summary>
        public LabelMultiLineTextBoxControl()
        {
            // Create controls
            InitializeComponent();
        }
        #endregion

        #region Methods

            #region Activate()
            /// <summary>
            /// Set focus to the text box
            /// </summary>
            internal void Activate()
            {
                // if the text box is enabled
                if (this.Enabled)
                {
                    // Set Focus
                    this.TextBox.Focus();
                }
            } 
            #endregion

            #region Clear()
            /// <summary>
            /// Clear the items in this control
            /// </summary>
            public void Clear()
            {
                // erase the text
                this.TextBox.Text = "";
            }
            #endregion

            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            private void Init()
            {
                // Setup for multiline mode
                this.TextBox.AcceptsReturn = true;
                this.TextBoxScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                this.TextBox.TextWrapping = TextWrapping.Wrap;
            }
            #endregion

            #region Setup(string labelText, string text)
            /// <summary>
            /// This method prepares this control to be shown.
            /// </summary>
            /// <param name="labelText"></param>
            /// <param name="textBoxText"></param>
            public void Setup(string labelText, string text)
            {
                // set the properties
                this.LabelText = labelText;
                this.Text = text;
            } 
            #endregion

            #region UIEnable()
            /// <summary>
            /// This method determines if the Textbox is enabled or not based upon the value
            /// of Editable and Updateable.
            /// </summary>
            private void UIEnable()
            {
                // enabled or not
                bool enabled = this.IsEnabled;
                
                // enable the text box if enabled
                this.TextBox.IsEnabled = enabled;
            }  
            #endregion

        #endregion

        #region Properties

            #region Editable
            /// <summary>
            /// This property gets or sets the value for 'Editable'.
            /// </summary>
            public bool Editable
            {
                get { return editable; }
                set
                {
                    // set the value
                    editable = value;

                    // enable the DockPanel if the TextBox is editable
                    if (this.HasTextBoxControl)
                    {
                        // set the value
                        this.TextBoxControl.IsReadOnly = !value;
                    }
                }
            }
            #endregion

            #region Enabled
            /// <summary>
            /// This property gets or sets the value for 'Enabled'.
            /// </summary>
            public bool Enabled
            {
                get { return enabled; }
                set
                {
                    // set the value
                    enabled = value;

                    // if the TextBoxControl exists
                    if (this.HasTextBoxControl)
                    {
                        // set the value on the control
                        this.TextBoxControl.IsEnabled = enabled;
                    }
                }
            }
            #endregion

            #region Encrypted
            /// <summary>
            /// Does this control need to encrypted and decrypt the value
            /// before Saving or Displaying.
            /// </summary>
            public bool Encrypted
            {
                get { return encrypted; }
                set { encrypted = value; }
            }
            #endregion

            #region FieldLabel
            /// <summary>
            /// This property returns the label from this control.
            /// </summary>
            public Label FieldLabel
            {
                get
                {
                    // return the Label from this control.
                    return this.Label;
                }
            }
            #endregion

            #region HasLabelControl
            /// <summary>
            /// This property returns true if this object has a 'LabelControl'.
            /// </summary>
            public bool HasLabelControl
            {
                get
                {
                    // initial value
                    bool hasLabelControl = (this.LabelControl != null);
                    
                    // return value
                    return hasLabelControl;
                }
            }
            #endregion
            
            #region HasTextBoxControl
            /// <summary>
            /// This property returns true if this object has a 'TextBoxControl'.
            /// </summary>
            public bool HasTextBoxControl
            {
                get
                {
                    // initial value
                    bool hasTextBoxControl = (this.TextBoxControl != null);
                    
                    // return value
                    return hasTextBoxControl;
                }
            }
            #endregion

            #region LabelControl
            /// <summary>
            /// This read only property returns the 'Label' from this control.
            /// </summary>
            public Label LabelControl
            {
                get
                {
                    // return the Label
                    return this.Label;
                }
            }
            #endregion

            #region LabelFontFamily
            /// <summary>
            /// This property gets or sets the value for 'LabelFontFamily'.
            /// </summary>
            public FontFamily LabelFontFamily
            {
                get { return labelFont; }
                set
                {
                    // set the value
                    labelFont = value;

                    // if the LabelControl exists
                    if (this.HasLabelControl)
                    {
                        this.LabelControl.FontFamily = value;
                    }
                }
            }
            #endregion

            #region LabelFontSize
            /// <summary>
            /// This property gets or sets the value for 'LabelFontSize'.
            /// </summary>
            public double LabelFontSize
            {
                get { return labelFontSize; }
                set
                {
                    // set the value
                    labelFontSize = value;

                    // if the Label exists
                    if (this.HasLabelControl)
                    {
                        // set the fontsize on the label
                        this.Label.FontSize = value;
                    }
                }
            }
            #endregion

            #region LabelFontWeight
            /// <summary>
            /// This property gets or sets the value for 'LabelFontWeight'.
            /// </summary>
            public FontWeight LabelFontWeight
            {
                get { return labelFontWeight; }
                set
                {
                    // set the value
                    labelFontWeight = value;

                    // if the LabelControl exists
                    if (this.HasLabelControl)
                    {
                        // set the value on the control
                        this.LabelControl.FontWeight = value;
                    }
                }
            }
            #endregion
            
            #region LabelText
            /// <summary>
            /// This property gets or sets the LabelText.
            /// </summary>
            public string LabelText
            {
                get { return labelText; }
                set
                {
                    // set the value
                    labelText = value;

                    // if the Label exists
                    if (this.Label != null)
                    {
                        // set the Label text
                        this.Label.Content = value;
                    }
                }
            }
            #endregion

            #region LabelTextAlign
            /// <summary>
            /// Set the TextAlign for the label.
            /// </summary>
            public HorizontalAlignment LabelTextAlign
            {
                get { return labelTextAlign; }
                set
                {
                    // set the value
                    labelTextAlign = value;

                    // if the label exists
                    if (this.Label != null)
                    {
                        // set the value
                        this.Label.HorizontalContentAlignment = value;
                    }
                }
            }
            #endregion

            #region LabelWidth
            /// <summary>
            /// The LabelWidth
            /// </summary>
            public int LabelWidth
            {
                get { return labelWidth; }
                set
                {
                    // set the label width
                    labelWidth = value;

                    // if the Label exists
                    if (this.Label != null)
                    {
                        // set the label width
                        this.Label.Width = value;
                    }
                }
            }
            #endregion

            #region TextAlign
            /// <summary>
            /// The alignment for the TextAlign.
            /// </summary>
            public HorizontalAlignment TextAlign
            {
                get
                {
                    // if the TextBox exists
                    if (this.TextBox != null)
                    {
                        // set the return value
                        textAlign = this.TextBox.HorizontalContentAlignment;
                    }

                    // return value
                    return textAlign;
                }
                set
                {
                    // set the value
                    textAlign = value;

                    // if the TextBox exists
                    if (this.TextBox != null)
                    {
                        // set the return value
                        this.TextBox.HorizontalContentAlignment = value;
                    }
                }
            }
            #endregion

            #region TextBoxControl
            /// <summary>
            /// This property returns the TextBox
            /// </summary>
            public TextBox TextBoxControl
            {
                get
                {
                    // return the TextBox
                    return this.TextBox;
                }
            }
            #endregion

            #region TextBoxFontFamily
            /// <summary>
            /// This property gets or sets the value for 'TextBoxFont'.
            /// </summary>
            public FontFamily TextBoxFontFamily
            {
                get { return textBoxFontFamily; }
                set
                {
                    // set the value
                    textBoxFontFamily = value;

                    // if the TextBoxControl exists
                    if (this.HasTextBoxControl)
                    {
                        // set the value on the control
                        this.TextBox.FontFamily = value;
                    }
                }
            }
            #endregion

            #region TextBoxFontSize
            /// <summary>
            /// This property gets or sets the value for 'TextBoxFontSize'.
            /// </summary>
            public double TextBoxFontSize
            {
                get { return textBoxFontSize; }
                set
                {
                    // set the value
                    textBoxFontSize = value;

                    // if the control exists
                    if (this.HasTextBoxControl)
                    {
                        // set the value on the control
                        this.TextBoxControl.FontSize = value;
                    }
                }
            }
            #endregion

            #region TextBoxFontWeight
            /// <summary>
            /// This property gets or sets the value for 'TextBoxFontWeight'.
            /// </summary>
            public FontWeight TextBoxFontWeight
            {
                get { return textBoxFontWeight; }
                set
                {
                    // set the value
                    textBoxFontWeight = value;

                    // if the TextBoxControl exists
                    if (this.HasTextBoxControl)
                    {
                        // set the value on the control
                        this.TextBox.FontWeight = value;
                    }
                }
            }
            #endregion

            #region Text
            /// <summary>
            /// The Text for the TextBox
            /// </summary>
            public string Text
            {
                get
                {
                    // if the TextBox exists
                    if (this.TextBox != null)
                    {
                        // set the value
                        this.text = this.TextBox.Text;
                    }

                    // return value
                    return text;
                }
                set
                {
                    // set the value
                    text = value;

                    // if the TextBox exists
                    if (this.TextBox != null)
                    {
                        /// set the TextBox text
                        this.TextBox.Text = value;
                    }
                }
            }
            #endregion

        #endregion
        
    }
    #endregion

}
