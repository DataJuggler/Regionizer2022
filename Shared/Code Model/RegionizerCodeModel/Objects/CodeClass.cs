

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataJuggler.Regionizer.CodeModel.Util;
using vsDTE = EnvDTE;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region CodeClass : CodeBlock
    /// <summary>
    /// This object represents a Class in a CSharpCodeFile.
    /// </summary>
    public class CodeClass : CodeBlock
    {

        #region Private Variables
        private List<CodePrivateVariable> privateVariables;
        private List<CodeProperty> properties;
        private List<CodeMethod> methods;
        private List<CodeEvent> events;
        private List<CodeConstructor> constructors;
        private CodeLine classDeclarationLine;
        private CodeNotes notes;
        private vsDTE.CodeElement dteClass;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a CodeClass object.
        /// </summary>
        public CodeClass(vsDTE.CodeElement dteClass, CodeLine classDeclarationLine) : base()
        {
            // set the DTEClass
            this.DTEClass = dteClass;

            // set the Class Declaration Line
            this.ClassDeclarationLine = classDeclarationLine;

            // perform initializations for this object
            Init();
        } 
        #endregion

        #region Methods

            #region GetName()
            /// <summary>
            /// This method gets the Name of this class
            /// </summary>
            /// <returns></returns>
            private string GetName()
            {
                // initial value
                string name = "";

                // local
                int classIndex = -1;

                // if hte CodeLines exists
                if (this.ClassDeclarationLine != null)
                {
                    // local copy
                    CodeLine codeLine = this.ClassDeclarationLine;
                    string lineText = codeLine.Text;

                    // if the codeLine has a TextLine
                    if (!String.IsNullOrEmpty(lineText))
                    {
                        // get the index of the word class
                        classIndex = lineText.IndexOf(" class ");

                        // if the classIndex was found
                        if (classIndex >= 0)
                        {
                            // Get the name
                            name = lineText.Substring(classIndex).TrimStart();
                        }
                    }
                }

                // return value
                return name;
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            private void Init()
            {
                // Create all the child objects
                this.Events = new List<CodeEvent>();
                this.Properties = new List<CodeProperty>();
                this.Methods = new List<CodeMethod>();
                this.Constructors = new List<CodeConstructor>();
                this.Notes = new CodeNotes();
            } 
            #endregion

        #endregion

        #region Properties

            #region ClassDeclarationLine
            /// <summary>
            /// This properety gets or sets the ClassDeclarationLine
            /// </summary>
            public CodeLine ClassDeclarationLine
            {
                get { return classDeclarationLine; }
                set { classDeclarationLine = value; }
            } 
            #endregion

            #region Constructors
            /// <summary>
            /// A collection of Constructors
            /// </summary>
            public List<CodeConstructor> Constructors
            {
                get { return constructors; }
                set { constructors = value; }
            } 
            #endregion

            #region DTEClass
            /// <summary>
            /// This property gets or sets the DTE class
            /// </summary>
            public vsDTE.CodeElement DTEClass
            {
                get { return dteClass; }
                set { dteClass = value; }
            } 
            #endregion

            #region Events
            /// <summary>
            /// The Events for this class.
            /// </summary>
            public List<CodeEvent> Events
            {
                get { return events; }
                set { events = value; }
            } 
            #endregion

            #region Methods
            /// <summary>
            /// The Methods for this class.
            /// </summary>
            public List<CodeMethod> Methods
            {
                get { return methods; }
                set { methods = value; }
            } 
            #endregion

            #region Notes
            /// <summary>
            /// A collection of CodeNotes for this class.
            /// </summary>
            public CodeNotes Notes
            {
                get { return notes; }
                set { notes = value; }
            } 
            #endregion

            #region PrivateVariables
            /// <summary>
            /// The Private Variables for this Class.
            /// </summary>
            public List<CodePrivateVariable> PrivateVariables
            {
                get { return privateVariables; }
                set { privateVariables = value; }
            } 
            #endregion

            #region Properties
            /// <summary>
            /// A collection of properties
            /// </summary>
            public List<CodeProperty> Properties
            {
                get { return properties; }
                set { properties = value; }
            } 
            #endregion

        #endregion
        
    } 
    #endregion

}
