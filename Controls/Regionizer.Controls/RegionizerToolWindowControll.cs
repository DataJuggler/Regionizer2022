

#region using statements

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#endregion

namespace Regionizer.Controls
{

    #region class RegionizerToolWindowControl : UserControl
    /// <summary>
    /// This control is the main window for Regionizer Visual Studio Package
    /// </summary>
    public partial class RegionizerToolWindowControl : UserControl
    {

        #region Constructor
        /// <summary>
        /// Create a new instance of a RegionizerToolWindowControl.
        /// </summary>
        public RegionizerToolWindowControl()
        {
            // Create Controls
            InitializeComponent();
        } 
        #endregion

        #region Events

            #region FormatDocumentButton_Click(object sender, EventArgs e)
            /// <summary>
            /// This method formats this document.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void FormatDocumentButton_Click(object sender, EventArgs e)
            {

            } 
            #endregion

        #endregion

        #region Methods

            public void SetActiveDocument()
            {
                
            }

        #endregion

    } 
    #endregion

}
