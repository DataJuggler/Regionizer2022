using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class CodeNotes : CodeBlock
    /// <summary>
    /// This object represents all comments that do not belong to a Method,
    /// Property, etc. or can represent a Class, Method, or Property summary.
    /// </summary>
    public class CodeNotes
    {

        #region Private Variables
        private List<CodeLine> codeLines;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new CodeNotes object.
        /// </summary>
        public CodeNotes() 
        {
            // Create the CodeLines
            this.CodeLines = new List<CodeLine>();
        } 
        #endregion

        #region Properties

            #region CodeLines
            /// <summary>
            /// The CodeLines for this object.
            /// </summary>
            public List<CodeLine> CodeLines
            {
                get { return codeLines; }
                set { codeLines = value; }
            } 
            #endregion

        #endregion

    } 
    #endregion

}
