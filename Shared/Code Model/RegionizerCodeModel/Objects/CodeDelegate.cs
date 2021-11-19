

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class CodeDelegate : CodeBlock
    /// <summary>
    /// This object represents a delegate in a CSharpClassFile.
    /// </summary>
    public class CodeDelegate : CodeBlock
    {

        #region Private Variables
        private CodeLine delegateDeclarationLine;
        #endregion

        #region CodeDelegate(CodeLine codeLine)
        /// <summary>
        /// Create a new instance of a CodeDelegate and set its codeLine property.
        /// </summary>
        /// <param name="codeLine"></param>
        public CodeDelegate(CodeLine codeLine)
        {
            // set the codeLine
            this.DelegateDeclarationLine = codeLine;
        } 
        #endregion

        #region Properties

            #region DelegateDeclarationLine
            /// <summary>
            /// This is the line that gets written when this Delegate is written out.
            /// </summary>
            public CodeLine DelegateDeclarationLine
            {
                get { return delegateDeclarationLine; }
                set { delegateDeclarationLine = value; }
            } 
            #endregion

            #region HasDeclarationLine
            /// <summary>
            /// This property returns true if thos object has a ClassDeclarationLine.
            /// </summary>
            public bool HasDeclarationLine
            {
                get
                {
                    // initial value
                    bool hasDeclarationLine = (this.DelegateDeclarationLine != null);
                    
                    // return value
                    return hasDeclarationLine;
                }
            } 
            #endregion

        #endregion

    } 
    #endregion

}
