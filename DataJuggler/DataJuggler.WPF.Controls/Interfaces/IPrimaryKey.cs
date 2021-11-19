

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DataJuggler.WPF.Controls.Interfaces
{

    #region interface IPrimaryKey
    /// <summary>
    /// This enumeration is used to return the PrimaryKey for any object of IPrimaryKey.
    /// </summary>
    public interface IPrimaryKey
    {

        #region PrimaryKeyValue
        /// <summary>
        /// This object must return a value for the PrimaryKeyID
        /// </summary>
        int PrimaryKeyValue
        {
            get;
        }
        #endregion

    }
    #endregion

}
