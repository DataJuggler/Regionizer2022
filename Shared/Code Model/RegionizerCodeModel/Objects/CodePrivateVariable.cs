

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class CodePrivateVariable : CodeLine
    /// <summary>
    /// This class represents a PrivateVariable in a Class File.
    /// </summary>
    public class CodePrivateVariable : CodeLine
    {

        #region Constructor
        /// <summary>
        /// Create a Private Variable object
        /// </summary>
        /// <param name="textLine"></param>
        public CodePrivateVariable(TextLine textLine) : base(textLine)
        {

        } 
        #endregion

    } 
    #endregion

}
