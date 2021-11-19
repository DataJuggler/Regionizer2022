

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

namespace DataJuggler.WPF.Controls
{

    #region class LabelCheckBoxControl
    /// <summary>
    /// Interaction logic for LabelCheckBoxControl.xaml
    /// </summary>
    public partial class LabelCheckBoxControl : UserControl
    {
        
        #region Private Variables
        private int labelWidth;
        private Brush labelForeground;
        private FontFamily labelFontFamily;
        private FontWeight labelFontWeight;
        private double labelFontSize;
        private OnCheckChangedHandler checkChangedHandler;
        private bool isChecked;
        private string labelText;
        #endregion
        
        #region Events
     
            #region CheckBox_Checked(object sender, EventArgs e)
            /// <summary>
            /// This event is fired when Check Box _ Checked
            /// </summary>
            public void CheckBox_Checked(object sender, EventArgs e)
            {
                // if the CheckedChangedHandler exists
                if (this.HasCheckChangedHandler)
                {
                    // return the value
                    this.CheckChangedHandler(this, this.IsChecked);
                }
            }
            #endregion
            
        #endregion
        
        #region Constructor
        /// <summary>
        /// This 
        /// </summary>
        public LabelCheckBoxControl()
        {
            // Create controls
            InitializeComponent();
        }
        #endregion

        #region Properties

            #region CheckChangedHandler
            /// <summary>
            /// This property gets or sets the value for 'CheckChangedHandler'.
            /// </summary>
            public OnCheckChangedHandler CheckChangedHandler
            {
                get { return checkChangedHandler; }
                set { checkChangedHandler = value; }
            }
            #endregion
            
            #region HasCheckChangedHandler
            /// <summary>
            /// This property returns true if this object has a 'CheckChangedHandler'.
            /// </summary>
            public bool HasCheckChangedHandler
            {
                get
                {
                    // initial value
                    bool hasCheckChangedHandler = (this.CheckChangedHandler != null);
                    
                    // return value
                    return hasCheckChangedHandler;
                }
            }
            #endregion
            
            #region HasLabel
            /// <summary>
            /// This property returns true if this object has a 'Label'.
            /// </summary>
            public bool HasLabel
            {
                get
                {
                    // initial value
                    bool hasLabel = (this.Label != null);
                    
                    // return value
                    return hasLabel;
                }
            }
            #endregion

            #region IsChecked
            /// <summary>
            /// This property gets or sets the value for 'IsChecked'.
            /// </summary>
            public bool IsChecked
            {
                get 
                {
                    // if the CheckBox exists
                    if (this.CheckBox != null)
                    {
                        // set the return value
                        isChecked = (bool) this.CheckBox.IsChecked;
                    }

                     // return value
                     return isChecked;
                }
                set 
                { 
                    // set the value
                    isChecked = value;

                    // if the CheckBox exists
                    if (this.CheckBox != null)
                    {
                        // set the value
                        this.CheckBox.IsChecked = value;
                    }
                }
            }
            #endregion
            
            #region LabelFontFamily
            /// <summary>
            /// This property gets or sets the value for 'LabelFontFamily'.
            /// </summary>
            public FontFamily LabelFontFamily
            {
                get { return labelFontFamily; }
                set 
                { 
                    // set the value
                    labelFontFamily = value;

                    // if the Label exists
                    if (this.HasLabel)
                    {
                        // set the value
                        this.Label.FontFamily = value;
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
                    if (this.HasLabel)
                    {   
                        // set the value
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

                    // if the Label exists
                    if (this.HasLabel)
                    {
                        // set the value
                        this.Label.FontWeight = value;
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

                    // if the label exists
                    if (this.HasLabel)
                    {
                        // set the value in the label
                        this.Label.Foreground = value;
                    }
                }
            }
            #endregion
            
            #region LabelText
            /// <summary>
            /// This property gets or sets the value for 'LabelText'.
            /// </summary>
            public string LabelText
            {
                get { return labelText; }
                set 
                { 
                    // set the value
                    labelText = value;

                    // if the Label exists
                    if (this.HasLabel)
                    {
                        // set the value
                        this.Label.Content = value;
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
            
        #endregion
        
    }
    #endregion

}
