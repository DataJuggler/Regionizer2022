

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Enumerations
{

    #region enum CodeScopeEnum
    /// <summary>
    /// This enum represents the AccessModifier for a CodeLine or CodeBlock object.
    /// </summary>
    public enum CodeScopeEnum : int
    {
        Private = 0,
        Protected = 1,
        ProtectedInternal = 2,
        Internal = 3,
        Public = 4
    } 
    #endregion

}
