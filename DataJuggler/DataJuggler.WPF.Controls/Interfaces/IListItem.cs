

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DataJuggler.WPF.Controls.Interfaces
{

    #region interface IListItem
    /// <summary>
    /// This interface is used to make any object have a DefaultItem property
    /// so that a list can have a preselected index
    /// </summary>
    public interface IListItem
    {
        
        #region Properties

            #region DefaultItem
            /// <summary>
            /// Is this object the default item?
            /// </summary>
            bool DefaultItem
            {
                get;
                set;
            } 
            #endregion

            #region DisplayName
            /// <summary>
            /// The name to display for this list item.
            /// </summary>
            string DisplayName
            {
                get;
                set;
            }
            #endregion

            #region PrimaryID
            /// <summary>
            /// The PrimaryKey of this object.
            /// </summary>
            int PrimaryID
            {
                get;
            }
            #endregion
        
        #endregion
        
    } 
    #endregion
    
}
