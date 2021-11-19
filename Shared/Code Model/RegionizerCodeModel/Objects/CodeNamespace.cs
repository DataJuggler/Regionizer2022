using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vsDTE = EnvDTE;

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region CodeBlock
    /// <summary>
    /// This object represents a Namespace object whish has 
    /// child CodeBlock objects for Delegates, Structures and Classes.
    /// </summary>
    public class CodeNamespace : CodeBlock
    {
        
        #region Private Variables
        private List<CodeClass> classes;
        private List<CodeDelegate> delegates;
        private List<CodeStructure> structures;
        private List<CodeEnumeration> enumerations;
        private CodeNotes notes;
        private CodeLine codeLine;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a CodeNamespace object.
        /// </summary>
        public CodeNamespace(CodeLine codeLine) : base()
        {
            // Create the child objects
            this.Classes = new List<CodeClass>();
            this.Delegates = new List<CodeDelegate>();
            this.Structures = new List<CodeStructure>();
            this.Enumerations = new List<CodeEnumeration>();
            this.Notes = new CodeNotes();
            this.CodeLine = codeLine;
        } 
        #endregion

        #region Methods

            #region CheckIfLineIsInsideAClass(int lineNumber)
            /// <summary>
            /// This method checks a LineNumber to see if it is inside one of the classes
            /// </summary>
            /// <param name="lineNumber"></param>
            /// <returns></returns>
            internal bool CheckIfLineIsInsideAClass(int lineNumber)
            {
                // initial value
                bool insideClass = false;

                // if the Classes exist
                if (this.Classes != null)
                {
                    // iterate the Classes
                    foreach (CodeClass codeClass in this.Classes)
                    {
                        // set the start and end lines
                        int startLine = codeClass.CodeElement.StartPoint.Line;
                        int endLine = codeClass.CodeElement.EndPoint.Line;

                        // if the lineNumer is in the range of this line
                        if ((lineNumber >= startLine) && (lineNumber <= endLine))
                        {
                            // set to true
                            insideClass = true;

                            // break out of the loop
                            break;
                        }
                    }
                }

                // return value
                return insideClass;
            } 
            #endregion

        #endregion

        #region Properties

            #region Classes
            /// <summary>
            /// This property gets or sets the Classes for this object.
            /// </summary>
            public List<CodeClass> Classes
            {
                get { return classes; }
                set { classes = value; }
            } 
            #endregion

            #region CodeLine
            /// <summary>
            /// The CodeLine for this object.
            /// </summary>
            public CodeLine CodeLine
            {
                get { return codeLine; }
                set { codeLine = value; }
            } 
            #endregion

            #region Delegates
            /// <summary>
            /// This property gets or sets the Delegates for this object.
            /// </summary>
            public List<CodeDelegate> Delegates
            {
                get { return delegates; }
                set { delegates = value; }
            } 
            #endregion

            #region Enumerations
            /// <summary>
            /// This property gets or sets the Enumerations for this object.
            /// </summary>
            public List<CodeEnumeration> Enumerations
            {
                get { return enumerations; }
                set { enumerations = value; }
            } 
            #endregion

            #region HasClasses
            /// <summary>
            /// If this object has classes
            /// </summary>
            public bool HasClasses
            {
                get
                {
                    // initial value
                    bool hasClasses = (this.Classes != null);

                    // return value
                    return hasClasses;
                }
            } 
            #endregion

            #region Notes
            /// <summary>
            /// This property gets or sets the Notes for this object.
            /// </summary>
            public CodeNotes Notes
            {
                get { return notes; }
                set { notes = value; }
            } 
            #endregion

            #region Structures
            /// <summary>
            /// This property gets or sets the Structures for this object.
            /// </summary>
            public List<CodeStructure> Structures
            {
                get { return structures; }
                set { structures = value; }
            } 
            #endregion

        #endregion

    } 
    #endregion

}
