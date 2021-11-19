

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class CodeConstructor : CodeBlock
    /// <summary>
    /// This property represents a Constructor in a CSharpClassFile.
    /// </summary>
    public class CodeConstructor : CodeBlock
    {

        #region Private Variables
        private CodeLine constructorDeclarationLine;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a 'CodeConstructor' object.
        /// </summary>
        public CodeConstructor() : base()
        {
            // Set the CodeType to Constructor
            this.CodeType = Enumerations.CodeTypeEnum.Constructor;
        } 
        #endregion
        
        #region Properties

            #region ConstructorDeclarationLine
            /// <summary>
            /// The line that declares a constructor
            /// </summary>
            public CodeLine ConstructorDeclarationLine
            {
                get { return constructorDeclarationLine; }
                set { constructorDeclarationLine = value; }
            } 
            #endregion
            
        #endregion

    } 
    #endregion

}
