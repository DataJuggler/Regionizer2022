

#region using statements

using System;
using System.Collections.Generic;
using System.IO;
using CM = DataJuggler.Regionizer.CodeModel.Objects;
using DataJuggler.Core.UltimateHelper.Objects;
using EnvDTE;
using System.Text.RegularExpressions;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Util
{

    #region class CSharpCodeParser
    /// <summary>
    /// This class is used to parse a CSharpCodeFile into objects.
    /// </summary>
    public class CSharpCodeParser
    {
        
        #region Private Variables
        #endregion
        
        #region Events
            
            #region GetEvents(CM.CSharpCodeFile codeFile, CM.CodeClass codeClass)
            /// <summary>
            /// This method gets the events
            /// </summary>
            /// <param name="codeClass"></param>
            /// <param name="codeClass_2"></param>
            /// <returns></returns>
            private static List<CM.CodeEvent> GetEvents(CM.CSharpCodeFile codeFile, CM.CodeClass codeClass)
            {
                // initial value
                List<CM.CodeEvent> codeEvents = new List<CM.CodeEvent>();
                
                // locals
                CM.CodeEvent codeEvent = null;
                
                try
                {
                    // verify the codeLines exist
                    if ((codeClass != null) && (codeClass.CodeLines != null))
                    {
                        // get all the Constructors
                        List<CodeElement> methods = FileCodeModelHelper.GetMethods(codeClass.DTEClass, false);
                        
                        // iterate the classLines
                        foreach (CodeElement method in methods)
                        {
                            // set the start line and end line
                            int startLine = method.StartPoint.Line;
                            int endLine = method.EndPoint.Line;
                            
                            // get the codeLine
                            CM.CodeLine codeLine = codeFile.CodeLines[startLine - 1];
                            
                            // if this is an event
                            if (codeLine.IsEvent)
                            {
                                // create the event
                                codeEvent = new CM.CodeEvent();
                                
                                // set the name
                                codeEvent.Name = method.Name;
                                
                                // get the summary
                                codeEvent.Summary = GetSummaryAboveLine(codeFile, startLine);
                                
                                // copy the lines
                                codeEvent.CodeLines = CopyLines(codeFile.CodeLines, startLine, endLine);
                                
                                // get the insert index
                                int insertIndex = GetInsertIndex(codeEvents, codeEvent);
                                
                                // if the insertIndex was found
                                if (insertIndex >= 0)
                                {
                                    // insert this item
                                    codeEvents.Insert(insertIndex, codeEvent);
                                }
                                else
                                {
                                    // add this item
                                    codeEvents.Add(codeEvent);
                                }
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                
                // return value
                return codeEvents;
            }
            #endregion
            
            #region GetInsertIndex(List<CM.CodeEvent> codeEvents, CM.CodeEvent codeEvent)
            /// <summary>
            /// This method gets the insert index
            /// </summary>
            /// <param name="codeEvents"></param>
            /// <param name="codeEvent"></param>
            /// <returns></returns>
            public static int GetInsertIndex(List<CM.CodeEvent> codeEvents, CM.CodeEvent codeEvent)
            {
                // if the insertIndex
                int insertIndex = -1;
                
                // start with index
                int index = -1;
                
                // get the insert index
                if ((codeEvents != null) && (codeEvent != null))
                {
                    // iterate the codeEvents
                    foreach (CM.CodeEvent tempCodeEvent in codeEvents)
                    {
                        // increment the index
                        index++;
                        
                        // if this object is at the insert index
                        int compare = String.Compare(tempCodeEvent.Name, codeEvent.Name);
                        
                        // if this is the insert index
                        if (compare >= 0)
                        {
                            // set the return value
                            insertIndex = index;
                            
                            // break out of the loop 
                            break;
                        }
                    }
                }
                
                // return value
                return insertIndex;
            }  
            #endregion
            
        #endregion
        
        #region Methods
            
            #region CopyLines(List<CM.CodeLine> sourceCodeLines, int startLine, int endLine)
            /// <summary>
            /// This method copies the lines from the source lines.
            /// StartLine must be atleast 1
            /// </summary>
            /// <param name="iList"></param>
            /// <param name="startLine"></param>
            /// <param name="endLine"></param>
            /// <returns></returns>
            public static List<CM.CodeLine> CopyLines(List<CM.CodeLine> sourceCodeLines, int startLine, int endLine)
            {
                // initial value
                List<CM.CodeLine> codeLines = new List<CM.CodeLine>();
                
                // if the sourceCodeLines exist
                if  ((sourceCodeLines != null) && (startLine > 0) && (sourceCodeLines.Count >= endLine))
                {
                    // iterate the codeLines
                    for (int lineNumber = startLine; lineNumber <= endLine; lineNumber++)
                    {
                        // set the index
                        int index = lineNumber - 1;
                        
                        // get the codeLine for the current line number
                        CM.CodeLine codeLine = sourceCodeLines[index];
                        
                        // trim the text start
                        codeLine.Text.TrimStart();
                        
                        // add this line
                        codeLines.Add(codeLine);
                    }
                }
                
                // return value
                return codeLines;
            }  
            #endregion
            
            #region CreateClasses(CM.CSharpCodeFile codeFile, CM.CodeNamespace codeNamespace)
            /// <summary>
            /// This method creates the classes for a Namespace
            /// </summary>
            /// <param name="codeNamespace"></param>
            /// <returns></returns>
            private static List<CM.CodeClass> CreateClasses(CM.CSharpCodeFile codeFile, CM.CodeNamespace codeNamespace)
            {
                // initial value
                List<CM.CodeClass> classes = new List<CM.CodeClass>();
                
                try
                {
                    // verify the namespace codeLines exist
                    if ((codeNamespace != null) &&  (codeNamespace.CodeLines != null) && (codeNamespace.CodeElement != null))
                    {
                        // get the class codeElements from EnvDTE
                        List<CodeElement> classCodeElements = FileCodeModelHelper.GetClasses(codeNamespace.CodeElement);
                        
                        // if the codeElements exist
                        if ((classCodeElements != null) && (classCodeElements.Count > 0))
                        {
                            // iterate the classElements
                            foreach (CodeElement classElement in classCodeElements)
                            {
                                // verify the classElement has children
                                if ((classElement != null) && (classElement.Children != null))
                                {
                                    // set the start and end lines
                                    int startLine = classElement.StartPoint.Line;
                                    int endLine = classElement.EndPoint.Line;
                                    
                                    // get the line for the class
                                    List<CM.CodeLine> codeLines = CopyLines(codeFile.CodeLines, startLine, endLine);
                                    
                                    // find the class declaration line in the codeLines
                                    CM.CodeLine classDeclarationLine = FindClassDeclarationLine(codeLines);
                                    
                                    // if the classDeclarationLine exists and the classDeclarationLine is actually a class declaration line
                                    if ((classDeclarationLine != null) && (classDeclarationLine.IsClassDeclarationLine))
                                    {
                                        // create a CodeClass
                                        CM.CodeClass codeClass = new CM.CodeClass(classElement, classDeclarationLine);
                                        
                                        // set the codeEleement
                                        codeClass.CodeElement = classElement;
                                        
                                        // set the name
                                        codeClass.Name = classElement.Name;
                                        
                                        // get the summary
                                        codeClass.Summary = GetSummaryAboveLine(codeFile, startLine);
                                        
                                        // find the Tags
                                        codeClass.Tags = GetTagsAboveLine(codeFile, classDeclarationLine.LineNumber);
                                        
                                        // copy the lines for the class
                                        codeClass.CodeLines = CopyLines(codeFile.CodeLines, startLine, endLine); 
                                        
                                        // add this class
                                        classes.Add(codeClass);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                
                // return value
                return classes;
            }
            #endregion
            
            #region CreateCodeLines(List<TextLine> textLines)
            /// <summary>
            /// This method creates the CodeLines from the TextLines
            /// </summary>
            /// <param name="iList"></param>
            /// <returns></returns>
            public static List<CM.CodeLine> CreateCodeLines(List<TextLine> textLines)
            {
                // initial value
                List<CM.CodeLine> codeLines = new List<CM.CodeLine>();

                // local
                int lineNumber = 0;
                
                // if the textLines exist
                if (textLines != null)
                {
                    // iterate the list
                    foreach (TextLine textLine in textLines)
                    {
                        // Increment the value for lineNumber
                        lineNumber++;

                        // create a code line object
                        CM.CodeLine codeLine = new CM.CodeLine(textLine);

                        // set the LineNumber
                        codeLine.LineNumber = lineNumber;
                        
                        // add this line
                        codeLines.Add(codeLine);
                    }
                }
                
                // return value
                return codeLines;
            }  
            #endregion
            
            #region CreateDelegates(CM.CSharpCodeFile codeFile, CM.CodeNamespace codeNamespace)
            /// <summary>
            /// This method creates the Delegates for a Namespace
            /// </summary>
            /// <param name="codeFile"></param>
            /// <param name="codeNamespace"></param>
            /// <returns></returns>
            private static List<CM.CodeDelegate> CreateDelegates(CM.CSharpCodeFile codeFile, CM.CodeNamespace codeNamespace)
            {
                // initial value
                List<CM.CodeDelegate> delegates = new List<CM.CodeDelegate>();
                
                // get the delegates
                List<CM.CodeDelegate> tempDelegates = new List<CM.CodeDelegate>();
                
                try
                {
                    // verify the namespace codeLines exist
                    if ((codeNamespace != null) && (codeNamespace.CodeLines != null) && (codeFile != null))
                    {   
                        // test if any of the objects 
                        foreach (CM.CodeLine codeLine in codeNamespace.CodeLines)
                        {
                            // if this a Delegate
                            if (codeLine.IsDelegate)
                            {
                                // Create the CodeDelegate
                                CM.CodeDelegate codeDelegate = new CM.CodeDelegate(codeLine);
                                
                                // we need to get the name of the delegate
                                codeDelegate.Name = GetDelegateName(codeDelegate);
                                
                                // check if there is a Summary
                                codeDelegate.Summary = GetSummaryAboveLine(codeFile, codeLine.LineNumber);
                                
                                // add to tempDelegates
                                tempDelegates.Add(codeDelegate);
                            }
                        }
                        
                        // now here we must add the delegates that are not part of a class
                        foreach (CM.CodeDelegate codeDelegate in tempDelegates)
                        {
                            // get the codeLine
                            CM.CodeLine codeLine = codeDelegate.DelegateDeclarationLine;
                            
                            // set the inside class
                            bool insideClass = codeNamespace.CheckIfLineIsInsideAClass(codeLine.LineNumber);
                            
                            // if this line is not inside of a class
                            if (!insideClass)
                            {
                                // get the insert index
                                int insertIndex = GetInsertIndex(delegates, codeDelegate);
                                
                                // if the insertIndex was found
                                if (insertIndex >= 0)
                                {
                                    // insert at the specified index
                                    delegates.Insert(insertIndex, codeDelegate);
                                }
                                else
                                {
                                    // add this delegate
                                    delegates.Add(codeDelegate);
                                }
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                
                // return value
                return delegates;
            }
            #endregion
            
            #region CreateNamespace(CM.CSharpCodeFile codeFile, FileCodeModel fileCodeModel)
            /// <summary>
            /// This method creates the Namespace from the codeFile
            /// </summary>
            /// <param name="codeFile"></param>
            /// <returns></returns>
            private static CM.CodeNamespace CreateNamespace(CM.CSharpCodeFile codeFile, FileCodeModel fileCodeModel)
            {
                // initial value
                CM.CodeNamespace codeNamespace = null;
                
                try
                {
                    // if the codeFile.CodeLines exists
                    if ((codeFile != null) && (codeFile.CodeLines != null))
                    {
                        // find the name space code element
                        CodeElement nameSpace = FileCodeModelHelper.GetNamespace(fileCodeModel);
                        
                        // if the nameSpace was found
                        if ((nameSpace != null) && (nameSpace.StartPoint != null) && (nameSpace.EndPoint != null))
                        {
                            // set the line of the StartPoint
                            int startLine = nameSpace.StartPoint.Line;
                            int endLine = nameSpace.EndPoint.Line;
                            
                            // get the codeLine for the nameSpace
                            CM.CodeLine namespaceCodeLine = codeFile.CodeLines[startLine - 1];
                            
                            // create the name space
                            codeNamespace= new CM.CodeNamespace(namespaceCodeLine);
                            
                            // set the codeElement
                            codeNamespace.CodeElement = nameSpace;
                            
                            // copy the lines
                            codeNamespace.CodeLines = CopyLines(codeFile.CodeLines, startLine, endLine);
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                
                // return value
                return codeNamespace;
            }
            #endregion
            
            #region CreateUsingStatements(CM.CSharpCodeFile codeFile)
            /// <summary>
            /// Create the UsingStatements
            /// </summary>
            /// <param name="codeFile"></param>
            /// <returns></returns>
            private static List<CM.CodeLine> CreateUsingStatements(CM.CSharpCodeFile codeFile)
            {
                // initial value
                List<CM.CodeLine> usingStatements = new List<CM.CodeLine>();
                
                // if there is a codeFile.CodeLines
                if ((codeFile != null) && (codeFile.CodeLines != null) && (codeFile.CodeLines.Count > 0))
                {
                    // iterate the lines
                    foreach (CM.CodeLine codeLine in codeFile.CodeLines)
                    {
                        // if this line is a using statement
                        if (codeLine.IsUsingStatement)
                        {
                            // add this line
                            usingStatements.Add(codeLine);
                        }
                    }
                }
                
                // return value
                return usingStatements;
            }
            #endregion
            
            #region FindClassDeclarationLine(List<CM.CodeLine> codeLines)
            /// <summary>
            /// This method finds a Class Declaration Line in a CodeLine collection
            /// </summary>
            /// <param name="codeLines"></param>
            /// <returns></returns>
            public static CM.CodeLine FindClassDeclarationLine(List<CM.CodeLine> codeLines)
            {
                // initial value
                CM.CodeLine classDeclarationLine = null;
                
                // verify the codeLines exist
                if (codeLines != null)
                {
                    // iterate the codeLines
                    foreach (CM.CodeLine codeLine in codeLines)
                    {
                        // if this is a Class Declaration Line
                        if (codeLine.IsClassDeclarationLine)
                        {
                            // set the return value
                            classDeclarationLine = codeLine;
                            
                            // break out of the loop
                            break;
                        }
                    }
                }
                
                // return value
                return classDeclarationLine;
            } 
            #endregion
            
            #region GetConstructors(CM.CSharpCodeFile codeFile, CM.CodeClass codeClass)
            /// <summary>
            /// This loads a collection of Constructors found in the codeClass passed in.
            /// </summary>
            /// <param name="codeClass"></param>
            /// <returns></returns>
            private static List<CM.CodeConstructor> GetConstructors(CM.CSharpCodeFile codeFile, CM.CodeClass codeClass)
            {
                // initial value
                List<CM.CodeConstructor> constructors = new List<CM.CodeConstructor>();
                
                // locals
                CM.CodeConstructor constructor = null;
                
                try
                {
                    // verify the codeLines exist
                    if ((codeClass != null) && (codeClass.CodeLines != null))
                    {
                        // get all the Constructors
                        List<CodeElement> methods = FileCodeModelHelper.GetMethods(codeClass.DTEClass, true);
                        
                        // iterate the classLines
                        foreach (CodeElement method in methods)
                        {
                            // set the start line and end line
                            int startLine = method.StartPoint.Line;
                            int endLine = method.EndPoint.Line;
                            
                            // create the constructor
                            constructor = new CM.CodeConstructor();
                            
                            // set the name
                            constructor.Name = method.Name;
                            
                            // get the summary
                            constructor.Summary = GetSummaryAboveLine(codeFile, startLine);
                            
                            // copy the lines
                            constructor.CodeLines = CopyLines(codeFile.CodeLines, startLine, endLine);
                            
                            // add this item
                            constructors.Add(constructor);
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                
                // return value
                return constructors;
            } 
            #endregion
            
            #region GetDelegateName(CM.CodeDelegate codeDelegate)
            /// <summary>
            /// This method gets the Delegate name from the delegate passed in.
            /// </summary>
            /// <param name="codeDelegate"></param>
            /// <returns></returns>
            private static string GetDelegateName(CM.CodeDelegate codeDelegate)
            {
                // initial value (in case the parsing fails)
                string delegateName = "Delegate";
                
                // set the words
                List<Word> words = CSharpCodeParser.ParseWords(codeDelegate.DelegateDeclarationLine.Text);
                
                // if the words exist
                foreach(Word word in words)
                {
                    // if this is the delegate
                    if (word.Text.Contains("("))
                    {
                        // trim the string
                        string temp = word.Text.Trim();
                        
                        // get the index
                        int index = temp.IndexOf("(");
                        
                        // if the index >= 0
                        if (index >= 0)
                        {
                            // set the return value
                            delegateName = temp.Substring(0, index);
                        }
                    }
                }
                
                // return value
                return delegateName;
            } 
            #endregion
            
            #region GetInsertIndex(List<CM.CodeProperty> codeProperties, CM.CodeProperty codeProperty)
            /// <summary>
            /// Thid property gets the Insert Index
            /// </summary>
            /// <param name="codeProperty"></param>
            /// <returns></returns>
            public static int GetInsertIndex(List<CM.CodeProperty> codeProperties, CM.CodeProperty codeProperty)
            {
                // initial value
                int insertIndex = -1;
                
                // start with index
                int index = -1;
                
                // get the insert index
                if ((codeProperties != null) && (codeProperty != null))
                {
                    // iterate the property
                    foreach (CM.CodeProperty tempCodeProperty in codeProperties)
                    {
                        // increment the index
                        index++;
                        
                        // if this object is at the insert index
                        int compare = String.Compare(tempCodeProperty.Name, codeProperty.Name);
                        
                        // if this is the insert index
                        if (compare >= 0)
                        {
                            // set the return value
                            insertIndex = index;
                            
                            // break out of the loop 
                            break;
                        }
                    }
                }
                
                // return value
                return insertIndex;
            }
            #endregion
            
            #region GetInsertIndex(List<CM.CodeMethod> codeMethods, CM.CodeMethod codeMethod)
            /// <summary>
            /// Thid method gets the Insert Index
            /// </summary>
            /// <param name="codeMethods"></param>
            /// <returns></returns>
            public static int GetInsertIndex(List<CM.CodeMethod> codeMethods, CM.CodeMethod codeMethod)
            {
                // initial value
                int insertIndex = -1;
                
                // start with index
                int index = -1;
                
                // get the insert index
                if  ((codeMethods != null) && (codeMethod != null))
                {
                    foreach (CM.CodeMethod tempCodeMethod in codeMethods)
                    {
                        // increment the index
                        index++;
                        
                        // if this object is at the insert index
                        int compare = String.Compare(tempCodeMethod.Name, codeMethod.Name);
                        
                        // if this is the insert index
                        if (compare >= 0)
                        {
                            // set the return value
                            insertIndex = index;
                            
                            // break out of the loop 
                            break;           
                        }
                    }
                }
                
                // return value
                return insertIndex;
            }  
            #endregion
            
            #region GetInsertIndex(List<CM.CodeDelegate> delegates, CM.CodeDelegate codeDelegate)
            /// <summary>
            /// This method gets the insert index for this codeDelegate
            /// </summary>
            /// <param name="delegates"></param>
            /// <param name="codeDelegate"></param>
            /// <returns></returns>
            public static int GetInsertIndex(List<CM.CodeDelegate> delegates, CM.CodeDelegate codeDelegate)
            {
                // initial value
                int insertIndex = -1;
                
                // local
                int index = -1;
                
                // verify both objects exist
                if ((delegates != null) && (codeDelegate != null))
                {
                    // iterate the codeDelegates
                    foreach (CM.CodeDelegate tempCodeDelegate in delegates)
                    {
                        // increment the index
                        index++;
                        
                        // if this object is at the insert index
                        int compare = String.Compare(tempCodeDelegate.Name, codeDelegate.Name);
                        
                        // if this is the insert index
                        if (compare >= 0)
                        {
                            // set the return value
                            insertIndex = index;
                            
                            // break out of the loop 
                            break;
                        }
                    }
                }
                
                // return value
                return insertIndex;
            } 
            #endregion
            
            #region GetMethods(CM.CSharpCodeFile codeFile, CM.CodeClass codeClass)
            /// <summary>
            /// This method gets the properties from this class
            /// </summary>
            /// <param name="codeFile"></param>
            /// <param name="codeClass"></param>
            /// <returns></returns>
            private static List<CM.CodeMethod> GetMethods(CM.CSharpCodeFile codeFile, CM.CodeClass codeClass)
            {
                // initial value
                List<CM.CodeMethod> codeMethods = new List<CM.CodeMethod>();
                
                // locals
                CM.CodeMethod codeMethod = null;
                
                try
                {
                    // verify the codeLines exist
                    if ((codeClass != null) && (codeClass.CodeLines != null))
                    {
                        // get all the Constructors
                        List<CodeElement> methods = FileCodeModelHelper.GetMethods(codeClass.DTEClass, false);
                        
                        // iterate the classLines
                        foreach (CodeElement method in methods)
                        {
                            // set the start line and end line
                            int startLine = method.StartPoint.Line;
                            int endLine = method.EndPoint.Line;
                            
                            // get the codeLine
                            CM.CodeLine codeLine = codeFile.CodeLines[startLine - 1];
                            
                            // if this is not an Event (than its a method ? )
                            if (!codeLine.IsEvent)
                            {
                                // create the method
                                codeMethod = new CM.CodeMethod();
                                
                                // set the name
                                codeMethod.Name = method.Name;
                                
                                // get the summary
                                codeMethod.Summary = GetSummaryAboveLine(codeFile, startLine);
                                
                                // copy the lines
                                codeMethod.CodeLines = CopyLines(codeFile.CodeLines, startLine, endLine);
                                
                                // int index
                                int index = GetInsertIndex(codeMethods, codeMethod);
                                
                                // int index was found
                                if (index >= 0)
                                {
                                    // add this item
                                    codeMethods.Insert(index, codeMethod);
                                }
                                else
                                {
                                    // add this item
                                    codeMethods.Add(codeMethod);
                                }
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                
                // return value
                return codeMethods;
            }
            #endregion
            
            #region GetPrivateVariables(CM.CodeClass codeClass)
            /// <summary>
            /// This method gets the Private Variables from a Class
            /// </summary>
            /// <param name="codeClass"></param>
            /// <returns></returns>
            private static List<CM.CodePrivateVariable> GetPrivateVariables(CM.CodeClass codeClass)
            {
                // initial value
                List<CM.CodePrivateVariable> privateVariables = new List<CM.CodePrivateVariable>();
                
                try
                {
                    // if the codeClass.CodeLines exists
                    if ((codeClass != null) && (codeClass.CodeLines != null))
                    {
                        // iterate the codeLines
                        foreach (CM.CodeLine codeLine in codeClass.CodeLines)
                        {
                            // if this is a private variable
                            if (codeLine.IsPrivateVariable)
                            {
                                // create the private variable
                                CM.CodePrivateVariable privateVariable = new CM.CodePrivateVariable(codeLine.TextLine);
                                
                                // add this line
                                privateVariables.Add(privateVariable);
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                
                // return value
                return privateVariables;
            } 
            #endregion
            
            #region GetProperties(CM.CSharpCodeFile codeFile, CM.CodeClass codeClass)
            /// <summary>
            /// This method gets the Properties for this class
            /// </summary>
            /// <param name="codeFile"></param>
            /// <param name="codeClass"></param>
            /// <returns></returns>
            private static List<CM.CodeProperty> GetProperties(CM.CSharpCodeFile codeFile, CM.CodeClass codeClass)
            {
                // initial value
                List<CM.CodeProperty> codeProperties = new List<CM.CodeProperty>();
                
                // locals
                CM.CodeProperty codeProperty = null;
                
                try
                {
                    // verify the codeLines exist
                    if ((codeClass != null) && (codeClass.CodeLines != null))
                    {
                        // get all the Constructors
                        List<CodeElement> properties = FileCodeModelHelper.GetProperties(codeClass.DTEClass);
                        
                        // iterate the classLines
                        foreach (CodeElement property in properties)
                        {
                            // set the start line and end line
                            int startLine = property.StartPoint.Line;
                            int endLine = property.EndPoint.Line;
                            
                            // get the codeLine
                            CM.CodeLine codeLine = codeFile.CodeLines[startLine - 1];
                            
                            // if this is a Tag
                            if (codeLine.IsTag)
                            {
                                do
                                {
                                    // set the codeLine
                                    codeLine = codeFile.CodeLines[startLine];
                                    
                                    // if the codeLine is a Tag
                                    if (codeLine.IsTag)
                                    {
                                        // increment the index
                                        startLine++;
                                    }    
                                    else
                                    {
                                        // break out of the loop
                                        break;
                                    }
                                    } while (true);
                                }
                                
                                // create the property
                                codeProperty = new CM.CodeProperty();
                                
                                // set the name
                                codeProperty.Name = property.Name;
                                
                                // get the summary
                                codeProperty.Summary = GetSummaryAboveLine(codeFile, startLine);
                                
                                // get the Tags
                                codeProperty.Tags = GetTagsAboveLine(codeFile, startLine);
                                
                                // copy the lines
                                codeProperty.CodeLines = CopyLines(codeFile.CodeLines, startLine, endLine);
                                
                                // get the insert index
                                int insertIndex = GetInsertIndex(codeProperties, codeProperty);
                                
                                // if the insertIndex was found
                                if (insertIndex >= 0)
                                {
                                    // insert this item
                                    codeProperties.Insert(insertIndex, codeProperty);
                                }
                                else
                                {
                                    // add this item
                                    codeProperties.Add(codeProperty);
                                }
                            }
                        }
                    }
                    catch (Exception error)
                    {
                        // for debugging only
                        string err = error.ToString();
                    }
                    
                    // return value
                    return codeProperties;
                } 
                #endregion
                
            #region GetSummaryAboveLine(CM.CSharpCodeFile codeFile, int lineNumber)
            /// <summary>
            /// This method gets the Summary above a line.
            /// This can be for a Class, Method, Property, Event, etc.
            /// </summary>
            /// <param name="codeFile"></param>
            /// <param name="p"></param>
            /// <returns></returns>
            public static CM.CodeNotes GetSummaryAboveLine(CM.CSharpCodeFile codeFile, int lineNumber)
            {
                // initial value
                CM.CodeNotes codeNotes = new CM.CodeNotes();
                    
                // temporary value to hold the initial item
                CM.CodeNotes tempCodeNotes = new CM.CodeNotes();
                    
                try
                {
                    // verify the codeFile exists
                    if ((codeFile != null) && (codeFile.CodeLines != null))
                    {
                        // if the lineNumber > 2
                        if (lineNumber >= 2)
                        {
                            // we need the lineNumber below the current line number (and our LineNumber is 1 based not 0 based)
                            lineNumber -= 2;
                                
                            // contine while the lineNumber is higher than 0
                            while (lineNumber > 0)
                            {
                                // get the CodeFile at this index
                                CM.CodeLine codeLine = codeFile.CodeLines[lineNumber];
                                    
                                // if the codeLine is a Tag
                                if (codeLine.IsTag)
                                {
                                    // do nothing for Tags, continue on for Summaries
                                }
                                else if (codeLine.IsComment)
                                {
                                    // set the text
                                    codeLine.Text = codeLine.Text.TrimStart();
                                        
                                    // add this Tag
                                    tempCodeNotes.CodeLines.Add(codeLine);
                                }
                                else
                                {
                                    // break out of the loop
                                    break;
                                }
                                    
                                // Modify the condition to force the exit
                                lineNumber--;
                            }
                        }
                    }
                        
                    // if there are one or more code
                    if ((tempCodeNotes != null) && (tempCodeNotes.CodeLines.Count > 0))
                    {
                        // iterate the  tempCodeNotes
                        for(int x = tempCodeNotes.CodeLines.Count - 1; x >= 0; x--)
                        {
                            // get this codeLine
                            CM.CodeLine codeLine = tempCodeNotes.CodeLines[x];
                                
                            // Add this item
                            codeNotes.CodeLines.Add(codeLine);
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                    
                // return value
                return codeNotes;
            } 
            #endregion
                
            #region GetTagsAboveLine(CM.CSharpCodeFile codeFile, int lineNumber)
            /// <summary>
            /// This method returns a collection of CodeTag objects
            /// </summary>
            /// <param name="?"></param>
            /// <param name="lineNumber"></param>
            /// <returns></returns>
            public static List<CM.CodeLine> GetTagsAboveLine(CM.CSharpCodeFile codeFile, int lineNumber)
            {
                // initial value
                List<CM.CodeLine> codeTags = new List<CM.CodeLine>();
                    
                // temporary value to hold tags
                List<CM.CodeLine> tempCodeTags = new List<CM.CodeLine>();
                    
                try
                {
                    // verify the codeFile exists
                    if ((codeFile != null) && (codeFile.CodeLines != null))
                    {
                        // if the lineNumber > 2
                        if (lineNumber >= 2)
                        {
                            // we need the lineNumber below the current line number (and our LineNumber is 1 based not 0 based)
                            lineNumber -= 2;
                                
                            // contine while the lineNumber is higher than 0
                            while (lineNumber > 0)
                            {
                                // get the CodeFile at this index
                                CM.CodeLine codeLine = codeFile.CodeLines[lineNumber];
                                    
                                // if the codeLine is a Tag
                                if (codeLine.IsTag)
                                {
                                    // set the text
                                    codeLine.Text = codeLine.Text.TrimStart();
                                        
                                    // add this Tag
                                    tempCodeTags.Add(codeLine);
                                }
                                else
                                {
                                    // break out of the loop
                                    break;
                                }
                                    
                                // Modify the condition to force the exit
                                lineNumber--;
                            }
                        }
                    }
                        
                    // if the codeTags exist
                    if ((tempCodeTags != null) && (tempCodeTags.Count > 0))
                    {
                        // iterate the  tempCodeNotes
                        for (int x = tempCodeTags.Count - 1; x >= 0; x--)
                        {
                            // get this codeLine
                            CM.CodeLine codeLine = tempCodeTags[x];
                                
                            // Add this item
                            codeTags.Add(codeLine);
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                    
                // return value
                return codeTags;
            } 
            #endregion
                
            #region ParseCSharpCodeFile(string documentText, FileCodeModel fileCodeModel)
            /// <summary>
            /// This method creates the CodeLines from the TextLines passed in.
            /// </summary>
            /// <param name="textLines"></param>
            public static CM.CSharpCodeFile ParseCSharpCodeFile(string documentText, FileCodeModel fileCodeModel)
            {
                // initial value
                CM.CSharpCodeFile codeFile = new CM.CSharpCodeFile();
                    
                try
                {
                    // if the textLines exist
                    if (!String.IsNullOrEmpty(documentText))
                    {
                        // set the textLines
                        codeFile.TextLines = ParseLines(documentText);
                            
                        // Create the CodeLines from the TextLines
                        codeFile.CodeLines = CreateCodeLines(codeFile.TextLines);
                            
                        // Create the using statements
                        codeFile.UsingStatements = CreateUsingStatements(codeFile);
                            
                        // Create the Namespace object
                        codeFile.Namespace = CreateNamespace(codeFile, fileCodeModel);
                            
                        // if the nameSpace exists
                        if  ((codeFile.Namespace != null) && (codeFile.Namespace.CodeLines != null))
                        {
                            // test only
                            int linesCount = codeFile.Namespace.CodeLines.Count;
                                
                            // Update: The classes must be added before the Delegates so the Delegates
                            //               can be checked to be inside a class or not;
                            //               If a delegate is inside a class the delegate must get written with the class
                            //               else the delegate must be written with the namespace
                                
                            // Create the classes for this codeFile
                            codeFile.Namespace.Classes = CreateClasses(codeFile, codeFile.Namespace);
                                
                            // Get any delegates in the Namespace outside of a Class
                            codeFile.Namespace.Delegates = CreateDelegates(codeFile, codeFile.Namespace);
                                
                            // if the codeFile.Namespace.Classes exist
                            if ((codeFile.Namespace.Classes != null) && (codeFile.Namespace.Classes.Count > 0))
                            {
                                // now we have the classes, we must revisit each Class to get the 
                                // content of each class now that we have the CodeLine objects.
                                foreach (CM.CodeClass codeClass in codeFile.Namespace.Classes)
                                {
                                    // test only
                                    int count = codeClass.CodeLines.Count;
                                        
                                    // get the name of the class for debugging only
                                    string className = codeClass.Name;
                                        
                                    // verify the class has lines
                                    if ((codeClass.CodeLines != null) && (codeClass.CodeLines.Count > 0))
                                    {
                                        // set the Private Variables
                                        codeClass.PrivateVariables = GetPrivateVariables(codeClass);
                                            
                                        // set the constructors
                                        codeClass.Constructors = GetConstructors(codeFile, codeClass);
                                            
                                        // set the events
                                        codeClass.Events = GetEvents(codeFile, codeClass);
                                            
                                        // set the properties
                                        codeClass.Methods = GetMethods(codeFile, codeClass);
                                            
                                        // set the properties
                                        codeClass.Properties = GetProperties(codeFile, codeClass);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                    
                // return value
                return codeFile;
            }
            #endregion
                
            #region ParseLines(string sourceText)
            /// <summary>
            /// This method parses the lines for a string.
            /// This could be a document or a code block.
            /// </summary>
            /// <param name="sourceText"></param>
            /// <returns></returns>
            public static List<TextLine> ParseLines(string sourceText)
            {
                List<TextLine> textLines = new List<TextLine>();
                    
                try
                {
                    // view the reader
                    using (StringReader reader = new StringReader(sourceText))
                    {
                        // LineNumber starts a 1 not 0
                        int lineNumber = 0;
                        string line;
                            
                        // read all lines in the string
                        while ((line = reader.ReadLine()) != null)
                        {
                            // increment lineNumber (LineNumber starts a 1 not 0)
                            lineNumber++;

                            // attempting to fix a bug with CreatePrivateVariables and FormatDocument
                            // where if a line has spadces at the end, it causes the parser to mess up
                            line = line.TrimEnd();
                                
                            // Create a text line
                            TextLine textLine = new TextLine(line, lineNumber);    
                                
                            // add this line
                            textLines.Add(textLine);
                        }
                    }
                        
                }
                catch(Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                    
                return textLines;
            } 
            #endregion
                
            #region ParseMethodNameFromText(CM.CodeLine codeLine)
            /// <summary>
            /// This method parses out the method name from the text.
            /// </summary>
            public static string ParseMethodNameFromText(CM.CodeLine codeLine)
            {
                // initial value
                string methodName = "";

                // if the codeLine exists
                if (codeLine != null)
                {
                    // trim the text
                    string temp = codeLine.Text.Trim();

                    // parse out the words
                    List<Word> words = ParseWords(temp);

                    // if the words exist
                    if  (words != null)
                    {
                        // iterate the words
                        foreach (Word word in words)
                        {
                            // if this word contains a 
                            if (word.Text.Contains("("))
                            {
                                // this is the word we want
                                int index = word.Text.IndexOf("(");

                                // if the index was found
                                if (index > 0)
                                {
                                    // set the name
                                    methodName = word.Text.Substring(0, index);
                                }
                            }
                        }
                    }
                }

                // return value
                return methodName;
            }
            #endregion
                
            #region ParseWords(string lineText)
            /// <summary>
            /// Parse the words for a line of text
            /// </summary>
            /// <param name="lineText"></param>
            /// <returns></returns>
            public static List<Word> ParseWords(string lineText)
            {
                // initial value
                List<Word> words = new List<Word>();
                    
                try
                {
                    // parse the words
                    string[] tempWords = lineText.Split(' ');
                        
                    // iterate the words
                    foreach (string tempWord in tempWords)
                    {
                        // do not add blank words
                        if (!String.IsNullOrEmpty(tempWord))
                        {
                            // Create a word
                            Word word = new Word(tempWord);
                                
                            // add a word
                            words.Add(word);
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                    
                // return value
                return words;
            }
            #endregion
                
            #region ParseWordsByCapitalLetters(string sourceWord)
            /// <summary>
            /// This method parses a word by its capital letters.
            /// </summary>
            /// <param name="sourceWord"></param>
            /// <returns></returns>
            public static List<Word> ParseWordsByCapitalLetters(string sourceWord)
            {
                // initial value
                List<Word> words = new List<Word>();

                try
                {
                    var regEx = new Regex(@" (?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | 
                        (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

                    // Get the temp string
                    string temp = String.Format("YYY{0}ZZZ", regEx.Replace(sourceWord, " ")); 

                    // replace out the ZZZ (why it ends up here I do not know
                    temp = temp.Replace("ZZZ", "");
                    temp = temp.Replace("YYY", "");

                    // now call the parse words
                    if (!String.IsNullOrEmpty(temp))
                    {
                        // parse the words
                        words = ParseWords(temp);
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }

                // return value
                return words;
            }
            #endregion
            
        #endregion
            
    }
    #endregion
    
}
