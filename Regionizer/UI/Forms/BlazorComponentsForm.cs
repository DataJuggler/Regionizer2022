

#region using statements

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataJuggler.Regionizer;
using Regionizer;

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
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'BlazorComponentsForm' object.
        /// </summary>
        public BlazorComponentsForm()
        {
            // Create Controls
            InitializeComponent();
        }
        #endregion
        
        #region Events
            
        #region AddCalendarButton_Click(object sender, EventArgs e)
        /// <summary>
        /// event is fired when the 'AddCalendarButton' is clicked.
        /// </summary>
        private void AddCalendarButton_Click(object sender, EventArgs e)
        {
            if (HasMainWindowCallback)
            {
                // Test only
                MainWindowCallback.AddBlazorComponent("Added from Regionizer 2022");
            }
        }
        #endregion
        
        #endregion
        
        #region Methods
            
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
