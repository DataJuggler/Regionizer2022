

#region using blocks

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataJuggler.Regionizer.CodeModel.Enumerations;
using vsDTE = EnvDTE;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region CodeBlock
    /// <summary>
    /// This class represents a collection of CodeLine objects.
    /// This can represent an entire document, or a namespace,
    /// a class, a method, a property or even a collection of private variables
    /// or even 1 single code block (CodeLine).
    /// </summary>
    public class CodeBlock
    {
        
        #region Private Variables
        private List<CodeLine> codeLines;
        private CodeTypeEnum codeType;
        private List<CodeBlock> children;
        private CodeScopeEnum scope;
        private CodeNotes summary;
        private string name;
        private List<CodeLine> tags;
        private vsDTE.CodeElement codeElement;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a 
        /// </summary>
        public CodeBlock()
        {
            // Create the child objects
            this.CodeLines = new List<CodeLine>();

            // Create the children
            this.Children = new List<CodeBlock>();

            // Create the Summary & Tags
            this.Summary = new CodeNotes();
            this.Tags = new List<CodeLine>();
        } 
        #endregion

        #region Methods

            #region ToString()
            /// <summary>
            /// This method returns the Name of this object.
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                // return the name
                return this.Name;
            } 
            #endregion

        #endregion

        #region Properties

            #region Children
            /// <summary>
            /// This property gets or sets the Children for this object.
            /// A Namespace for instance has Classes, Delegates, Struct
            /// </summary>
            public List<CodeBlock> Children
            {
                get { return children; }
                set { children = value; }
            } 
            #endregion

            #region CodeElement
            /// <summary>
            /// This property gets or sets the CodeElement for this code block.
            /// </summary>
            public vsDTE.CodeElement CodeElement
            {
                get { return codeElement; }
                set { codeElement = value; }
            } 
            #endregion

            #region CodeLines
            /// <summary>
            /// A collection of lines that make up a block of code.
            /// </summary>
            public List<CodeLine> CodeLines
            {
                get { return codeLines; }
                set { codeLines = value; }
            } 
            #endregion

            #region CodeType
            /// <summary>
            /// This property gets or sets the Type of code 
            /// this object represents.
            /// </summary>
            public CodeTypeEnum CodeType
            {
                get { return codeType; }
                set { codeType = value; }
            } 
            #endregion

            #region Name
            /// <summary>
            /// This property gets or sets the Name of this Method, Property, Event etc.
            /// </summary>
            public string Name
            {
                get { return name; }
                set { name = value; }
            } 
            #endregion

            #region Scope
            /// <summary>
            /// This property gets or sets the Scope (Access Modifier) for a CSharpClassFile
            /// </summary>
            public CodeScopeEnum Scope
            {
                get { return scope; }
                set { scope = value; }
            } 
            #endregion

            #region Summary
            /// <summary>
            /// The Summary for this method.
            /// </summary>
            public CodeNotes Summary
            {
                get { return summary; }
                set { summary = value; }
            } 
            #endregion

            #region Tags
            /// <summary>
            /// The Tags for this object.
            /// </summary>
            public List<CodeLine> Tags
            {
                get { return tags; }
                set { tags = value; }
            } 
            #endregion

        #endregion

    } 
    #endregion

}
