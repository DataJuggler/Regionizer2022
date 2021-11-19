

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

    #region class LabelPasswordBoxControl
    /// <summary>
    /// Interaction logic for LabelPasswordBoxControl.xaml
    /// </summary>
    public partial class LabelPasswordBoxControl : UserControl
    {
        
        #region Private Variables
        private int labelWidth;
        private string labelText;
        private string password;
        private HorizontalAlignment passwordAlign;
        private bool editable;
        private bool enabled;
        private bool encrypted;
        private HorizontalAlignment labelTextAlign;
        private FontFamily labelFont;
        private FontWeight labelFontWeight;
        private double labelFontSize;
        private FontFamily passwordBoxFontFamily;
        private FontWeight passwordBoxFontWeight;
        private double passwordBoxFontSize;
        private Brush labelForeground;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a LabelComboBoxControl object.
        /// </summary>
        public LabelPasswordBoxControl()
        {
            // Create controls
            InitializeComponent();
        }
        #endregion

        #region Methods

            #region Activate()
            /// <summary>
            /// Set focus to the password box
            /// </summary>
            internal void Activate()
            {
                // if the password box is enabled
                if (this.Enabled)
                {
                    // Set Focus
                    this.PasswordBox.Focus();
                }
            } 
            #endregion

            #region Clear()
            /// <summary>
            /// Clear the items in this control
            /// </summary>
            public void Clear()
            {
                // erase the password
                this.PasswordBox.Password = "";
            }
            #endregion

            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            private void Init()
            {
                // set the LabelPasswordAlign
                this.LabelTextAlign = HorizontalAlignment.Right;
            }
            #endregion

            #region Setup(string labelText, string password)
            /// <summary>
            /// This method prepares this control to be shown.
            /// </summary>
            /// <param name="labelText"></param>
            /// <param name="passwordBoxPassword"></param>
            public void Setup(string labelText, string password)
            {
                // set the properties
                this.LabelText = labelText;
                this.Password = password;
            } 
            #endregion

            #region UIEnable()
            /// <summary>
            /// This method determines if the Passwordbox is enabled or not based upon the value
            /// of Editable and Updateable.
            /// </summary>
            private void UIEnable()
            {
                // enabled or not
                bool enabled = this.IsEnabled;
                
                // enable the password box if enabled
                this.PasswordBox.IsEnabled = enabled;
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

                    // enable the DockPanel if the PasswordBox is editable
                    if (this.HasPasswordBoxControl)
                    {
                        // set the value
                        this.PasswordBoxControl.IsEnabled = value;
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

                    // if the PasswordBoxControl exists
                    if (this.HasPasswordBoxControl)
                    {
                        // set the value on the control
                        this.PasswordBoxControl.IsEnabled = enabled;
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
            
            #region HasPasswordBoxControl
            /// <summary>
            /// This property returns true if this object has a 'PasswordBoxControl'.
            /// </summary>
            public bool HasPasswordBoxControl
            {
                get
                {
                    // initial value
                    bool hasPasswordBoxControl = (this.PasswordBoxControl != null);
                    
                    // return value
                    return hasPasswordBoxControl;
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

            #region LabelForeground
            /// <summary>
            /// This property gets or sets the value for 'LabelForeground'.
            /// </summary>
            public Brush LabelForeground
            {
                get { return labelForeground; }
                set
                {
                    // set the value
                    labelForeground = value;

                    // if the control exists
                    if (this.HasLabelControl)
                    {
                        // set the value in the contorl
                        this.LabelControl.Foreground = value;
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

            #region PasswordBoxControl
            /// <summary>
            /// This property returns the PasswordBox
            /// </summary>
            public PasswordBox PasswordBoxControl
            {
                get
                {
                    // return the PasswordBox
                    return this.PasswordBox;
                }
            }
            #endregion

            #region PasswordAlign
            /// <summary>
            /// The alignment for the PasswordAlign.
            /// </summary>
            public HorizontalAlignment PasswordAlign
            {
                get
                {
                    // if the PasswordBox exists
                    if (this.PasswordBox != null)
                    {
                        // set the return value
                        passwordAlign = this.PasswordBox.HorizontalContentAlignment;
                    }

                    // return value
                    return passwordAlign;
                }
                set
                {
                    // set the value
                    passwordAlign = value;

                    // if the PasswordBox exists
                    if (this.PasswordBox != null)
                    {
                        // set the return value
                        this.PasswordBox.HorizontalContentAlignment = value;
                    }
                }
            }
            #endregion

            #region PasswordBoxFontFamily
            /// <summary>
            /// This property gets or sets the value for 'PasswordBoxFont'.
            /// </summary>
            public FontFamily PasswordBoxFontFamily
            {
                get { return passwordBoxFontFamily; }
                set
                {
                    // set the value
                    passwordBoxFontFamily = value;

                    // if the PasswordBoxControl exists
                    if (this.HasPasswordBoxControl)
                    {
                        // set the value on the control
                        this.PasswordBox.FontFamily = value;
                    }
                }
            }
            #endregion

            #region PasswordBoxFontSize
            /// <summary>
            /// This property gets or sets the value for 'PasswordBoxFontSize'.
            /// </summary>
            public double PasswordBoxFontSize
            {
                get { return passwordBoxFontSize; }
                set
                {
                    // set the value
                    passwordBoxFontSize = value;

                    // if the control exists
                    if (this.HasPasswordBoxControl)
                    {
                        // set the value on the control
                        this.PasswordBoxControl.FontSize = value;
                    }
                }
            }
            #endregion

            #region PasswordBoxFontWeight
            /// <summary>
            /// This property gets or sets the value for 'PasswordBoxFontWeight'.
            /// </summary>
            public FontWeight PasswordBoxFontWeight
            {
                get { return passwordBoxFontWeight; }
                set
                {
                    // set the value
                    passwordBoxFontWeight = value;

                    // if the PasswordBoxControl exists
                    if (this.HasPasswordBoxControl)
                    {
                        // set the value on the control
                        this.PasswordBox.FontWeight = value;
                    }
                }
            }
            #endregion

            #region Password
            /// <summary>
            /// The Password for the PasswordBox
            /// </summary>
            public string Password
            {
                get
                {
                    // if the PasswordBox exists
                    if (this.PasswordBox != null)
                    {
                        // set the value
                        this.password = this.PasswordBox.Password;
                    }

                    // return value
                    return password;
                }
                set
                {
                    // set the value
                    password = value;

                    // if the PasswordBox exists
                    if (this.PasswordBox != null)
                    {
                        /// set the PasswordBox password
                        this.PasswordBox.Password = value;
                    }
                }
            }
            #endregion

        #endregion
        
    }
    #endregion

}
