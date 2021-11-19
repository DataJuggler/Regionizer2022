

#region using statements

using System.Windows.Controls;

#endregion

namespace DataJuggler.WPF.Controls.Interfaces
{

    #region interface ISelectedIndexListener
    /// <summary>
    /// This is used by controls that use that host the LabelComboControl (or others later)
    /// </summary>
    public interface ISelectedIndexListener
    {

        #region Methods

            #region OnSelectedIndexChanged(Control control, int selectedIndex, object selectedItem);
            /// <summary>
            /// The Selected has changed.
            /// </summary>
            /// <param name="control"></param>
            /// <param name="selectedIndex"></param>
            void OnSelectedIndexChanged(Control control, int selectedIndex, object selectedItem);
            #endregion

        #endregion

    } 
    #endregion
    
}
