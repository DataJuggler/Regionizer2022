

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataJuggler.Regionizer.CodeModel.Util;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class CSharpCodeFile
    /// <summary>
    /// This class respresents a complete CSharp code document.
    /// </summary>
    public class CSharpCodeFile : CodeBlock
    {

        #region Private Variables
        private List<TextLine> textLines;
        private List<CodeLine> usingStatements;
        private CodeNamespace nameSpace;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a CSharpCodeFile object.
        /// </summary>
        public CSharpCodeFile() : base()
        {
            // create the child objects
            this.TextLines = new List<TextLine>();
            this.UsingStatements = new List<CodeLine>();
        } 
        #endregion

        #region Properties

            #region Namespace
            /// <summary>
            /// The Namespace for this document.
            /// </summary>
            public CodeNamespace Namespace
            {
                get { return nameSpace; }
                set { nameSpace = value; }
            } 
            #endregion

            #region TextLines
            /// <summary>
            /// This property gets or sets a collection of TextLine that make up this document.
            /// </summary>
            public List<TextLine> TextLines
            {
                get { return textLines; }
                set { textLines = value; }
            } 
            #endregion

            #region UsingStatements
            /// <summary>
            /// This property gets or sets the UsingStatements for this object.
            /// </summary>
            public List<CodeLine> UsingStatements
            {
                get { return usingStatements; }
                set { usingStatements = value; }
            } 
            #endregion

        #endregion

    } 
    #endregion

}
