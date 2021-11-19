

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{
    
    #region class CodeEvent : CodeBlock
		/// <summary>
    /// This class represents an Event in a CSharp Class File.
    /// </summary>
    public class CodeEvent : CodeBlock
    {

        #region Private Variables
        private CodeLine eventDeclarationLine;
        #endregion

        #region Properties

            #region EventDeclarationLine
            /// <summary>
            /// This property gets or sets the EventDeclarationLine.
            /// </summary>
            public CodeLine EventDeclarationLine
            {
                get { return eventDeclarationLine; }
                set { eventDeclarationLine = value; }
            } 
            #endregion

        #endregion

    } 
	#endregion

}
