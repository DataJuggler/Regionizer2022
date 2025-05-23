﻿

#region using statements

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DataJuggler.Regionizer.CodeModel.Enumerations;
using DataJuggler.Regionizer.CodeModel.Util;
using EnvDTE;
using CM = DataJuggler.Regionizer.CodeModel.Objects;
using DataJuggler.Regionizer.Commenting.Inspectors;
using DataJuggler.Regionizer.CodeModel.Objects;
using DataJuggler.Core.UltimateHelper;
using DataJuggler.Core.UltimateHelper.Objects;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using MessageBoxButtons = System.Windows.MessageBoxButton;
using MessageBoxOptions = System.Windows.MessageBoxOptions;
using static System.Net.Mime.MediaTypeNames;

#endregion

namespace DataJuggler.Regionizer
{

    #region class RegionizerCodeManager
    /// <summary>
    /// This class is used to format and manipulate CSharp code files
    /// </summary>
    public class RegionizerCodeManager
    {
        
        #region Private Variables
        private EnvDTE.Document activeDocument;
        private int indent;
        private CM.CommentDictionary commentDictionairy;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new RegionizerCodeManager object.
        /// </summary>
        /// <param name="document"></param>
        public RegionizerCodeManager(EnvDTE.Document document)
        {
            // Set the ActiveDocument
            ActiveDocument = document;
        } 
        #endregion
        
        #region Events
            
            #region AddEvents(IList<CM.CodeEvent> events)
            /// <summary>
            /// This method writes out the events in the file.
            /// </summary>
            /// <param name="events"></param>
            private void AddEvents(IList<CM.CodeEvent> events)
            {
                // if there are one or more events
                if (events != null)
                {
                    // Begin a region
                    BeginRegion("Events");
                    
                    // increase indent
                    Indent++;
                    
                    // Add a Blank Line
                    AddBlankLine();
                    
                    if (ListHelper.HasOneOrMoreItems(events.ToList()))
                    {
                        // iterate the events
                        foreach (CM.CodeEvent codeEvent in events)
                        {
                            // write out this Event
                            WriteEvent(codeEvent, true);
                        }
                    }
                    
                    // decrease indent
                    Indent--;
                    
                    // Close the Region
                    EndRegion();
                    
                    // Add a Blank Line
                    AddBlankLine();
                }
            }
            #endregion
            
            #region GetEventInsertLineNumber(string eventName, CM.CSharpCodeFile codeFile)
            /// <summary>
            /// This method gets the line number to insert an Event
            /// </summary>
            /// <param name="eventName"></param>
            /// <param name="codeFile"></param>
            /// <returns></returns>
            private int GetEventInsertLineNumber(string eventName, CM.CSharpCodeFile codeFile)
            {
                // initial value
                int insertLineNumber = 0;
                
                // locals
                bool eventRegionStarted = false;
                int openRegionCount = 0;
                
                // verify the codeFile exists
                if ((codeFile != null) && (codeFile.CodeLines != null))
                {
                    // iterate the files
                    foreach (CM.CodeLine codeLine in codeFile.CodeLines)
                    {
                        // if a event region has been started
                        if (eventRegionStarted)
                        {
                            // if the codeLine is a region
                            if (codeLine.IsRegion)
                            {
                                // increment the count
                                openRegionCount++;
                                
                                // if this object is at the insert index
                                string regionName = "#region " + eventName.Trim();
                                string temp = codeLine.Text.Trim();
                                int compare = String.Compare(temp, regionName);
                                
                                // if this is the insert index
                                if (compare >= 0)
                                {
                                    // set the return value
                                    insertLineNumber = codeLine.LineNumber;
                                    
                                    // break out of the loop 
                                    break;
                                }
                            }
                            else if (codeLine.IsEndRegion)
                            {
                                // decrement the count
                                openRegionCount--;
                            }
                            
                            // if we end up in a Negative Number, we have left tthe Properties Region
                            if (openRegionCount < 0)
                            {
                                // set the insertLineNumber
                                insertLineNumber = codeLine.LineNumber;
                                
                                // break out of the loop
                                break;
                            }
                        }
                        else
                        {
                            // here the Properties region has not been started
                            
                            // if this is a region
                            if (codeLine.IsRegion)
                            {
                                // if the text is Properties
                                if (codeLine.Text.Contains("Events"))
                                {
                                    // has the event region been reached yet
                                    eventRegionStarted = true;
                                }
                            }
                        }
                    }
                }
                
                // return value
                return insertLineNumber;
            } 
            #endregion
            
            #region InsertEvent(string eventName, string returnType, CM.CSharpCodeFile codeFile)
            /// <summary>
            /// This Event inserts an event
            /// </summary>
            internal void InsertEvent(string eventName, string returnType, CM.CSharpCodeFile codeFile)
            {
                // Set the Indent to 3
                Indent = 3;
                
                // create the event
                CM.CodeEvent codeEvent = new CM.CodeEvent();
                
                // set the name
                codeEvent.Name = eventName;
                
                // get the return type
                string eventDeclarationLineText = "public " + returnType + " " + eventName + "(object sender, EventArgs e)";
                
                // create the codeLines that make up this event
                CM.CodeLine eventDeclarationLine = new CM.CodeLine(eventDeclarationLineText);
                CM.CodeLine openBracket = new CM.CodeLine("{");
                
                // create a new line
                CM.CodeLine blankLine = new CM.CodeLine(Environment.NewLine);
                
                // create the CloseBracket
                CM.CodeLine closeBracket = new CM.CodeLine("}");

                // Update: Building a Smart Description
                string description = DescriptionHelper.GetEventDescription(eventName);

                // create a summary
                string summary1Text = "/// <summary>";
                string summary2Text = "/// This event is fired when " + description;
                string summary3Text = @"/// </summary>";
                
                // create the codeLine
                CM.CodeLine summary1 = new CM.CodeLine(summary1Text);
                CM.CodeLine summary2 = new CM.CodeLine(summary2Text);
                CM.CodeLine summary3 = new CM.CodeLine(summary3Text);
                
                // Add the summary
                codeEvent.Summary.CodeLines.Add(summary1);
                codeEvent.Summary.CodeLines.Add(summary2);
                codeEvent.Summary.CodeLines.Add(summary3);
                
                // Now it is time to insert the codeLines
                codeEvent.CodeLines.Add(eventDeclarationLine);
                codeEvent.CodeLines.Add(openBracket);
                codeEvent.CodeLines.Add(blankLine);
                
                // add the close bracket
                codeEvent.CodeLines.Add(closeBracket);
                
                // before writing this event we need to find the insert index
                int lineNumber = GetEventInsertLineNumber(codeEvent.Name, codeFile);

                // if the lineNumber exists
                if (lineNumber > 0)
                {
                    // get the textDoc
                    TextDocument textDoc = GetActiveTextDocument();
                
                    // if the textDoc was found
                    if (textDoc != null)
                    {
                        // go to this line
                        textDoc.Selection.GotoLine(lineNumber, false);
                    }
                }
                else
                {   
                    // Show the user a message
                    MessageBox.Show("The 'Events' region does not exist in the current document." + Environment.NewLine + "Create the 'Events' region and try again.", "Events Region Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
                // now write the event
                WriteEvent(codeEvent, true);
            }
            #endregion

            #region InsertReadOnlyProperty(string propertyFullName, string returnType, CM.CSharpCodeFile codeFile)
            /// <summary>
            /// This method inserts a Read Only Property.
            /// Example: in Regionizer Select Add Read Only Property
            /// </summary>
            internal void InsertReadOnlyProperty(string propertyFullName, string returnType, CM.CSharpCodeFile codeFile)
            {
                // Set the Indent to 3
                Indent = 3;
                
                // create the event
                CM.CodeProperty codeProperty = new CM.CodeProperty();

                // if the property full name exists
                if (TextHelper.Exists(propertyFullName, returnType))
                {
                    // get the words
                    List<DataJuggler.Core.UltimateHelper.Objects.Word> words = WordParser.GetWords(propertyFullName);

                    // if the words were found
                    if (ListHelper.HasXOrMoreItems(words, 2))
                    {
                        // get the propertyName
                        string propertyName = words[1].Text;
                        string objectName = words[0].Text;

                        // set the name
                        codeProperty.Name = propertyName;
                
                        // get the propertyDeclarationLineText
                        string propertyDeclarationLineText = "public " + returnType + " " + propertyName;
                
                        // create the codeLines that make up this event
                        CM.CodeLine propertyDeclarationLine = new CM.CodeLine(propertyDeclarationLineText);
                        CM.CodeLine openBracket = new CM.CodeLine("{");
                
                        // create a new line
                        CM.CodeLine blankLine = new CM.CodeLine(Environment.NewLine);
                
                        // create the CloseBracket
                        CM.CodeLine closeBracket = new CM.CodeLine("}");

                        // create the region line
                        string regionText = "#region " + propertyName;
                        CodeLine regionLine = new CodeLine(regionText);

                        // Add the region line
                        codeProperty.CodeLines.Add(regionLine);

                        // create a summary
                        string summary1Text = "/// <summary>";
                        string summary2Text = "/// This read only property returns the value of " + propertyName + " from the object " + objectName + ".";
                        string summary3Text = "/// </summary>";
                
                        // create the codeLine
                        CM.CodeLine summary1 = new CM.CodeLine(summary1Text);
                        CM.CodeLine summary2 = new CM.CodeLine(summary2Text);
                        CM.CodeLine summary3 = new CM.CodeLine(summary3Text);
                
                        // Add the summary
                        codeProperty.CodeLines.Add(summary1);
                        codeProperty.CodeLines.Add(summary2);
                        codeProperty.CodeLines.Add(summary3);
                
                        // Now it is time to insert the codeLines
                        codeProperty.CodeLines.Add(propertyDeclarationLine);
                        codeProperty.CodeLines.Add(openBracket);

                        // get a lowercase variable name
                        string variableName = CapitalizeFirstChar(propertyName, true);

                        // get the defaultValue
                        string defaultvalue = GetDefaultValue(returnType);

                        // add a blank line
                        codeProperty.CodeLines.Add(blankLine);

                        // write get lines
                        string getText = TextHelper.Indent(4) + "get";
                        string getText2 = TextHelper.Indent(4) + openBracket;
                        string getText3 = TextHelper.Indent(8) + "// initial value";
                        string getText4 = TextHelper.Indent(8) + returnType + " " + variableName + " = " + defaultvalue + ";";

                        // if this is null, we have to remove the quotes
                        if (defaultvalue == "null")
                        {
                            getText4 = TextHelper.Indent(8) + returnType + " " + variableName + " = null;";
                        }
                        
                        string getText5 = TextHelper.Indent(8);
                        string getText6 = TextHelper.Indent(8) + "// if " + objectName + " exists";
                        string getText7 = TextHelper.Indent(8) + "if (" + objectName + " != null)";
                        string getText8 = TextHelper.Indent(8) + openBracket;
                        string getText9 = TextHelper.Indent(12) + "// set the return value";
                        string getText10 = TextHelper.Indent(12) + variableName + " = " + objectName + "." + propertyName + ";";
                        string getText11 = TextHelper.Indent(8) + closeBracket;

                        // write return value
                        string getText12 = TextHelper.Indent(8);
                        string getText13 = TextHelper.Indent(8) + "// return value";
                        string getText14 = TextHelper.Indent(8) + "return " + variableName + ";";
                        string getText15 = TextHelper.Indent(4) + closeBracket;

                        // create the codeLines from the strings
                        CodeLine line1 = new CodeLine(getText);
                        CodeLine line2 = new CodeLine(getText2);
                        CodeLine line3 = new CodeLine(getText3);
                        CodeLine line4 = new CodeLine(getText4);
                        CodeLine line5 = new CodeLine(getText5);
                        CodeLine line6 = new CodeLine(getText6);
                        CodeLine line7 = new CodeLine(getText7);
                        CodeLine line8 = new CodeLine(getText8);
                        CodeLine line9 = new CodeLine(getText9);
                        CodeLine line10 = new CodeLine(getText10);
                        CodeLine line11 = new CodeLine(getText11);
                        CodeLine line12 = new CodeLine(getText12);
                        CodeLine line13 = new CodeLine(getText13);
                        CodeLine line14 = new CodeLine(getText14);
                        CodeLine line15 = new CodeLine(getText15);

                        // add
                        codeProperty.CodeLines.Add(line1);
                        codeProperty.CodeLines.Add(line2);
                        codeProperty.CodeLines.Add(line3);
                        codeProperty.CodeLines.Add(line4);
                        codeProperty.CodeLines.Add(line5);
                        codeProperty.CodeLines.Add(line6);
                        codeProperty.CodeLines.Add(line7);
                        codeProperty.CodeLines.Add(line8);
                        codeProperty.CodeLines.Add(line9);
                        codeProperty.CodeLines.Add(line10);
                        codeProperty.CodeLines.Add(line11);
                        codeProperty.CodeLines.Add(line12);
                        codeProperty.CodeLines.Add(line13);
                        codeProperty.CodeLines.Add(line14);
                        codeProperty.CodeLines.Add(line15);

                        // add the close bracket
                        codeProperty.CodeLines.Add(closeBracket);

                        // create the region line
                        string endRegionText = "#endregion";
                        CodeLine endRegionLine = new CodeLine(endRegionText);

                        // Add the region line
                        codeProperty.CodeLines.Add(endRegionLine);

                        // Add the region line
                        codeProperty.CodeLines.Add(blankLine);

                        // Get the value
                        int propertyIndent = 0;
                
                        // before writing this event we need to find the insert index
                        int lineNumber = GetPropertyInsertLineNumber(propertyName, codeFile, out propertyIndent);

                        // if the lineNumber exists
                        if (lineNumber > 0)
                        {
                            // Set the Indent
                            Indent = propertyIndent;

                            // get the textDoc
                            TextDocument textDoc = GetActiveTextDocument();
                
                            // if the textDoc was found
                            if (textDoc != null)
                            {
                                // go to this line
                                textDoc.Selection.GotoLine(lineNumber, false);
                            }

                            // now write the property
                            WriteCodeLines(codeProperty.CodeLines);
                        }
                    }
                }
            }
            #endregion
            
            #region WriteEvent(CM.CodeEvent codeEvent, bool surroundWithRegion)
            /// <summary>
            /// This method writes out an Event
            /// </summary>
            /// <param name="codeEvent"></param>
            /// <param name="surroundWithRegion"></param>
            private void WriteEvent(CM.CodeEvent codeEvent, bool surroundWithRegion)
            {
                // locals
                string eventName = "";

                // verify the codeEvent exists
                if (codeEvent != null)
                {
                    // if a Region needs to surround this Constructor
                    if (surroundWithRegion)
                    {
                        // set the event name
                        eventName = codeEvent.Name;
                        
                        // if the codeEvent.CodesLines exist
                        if ((codeEvent.CodeLines != null) && (codeEvent.CodeLines.Count > 0))
                        {
                            // get the first line of the event
                            string eventDeclarationLine = codeEvent.CodeLines[0].Text;
                            
                            // Get the index of the open paren
                            int parenIndex = eventDeclarationLine.IndexOf("(");
                            
                            // if the parenIndex exists
                            if (parenIndex >= 0)
                            {
                                // get the parameters
                                string parameters = eventDeclarationLine.Substring(parenIndex);
                                
                                // Start the Region
                                BeginRegion(eventName + parameters);
                            }
                            else
                            {
                                // Start the Region
                                BeginRegion(eventName);
                            }
                        }
                    }
                    
                    // Write the Summary for this ev
                    WriteSummary(eventName, codeEvent.Summary, CodeTypeEnum.Event);
                    
                    // Write the Tags
                    WriteTags(codeEvent.Tags);
                    
                    // Write the CodeLines
                    WriteCodeLines(codeEvent.CodeLines);
                    
                    // if a Region needs to surround this Constructor
                    if (surroundWithRegion)
                    {
                        // write the end region
                        EndRegion();
                    }
                    
                    // Add a Blank Line
                    AddBlankLine();
                }
            }
            #endregion
            
        #endregion
        
        #region Methods
            
            #region AddBlankLine()
            /// <summary>
            /// Add a blank line
            /// </summary>
            private void AddBlankLine()
            {
                // Create a blank line
                string blankLine = Environment.NewLine;
                
                // insert a blank line
                Insert(blankLine);
            } 
            #endregion
            
            #region AddCloseBracket(bool decreaseIndentAfterInsertion)
            /// <summary>
            /// Add a CloseBracket
            /// </summary>
            private void AddCloseBracket(bool decreaseIndentAfterInsertion)
            {
                // Add the open bracket
                Insert("}");
                
                // if decrease indent after the insertion is true
                if (decreaseIndentAfterInsertion)
                {
                    // decrease the indention
                    Indent--;
                }
            }
            #endregion

            #region AddConstructors(IList<CM.CodeConstructor> constructors, string className)
            /// <summary>
            /// This method writes out the Constructors for this object.
            /// </summary>
            /// <param name="constructors"></param>
            private void AddConstructors(IList<CM.CodeConstructor> constructors, string className)
            {
                // if there are one or more Constructors
                if  ((constructors != null) && (constructors.Count > 0))
                {
                    // if there is only 1 constructor
                    if (constructors.Count == 1)
                    {
                        // set the first constructor
                        CM.CodeConstructor constructor = constructors[0];
                        
                        // Write the Constructor
                        WriteConstructor(constructor, true, className);
                    }
                    else
                    {
                        // we have two or more constructors
                        
                        // Begin a region
                        BeginRegion("Constructors");
                        
                        // increase indent
                        Indent++;
                        
                        // Add a Blank Line
                        AddBlankLine();
                        
                        // iterate the constructors
                        foreach(CM.CodeConstructor constructor in constructors)
                        {
                            // write out this constructor
                            WriteConstructor(constructor, true, className);
                        }
                        
                        // decrease indent
                        Indent--;
                        
                        // Close the Region
                        EndRegion();
                        
                        // Add a Blank Line
                        AddBlankLine();
                    }
                }
            }
            #endregion
            
            #region AddMethods(IList<CM.CodeMethod> methods)
            /// <summary>
            /// This method add the Methods
            /// </summary>
            /// <param name="properties"></param>
            private void AddMethods(IList<CM.CodeMethod> methods)
            {
                // if there are one or more properties
                if (methods != null)
                {
                    // Begin a region
                    BeginRegion("Methods");
                    
                    // increase indent
                    Indent++;
                    
                    // Add a Blank Line
                    AddBlankLine();
                    
                    if (ListHelper.HasOneOrMoreItems(methods.ToList()))
                    {
                        // iterate the properties
                        foreach (CM.CodeMethod codeMethod in methods)
                        {
                            // write out this Method
                            WriteMethod(codeMethod, true);
                        }
                    }
                    
                    // decrease indent
                    Indent--;
                    
                    // Close the Region
                    EndRegion();
                    
                    // Add a Blank Line
                    AddBlankLine();
                }
            }
            #endregion
            
            #region AddOpenBracket(bool increaseIndentAfterInsertion)
            /// <summary>
            /// Add an open bracket
            /// </summary>
            private void AddOpenBracket(bool increaseIndentAfterInsertion)
            {
                // Add the open bracket
                Insert("{");
                
                // if true
                if (increaseIndentAfterInsertion)
                {
                    // if the indent should be increased
                    Indent++;
                }
            }  
            #endregion
            
            #region AddPrivateVariables(IList<CM.CodePrivateVariable> privateVariables)
            /// <summary>
            /// This method adds the private variables to the document
            /// </summary>
            /// <param name="iList"></param>
            private void AddPrivateVariables(IList<CM.CodePrivateVariable> privateVariables)
            {
                // add the region
                BeginRegion("Private Variables");
                
                // if the privateVariables exist
                if ((privateVariables != null) && (privateVariables.Count > 0))
                {
                    // Update 8.25.2023: Sorting the private variables in alphabetical order
                    privateVariables = privateVariables.OrderBy(x => x.PrivateVariableName).ToList();

                    // iterate the privateVariables
                    foreach (CM.CodePrivateVariable privateVariable in privateVariables)
                    {
                        // trim the start
                        string text = privateVariable.Text.TrimStart();
                        
                        // add this private variable
                        Insert(text);
                    }
                }
                
                // add the end region
                EndRegion();
                
                // Add a blank line
                AddBlankLine();
            } 
            #endregion
            
            #region AddProperties(IList<CM.CodeProperty> properties)
            /// <summary>
            /// This method adds the Properties 
            /// </summary>
            /// <param name="properties"></param>
            private void AddProperties(IList<CM.CodeProperty> properties)
            {
                // if there are one or more properties
                if (properties != null)
                {
                    // Begin a region
                    BeginRegion("Properties");
                    
                    // increase indent
                    Indent++;
                    
                    // Add a Blank Line
                    AddBlankLine();

                    // If the properties collection exists and has one or more items
                    if (ListHelper.HasOneOrMoreItems(properties.ToList()))
                    {
                        // iterate the properties
                        foreach (CM.CodeProperty codeProperty in properties)
                        {
                            // write out this Property
                            WriteProperty(codeProperty, true);
                        }
                    }
                    
                    // decrease indent
                    Indent--;
                    
                    // Close the Region
                    EndRegion();
                    
                    // Add a Blank Line
                    AddBlankLine();
                }
            }            
            #endregion

            #region AutoComment(DictionaryInfo dictionaryInfo)
            /// <summary>
            /// This method is used to Auto Comment the selected line or line below
            /// </summary>
            internal void AutoComment(DictionaryInfo dictionaryInfo)
            {
                // initial value
                string sourceCode = "";

                // if the CommentDictionary exists
                if ((HasCommentDictionary) && (dictionaryInfo != null))
                {
                    // get the TextDocument from the ActiveDocument
                    TextDocument textDoc = GetActiveTextDocument();

                    // Get the fileCodeModel
                    FileCodeModel fileCodeModel = GetActiveFileCodeModel();

                    // if the textDocument exists
                    if ((textDoc != null) && (fileCodeModel != null))
                    {
                        // get the document text
                        textDoc.Selection.LineDown(false);

                        // Select the current line
                        textDoc.Selection.SelectLine();

                        // get the text from the line below the cursor
                        sourceCode = GetSelectedText(true);

                        // create an instance of an InspectorBaseClass object
                        InspectorBaseClass inspector = new InspectorBaseClass();

                        // get the autoCommentText
                        string autoCommentText = inspector.GetAutoCommentText(CommentDictionary, sourceCode, dictionaryInfo);

                        // if the autoCommentText exists
                        if (!String.IsNullOrEmpty(autoCommentText))
                        {
                            // Find the indent
                            int indent = FindIndent(sourceCode);

                            // Create an override for this
                            string indentChars = GetIndentText(indent);

                            // get the commentText text
                            string commentText = indentChars + "// " + autoCommentText;

                            // now go up one line where we started
                            textDoc.Selection.LineUp();

                            // now write out the
                            Insert(commentText, false);

                            // move to the end of the line
                            textDoc.Selection.EndOfLine();

                            // Delete the extra whitespace at the end of the line
                            textDoc.Selection.DeleteWhitespace(vsWhitespaceOptions.vsWhitespaceOptionsHorizontal);
                        }
                    }
                }
            }
            #endregion
            
            #region BeginRegion(string name)
            /// <summary>
            /// Begin a region
            /// </summary>
            /// <param name="?"></param>
            private void BeginRegion(string name)
            {
                // Add the region text
                string regionText = "#region " + name;
                
                // insert this text
                Insert(regionText);
            } 
            #endregion

            #region CapitalizeFirstChar(string word, bool lowerCase)
            /// <summary>
            /// This method Capitalizes the first character
            /// </summary>
            /// <param name="word"></param>
            /// <param name="lowerCase"></param>
            /// <returns></returns>
            public string CapitalizeFirstChar(string word, bool lowerCase)
            {
                // set the newWord
                string newWord = "";

                // If Null String
                if (String.IsNullOrEmpty(word))
                {
                    // Return Null String
                    return newWord;
                }

                // Create Char Array
                Char[] letters = word.ToCharArray();

                // if lower case
                if (lowerCase)
                {
                    // if this word is less than 3
                    if (word.Length < 3)
                    {
                        // go with all lower case here
                        newWord = word.ToLower();

                        // return the newWord
                        return newWord;
                    }
                    else
                    {
                        // Capitalize First Character
                        letters[0] = Char.ToLower(letters[0]);
                    }
                }
                else
                {
                    // Capitalize First Character
                    letters[0] = Char.ToUpper(letters[0]);
                }

                // set the newWord
                newWord = new string(letters);

                // return new string
                return newWord;

            }
            #endregion
            
            #region CheckIfProperty(IList<CM.CodeLine> codeLines)
            /// <summary>
            /// This Method [Enter Description Here].
            /// </summary>
            private bool CheckIfProperty(IList<CM.CodeLine> codeLines)
            {
                // initial value
                bool isProperty = false;

                // if there are one or more CodeLines
                if ((codeLines != null) && (codeLines.Count > 0))
                {
                    // itereate CodeLines
                    foreach (CM.CodeLine codeLine in codeLines)
                    {
                        // if the codeLine exist
                        if (codeLine.IsCodeLine)
                        {
                            // get the temp
                            string temp = codeLine.Text.Trim();

                            // if the codeLine exists
                            if (temp.Contains("get"))
                            {
                                // this is a property
                                isProperty = true;

                                // break;
                                break;
                            }
                            else if (temp.Contains("set"))
                            {
                                // this is a property
                                isProperty = true;

                                // break;
                                break;
                            }
                        }
                    }
                }

                // return value
                return isProperty;
            }
            #endregion
            
            #region CheckIfVowel(string text)
            /// <summary>
            /// This Method checks if the string given starts with a Vowel
            /// </summary>
            private bool CheckIfVowel(string text)
            {
                // initial value
                bool isVowel = false;
                
                // local
                string firstChar = "";
                
                // if the firstChar exists
                if (!String.IsNullOrEmpty(text))
                {
                    // set the firstChar
                    firstChar = text[0].ToString().ToLower();    
                    
                    // check for a vowel
                    switch(firstChar)
                    {
                        case "a":
                        case "e":
                        case "i":
                        case "o":
                        case "u":
                        
                        // this is a vowel
                        isVowel = true;
                        
                        break;
                    }
                }
                
                // return value
                return isVowel;
            }
            #endregion
            
            #region Clear(TextDocument textDoc)
            /// <summary>
            /// This method clears the text of the ActiveDocument
            /// </summary>
            /// <param name="textDoc"></param>
            private void Clear(TextDocument textDoc)
            {
                // if the textDoc exists
                if (textDoc != null)
                {
                    TextSelection textSelection = textDoc.Selection;
                    
                    // move to the start of the document
                    textDoc.Selection.StartOfDocument(false);
                    
                    // Move to the end of the doc
                    textDoc.Selection.EndOfDocument(true);
                    
                    // clear the text
                    textDoc.Selection.Text = "";
                }
            }
            #endregion
            
            #region CollapseAllRegions(EnvDTE.DTE dte)
            /// <summary>
            /// This method is not supported at this time
            /// </summary>
            internal void CollapseAllRegions(CM.CSharpCodeFile codeFile, EnvDTE.DTE dte)
            {
                try
                {
                    // not implemented at this time
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
            }
        #endregion

            #region CreateMethodCodeLines(CodeScopeEnum scope, string name, string returnType, string parameters)
            /// <summary>
            /// returns a list of Method Code Lines
            /// </summary>
            public List<CM.CodeLine> CreateMethodCodeLines(CodeScopeEnum scope, string name, string returnType, string parameters)
            {
                // initial value
                List<CM.CodeLine> lines = new List<CodeLine>();

                // locals
                /*
                InvokeAsync(() =>
                {
                    // Refresh
                    StateHasChanged();
                });
                */

                CM.CodeLine blankLine = new CodeLine("");
                CM.CodeLine refresh1 = new CodeLine("InvokeAsync(() =>");
                CM.CodeLine refresh2 = new CodeLine("{");
                CM.CodeLine refresh3 = new CodeLine("// Refresh");
                CM.CodeLine refresh4 = new CodeLine("StateHasChanged();");
                CM.CodeLine refresh5 = new CodeLine("});");

                // Create each line
                CM.CodeLine line1 = new CodeLine(scope.ToString().ToLower() + " " + returnType + " " + name + "(" + parameters + ")");
                CM.CodeLine line2 = new CodeLine("{");
                CM.CodeLine line4 = new CodeLine("}");

                // Add each line
                lines.Add(line1);
                lines.Add(line2);

                // if the Refresh method
                if (name == "Refresh")
                {
                    // add a blank line
                    lines.Add(refresh1);
                    lines.Add(refresh2);
                    lines.Add(refresh3);
                    lines.Add(refresh4);
                    lines.Add(refresh5);
                }
                else
                {
                    // add a blank line
                    lines.Add(blankLine);
                }
                lines.Add(line4);

                // return value
                return lines;
            }
            #endregion
            
            #region CreatePropertyCodeLines(CodeScopeEnum scope, string name, string returnType, bool register = false)
            /// <summary>
            /// returns a list of Code Lines that make up a property
            /// </summary>
            public static List<CodeLine> CreatePropertyCodeLines(CodeScopeEnum scope, string name, string returnType, bool register = false)
            {
                // initial value
                List<CodeLine> codeLines = new List<CodeLine>();

                // locals
                CM.CodeLine line9B = new CM.CodeLine("");
                CM.CodeLine line9C = new CM.CodeLine("// If the parent exists");
                CM.CodeLine line9D = new CM.CodeLine("if (parent != null)");
                CM.CodeLine line9E = new CM.CodeLine("{");
                CM.CodeLine line9F = new CM.CodeLine("// register with the parent");
                CM.CodeLine line9G = new CM.CodeLine("parent.Register(this);");
                CM.CodeLine line9H = new CM.CodeLine("}");

                // Lines
                CM.CodeLine lineParameter = new CodeLine("[Parameter]");
                CM.CodeLine line1 = new CM.CodeLine(scope.ToString().ToLower() + " " + returnType + " " + TextHelper.CapitalizeFirstChar(name, false));
                CM.CodeLine line2 = new CM.CodeLine("{");
                CM.CodeLine line3 = new CM.CodeLine("get");
                CM.CodeLine line4 = new CM.CodeLine("{");
                CM.CodeLine line5 = new CodeLine("return " + TextHelper.CapitalizeFirstChar(name, true) + ";");
                CM.CodeLine line6 = new CM.CodeLine("}");
                CM.CodeLine line7 = new CM.CodeLine("set");
                CM.CodeLine line8 = new CM.CodeLine("{");
                CM.CodeLine line9 = new CodeLine(TextHelper.CapitalizeFirstChar(name, true) + " = value;");
                CM.CodeLine line10 = new CM.CodeLine("}");
                CM.CodeLine line11 = new CM.CodeLine("}");

                // Now add these codeLines
                codeLines.Add(lineParameter);
                codeLines.Add(line1);                
                codeLines.Add(line2);
                codeLines.Add(line3);
                codeLines.Add(line4);
                codeLines.Add(line5);
                codeLines.Add(line6);
                codeLines.Add(line7);
                codeLines.Add(line8);
                codeLines.Add(line9);

                // if this is Parent property
                if (name == "Parent")
                {
                    codeLines.Add(line9B);
                    codeLines.Add(line9C);
                    codeLines.Add(line9D);
                    codeLines.Add(line9E);
                    codeLines.Add(line9F);
                    codeLines.Add(line9G);
                    codeLines.Add(line9H);
                }

                codeLines.Add(line10);
                codeLines.Add(line11);
                
                // return value
                return codeLines;
            }
            #endregion
            
            #region CreateSummary(string description)
            /// <summary>
            /// returns the Summary
            /// </summary>
            public static CodeNotes CreateSummary(string description)
            {
                // initial value
                CodeNotes summary = new CodeNotes();

                // Add each line
                summary.CodeLines.Add(new CodeLine(@"/// <summary>"));                
                summary.CodeLines.Add(new CodeLine(@"/// " + description));
                summary.CodeLines.Add(new CodeLine(@"/// </summary>"));

                // return value
                return summary;
            }
            #endregion
            
            #region CreateSummary(CodeTypeEnum codeType, string eventOrMethodName, string returnType = "", bool isReadOnly = false, string className = "")
            /// <summary>
            /// This method creates a Summary for CodeBlocks that do not have a summary
            /// </summary>
            /// <param name="codeType"></param>
            /// <returns></returns>
            private CM.CodeNotes CreateSummary(CodeTypeEnum codeType, string codeName, string returnType = "", bool isReadOnly = false, string className = "")
            {
                // initial value
                CM.CodeNotes summary = new CM.CodeNotes();
                
                // Create the lines that make up this Summary
                CM.CodeLine summaryLine1 = new CM.CodeLine(" /// <summary>");

                // set the description
                string description = "";

                // if the codeName Name exists
                if (!String.IsNullOrEmpty(codeName))
                {
                    if (codeType == CodeTypeEnum.Event)
                    {
                        // set the description
                        description = "event is fired when " + DescriptionHelper.GetEventDescription(codeName);
                    }
                    else if (codeType == CodeTypeEnum.Method)
                    {
                        // set the description
                        description = "method " + DescriptionHelper.GetMethodDescription(codeName, returnType);
                    }
                    else if (codeType == CodeTypeEnum.Constructor)
                    {
                        // set the description
                        description = "Create a new instance of a '" + className + "' object.";
                    }
                    else if (codeType == CodeTypeEnum.Class)
                    {
                        // set the description
                        description = "This class [Enter Class Description]";
                    }
                    else if (codeType == CodeTypeEnum.Property)
                    {
                        // Update 7.25.2022 - This now writes out the property name. Should have done this a long time ago.
                        description = "This property gets or sets the value for " + codeName + ".";
                    }
                    else
                    {
                        // set the description
                        description = "method [Enter Method Description]";
                    }
                }


                CM.CodeLine summaryLine2 = new CM.CodeLine("/// " + description);
                CM.CodeLine summaryLine3 = new CM.CodeLine(" /// </summary>");
                
                // now add the summaryLines to the summary
                summary.CodeLines.Add(summaryLine1);
                summary.CodeLines.Add(summaryLine2);
                summary.CodeLines.Add(summaryLine3);
                
                // return value
                return summary;
            } 
            #endregion

            #region CreateSummaryForProperty(string propertyName, bool isReadOnly = false)
            /// <summary>
            /// This method creates a Summary for CodeBlocks that do not have a summary
            /// </summary>
            /// <param name="codeType"></param>
            /// <returns></returns>
            private CM.CodeNotes CreateSummaryForProperty(string propertyName, bool isReadOnly = false)
            {
                // initial value
                CM.CodeNotes summary = new CM.CodeNotes();

                // locals
                CM.CodeLine summaryLine1 = null;
                CM.CodeLine summaryLine2 = null;
                CM.CodeLine summaryLine3 = null;

                // if isReadOnly
                if (isReadOnly)
                {
                    // Create the lines that make up this Summary
                    summaryLine1 = new CM.CodeLine(" /// <summary>");
                    summaryLine2 = new CM.CodeLine("/// This read only property returns the value for '" + propertyName + "'.");
                    summaryLine3 = new CM.CodeLine(" /// </summary>");
                }
                else
                {
                    // Create the lines that make up this Summary
                    summaryLine1 = new CM.CodeLine(" /// <summary>");
                    summaryLine2 = new CM.CodeLine("/// This property gets or sets the value for '" + propertyName + "'.");
                    summaryLine3 = new CM.CodeLine(" /// </summary>");
                }

                // now add the summaryLines to the summary
                summary.CodeLines.Add(summaryLine1);
                summary.CodeLines.Add(summaryLine2);
                summary.CodeLines.Add(summaryLine3);

                // return value
                return summary;
            }
            #endregion
            
            #region DeleteLine()
            /// <summary>
            /// This method deletes the current line.
            /// </summary>
            private void DeleteLine()
            {
                try
                {
                    TextSelection objSel = (TextSelection)(ActiveDocument.Selection);
                    VirtualPoint objActive = objSel.ActivePoint;

                    // Collapse the selection to the beginning of the line.
                    objSel.StartOfLine((EnvDTE.vsStartOfLineOptions)(0), false);

                    // objActive is "live", tied to the position of the actual 
                    // selection, so it reflects the new position.
                    long iCol = objActive.DisplayColumn;

                    // Move the selection to the end of the line.
                    objSel.EndOfLine(false);

                    // Delete the current line
                    objSel.Delete();
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
            }
            #endregion

            #region DetermineIfReadOnlyProperty(List<CM.CodeLine> codeLines)
            /// <summary>
            /// This method returns true if this property has a Get but not a Set.
            /// </summary>
            private bool DetermineIfReadOnlyProperty(List<CM.CodeLine> codeLines)
            {
                // initial value
                bool isReadOnly = false;

                // locals
                bool hasGet = false;
                bool hasSet = false;

                // if the codeProperty exists
                if (codeLines != null)
                {
                    // iterate the lines
                    foreach (CM.CodeLine codeLine in codeLines)
                    {
                        // if the text exists
                        if (!String.IsNullOrEmpty(codeLine.Text))
                        {
                            // if contains Get
                            if (codeLine.Text.Contains("get"))
                            {
                                // set to true
                                hasGet = true;
                            }
                            if (codeLine.Text.Contains("set"))
                            {
                                // set to true
                                hasSet = true;

                                // break out of the loop
                                break;
                            }
                        }
                    }

                    // if contains both
                    if ((hasSet) && (hasGet))
                    {
                        // not read only
                        isReadOnly = false;
                    }
                    else if (hasGet)
                    {
                        // not read only
                        isReadOnly = true;
                    }
                }

                // return value
                return isReadOnly;
             }
            #endregion
            
            #region EndRegion()
            /// <summary>
            /// Write the #endregion line
            /// </summary>
            /// <param name="?"></param>
            private void EndRegion()
            {
                // Add the region text
                string regionText = "#endregion";
                
                // insert this text
                Insert(regionText);
            }
            #endregion

            #region ExpandAllRegions(EnvDTE.DTE dte)
            /// <summary>
            /// This method expands all regions
            /// </summary>
            public void ExpandAllRegions(EnvDTE.DTE dte)
            {
                // collapse all regions
                // ToggleAllRegions(false, dte);
            }
            #endregion
            
            #region FindFirstEndRegionAfterLineNumber(int lineNumber, CM.CSharpCodeFile codeFile)
            /// <summary>
            /// This method finds the first endregion after a line number
            /// </summary>
            /// <param name="lineNumber"></param>
            /// <param name="codeFile"></param>
            /// <returns></returns>
            private int FindFirstEndRegionAfterLineNumber(int lineNumber, CM.CSharpCodeFile codeFile)
            {
                // initial value
                int endRegionLineNumber = 0;
                
                // if the codeFile exists
                if  ((codeFile != null) && (codeFile.CodeLines != null))
                {
                    // iterate the codeFile.CodeLines
                    for (int x = lineNumber; x < codeFile.CodeLines.Count; x++)
                    {
                        // set the codeLine
                        CM.CodeLine codeLine = codeFile.CodeLines[x];
                        
                        // if this line is an end region line
                        if (codeLine.IsEndRegion)
                        {
                            // set the return value
                            endRegionLineNumber = codeLine.LineNumber;
                            
                            // break out of the loop
                            break;
                        }
                    }
                }
                
                // return value
                return endRegionLineNumber;
            } 
            #endregion

            #region FindIndent(string sourceCode)
            /// <summary>
            /// This method returns the Indent
            /// </summary>
            private int FindIndent(string selectedText)
            {
                // initial value
                int indent = -1;

                // local
                int index = -1;

                // if the SelectedText exists
                if (!String.IsNullOrEmpty(selectedText))
                {
                    // iterate the chars
                    foreach (char c in selectedText)
                    {
                        // increment
                        index++;

                        // if NOT whiteSpace
                        if (!Char.IsWhiteSpace(c))
                        {
                            // break out of loop
                            indent = index;

                            // break out of the loop
                            break;
                        }
                    }
                }

                // return value
                return indent;
            }
            #endregion
            
            #region FindMatchingEndRegion(CM.CSharpCodeFile codeFile, int lineNumber)
            /// <summary>
            /// This Method finds the Matching EndRegion for starting at the lineNumber
            /// </summary>
            private CM.CodeLine FindMatchingEndRegion(CM.CSharpCodeFile codeFile, int lineNumber)
            {
                // initial value
                CM.CodeLine endRegionLine = null;
                
                // set the number of open regions
                int openRegionsCount = 0;
                
                // verify the codeFile exists
                if ((codeFile != null) && (codeFile.CodeLines != null))
                {
                    // iterate the codeLines
                    for(int x = lineNumber; x < codeFile.CodeLines.Count; x++)
                    {
                        // get the line at the current index
                        CM.CodeLine line = codeFile.CodeLines[x];
                        
                        // if this is another Region
                        if (line.IsRegion)
                        {
                            // increment the count
                            openRegionsCount++;
                        }
                        
                        // if this is an EndRegion
                        if (line.IsEndRegion)
                        {
                            // decrement the count
                            openRegionsCount--;
                            
                            // if the count is less than zero (Not the Robert Downey Junior Movie)
                            if (openRegionsCount < 0)
                            {
                                // this is the EndRegion line being sought
                                endRegionLine = line;
                                
                                // break out of the loop
                                break;
                            }
                        }
                    }
                }
                
                // return value
                return endRegionLine;
            }
            #endregion
            
            #region FormatDocument()
            /// <summary>
            /// This method Formats a CSharp code file.
            /// </summary>
            /// <returns></returns>
            public bool FormatDocument()
            {
                // initial value
                bool documentFormatted = false;
                string documentText = "";

                // locals
                bool abort = false;
                
                // get the TextDocument from the ActiveDocument
                TextDocument textDoc = GetActiveTextDocument();

                // Get the fileCodeModel
                FileCodeModel fileCodeModel = GetActiveFileCodeModel();
                
                // if the textDocument exists
                if  ((textDoc != null) && (fileCodeModel != null))
                {
                    // get the document text
                    documentText = GetDocumentText(textDoc);
                    
                    // set the codeFile
                    CM.CSharpCodeFile codeFile = CSharpCodeParser.ParseCSharpCodeFile(documentText, fileCodeModel);

                    // if the codeFile exists
                    if ((codeFile != null) && (codeFile.Namespace != null))
                    {
                        // get the current name space
                        CM.CodeNamespace currentNamespace = codeFile.Namespace;

                        // if there are one or more classes
                        if ((currentNamespace.HasClasses) && (currentNamespace.Classes.Count > 1))
                        {
                            // Get the user's confirmation before proceeding
                            MessageBoxResult result = MessageBox.Show("The Active Document contains more than one class file. Regionizer works best with a single class per file. Do you wish to continue?", "Proceed At Own Risk", MessageBoxButton.YesNo, MessageBoxImage.Question);

                            // if 'Yes' was clicked
                            if (result != MessageBoxResult.Yes)
                            {
                                // abort
                                abort = true;
                            }
                        }

                        // if we did not abort
                        if (!abort)
                        {
                            // before writing we need to clear the text of the active document
                            Clear(textDoc);
                        
                            // Add a blank live
                            AddBlankLine();
                        
                            // Add a blank live
                            AddBlankLine();
                        
                            // Write out the using statements
                            BeginRegion("using statements");
                        
                            // Add a blank live
                            AddBlankLine();
                        
                            // Write the code lines
                            WriteCodeLines(codeFile.UsingStatements);
                        
                            // Add a blank live
                            AddBlankLine();
                        
                            // Write End region Line
                            EndRegion();
                        
                            // Add a blank live
                            AddBlankLine();
                        
                            // Write the Namespace (and all the child objects)
                            WriteNamespace(codeFile.Namespace);

                            // now move up to the top
                            textDoc.Selection.GotoLine(1);
                        
                            // true for now
                            documentFormatted = true;
                        }
                    }
                }
                
                // return value
                return documentFormatted;
            }
            #endregion
            
            #region FormatSelection()
            /// <summary>
            /// This method cuts the selected text and then inserts the cut lines into the proper regions.
            /// This should be a method, property or event; Constructors are not supported at this time.
            /// At this time only a single Method, Property or Event should be selected; In the future 
            /// multiple selections might be handled, but this is mainly to format the code that is
            /// created when Visual Studio creates code for you with eith Generate - Method, or
            /// when an Event is created or even for a Property.
            /// Use the Format Document method to format the entire document.
            /// </summary>
            internal void FormatSelection()
            {
                // initial value
                bool formatted = false;
                string selectedText = "";

                // get the TextDocument from the ActiveDocument
                TextDocument textDoc = GetActiveTextDocument();
                
                // Get the fileCodeModel
                FileCodeModel fileCodeModel = GetActiveFileCodeModel();

                // if the textDocument exists
                if  ((textDoc != null) && (fileCodeModel != null))
                {
                    // get the document text
                    selectedText = GetSelectedText(false);

                    // parse the lines out of the selected text
                    List<TextLine> textLines = CSharpCodeParser.ParseLines(selectedText);
                    
                    // now create the codeLines from the textLines
                    List<CM.CodeLine> tempCodeLines = CSharpCodeParser.CreateCodeLines(textLines);

                    // copy the codeLines
                    List<CM.CodeLine> codeLines = new List<CM.CodeLine>();

                    // remove any blank lines above which might be selected for copying purposes the original does not leave a blank space
                    bool firstLine = false;

                    // if the tempCodeLines exist
                    if (tempCodeLines != null)
                    {
                        // iterate the lines
                        foreach (CM.CodeLine tempLine in tempCodeLines)
                        {  
                            // if we have not reached the first line yet
                            if (!firstLine)
                            {
                                // if this line is empty
                                if (!tempLine.IsEmpty)
                                {
                                    // add this line
                                    codeLines.Add(tempLine);

                                    // the first line has been reached so now empty lines can be added
                                    firstLine = true;
                                }
                            }
                            else
                            {
                                // add this line
                                codeLines.Add(tempLine);
                            }
                        }
                    }

                    // get the document text
                    string documentText = GetDocumentText(textDoc);

                    // get the codeFile
                    CM.CSharpCodeFile codeFile = CSharpCodeParser.ParseCSharpCodeFile(documentText, fileCodeModel);
                    
                    // locals
                    int insertLine = 0;
                    int startCopyLine = 1;
                    int skippedLines = 0;
                    int endCopyLine = 0;

                    // if the codeLines exist
                    if ((codeLines != null) && (codeLines.Count > 0) && (codeFile != null))
                    {
                        // get the top line
                        CM.CodeLine codeLine = GetFirstCodeLine(codeLines, out skippedLines);
                        
                        // if the codeLine exists
                        if  ((codeLine != null) && (codeLine.IsCodeLine))
                        {
                            // set the startCopyLine + the skippedLines (the Summary and Tags are written separately is why we need SkippedLines)
                            startCopyLine += skippedLines;

                            // here we need to set the values for the startCopyLine & endCopyLine
                            if ((startCopyLine > 0) && (startCopyLine < codeLines.Count))
                            {
                                // set the endLine 
                                endCopyLine = codeLines.Count;
                            }

                            // verify we have a StartCopy Line & EndCopyLine before continuing
                            if ((startCopyLine > 0) && (endCopyLine > startCopyLine))
                            {
                                // if this is a Method
                                if (codeLine.IsMethod)
                                {
                                    // Set to 3
                                    Indent = 3;

                                    // This is an Event
                                    if (codeLine.IsEvent)
                                    {
                                        // This is an Event

                                        // Create the event
                                        CM.CodeEvent codeEvent = new CM.CodeEvent();

                                        // parse out the method name
                                        codeEvent.Name = CSharpCodeParser.ParseMethodNameFromText(codeLine);

                                        // insert the line
                                        insertLine = GetEventInsertLineNumber(codeEvent.Name, codeFile);

                                        // if the insertLine was found
                                        if (insertLine > 0)
                                        {
                                            // Get the currentLine of the insert index
                                            var currentLine = codeFile.CodeLines[insertLine -1];

                                            // Reset the indent
                                            Indent = currentLine.Indent;

                                            // move to the line number desired
                                            textDoc.Selection.GotoLine(insertLine);

                                            // now get the summary
                                            codeEvent.EventDeclarationLine = codeLine;

                                            // Now get the summary
                                            codeEvent.Summary = CSharpCodeParser.GetSummaryAboveLine(codeFile, codeLine.LineNumber);

                                            // copy the codeLines
                                            codeEvent.CodeLines = CSharpCodeParser.CopyLines(codeLines, startCopyLine, endCopyLine);
                                            
                                            // if the codeEvent.Summary was not found
                                            if ((codeEvent.Summary == null) || (codeEvent.Summary.CodeLines.Count < 3))
                                            {
                                                // Create the summary
                                                codeEvent.Summary = CreateSummary(CodeTypeEnum.Event, codeEvent.Name, "");    
                                            }

                                            // Write the event and surround this event with a region
                                            WriteEvent(codeEvent, true);

                                            // set formatted to true
                                            formatted = true;
                                        }
                                        else
                                        {
                                            // Show the user a message 
                                            MessageBox.Show("The 'Events' region does not exist in the current document." + Environment.NewLine + "Create the 'Events' region and try again.", "Events Region Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                                        }
                                    }
                                    else
                                    {
                                        // This is a Method

                                        // Create the method
                                        CM.CodeMethod codeMethod = new CM.CodeMethod();

                                        // parse out the method name
                                        codeMethod.Name = CSharpCodeParser.ParseMethodNameFromText(codeLine);

                                        // local
                                        int methodsRegionIndent = 0;
                
                                        // before writing this method we need to find the insert index
                                        insertLine = GetMethodInsertLineNumber(codeMethod.Name, codeFile, out methodsRegionIndent);

                                        // if the insertLine
                                        if (insertLine > 0)
                                        {
                                            // Reset the indent
                                            Indent = methodsRegionIndent;

                                            // move to the line number desired
                                            textDoc.Selection.GotoLine(insertLine);

                                            // Now get the summary
                                            codeMethod.Summary = CSharpCodeParser.GetSummaryAboveLine(codeFile, codeLine.LineNumber);

                                            // copy the codeLines
                                            codeMethod.CodeLines = CSharpCodeParser.CopyLines(codeLines, startCopyLine, endCopyLine);

                                            // if the codeMethod.Summary was not found
                                            if ((codeMethod.Summary == null) || (codeMethod.Summary.CodeLines.Count < 3))
                                            {
                                                // Get the return type
                                                string returnType = GetReturnTypeFromMethodLine(codeMethod.CodeLines[0].Text);

                                                // Create the summary
                                                codeMethod.Summary = CreateSummary(CodeTypeEnum.Method, codeMethod.Name, returnType);
                                            }

                                            // Surround this method with a region
                                            WriteMethod(codeMethod, true);

                                            // set formatted to true
                                            formatted = true;
                                        }
                                        else
                                        {
                                            // Show the user a message
                                            MessageBox.Show("The 'Methods' region does not exist in the current document." + Environment.NewLine + "Create the 'Methods' region and try again.", "Methods Region Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                                        }
                                    }
                                }
                                else
                                {   
                                    // Check if this is a Property
                                    bool isProperty = CheckIfProperty(codeLines);
                                
                                    // if this is a Property
                                    if (isProperty)
                                    {
                                        // find the insert index for a property
                                        IList<Word> words = CSharpCodeParser.ParseWords(codeLine.Text);

                                        // if there are two or more words
                                        if ((words != null) && (words.Count > 1))
                                        {
                                            // set the property name
                                            string propertyName = words[words.Count - 1].Text.Replace(";", "");

                                             // Get the value
                                            int propertyIndent = 3;                

                                            // get the insert 
                                            insertLine = GetPropertyInsertLineNumber(propertyName, codeFile, out propertyIndent);

                                            // if the insertLine
                                            if (insertLine > 0)
                                            {
                                                // Get the currentLine of the insert index
                                                var currentLine = codeFile.CodeLines[insertLine -1];

                                                // Reset the indent
                                                Indent = currentLine.Indent;

                                                // go to the line needed
                                                textDoc.Selection.GotoLine(insertLine);

                                                // Create the property
                                                CM.CodeProperty codeProperty = new CM.CodeProperty();

                                                // set the property name
                                                codeProperty.Name = propertyName;

                                                // get the summary above the line
                                                codeProperty.Summary = CSharpCodeParser.GetSummaryAboveLine(codeFile, codeLine.LineNumber);

                                                // create the summary
                                                if ((codeProperty.Summary == null) || (codeProperty.Summary.CodeLines.Count < 3))
                                                {
                                                    // is this a readOnlyProperty
                                                    bool isReadOnlyProperty = DetermineIfReadOnlyProperty(tempCodeLines);

                                                    // create the summary for the property
                                                    codeProperty.Summary = CreateSummaryForProperty(propertyName, isReadOnlyProperty);
                                                }

                                                // get the Tags if any
                                                codeProperty.Tags = CSharpCodeParser.GetTagsAboveLine(codeFile, codeLine.LineNumber);

                                                // copy the codeLines
                                                codeProperty.CodeLines = CSharpCodeParser.CopyLines(codeLines, startCopyLine, endCopyLine);

                                                // Set to the first property
                                                Indent = propertyIndent;

                                                // now write the property
                                                WriteProperty(codeProperty, true);

                                                // set formatted to true
                                                formatted = true;
                                            }
                                            else
                                            {
                                                // Show the user a message
                                                MessageBox.Show("The 'Properties' region does not exist in the current document." + Environment.NewLine + "Create the 'Properties' region and try again.", "Properties Region Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // if !formatted
                if (!formatted)
                {
                    // Show the user it failed
                    MessageBox.Show("The selected text could not be formatted.", "Format Selection Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            #endregion
            
            #region GetActiveFileCodeModel()
            /// <summary>
            /// This method loads the FileCodeModel from the 
            /// ActiveDocument.ProjectItem.FileCodeModel.
            /// </summary>
            /// <returns></returns>
            public FileCodeModel GetActiveFileCodeModel()
            {
                // initial value
                FileCodeModel fileCodeModel = null;
                
                // if the ActiveDocument exists
                if ((HasActiveDocument) && (ActiveDocument.ProjectItem != null))
                {
                    // get the fileCodeModel
                    fileCodeModel = ActiveDocument.ProjectItem.FileCodeModel;
                }
                
                // return value
                return fileCodeModel;
            } 
            #endregion
            
            #region GetActiveTextDocument()
            /// <summary>
            /// This method returns the ActiveTextDocument
            /// </summary>
            /// <returns></returns>
            public TextDocument GetActiveTextDocument()
            {
                // initial value
                TextDocument textDoc = null;
                
                // if the ActiveDocumente exists
                if (ActiveDocument != null)
                {
                    // Set the Selection
                    object obj = ActiveDocument.Object("TextDocument");
                    textDoc = (TextDocument) obj;
                }
                
                // return the textDoc
                return textDoc;
            } 
            #endregion
            
            #region GetCodeFile()
            /// <summary>
            /// This method returns a Parsed CSharp code file
            /// </summary>
            /// <returns></returns>
            internal CM.CSharpCodeFile GetCodeFile()
            {
                // initial value
                CM.CSharpCodeFile codeFile = null;
                
                // initial value
                string documentText = "";
                
                // get the TextDocument from the ActiveDocument
                TextDocument textDoc = GetActiveTextDocument();
                
                // Get the fileCodeModel
                FileCodeModel fileCodeModel = GetActiveFileCodeModel();
                
                // if the textDocument exists
                if  ((textDoc != null) && (fileCodeModel != null))
                {
                    // get the document text
                    documentText = GetDocumentText(textDoc);
                    
                    // set the codeFile
                    codeFile = CSharpCodeParser.ParseCSharpCodeFile(documentText, fileCodeModel);
                }
                
                // return value
                return codeFile;
            } 
            #endregion
            
            #region GetCursorTextPoint()
            /// <summary>
            /// This method returns the Cursor at the current Text Point
            /// </summary>
            private EnvDTE.TextPoint GetCursorTextPoint()
            {
                // local
                EnvDTE.TextDocument textDoc = GetActiveTextDocument();
                
                // get the text point
                EnvDTE.TextPoint textPoint = default(EnvDTE.TextPoint);
                
                try
                {
                    // if the textDoc exists
                    if ((textDoc != null) && (textDoc.Selection != null))
                    {
                        // get the textPoint
                        textPoint = textDoc.Selection.ActivePoint;
                    }
                }
                catch (Exception exception)
                {
                    // for debugging only
                    string error = exception.ToString();
                }

                // return value
                return textPoint;
            }
            #endregion
            
            #region GetDefaultValue(string returnType)
            /// <summary>
            /// returns the Default Value
            /// </summary>
            public string GetDefaultValue(string returnType)
            {
                // initial value
                string defaultValue = "";

                if (TextHelper.Exists(returnType))
                {
                    // certain return types have different default values
                    switch(returnType)
                    {
                        case "string":

                            StringBuilder sb = new StringBuilder('"');
                            sb.Append('"');
                            sb.Append('"');
                            string doubleQuotes = sb.ToString();

                            // Set to an empty string
                            defaultValue = sb.ToString();

                            // required
                            break;

                        case "bool":
                        
                            // use false forf default value     
                            defaultValue = "false";

                            // required
                            break;

                        case "int":
                        case "double":
                        
                            // use false forf default value     
                            defaultValue = "0";

                            // required
                            break;

                        case "DateTime":

                            // add a date time
                            defaultValue = " new DateTime(1900, 1, 1);";

                            // required
                            break;

                        default: 

                            // set to null
                            defaultValue = "null";

                              // required
                            break;
                    }
                }
                
                // return value
                return defaultValue;
            }
            #endregion
            
            #region GetDocumentText(TextDocument textDoc)
            /// <summary>
            /// Get the text of this document
            /// </summary>
            /// <param name="textDoc"></param>
            /// <returns></returns>
            public string GetDocumentText(TextDocument textDoc)
            {
                // move to the Start of the text doc
                EditPoint startPoint = textDoc.StartPoint.CreateEditPoint();
                
                // set the return value
                string documentText = startPoint.GetText(textDoc.EndPoint);
                
                // return value
                return documentText;
            }
            #endregion

            #region GetFirstCodeLine(IList<CM.CodeLine> codeLines, out int skippedLines)
            /// <summary>
            /// This Method returns the first code line
            /// </summary>
            private CM.CodeLine GetFirstCodeLine(IList<CM.CodeLine> codeLines, out int skippedLines)
            {
                // initial value
                CM.CodeLine firstCodeLine = null;

                // set the value for SkippedLines
                skippedLines = 0;
                
                // if the codeLines exist
                if (codeLines != null)
                {
                    // iterate the lines
                    foreach (CM.CodeLine codeLine in codeLines)
                    {
                        // if this is not a summary, tag or a Region, then this is the firstCodeLine
                        if (codeLine.IsCodeLine)
                        {
                            // set the return value
                            firstCodeLine = codeLine;
                            
                            // break out of loop
                            break;
                        }
                        else
                        {
                            // increment skippedLines
                            skippedLines++;
                        }
                    }
                }
                
                // return value
                return firstCodeLine;
            }
            #endregion

            #region GetIndentText()
            /// <summary>
            /// This method gets the indent text
            /// </summary>
            /// <returns></returns>
            private string GetIndentText()
            {
                // initial value
                string indentText = "";

                // local
                string tab = "    ";
                int indent = Indent;

                // iterate once for each indention
                for (int x = 0; x < indent; x++)
                {
                    // add the tab for each column to indent
                    indentText += tab;
                }

                // return value
                return indentText;
            }
            #endregion

            #region GetIndentText(int indentCharacters)
            /// <summary>
            /// This method gets the indent text by the number of spaces to indent
            /// </summary>
            /// <returns></returns>
            private string GetIndentText(int indentCharacters)
            {
                // initial value
                string indentText = "";

                // local
                string spaceChar = " ";

                // iterate once for each indent Character
                for (int x = 0; x < indentCharacters; x++)
                {
                    // add the tab for each column to indent
                    indentText += spaceChar;
                }

                // return value
                return indentText;
            }
            #endregion
            
            #region GetMethodInsertLineNumber(string methodName, CM.CSharpCodeFile codeFile, out int methodsRegionIndent) 
            /// <summary>
            /// This method gets the Insert LineNumber for a Method
            /// </summary>
            /// <param name="methodName"></param>
            /// <param name="codeFile"></param>
            /// <returns></returns>
            private int GetMethodInsertLineNumber(string methodName, CM.CSharpCodeFile codeFile, out int methodsRegionIndent)
            {
                // initial value
                int insertLineNumber = 0;

                // locals
                bool methodRegionStarted = false;
                int openRegionCount = 0;
                bool methodsRegionIndentSet = false;

                // Set the out value
                methodsRegionIndent = 3;
                
                // verify the codeFile exists
                if ((codeFile != null) && (codeFile.CodeLines != null))
                {
                    // iterate the files
                    foreach (CM.CodeLine codeLine in codeFile.CodeLines)
                    {
                        // if a method region has been started
                        if (methodRegionStarted)
                        {
                            // if the codeLine is a region
                            if (codeLine.IsRegion)
                            {
                                // increment the count
                                openRegionCount++;

                                // if methodsRegionIndentSet is false
                                if (!methodsRegionIndentSet)
                                {
                                    // Set the indent value
                                    methodsRegionIndent = codeLine.Indent;

                                    // set to true so only gets set once
                                    methodsRegionIndentSet = true;
                                }
                                
                                // if this object is at the insert index
                                string regionName = "#region " + methodName.Trim();
                                string temp = codeLine.Text.Trim();
                                int compare = String.Compare(temp, regionName);
                                
                                // if this is the insert index
                                if (compare >= 0)
                                {
                                    // set the return value
                                    insertLineNumber = codeLine.LineNumber;

                                    // break out of the loop 
                                    break;
                                }
                            }
                            else if (codeLine.IsEndRegion)
                            {
                                // decrement the count
                                openRegionCount--;
                            }
                            
                            // if we end up in a Negative Number, we have left tthe Properties Region
                            if (openRegionCount < 0)
                            {
                                // set the insertLineNumber
                                insertLineNumber = codeLine.LineNumber;
                                
                                // break out of the loop
                                break;
                            }
                        }
                        else
                        {
                            // here the Properties region has not been started
                            
                            // if this is a region
                            if (codeLine.IsRegion)
                            {
                                // if the text is Properties
                                if (codeLine.Text.Contains("Methods"))
                                {
                                    // has the method region been reached yet
                                    methodRegionStarted = true;
                                }
                            }
                        }
                    }
                }
                
                // return value
                return insertLineNumber;
            } 
            #endregion
            
            #region GetPrivateVariableNameFromPrivateVariableText(string privateVariableText)
            /// <summary>
            /// This method returns the Private Variable Name from the text.
            /// </summary>
            /// <param name="privateVariableText"></param>
            /// <returns></returns>
            private string GetPrivateVariableNameFromPrivateVariableText(string privateVariableText)
            {
                // initial value
                string privateVariableName = "";
                
                // if the PrivateVariableText exists
                if (!String.IsNullOrEmpty(privateVariableText))
                {
                    // get the words
                    IList<Word> words = CSharpCodeParser.ParseWords(privateVariableText);
                    
                    // if there are one or more Words
                    if ((words != null) && (words.Count > 0))
                    {
                        // iterate the words
                        foreach (Word word in words)
                        {
                            // if this is the Last Word
                            if (word.Text.Contains(";"))
                            {
                                // get the name of the Property
                                privateVariableName = word.Text.Replace(";", "").Trim();
                                
                                // break out
                                break;
                            }
                        }
                    }
                }
                
                // return value
                return privateVariableName;
            } 
            #endregion
            
            #region GetPrivateVariableRegionLineNumber(CM.CSharpCodeFile codeFile)
            /// <summary>
            /// This method gets the line number of the Region "Private Variables"
            /// </summary>
            /// <returns></returns>
            private int GetPrivateVariableRegionLineNumber(CM.CSharpCodeFile codeFile)
            {
                // initial value
                int lineNumber = 0;
                
                // get the codeFile
                if (codeFile != null)
                {
                    // if there are one or more codeLines
                    if ((codeFile.CodeLines != null) && (codeFile.CodeLines.Count > 0))
                    {
                        // iterate the codeLines
                        foreach (CM.CodeLine codeLine in codeFile.CodeLines)
                        {
                            // if this is a Region Line
                            if (codeLine.IsRegion)
                            {
                                // if the Private Variables exists
                                if (codeLine.Text.ToLower().Contains("private variables"))
                                {
                                    // set the return value
                                    lineNumber = codeLine.LineNumber;
                                }
                            }
                        }
                    }
                }
                
                // return the lineNumber
                return lineNumber;
            } 
            #endregion
            
            #region GetPropertyInsertLineNumber(string propertyName, CM.CSharpCodeFile codeFile, out int propertyRegionIndent)
            /// <summary>
            /// This method returns the LineNumber for the property that you need to inserted.
            /// </summary>
            /// <param name="propertyName"></param>
            /// <returns></returns>
            private int GetPropertyInsertLineNumber(string propertyName, CM.CSharpCodeFile codeFile, out int propertyRegionIndent)
            {
                // initial value
                int insertLineNumber = 0;
                
                // locals
                bool propertyRegionStarted = false;
                int openRegionCount = 0;
                bool propertyRegionIndentSet = false;

                // Set the Out parameter                
                propertyRegionIndent = 3;
                
                // verify the codeFile exists
                if ((codeFile != null) && (codeFile.CodeLines != null))
                {
                    // iterate the files
                    foreach (CM.CodeLine codeLine in codeFile.CodeLines)
                    {   
                        // if a property region has been started
                        if (propertyRegionStarted)
                        {
                            // if the codeLine is a region
                            if (codeLine.IsRegion)
                            {
                                // Setting indent to the indent of the first property

                                // if not set yet
                                if (!propertyRegionIndentSet)
                                {
                                    // Set the out parameter
                                    propertyRegionIndent = codeLine.Indent;

                                    // Only set once
                                    propertyRegionIndentSet = true;
                                }

                                // increment the count
                                openRegionCount++;
                                
                                // if this object is at the insert index
                                string regionName = "#region " + propertyName.Trim();
                                string temp = codeLine.Text.Trim();
                                int compare = String.Compare(temp, regionName);
                                
                                // if this is the insert index
                                if (compare >= 0)
                                {
                                    // set the return value
                                    insertLineNumber = codeLine.LineNumber;
                                    
                                    // break out of the loop 
                                    break;
                                }   
                            }
                            else if (codeLine.IsEndRegion)
                            {
                                // decrement the count
                                openRegionCount--;
                            }
                            
                            // if we end up in a Negative Number, we have left tthe Properties Region
                            if (openRegionCount < 0)
                            {
                                // set the insertLineNumber
                                insertLineNumber = codeLine.LineNumber;
                                
                                // break out of the loop
                                break;
                            }
                        }
                        else
                        {
                            // here the Properties region has not been started
                            
                            // if this is a region
                            if (codeLine.IsRegion)
                            {
                                // if the text is Properties
                                if (codeLine.Text.Contains("Properties"))
                                {
                                    // has the property region been reached yet
                                    propertyRegionStarted = true;
                                }
                            }
                        }
                    }
                }
                
                // return value
                return insertLineNumber;
            } 
            #endregion
            
            #region GetPropertyNameFromPrivateVariableText(string privateVariableText)
            /// <summary>
            /// This method returns the Name for a new property that is created from a private variable that is selected.
            /// </summary>
            /// <param name="privateVariableText"></param>
            /// <returns></returns>
            private string GetPropertyNameFromPrivateVariableText(string privateVariableText)
            {
                // initial value
                string propertyName = "";
                
                // if the PrivateVariableText exists
                if (!String.IsNullOrEmpty(privateVariableText))
                {
                    // get the words
                    IList<Word> words = CSharpCodeParser.ParseWords(privateVariableText);
                    
                    // if there are one or more Words
                    if ((words != null) && (words.Count > 0))
                    {
                        // iterate the words
                        foreach (Word word in words)
                        {
                            // if this is the Last Word
                            if (word.Text.Contains(";"))
                            {
                                // get the name of the Property
                                string property = word.Text.Replace(";", "").Replace("_", "").Trim();
                                
                                // verify the property exists
                                if (!String.IsNullOrEmpty(property))
                                {
                                    // new we need to capitalize the first char
                                    string firstChar = property[0].ToString().ToUpper();
                                    
                                    // Get the length
                                    if (property.Length > 1)
                                    {
                                        // set the return value
                                        propertyName = firstChar + property.Substring(1);
                                        
                                        // break out of loop
                                        break;
                                    }
                                    else if (property.Length == 1)
                                    {
                                        // set the return value
                                        propertyName = firstChar;
                                        
                                        // break out of loop
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                
                // return value
                return propertyName;
            }  
            #endregion

            #region GetReturnTypeFromMethodLine(string methodOrEventDeclarationLine)
            /// <summary>
            /// This method returns the returnType for the method or event line given.
            /// </summary>
            private string GetReturnTypeFromMethodLine(string methodOrEventDeclarationLine)
            {
                string returnType = "";

                // get the words
                List<Word> words = CSharpCodeParser.ParseWords(methodOrEventDeclarationLine);

                // Get the return type from this line
                if ((words != null) && (words.Count > 2))
                {
                    // Get the text of the word two words from the right
                    returnType = words[words.Count - 2].Text;
                }

                // return value
                return returnType;
            }
            #endregion
            
            #region GetReturnTypeFromPrivateVariableText(string privateVariableText)
            /// <summary>
            /// This method returns the type of data for the private variable text
            /// </summary>
            /// <param name="privateVariableText"></param>
            /// <returns></returns>
            private string GetReturnTypeFromPrivateVariableText(string privateVariableText)
            {
                // initial value
                string returnType = "";
                
                // if the privateVariableText exists
                if (!String.IsNullOrEmpty(privateVariableText))
                {
                    // get the words
                    IList<Word> words = CSharpCodeParser.ParseWords(privateVariableText);
                    
                    // if the words exist
                    if ((words != null) && (words.Count > 0))
                    {
                        // set the return type
                        returnType = words[1].Text;
                    }
                }
                
                // return value
                return returnType;
            }
            #endregion
            
            #region GetSelectedText(ref string dataType)
            /// <summary>
            /// This method gets the SelectedText and parses out the datatype from the line.
            /// This is used for the HasPropertyCreator or other uses
            /// </summary>
            /// <param name="dataType"></param>
            /// <returns></returns>
            internal string GetSelectedText(ref string dataType)
            {
                // initial value
                string selectedText = "";
                
                try
                {
                    // get the textDoc
                    EnvDTE.TextDocument textDoc = GetActiveTextDocument();
                    
                    // if the textDoc.Selection exists
                    if ((textDoc != null) && (textDoc.Selection != null))
                    {
                        // set the SelectedText
                        selectedText = textDoc.Selection.Text;
                        
                        // get the word to the left
                        textDoc.Selection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText, false);
                        textDoc.Selection.EndOfLine(true);
                        
                        // this should be the text if a Property is selected.
                        string lineText = textDoc.Selection.Text;
                        
                        // Get the Words
                        IList<Word> words = CSharpCodeParser.ParseWords(lineText);
                        
                        // if there are one or more wods
                        if ((words != null) && (words.Count >= 2))
                        {
                            // set the return value for dataType
                            dataType = words[words.Count - 2].Text;
                        }
                        
                        // after selecting, clear the text so we do not lose our source
                        textDoc.Selection.EndOfLine(false);
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                
                // return value
                return selectedText;
            } 
            #endregion

            #region GetSelectedText(bool keepSelection)
            /// <summary>
            /// This method gets the SelectedText.
            /// </summary>
            /// <returns></returns>
            internal string GetSelectedText(bool keepSelection)
            {
                // initial value
                string selectedText = "";
                
                try
                {
                    // get the textDoc
                    EnvDTE.TextDocument textDoc = GetActiveTextDocument();
                    
                    // if the textDoc.Selection exists
                    if ((textDoc != null) && (textDoc.Selection != null))
                    {
                        // set the SelectedText
                        selectedText = textDoc.Selection.Text;

                        // if we should not keep the source text
                        if (!keepSelection)
                        {
                            // after selecting, clear the text so we do not lose our source
                            textDoc.Selection.Delete();
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
                
                // retur value
                return selectedText;
            } 
            #endregion
            
            #region HandleIBlazorComponentClassDeclaration(CM.CodeLine classDeclarationLine)
            /// <summary>
            /// returns the I Blazor Component Class Declaration
            /// </summary>
            public CM.CodeLine HandleIBlazorComponentClassDeclaration(CM.CodeLine classDeclarationLine)
            {
                // Check if the text contains "IBlazorComponent"
                if (!classDeclarationLine.Text.Contains("IBlazorComponent"))
                {
                    // Check if it inherits from any base class or implements any interfaces
                    if (classDeclarationLine.Text.Contains(":"))
                    {
                        // Add ", IBlazorComponent" if it already has a base class or interface(s)
                        classDeclarationLine.Text += ", IBlazorComponent";
                    }
                    else
                    {
                        // Add " : IBlazorComponent" if it doesn't have any base class or interface(s)
                        classDeclarationLine.Text += " : IBlazorComponent";
                    }
                }

                // return value
                return classDeclarationLine;
            }
            #endregion

            #region HandleIBlazorComponentParentClassDeclaration(CM.CodeLine classDeclarationLine)
            /// <summary>
            /// returns the I Blazor Component Class Declaration
            /// </summary>
            public CM.CodeLine HandleIBlazorComponentParentClassDeclaration(CM.CodeLine classDeclarationLine)
            {
                // Check if the text contains "IBlazorComponentParent"
                if (!classDeclarationLine.Text.Contains("IBlazorComponentParent"))
                {
                    // Check if it inherits from any base class or implements any interfaces
                    if (classDeclarationLine.Text.Contains(":"))
                    {
                        // Add ", IBlazorComponentParent" if it already has a base class or interface(s)
                        classDeclarationLine.Text += ", IBlazorComponentParent";
                    }
                    else
                    {
                        // Add " : IBlazorComponentParent" if it doesn't have any base class or interface(s)
                        classDeclarationLine.Text += " : IBlazorComponentParent";
                    }
                }

                // return value
                return classDeclarationLine;
            }
            #endregion
            
            #region HandleIBlazorComponentMethods()
            /// <summary>
            /// returns a list of I Blazor Component Methods
            /// </summary>
            public List<CM.CodeMethod> HandleIBlazorComponentMethods(List<CM.CodeMethod> methods)
            {
                // locals
                bool hasReceiveDataMethod = false;

                // If the methods collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(methods))
                {
                    // set the value for hasReceiveDataMethod
                    hasReceiveDataMethod = (methods.FirstOrDefault(x => x.Name == "ReceiveData") != null);
                }
                else
                {
                    // Create a new collection of 'CodeMethod' objects.
                    methods = new List<CodeMethod>();
                }

                // if the value for hasReceiveDataMethod is false
                if (!hasReceiveDataMethod)
                {
                    CM.CodeMethod method = new CodeMethod();
                    method.Name = "ReceiveData";
                    method.Summary = CreateSummary("This method is used to receive messages from other components or pages");
                    method.CodeLines = CreateMethodCodeLines(CodeScopeEnum.Public, "ReceiveData", "void", "Message message");
                    method.CodeType = CodeTypeEnum.Method;

                    // Add this method
                    methods.Add(method);
                }

                // Order the methods
                methods = methods.OrderBy(x => x.Name).ToList();

                // return value
                return methods;
            }
            #endregion
            
            #region HandleIBlazorComponentParentMethods(List<CM.CodeMethod> methods)
            /// <summary>
            /// returns a list of I Blazor Component Parent Methods
            /// </summary>
            public List<CM.CodeMethod> HandleIBlazorComponentParentMethods(List<CM.CodeMethod> methods)
            {
                 // locals
                bool hasReceiveDataMethod = false;
                bool hasRefreshMethod = false;
                bool hasRegisterMethod = false;

                // If the methods collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(methods))
                {
                    // set the value for hasReceiveDataMethod
                    hasReceiveDataMethod = (methods.FirstOrDefault(x => x.Name == "ReceiveData") != null);
                    hasRefreshMethod = (methods.FirstOrDefault(x => x.Name == "Refresh") != null);
                    hasRegisterMethod = (methods.FirstOrDefault(x => x.Name == "Register") != null);
                }
                else
                {
                    // Create a new collection of 'CodeMethod' objects.
                    methods = new List<CodeMethod>();
                }

                // if the value for hasReceiveDataMethod is false
                if (!hasReceiveDataMethod)
                {
                    CM.CodeMethod method = new CodeMethod();
                    method.Name = "ReceiveData";
                    method.Summary = CreateSummary("This method is used to receive messages from other components or pages");
                    method.CodeLines = CreateMethodCodeLines(CodeScopeEnum.Public, "ReceiveData", "void", "Message message");
                    method.CodeType = CodeTypeEnum.Method;

                    // Add this method
                    methods.Add(method);
                }

                // if the value for hasRefreshMethod is false
                if (!hasRefreshMethod)
                {
                    CM.CodeMethod method = new CodeMethod();
                    method.Name = "Refresh";
                    method.Summary = CreateSummary("This method is used to update the UI after changes");
                    method.CodeLines = CreateMethodCodeLines(CodeScopeEnum.Public, "Refresh", "void", "");
                    method.CodeType = CodeTypeEnum.Method;

                    // Add this method
                    methods.Add(method);
                }

                // if the value for hasRegisterMethod is false
                if (!hasRegisterMethod)
                {
                    CM.CodeMethod method = new CodeMethod();
                    method.Name = "Register";
                    method.Summary = CreateSummary("This method is used to store child controls that are on this component or page");
                    method.CodeLines = CreateMethodCodeLines(CodeScopeEnum.Public, "Register", "void", "IBlazorComponent component");
                    method.CodeType = CodeTypeEnum.Method;

                    // Add this method
                    methods.Add(method);
                }

                // Order the methods
                methods = methods.OrderBy(x => x.Name).ToList();

                // return value
                return methods;
            }
            #endregion
            
            #region HandleIBlazorComponentPrivateVariables(List<CM.CodePrivateVariable> privateVariables)
            /// <summary>
            /// returns a list of I Blazor Component Private Variables
            /// </summary>
            public static List<CM.CodePrivateVariable> HandleIBlazorComponentPrivateVariables(List<CM.CodePrivateVariable> privateVariables)
            {
                // locals
                bool hasNameVariable = false;
                bool hasParentVariable = false;

                // If the privateVariables collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(privateVariables))
                {
                    // Check if any variable is "private string name;" or "private IBlazorComponent parent;"
                    hasNameVariable = privateVariables.Any(v => v.Text.Contains("private string name;"));
                    hasParentVariable = privateVariables.Any(v => v.Text.Contains("private IBlazorComponentParent parent;"));
                }
                else
                {
                    // Recreate the privateVariables list if it is null or empty
                    privateVariables = new List<CM.CodePrivateVariable>();
                }

                // Add "private string name;" if it doesn't exist
                if (!hasNameVariable)
                {
                    // Add this private variable
                    privateVariables.Add(new CM.CodePrivateVariable("private string name;"));
                }

                // Add "private IBlazorComponent parent;" if it doesn't exist
                if (!hasParentVariable)
                {
                    // Add this private variable
                    privateVariables.Add(new CM.CodePrivateVariable("private IBlazorComponentParent parent;"));
                }

                // if the private variables exist
                if (ListHelper.HasOneOrMoreItems(privateVariables))
                {
                    // Sort
                    privateVariables = privateVariables.OrderBy(x => x.PrivateVariableName).ToList();
                }

                // return value
                return privateVariables;
            }
            #endregion
            
            #region HandleIBlazorComponentProperties(List<CM.CodeProperty> properties)
            /// <summary>
            /// returns a list of I Blazor Component Properties that implement IBlazorComponent
            /// </summary>
            public static List<CM.CodeProperty> HandleIBlazorComponentProperties(List<CM.CodeProperty> properties)
            {
                // locals
                bool hasNameProperty = false;
                bool hasParentProperty = false;

                // If the properties collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(properties))
                {
                    // Check if any property is "public string Name" or "public IBlazorComponent Parent"
                    hasNameProperty = properties.Any(p => p.Name == "Name");
                    hasParentProperty = properties.Any(p => p.Name == "Parent");
                }
                else
                {
                    // Recreate the properties list if it is null or empty
                    properties = new List<CM.CodeProperty>();
                }

                // Add "public string Name" if it doesn't exist
                if (!hasNameProperty)
                {
                    // Create a nameProperty
                    CM.CodeProperty nameProperty = new CM.CodeProperty();                    
                    nameProperty.Scope = CodeScopeEnum.Public;
                    nameProperty.Summary = CreateSummary("This property gets or sets the value for Name");
                    nameProperty.Name = "Name";
                    nameProperty.CodeLines = CreatePropertyCodeLines(CodeScopeEnum.Public, "Name", "string");

                    // Add this property
                    properties.Add(nameProperty);
                }

                // Add "public IBlazorComponent Parent" if it doesn't exist
                if (!hasParentProperty)
                {
                     // Create a nameProperty
                    CM.CodeProperty parentProperty = new CM.CodeProperty();                    
                    parentProperty.Scope = CodeScopeEnum.Public;
                    parentProperty.Summary = CreateSummary("This property gets or sets the value for Parent");
                    parentProperty.Name = "Parent";
                    parentProperty.CodeLines = CreatePropertyCodeLines(CodeScopeEnum.Public, "Parent", "IBlazorComponentParent");

                    // Add this property
                    properties.Add(parentProperty);
                }

                // If the properties collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(properties))
                {
                    // Sort
                    properties = properties.OrderBy(x => x.Name).ToList();
                }

                // return value
                return properties;
            }
            #endregion
            
            #region HandleIBlazorComponentUsingStatements(List<CodeLine> usingStatements)
            /// <summary>
            /// returns a list of I Blazor Component Using Statements
            /// </summary>
            public List<CodeLine> HandleIBlazorComponentUsingStatements(List<CodeLine> usingStatements)
            {
                // Text to check or add
                string reference1 = "using DataJuggler.Blazor.Components;";
                string reference2 = "using DataJuggler.Blazor.Components.Interfaces;";
                string reference3 = "using Microsoft.AspNetCore.Components;";
    
                // Initialize usingStatements if it's null
                if (usingStatements == null)
                {
                    // create
                    usingStatements = new List<CodeLine>();
                }
    
                // Use LINQ to check for existing references
                bool hasReference1 = usingStatements.Any(x => x.Text == reference1);
                bool hasReference2 = usingStatements.Any(x => x.Text == reference2);
                bool hasReference3 = usingStatements.Any(x => x.Text == reference3);
    
                // Add the references if they don't exist
                if (!hasReference1)
                {
                    // Add Reference 1
                    usingStatements.Add(new CodeLine(reference1));
                }
    
                // if the value for hasReference2 is false
                if (!hasReference2)
                {
                    // Add Reference 2
                    usingStatements.Add(new CodeLine(reference2));
                }

                // if the value for hasReference3 is false
                if (!hasReference3)
                {
                    // Add Reference 3
                    usingStatements.Add(new CodeLine(reference3));
                }

                // return value
                return usingStatements;
            }
            #endregion
            
            #region ImplementIBlazorComponentInterface()
            /// <summary>
            /// Implement I Blazor Component Interface. This method is similar to FormatDocument,
            /// but some changes are made to implenent the interface.
            /// </summary>
            public void ImplementIBlazorComponentInterface()
            {
                // locals
                bool abort = false;
                
                // get the TextDocument from the ActiveDocument
                TextDocument textDoc = GetActiveTextDocument();

                // Get the fileCodeModel
                FileCodeModel fileCodeModel = GetActiveFileCodeModel();
                
                // if the textDocument exists
                if  ((textDoc != null) && (fileCodeModel != null))
                {
                    // get the document text
                    string documentText = GetDocumentText(textDoc);
                    
                    // set the codeFile
                    CM.CSharpCodeFile codeFile = CSharpCodeParser.ParseCSharpCodeFile(documentText, fileCodeModel);

                    // if the codeFile exists
                    if ((codeFile != null) && (codeFile.Namespace != null))
                    {  
                        // get the current name space
                        CM.CodeNamespace currentNamespace = codeFile.Namespace;

                        // if there are one or more classes
                        if ((currentNamespace.HasClasses) && (currentNamespace.Classes.Count > 1))
                        {
                            // Get the user's confirmation before proceeding
                            MessageBoxResult result = MessageBox.Show("The Active Document contains more than one class file. Regionizer works best with a single class per file. Do you wish to continue?", "Proceed At Own Risk", MessageBoxButton.YesNo, MessageBoxImage.Question);

                            // if 'Yes' was clicked
                            if (result != MessageBoxResult.Yes)
                            {
                                // abort
                                abort = true;
                            }
                        }
                        else if ((currentNamespace.HasClasses) && (currentNamespace.Classes.Count == 0))
                        {
                            // Get the user's confirmation before proceeding
                            MessageBoxResult result = MessageBox.Show("The Active Document does not contain any classes", "Invalid Document");
                            
                            // abort
                            abort = true;
                        }

                        // if we did not abort
                        if (!abort)
                        {
                            // Get a reference to the first class
                            CM.CodeClass activeClass = codeFile.Namespace.Classes[0];

                            // before writing we need to clear the text of the active document
                            Clear(textDoc);
                        
                            // Add a blank live
                            AddBlankLine();
                        
                            // Add a blank live
                            AddBlankLine();
                        
                            // Write out the using statements
                            BeginRegion("using statements");
                        
                            // Add a blank live
                            AddBlankLine();

                            // Update the UsingStatements
                            codeFile.UsingStatements = HandleIBlazorComponentUsingStatements(codeFile.UsingStatements);
                        
                            // Write the code lines
                            WriteCodeLines(codeFile.UsingStatements);

                            // Get the ClassDeclarationLine
                            activeClass.ClassDeclarationLine = HandleIBlazorComponentClassDeclaration(activeClass.ClassDeclarationLine);

                            // Set the PrivateVariables
                            activeClass.PrivateVariables = HandleIBlazorComponentPrivateVariables(activeClass.PrivateVariables);

                            // Set the Properties
                            activeClass.Properties = HandleIBlazorComponentProperties(activeClass.Properties);

                            // Add ReceiveData if not present
                            activeClass.Methods = HandleIBlazorComponentMethods(activeClass.Methods);

                            // Add a blank live
                            AddBlankLine();
                        
                            // Write End region Line
                            EndRegion();
                        
                            // Add a blank live
                            AddBlankLine();
                        
                            // Write the Namespace (and all the child objects)
                            WriteNamespace(codeFile.Namespace);

                            // now move up to the top
                            textDoc.Selection.GotoLine(1);
                        }
                    }
                }
            }
            #endregion
            
            #region ImplementIBlazorComponentParentInterface()
            /// <summary>
            /// Implement I Blazor Component Parent Interface
            /// </summary>
            public void ImplementIBlazorComponentParentInterface()
            {
                // locals
                bool abort = false;
                
                // get the TextDocument from the ActiveDocument
                TextDocument textDoc = GetActiveTextDocument();

                // Get the fileCodeModel
                FileCodeModel fileCodeModel = GetActiveFileCodeModel();
                
                // if the textDocument exists
                if  ((textDoc != null) && (fileCodeModel != null))
                {
                    // get the document text
                    string documentText = GetDocumentText(textDoc);
                    
                    // set the codeFile
                    CM.CSharpCodeFile codeFile = CSharpCodeParser.ParseCSharpCodeFile(documentText, fileCodeModel);

                    // if the codeFile exists
                    if ((codeFile != null) && (codeFile.Namespace != null))
                    {  
                        // get the current name space
                        CM.CodeNamespace currentNamespace = codeFile.Namespace;

                        // if there are one or more classes
                        if ((currentNamespace.HasClasses) && (currentNamespace.Classes.Count > 1))
                        {
                            // Get the user's confirmation before proceeding
                            MessageBoxResult result = MessageBox.Show("The Active Document contains more than one class file. Regionizer works best with a single class per file. Do you wish to continue?", "Proceed At Own Risk", MessageBoxButton.YesNo, MessageBoxImage.Question);

                            // if 'Yes' was clicked
                            if (result != MessageBoxResult.Yes)
                            {
                                // abort
                                abort = true;
                            }
                        }
                        else if ((currentNamespace.HasClasses) && (currentNamespace.Classes.Count == 0))
                        {
                            // Get the user's confirmation before proceeding
                            MessageBoxResult result = MessageBox.Show("The Active Document does not contain any classes", "Invalid Document");
                            
                            // abort
                            abort = true;
                        }

                        // if we did not abort
                        if (!abort)
                        {
                            // Get a reference to the first class
                            CM.CodeClass activeClass = codeFile.Namespace.Classes[0];

                            // before writing we need to clear the text of the active document
                            Clear(textDoc);
                        
                            // Add a blank live
                            AddBlankLine();
                        
                            // Add a blank live
                            AddBlankLine();
                        
                            // Write out the using statements
                            BeginRegion("using statements");
                        
                            // Add a blank live
                            AddBlankLine();

                            // Update the UsingStatements
                            codeFile.UsingStatements = HandleIBlazorComponentUsingStatements(codeFile.UsingStatements);
                        
                            // Write the code lines
                            WriteCodeLines(codeFile.UsingStatements);

                            // Get the ClassDeclarationLine                            
                            activeClass.ClassDeclarationLine = HandleIBlazorComponentParentClassDeclaration(activeClass.ClassDeclarationLine);

                            // Add ReceiveData, Refresh and Register methods if not present
                            // Change to IBlazorComponentParentMethods
                            activeClass.Methods = HandleIBlazorComponentParentMethods(activeClass.Methods);

                            // Add a blank live
                            AddBlankLine();
                        
                            // Write End region Line
                            EndRegion();
                        
                            // Add a blank live
                            AddBlankLine();
                        
                            // Write the Namespace (and all the child objects)
                            WriteNamespace(codeFile.Namespace);

                            // now move up to the top
                            textDoc.Selection.GotoLine(1);
                        }
                    }
                }
            }
            #endregion
            
            #region Insert(CM.CodeLine codeLine)
            /// <summary>
            /// This method inserts text into the active document.
            /// </summary>
            /// <param name="codeLine"></param>
            private void Insert(CM.CodeLine codeLine)
            {
                // insert this codeLine to the selected location in the textDoc
                Insert(codeLine, 0, false, false);
            }   
            #endregion
            
            #region Insert(CM.CodeLine codeLine, int lineNumber)
            /// <summary>
            /// This method inserts text into the active document.
            /// </summary>
            /// <param name="codeLine"></param>
            private void Insert(CM.CodeLine codeLine, int lineNumber)
            {
                // insert this codeLine to the lineNumber listed
                Insert(codeLine, lineNumber, false, false);
            }
            #endregion
            
            #region Insert(CM.CodeLine codeLine, int lineNumber, bool addBlankLineAbove, bool addBlankLineBelow)
            /// <summary>
            /// This method inserts text into the active document.
            /// </summary>
            /// <param name="codeLine"></param>
            private void Insert(CM.CodeLine codeLine, int lineNumber, bool addBlankLineAbove, bool addBlankLineBelow)
            {
                // get the active TextDoc
                TextDocument textDoc = GetActiveTextDocument();
                
                // if the textDoc exists
                if (textDoc != null)
                {
                    // if the lineNumber is set
                    if (lineNumber > 0)
                    {
                        // move to the line number
                        textDoc.Selection.GotoLine(lineNumber);
                    }
                    
                    // set to the newLine
                    string blankLine = Environment.NewLine;
                    
                    // if add blank line above
                    if (addBlankLineAbove)
                    {
                        // insert a blank line
                        Insert(blankLine);
                    }
                    
                    // if the codeLine.TextLine exists
                    if  (codeLine != null) 
                    {
                        // if there is Text
                        if ((codeLine.TextLine != null) && (!String.IsNullOrEmpty(codeLine.Text.Trim())))
                        {
                            // if the new line is not at the end of this line
                            if (!codeLine.TextLine.Text.EndsWith(Environment.NewLine))
                            {
                                // Add the new line character
                                codeLine.TextLine.Text += Environment.NewLine;
                            }
                            
                            // Set the textDoc
                            string indentText = GetIndentText();
                            string leftTrimmedText = indentText + codeLine.TextLine.Text.TrimStart();
                            
                            // Insert the text                                                        
                            textDoc.Selection.Insert(leftTrimmedText);
                        }
                        else
                        {
                            // Add a blank line
                            string textToInsert = Environment.NewLine;
                            textDoc.Selection.Insert(textToInsert);
                        }
                    }
                    else
                    {
                        // for debugging only
                        throw new Exception("CodeLine Does Not Exist");
                    }
                    
                    // if we need to add a blank link below
                    if (addBlankLineBelow)
                    {
                        // insert a blank line
                        Insert(blankLine);
                    }
                }
            }
            #endregion

            #region Insert(string lineText, bool endWithNewLine = true)
            /// <summary>
            /// This method inserts text into the active document.
            /// </summary>
            /// <param name="codeLine"></param>
            private void Insert(string lineText, bool endWithNewLine = true)
            {
                // insert this lineText to the selected location in the textDoc
                Insert(lineText, 0, false, false, endWithNewLine);
            }
            #endregion
            
            #region Insert(string lineText, int lineNumber)
            /// <summary>
            /// This method inserts text into the active document.
            /// </summary>
            /// <param name="codeLine"></param>
            private void Insert(string lineText, int lineNumber)
            {
                // insert this codeLine to the lineNumber listed
                Insert(lineText, lineNumber, false, false);
            }
            #endregion

            #region Insert(string lineText, int lineNumber, bool addBlankLineAbove, bool addBlankLineBelow, bool endWithNewLine = true)
            /// <summary>
            /// This method inserts text into the active document.
            /// </summary>
            /// <param name="codeLine"></param>
            private void Insert(string lineText, int lineNumber, bool addBlankLineAbove, bool addBlankLineBelow, bool endWithNewLine = true)
            {
                // get the active TextDoc
                TextDocument textDoc = GetActiveTextDocument();
                
                // if the textDoc exists
                if (textDoc != null)
                {
                    // if the lineNumber is set
                    if (lineNumber > 0)
                    {
                        // move to the line number
                        textDoc.Selection.GotoLine(lineNumber);
                    }
                    
                    // set to the newLine
                    string blankLine = Environment.NewLine;
                    
                    // if add a blank line above
                    if (addBlankLineAbove)
                    {
                        // insert a blank line
                        Insert(blankLine);
                    }
                    
                    // if we should end with new line
                    if (endWithNewLine)
                    {
                        // if the line does not end with a new line
                        if (!lineText.EndsWith(Environment.NewLine))
                        {
                            // Add a new line character
                            lineText += Environment.NewLine;
                        }
                    }
                    
                    // Set the textDoc
                    string textToInsert = GetIndentText() + lineText;

                    // insert the text
                    textDoc.Selection.Insert(textToInsert);
                    
                    // if add a blank line below
                    if (addBlankLineBelow)
                    {
                        // insert a blank line
                        Insert(blankLine);
                    }
                }
            }
            #endregion
            
            #region InsertBlazorComponent(string text)
            /// <summary>
            /// Insert Blazor Component
            /// </summary>c
            public void InsertBlazorComponent(string text)
            {
                TextDocument textDocument = GetActiveTextDocument();
                
                int lineNumber = textDocument.Selection.CurrentLine;

                // Insert this text
                Insert(text + Environment.NewLine, lineNumber, false, false);
            }
            #endregion
            
            #region InsertForEachLoop(string returnType, string collectionName)
            /// <summary>
            /// This method writes out a For Each Loop
            /// </summary>
            internal void InsertForEachLoop(string returnType, string collectionName)
            {
                // Before running this example, open a text document.
                TextSelection objSel = (TextSelection) ActiveDocument.Selection;
                VirtualPoint objActive = objSel.ActivePoint;

                // Set the spaces
                string spaces = "                    ";

                // Collapse the selection to the beginning of the line.
                objSel.StartOfLine((EnvDTE.vsStartOfLineOptions)(0), false);
                
                // objActive is "live", tied to the position of the actual 
                // selection, so it reflects the new position.
                long iCol = objActive.DisplayColumn; 

                // get the commentLine
                string commentLine = "// Iterate the items in the collection";
                string variableName = CapitalizeFirstChar(returnType, true);

                // write out the for each loop
                Insert(spaces + commentLine);
                Insert(spaces + "foreach (" + returnType + " " + variableName + " in " + collectionName + ")");
                Insert(spaces + "{");
                AddBlankLine();
                Insert(spaces + "}", false);
            }
            #endregion
            
            #region InsertHasProperty(string propertyName, string dataType, CM.CSharpCodeFile codeFile)
            /// <summary>
            /// This Method Creates a property that returns true if the object exists;
            /// Examples:
            /// String Example: HasTitle = (!String.IsNullOrEmpty(Title));
            /// Object Example: HasManager = (Manager != null);
            /// </summary>
            internal void InsertHasProperty(string propertyName, string dataType, CM.CSharpCodeFile codeFile)
            {
                // Set the Indent to 3
                Indent = 3;
                
                // create the property
                CM.CodeProperty property = new CM.CodeProperty();
                
                // set the name
                property.Name = "Has" + propertyName;
                
                // get the return type
                string propertyDeclarationLineText = "public bool " + property.Name;
                
                // create the codeLines that make up this property
                CM.CodeLine propertyDeclarationLine = new CM.CodeLine(propertyDeclarationLineText);
                CM.CodeLine openBracket = new CM.CodeLine("{");
                
                // set the getLineText
                string getLineText = "get";
                CM.CodeLine getLine = new CM.CodeLine(getLineText);
                
                // Create another OpenBracket
                CM.CodeLine openBracket2 = new CM.CodeLine("{");
                
                // set the initialValueComment
                string initialValueCommentText = "// initial value";
                CM.CodeLine initialValueCommentLine = new CM.CodeLine(initialValueCommentText);
                
                // create the initialValue line
                string variableName = "has" + propertyName;
                
                // get the equation
                string equation = "(" + propertyName + " != null);";
                
                // if the dataType is set
                if (!String.IsNullOrEmpty(dataType))
                {
                    // if this is a string
                    if (dataType.ToLower() == "string")
                    {
                        // change for a string
                        equation = "(!String.IsNullOrEmpty(" + propertyName + "));";
                    }
                    else if ((dataType.ToLower() == "int") || (dataType.ToLower() == "double"))
                    {
                        // change for a string
                        equation = "(" + propertyName + " > 0);";
                    }
                }
                
                // get the text for the initialValueLine
                string initialValueText = "bool " + variableName + " = " + equation;
                
                // Create the InitialValueLine
                CM.CodeLine initialValueLine = new CM.CodeLine(initialValueText);
                
                // create a new line
                CM.CodeLine blankLine = new CM.CodeLine(Environment.NewLine);
                
                // Create a comment for return value
                CM.CodeLine returnValueComment = new CM.CodeLine("// return value");
                
                // Now create the Return Value line
                string returnValueText = "return " + variableName + ";";
                CM.CodeLine  returnValueLine = new CM.CodeLine(returnValueText);
                
                // create the CloseBracket
                CM.CodeLine closeBracket = new CM.CodeLine("}");
                
                // create the CloseBracket
                CM.CodeLine closeBracket2 = new CM.CodeLine("}");
                
                // create a summary
                string summary1Text = "/// <summary>";
                string summary2Text = "/// This property returns true if this object has a '" + propertyName + "'.";
                
                // if this is a vowel for the first character
                bool isVowel = CheckIfVowel(propertyName);
                
                // if this is a vowel
                if (isVowel)
                {
                    // replace out the text
                    summary2Text = summary2Text.Replace("has a ", "has an ");
                }
                
                // set the last line of the summary
                string summary3Text = @"/// </summary>";
                
                // verify the data type is set
                if (!String.IsNullOrEmpty(dataType))
                {
                    // if this is a string
                    if (dataType.ToLower() == "string")
                    {
                        // change for a string
                        summary2Text = "/// This property returns true if the '" + propertyName + "' exists.";
                    }
                    else if ((dataType.ToLower() == "int") || (dataType.ToLower() == "double"))
                    {
                        // change for a string
                        summary2Text = "/// This property returns true if the '" + propertyName + "' is set.";
                    }
                }
                
                // create the codeLine
                CM.CodeLine summary1 = new CM.CodeLine(summary1Text);
                CM.CodeLine summary2 = new CM.CodeLine(summary2Text);
                CM.CodeLine summary3 = new CM.CodeLine(summary3Text);
                
                // Add the summary
                property.Summary.CodeLines.Add(summary1);
                property.Summary.CodeLines.Add(summary2);
                property.Summary.CodeLines.Add(summary3);
                
                // Now it is time to insert the codeLines
                property.CodeLines.Add(propertyDeclarationLine);
                property.CodeLines.Add(openBracket);
                property.CodeLines.Add(getLine);
                property.CodeLines.Add(openBracket2);
                property.CodeLines.Add(initialValueCommentLine);
                property.CodeLines.Add(initialValueLine);
                property.CodeLines.Add(blankLine);
                property.CodeLines.Add(returnValueComment);
                property.CodeLines.Add(returnValueLine);
                property.CodeLines.Add(closeBracket2);
                property.CodeLines.Add(closeBracket);

                // default to 3
                int propertyIndent = 3;
                
                // before writing this property we need to find the insert index
                int lineNumber = GetPropertyInsertLineNumber(property.Name, codeFile, out propertyIndent);
                
                // get the textDoc
                TextDocument textDoc = GetActiveTextDocument();
                
                // if the textDoc was found
                if ((textDoc != null) && (lineNumber > 0))
                {
                    // Get the currentLine
                    var currentLine = codeFile.CodeLines[lineNumber - 1];

                    // Reset the Indent
                    Indent = propertyIndent;

                    // go to this line
                    textDoc.Selection.GotoLine(lineNumber, false);
                }
                
                // now write the property
                WriteProperty(property, true);
            }
            #endregion

            #region InsertIfExistsBlock(string commentLine, string testLine)
            /// <summary>
            /// This method inserts the If Exists Block
            /// </summary>
            internal void InsertIfExistsBlock(string commentLine, string testLine)
            {
                try
                {
                    // if commentLine starts with 
                    if (!commentLine.StartsWith("//"))
                    {
                        // prepend the comment keys
                        commentLine = "// " + commentLine;
                    }

                    // set the spaces
                    string spaces = "                ";

                    // Add the comment
                    Insert(commentLine);
                    Insert(spaces + testLine);
                    Insert(spaces + "{");
                    AddBlankLine();
                    Insert(spaces + "}", false);
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
            }
            #endregion

            #region InsertIfExistsStatement(string commentLine, string testLine)
            /// <summary>
            /// This method inserts the If Exists Block
            /// </summary>
            internal void InsertIfExistsStatement(string commentLine, string testLine)
            {
                try
                {
                    // if commentLine starts with 
                    if (!commentLine.StartsWith("//"))
                    {
                        // prepend the commentText keys
                        commentLine = "// " + commentLine;
                    }

                    // set the spaces
                    string spaces = "                ";

                    // Add the commentText
                    Insert(commentLine);
                    Insert(spaces + testLine);
                    Insert(spaces + "{");
                    AddBlankLine();
                    Insert(spaces + "}", false);
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
            }
            #endregion
            
            #region InsertInitialValue(string returnType, string collectionName)
            /// <summary>
            /// This method inserts text similiar to the following:
            /// 
            /// // initial value
            /// Contact contact = null;
            /// 
            /// // return value
            /// return contact;
            /// 
            /// The code above will be inserted when the returnType = Contact.
            /// </summary>
            internal void InsertInitialValue(string returnType, string variableName)
            {
                // if the returnType exists
                if (!String.IsNullOrEmpty(returnType))
                {
                    // if the string exists
                    if (!String.IsNullOrEmpty(variableName))
                    {
                        // make sure the first character is lower case for the variable name
                        variableName = CapitalizeFirstChar(variableName, true);
                    }

                    // get the default value in a method now
                    string defaultValue = GetDefaultValue(returnType);
                    
                    

                    // initial value
                    Insert("// initial value");
                    Insert("                " + returnType + " " + variableName + " = " + defaultValue + ";");

                    // add a blank line
                    Insert(Environment.NewLine);

                    // add the return value
                    Insert("                // return value");
                    Insert("                return " + variableName + ";", false);
                }
                else
                {
                    // here we do not have a returnType

                    // initial value
                    Insert("// initial value");
                    Insert(Environment.NewLine);

                    // add a blank line
                    Insert(Environment.NewLine);

                    // add the return value
                    Insert("                // return value");
                    Insert(Environment.NewLine);
                }
            }
            #endregion
            
            #region InsertMethod(string methodName, string returnType, CM.CSharpCodeFile codeFile)
            /// <summary>
            /// This Method inserts a Method
            /// </summary>
            internal void InsertMethod(string methodName, string returnType, CM.CSharpCodeFile codeFile)
            {
                // Set the Indent to 3
                Indent = 3;
                
                // create the method
                CM.CodeMethod method = new CM.CodeMethod();
                
                // set the name
                method.Name = methodName;
                
                // get the return type
                string methodDeclarationLineText = "public " + returnType + " " + methodName + "()";
                
                // create the codeLines that make up this method
                CM.CodeLine methodDeclarationLine = new CM.CodeLine(methodDeclarationLineText);
                CM.CodeLine openBracket = new CM.CodeLine("{");
                
                // set the initialValueComment
                string initialValueCommentText = "// initial value";
                CM.CodeLine initialValueCommentLine = new CM.CodeLine(initialValueCommentText);
                
                // create the initialValue line
                string variableName = "";
                
                // set the copy of the name
                string copyMethodName = methodName;
                
                // if the variabl
                if (copyMethodName.StartsWith("Get"))
                {
                    // replace out the get
                    copyMethodName = copyMethodName.Replace("Get", "");
                }
                else if (copyMethodName.StartsWith("Find"))
                {
                    // replace out the find
                    copyMethodName = copyMethodName.Replace("Find", "");
                }

                // if the length is only 1 character name
                if (copyMethodName.Length == 1)
                {
                    // set the value
                    variableName = copyMethodName[0].ToString().ToLower();
                }
                else
                {
                    // lower case only the first char
                    variableName = copyMethodName[0].ToString().ToLower() + copyMethodName.Substring(1);
                }

                // get the list of words
                List<Word> words = CSharpCodeParser.ParseWordsByCapitalLetters(copyMethodName);

                // set the firstWord
                string firstWord = "";

                // if there is at least one word
                if ((words != null) && (words.Count > 0))
                {
                    // get the first word
                    firstWord = words[0].Text;
                }

                // is the first word a verb
                bool firstWordIsAVerb = false;

                // if the first word exists
                if (!String.IsNullOrEmpty(firstWord))
                {
                    // is this a verb
                    firstWordIsAVerb = DescriptionHelper.IsVerb(firstWord);
                }

                // replace out a ";"
                variableName = variableName.Replace(";", "");

                // if the first word is a verb, replace it out of the string
                if (firstWordIsAVerb)
                {
                    // Remove the first word
                    variableName = variableName.Replace(firstWord.ToLower(), "");
                }
                
                // get a lowercase first word
                variableName = CapitalizeFirstChar(variableName, true);
                
                // get the text for the initialValueLine
                string initialValueText = returnType + " " + variableName + " = null;";
                
                // if the returnType is a string
                if (returnType == "string")
                {
                    // set the initial valueText
                    initialValueText = returnType + " " + variableName + " = ";
                    
                    // Set the sb
                    StringBuilder sb = new StringBuilder(initialValueText);
                    
                    // append a quote
                    sb.Append('"');
                    
                    // append a quote
                    sb.Append('"');
                    
                    // add the semicolon
                    sb.Append(";");
                    
                    // set the value
                    initialValueText = sb.ToString();
                }
                else if  ((returnType == "int") || (returnType == "double"))
                {
                    // set the initial valueText
                    initialValueText = returnType + " " + variableName + " = 0;";
                }
                else if (returnType == "bool")
                {
                    // set the initial valueText
                    initialValueText = returnType + " " + variableName + " = false;";
                }
                
                // Create the InitialValueLine
                CM.CodeLine initialValueLine = new CM.CodeLine(initialValueText);
                
                // create a new line
                CM.CodeLine blankLine = new CM.CodeLine(Environment.NewLine);
                
                // Create a comment for return value
                CM.CodeLine returnValueComment = new CM.CodeLine("// return value");
                
                // Now create the Return Value line
                string returnValueText = "return " + variableName + ";";
                CM.CodeLine returnValueLine = new CM.CodeLine(returnValueText);
                
                // create the CloseBracket
                CM.CodeLine closeBracket = new CM.CodeLine("}");

                // Update: Building a Smart Description
                string description = DescriptionHelper.GetMethodDescription(methodName, returnType);
                
                // create a summary
                string summary1Text = "/// <summary>";
                string summary2Text = "/// " + description;
                string summary3Text = @"/// </summary>";
                
                // create the codeLine
                CM.CodeLine summary1 = new CM.CodeLine(summary1Text);
                CM.CodeLine summary2 = new CM.CodeLine(summary2Text);
                CM.CodeLine summary3 = new CM.CodeLine(summary3Text);
                
                // Add the summary
                method.Summary.CodeLines.Add(summary1);
                method.Summary.CodeLines.Add(summary2);
                method.Summary.CodeLines.Add(summary3);
                
                // Now it is time to insert the codeLines
                method.CodeLines.Add(methodDeclarationLine);
                method.CodeLines.Add(openBracket);
                
                // if there is a return value
                if (returnType != "void")
                {
                    method.CodeLines.Add(initialValueCommentLine);
                    method.CodeLines.Add(initialValueLine);
                    method.CodeLines.Add(blankLine);
                    method.CodeLines.Add(returnValueComment);
                    method.CodeLines.Add(returnValueLine);
                }
                
                // add the close bracket
                method.CodeLines.Add(closeBracket);

                // local
                int methodsRegionIndent = 0;
                
                // before writing this method we need to find the insert index
                int lineNumber = GetMethodInsertLineNumber(method.Name, codeFile, out methodsRegionIndent);
                
                // If the value for lineNumber is set
                if (lineNumber > 0)
                {
                    // Get the currentLine of the insert index
                    var currentLine = codeFile.CodeLines[lineNumber -1];

                    // Set the Indent
                    Indent = methodsRegionIndent;

                    // get the textDoc
                    TextDocument textDoc = GetActiveTextDocument();
                
                    // if the textDoc was found
                    if (textDoc != null)
                    {
                        // go to this line
                        textDoc.Selection.GotoLine(lineNumber, false);
                    }
                
                    // now write the method
                    WriteMethod(method, true);
                }
                else
                {   
                    // Show the user a message
                    MessageBox.Show("The 'Methods' region does not exist in the current document." + Environment.NewLine + "Create the 'Methods' region and try again.", "Methods Region Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            #endregion
            
            #region InsertPrivateVariable(string privateVariableText, CM.CSharpCodeFile codeFile)
            /// <summary>
            /// This method inserts a Private Variable
            /// </summary>
            /// <param name="privateVariableText"></param>
            internal void InsertPrivateVariable(string privateVariableText, CM.CSharpCodeFile codeFile)
            {
                // get the line number of the private variable line number
                int lineNumber = GetPrivateVariableRegionLineNumber(codeFile);
                
                // if the lineNumber wasound
                if (lineNumber > 0)
                {
                    // get the line number of the first end region after the region that was found
                    int endRegionLineNumber = FindFirstEndRegionAfterLineNumber(lineNumber, codeFile);
                    
                    // if the endRegionLineNumber 
                    if (endRegionLineNumber > 0)
                    {
                        // the indent for Private Variable Insert is 2
                        Indent = 2;
                        
                        // Insert the line
                        Insert(privateVariableText, endRegionLineNumber, false, false);
                    }
                }
                else
                {
                    // Show the user a message
                    MessageBox.Show("The 'Private Variables' region does not exist in the current document." + Environment.NewLine + "Create the 'Private Variables' region and try again.", "Private Variables Region Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            #endregion
            
            #region InsertPropertiesFromSelectedText(string privateVariableText, RegionizerCodeManager codeManager)
            /// <summary>
            /// This method inserts properties from the Text that is selected.
            /// </summary>
            /// <param name="privateVariableText"></param>
            /// <param name="codeFile"></param>
            internal void InsertPropertiesFromSelectedText(string privateVariableText, RegionizerCodeManager codeManager)
            {
                // verify the codeManager exists and the text exists
                if ((codeManager != null) && (!String.IsNullOrEmpty(privateVariableText)))
                {
                    List<TextLine> textLines = CSharpCodeParser.ParseLines(privateVariableText);
                    
                    // get the codeLines
                    List<CM.CodeLine> codeLines = CSharpCodeParser.CreateCodeLines(textLines);
                    
                    // if there are one or more codeLines
                    if ((codeLines != null) && (codeLines.Count > 0))
                    {
                        // Determine the current indent

                        // set the indent 3
                        Indent = 3;
                        
                        // if the codeLine exist
                        foreach (CM.CodeLine codeLine in codeLines)
                        {
                            // if the codeLine is a private variable
                            if (codeLine.IsPrivateVariable)
                            {
                                // update: We need to get the CodeFile in between each private variable so that
                                //              the property gets inserted into the correct location
                                CM.CSharpCodeFile codeFile = codeManager.GetCodeFile();

                                // create the property
                                CM.CodeProperty property = new CM.CodeProperty();
                                
                                // set the private variable name
                                string privateVariableName = GetPrivateVariableNameFromPrivateVariableText(codeLine.Text);
                                
                                // set the name
                                property.Name = GetPropertyNameFromPrivateVariableText(codeLine.Text);
                                
                                // set the return type
                                string returnType = GetReturnTypeFromPrivateVariableText(codeLine.Text);
                                
                                // get the return type
                                string propertyDeclarationLineText = "public " + returnType + " " + property.Name;
                                
                                // create the codeLines that make up this property
                                CM.CodeLine propertyDeclarationLine = new CM.CodeLine(propertyDeclarationLineText);
                                CM.CodeLine openBracket = new CM.CodeLine("{");
                                
                                // set the getLineText
                                string getLineText = "get { return " + privateVariableName + "; }";
                                CM.CodeLine getLine = new CM.CodeLine(getLineText);
                                
                                // set the setLineText
                                string setLineText = "set { " + privateVariableName + " = value; }";
                                CM.CodeLine setLine = new CM.CodeLine(setLineText);
                                
                                // create the CloseBracket
                                CM.CodeLine closeBracket = new CM.CodeLine("}");

                                // Create the summary
                                property.Summary = CreateSummary("This property gets or sets the value for '" + property.Name + "'.");
                                
                                // Now it is time to insert the codeLines
                                property.CodeLines.Add(propertyDeclarationLine);
                                property.CodeLines.Add(openBracket);
                                property.CodeLines.Add(getLine);
                                property.CodeLines.Add(setLine);
                                property.CodeLines.Add(closeBracket);

                                // Default to 3
                                int propertyIndent = 3;
                                
                                // before writing this property we need to find the insert index
                                int lineNumber = GetPropertyInsertLineNumber(property.Name, codeFile, out propertyIndent);
                                
                                // if the lineNumber was found
                                if (lineNumber > 0)
                                {
                                    // Get the currentLine
                                    var currentLine = codeFile.CodeLines[lineNumber - 1];

                                    // Set the Indent for Properties
                                    Indent = propertyIndent;
                                    
                                    // get the textDoc
                                    TextDocument textDoc = GetActiveTextDocument();
                                
                                    // if the textDoc was found
                                    if (textDoc != null)
                                    {
                                        // go to this line
                                        textDoc.Selection.GotoLine(lineNumber, false);

                                        // now write the property
                                        WriteProperty(property, true);
                                    }
                                }
                                else
                                {
                                    // Show the user a message
                                    MessageBox.Show("The 'Properties' region does not exist in the current document." + Environment.NewLine + "Create the 'Properties' region and try again.", "Properties Region Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);

                                    // break out of the loop
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region StoreArgs(List<CodeLine> codeLines)
            /// <summary>
            /// Store Args
            /// </summary>
            public void StoreArgs(List<CodeLine> codeLines)
            {
                // If the codeLines collection exists and has one or more items
                if(ListHelper.HasOneOrMoreItems(codeLines))
                {
                    // get the textDoc
                    TextDocument textDoc = GetActiveTextDocument();
                                
                    // if the textDoc was found
                    if (textDoc != null)
                    {
                         // get the document text
                        string documentText = GetDocumentText(textDoc);

                        // if the documentText exists
                        if (TextHelper.Exists(documentText))
                        {
                            // get the lineNumber
                            int lineNumber = 0;

                            // get the documentText
                            List<TextLine> textLines = TextHelper.GetTextLines(documentText);

                            // get the codeLines so the lineNumber to insert at is found
                            List<CodeLine> document = CSharpCodeParser.CreateCodeLines(textLines);

                            // If the document collection exists and has one or more items
                            if (ListHelper.HasOneOrMoreItems(document))
                            {
                                // get the codeLine
                                CodeLine codeLine = document.FirstOrDefault(x => x.IsComment && x.Text.ToLower().Contains("store arg"));

                                // If the codeLine object exists
                                if (NullHelper.Exists(codeLine))
                                {
                                    // get the lineNumber
                                    lineNumber = codeLine.LineNumber;

                                    // Get the indent
                                    codeLine.Indent = TextHelper.GetSpacesCount(codeLine.Text);

                                    // if the lineNumber is set
                                    if (lineNumber > 0)
                                    {
                                        // go to this line
                                        textDoc.Selection.GotoLine(lineNumber + 1, false);

                                        // write the code lines
                                        WriteCodeLines(codeLines, codeLine.Indent / 4);
                                    }
                                }
                            }                            
                        }
                    }
                }
            }
            #endregion
            
            #region WriteClass(CodeModel.Objects.CodeClass codeClass)
            /// <summary>
            /// This method writes the class object passed in.
            /// </summary>
            /// <param name="codeClass"></param>
            private void WriteClass(CodeModel.Objects.CodeClass codeClass)
            {
                // if the codeClass exists
                if (codeClass != null)
                {
                    // increase the indent
                    Indent++;
                    
                    // set the className
                    string className = "class " + codeClass.Name;
                    
                    // begin a region for the ClassName
                    BeginRegion(className);
                    
                    // Write the Summary
                    WriteSummary(codeClass.Name, codeClass.Summary, CodeTypeEnum.Class);
                    
                    // Write the Tags for this class
                    WriteTags(codeClass.Tags);
                    
                    // Insert the class declaration line
                    Insert(codeClass.ClassDeclarationLine);
                    
                    // insert the open bracket
                    AddOpenBracket(false);
                    
                    // increase the indent
                    indent++;
                    
                    // Add a blank line
                    AddBlankLine();
                    
                    // Write the Private Variables
                    AddPrivateVariables(codeClass.PrivateVariables);
                    
                    // Write the Constructors
                    AddConstructors(codeClass.Constructors, codeClass.Name);
                    
                    // Write the Events
                    AddEvents(codeClass.Events);
                    
                    // Write the Methods
                    AddMethods(codeClass.Methods);
                    
                    // Add the properties
                    AddProperties(codeClass.Properties);
                    
                    // decrease the indent
                    indent--;
                    
                    // insert the close bracket
                    AddCloseBracket(false);
                    
                    // add the endregion
                    EndRegion();
                    
                    // decreate the indent
                    Indent--;
                }  
            }
            #endregion
            
            #region WriteCodeLines(IList<CM.CodeLine> codeLines, int indentAmount = 0)
            /// <summary>
            /// This method writes the code lines
            /// </summary>
            /// <param name="codeLines"></param>
            private void WriteCodeLines(IList<CM.CodeLine> codeLines, int indentAmount = 0)
            {
                // if the codeLines exist
                if (codeLines != null)
                {
                    // iterate the codeLines
                    foreach (CM.CodeLine codeLine in codeLines)
                    {
                        // if this is an Close bracket
                        if (codeLine.IsCloseBracket)
                        {
                            // if not an OpenBracket
                            if (!codeLine.IsOpenBracket)
                            {
                                // increase the indent
                                Indent--;
                            }
                        }

                        // if the indentAount is set
                        if (indentAmount > 0)
                        {
                            // set the indent amount
                            codeLine.Indent = indentAmount;
                        }
                        
                        // Insert this line
                        Insert(codeLine);
                        
                        // if this is an open bracket
                        if (codeLine.IsOpenBracket)
                        {
                            // if not a close bracket
                            if (!codeLine.IsCloseBracket)
                            {
                                // increase the indent
                                Indent++;
                            }
                        }
                    }
                }
            }
            #endregion

            #region WriteConstructor(CM.CodeConstructor constructor, bool surroundWithRegion, string className
            /// <summary>
            /// This method writes the Constructor passed in.
            /// </summary>
            /// <param name="constructor"></param>
            /// <param name="surroundWithRegion"></param>
            /// <param name="className">The type of object being created</param>
            private void WriteConstructor(CM.CodeConstructor constructor, bool surroundWithRegion, string className)
            {
                // verify the constructor exists
                if (constructor != null)
                {
                    // if a Region needs to surround this Constructor
                    if (surroundWithRegion)
                    {
                        // default for now
                        string constructorText = "Constructor";
                        
                        // Start the Region
                        BeginRegion(constructorText);
                    }
                    
                    // Write the Summary for this constructor
                    WriteSummary("constructor", constructor.Summary, CodeTypeEnum.Constructor, "", className);
                    
                    // Write the Tags
                    WriteTags(constructor.Tags);
                    
                    // Write the CodeLines
                    WriteCodeLines(constructor.CodeLines);
                    
                    // if a Region needs to surround this Constructor
                    if (surroundWithRegion)
                    {
                        // write the end region
                        EndRegion();
                    }
                    
                    // Add a Blank Line
                    AddBlankLine();
                }
            } 
            #endregion
            
            #region WriteDelegate(CM.CodeDelegate codeDelegate, bool surroundWithRegion)
            /// <summary>
            /// This method writes out a Delegate
            /// </summary>
            /// <param name="codeDelegate"></param>
            /// <param name="surroundWithRegion"></param>
            private void WriteDelegate(CM.CodeDelegate codeDelegate, bool surroundWithRegion)
            {
                // if the codeDelegate exists
                if ((codeDelegate != null) && (codeDelegate.HasDeclarationLine))
                {
                    // increase the indent
                    Indent++;
                    
                    // if surroundWithRegion
                    if (surroundWithRegion)
                    {
                        // set the delegateName
                        string delegateName = "delegate " + codeDelegate.Name;
                        
                        // begin a region for the DelegateName
                        BeginRegion(delegateName);
                    }
                    
                    // Write the Summary
                    WriteSummary("delegate", codeDelegate.Summary, CodeTypeEnum.Delegate);
                    
                    // Insert the delegate declaration line
                    Insert(codeDelegate.DelegateDeclarationLine);
                    
                    // if surroundWithRegion
                    if (surroundWithRegion)
                    {
                        // add the endregion
                        EndRegion();
                    }
                    
                    // decreate the indent
                    Indent--;
                    
                    // Add a blank line
                    AddBlankLine();
                }  
            } 
            #endregion
            
            #region WriteMethod(CM.CodeMethod codeMethod, bool surroundWithRegion)
            /// <summary>
            /// This method writes out the current method.
            /// </summary>
            /// <param name="codeMethod"></param>
            /// <param name="surrondWithRegion"></param>
            private void WriteMethod(CM.CodeMethod codeMethod, bool surroundWithRegion)
            {
                // verify the codeMethod exists
                if (codeMethod != null)
                {
                    // if a Region needs to surround this Constructor
                    if (surroundWithRegion)
                    {
                        // default for now
                        string methodName = codeMethod.Name;
                        
                        // if the Method has one or more codelines
                        if ((codeMethod.CodeLines != null) && (codeMethod.CodeLines.Count > 0))
                        {
                            // get the first line
                            string methodDeclarationLineText = codeMethod.CodeLines[0].Text;
                            
                            // Get the index of the open paren
                            int parenIndex = methodDeclarationLineText.IndexOf("(");
                            
                            // if the parenIndex exists
                            if (parenIndex >= 0)
                            {
                                // get the parameters
                                string parameters = methodDeclarationLineText.Substring(parenIndex);
                                
                                // Start the Region
                                BeginRegion(methodName + parameters);        
                            }
                            else
                            {
                                // Start the Region
                                BeginRegion(methodName);        
                            }
                        }
                    }
                    
                    // Write the Summary for this constructor
                    WriteSummary(codeMethod.Name, codeMethod.Summary, CodeTypeEnum.Method);
                    
                    // Write the Tags
                    WriteTags(codeMethod.Tags);
                    
                    // Write the CodeLines
                    WriteCodeLines(codeMethod.CodeLines);
                    
                    // if a Region needs to surround this Constructor
                    if (surroundWithRegion)
                    {
                        // write the end region
                        EndRegion();
                    }
                    
                    // Add a Blank Line
                    AddBlankLine();
                }
            }  
            #endregion
            
            #region WriteNamespace(DataJuggler.Regionizer.CodeModel.Objects.CodeNamespace codeNamespace)
            /// <summary>
            /// This method writes out a Namespace (which includes Classes, Delegates, Enums, Structs, etc.
            /// </summary>
            /// <param name="codeNamespace"></param>
            public void WriteNamespace(DataJuggler.Regionizer.CodeModel.Objects.CodeNamespace codeNamespace)
            {
                // if the namespace object exists
                if (codeNamespace != null)
                {
                    // Insert this line
                    Insert(codeNamespace.CodeLine);
                    
                    // Add the Open Bracket
                    AddOpenBracket(false);
                    
                    // add a blank line
                    AddBlankLine();
                    
                    // if there are Delegates
                    if ((codeNamespace.Delegates != null) && (codeNamespace.Delegates.Count > 0))
                    {
                        // iterate the delegates
                        foreach (CM.CodeDelegate codeDelegate in codeNamespace.Delegates)
                        {
                            // write out this delegate
                            WriteDelegate(codeDelegate, true);
                        }
                    }
                    
                    // if there are one or more Classes
                    if ((codeNamespace.HasClasses) && (codeNamespace.Classes.Count > 0))
                    {
                        // Write out the class
                        foreach (CM.CodeClass codeClass in codeNamespace.Classes)
                        {
                            // write each class
                            WriteClass(codeClass);
                        }
                        
                        // add a blank line
                        AddBlankLine();    
                    }
                    
                    // Add Close Bracket
                    AddCloseBracket(false);
                }
            }
            #endregion
            
            #region WriteProperty(CM.CodeProperty codeProperty, bool surroundWithRegion)
            /// <summary>
            /// This method adds this Property
            /// </summary>
            /// <param name="codeProperty"></param>
            /// <param name="surroundWithRegion"></param>
            private void WriteProperty(CM.CodeProperty codeProperty, bool surroundWithRegion)
            {
                // verify the codeProperty exists
                if (codeProperty != null)
                {
                    // if a Region needs to surround this Constructor
                    if (surroundWithRegion)
                    {
                        // default for now
                        string propertyName = codeProperty.Name;
                        
                        // Start the Region
                        BeginRegion(propertyName);
                    }

                    // update 12.7.2012: Changing the description for read only properties
                    bool isReadOnly = DetermineIfReadOnlyProperty(codeProperty.CodeLines);

                    // if this is a read only property
                    if (isReadOnly)
                    {
                        // write a read only property
                        WriteReadOnlyPropertySummary(codeProperty.Name, codeProperty.Summary);
                    }
                    else
                    {
                        // Write the Summary for this property
                        WriteSummary(codeProperty.Name, codeProperty.Summary, CodeTypeEnum.Property);
                    }
                    
                    // Write the CodeLines
                    WriteCodeLines(codeProperty.CodeLines);
                    
                    // if a Region needs to surround this Constructor
                    if (surroundWithRegion)
                    {
                        // write the end region
                        EndRegion();
                    }
                    
                    // Add a Blank Line
                    AddBlankLine();
                }
            }
            #endregion

            #region WriteReadOnlyPropertySummary(string propertyName, CM.CodeNotes summary, string returnType = "")
            /// <summary>
            /// This method returns the Read Only Property Summary
            /// </summary>
            private void WriteReadOnlyPropertySummary(string propertyName, CM.CodeNotes summary, string returnType = "")
            {
                // if the summary does not exist
                if ((summary == null) || (summary.CodeLines.Count < 1))
                {
                    // create the summary
                    summary = CreateSummary(CodeTypeEnum.Property, propertyName, returnType, true);
                }

                // add each tag for this class
                foreach (CM.CodeLine summaryLine in summary.CodeLines)
                {
                    // perform an insert
                    Insert(summaryLine);
                }
            }
        #endregion

            #region WriteSummary(string objectName, CM.CodeNotes summary, CodeTypeEnum codeType, string returnType = "", string className = "")
            /// <summary>
            /// This method writes the Tags
            /// </summary>
            /// <param name="tags"></param>
            private void WriteSummary(string objectName, CM.CodeNotes summary, CodeTypeEnum codeType, string returnType = "", string className = "")
            {
                // if the summary does not exist
                if  ((summary == null) || (summary.CodeLines.Count < 1))
                {
                    // create the summary
                    summary = CreateSummary(codeType, objectName, returnType, false, className);
                }
                
                // add each tag for this class
                foreach (CM.CodeLine summaryLine in summary.CodeLines)
                {
                    // perform an insert
                    Insert(summaryLine);
                }
            }
            #endregion
            
            #region WriteTags(IList<CM.CodeLine> tags)
            /// <summary>
            /// This method writes the Tags
            /// </summary>
            /// <param name="tags"></param>
            private void WriteTags(IList<CM.CodeLine> tags)
            {
                // if the tags exist
                if (tags != null)
                {
                    // add each tag for this class
                    foreach(CM.CodeLine tag in tags)
                    {
                        // perform an insert
                        Insert(tag);
                    }
                }
            }  
            #endregion
            
        #endregion
        
        #region Properties
            
            #region ActiveDocument
            /// <summary>
            /// This method gets or sets the ActiveDocument
            /// </summary>
            public EnvDTE.Document ActiveDocument
            {
                get { return activeDocument; }
                set { activeDocument = value; }
            }
            #endregion

            #region CommentDictionary
            /// <summary>
            /// This property gets or sets the value for 'CommentDictionary'.
            /// </summary>
            public CM.CommentDictionary CommentDictionary
            {
                get { return commentDictionairy; }
                set { commentDictionairy = value; }
            }
            #endregion

            #region HasActiveDocument
            /// <summary>
            /// This property returns true if this object has a 'activeDocument'.
            /// </summary>
            public bool HasActiveDocument
            {
                get
                {
                    // initial value
                    bool activeDocument = (ActiveDocument != null);
                    
                    // return value
                    return activeDocument;
                }
            }
            #endregion

            #region HasCommentDictionary
            /// <summary>
            /// This property returns true if this object has a 'CommentDictionary'.
            /// </summary>
            public bool HasCommentDictionary
            {
                get
                {
                    // initial value
                    bool hasCommentDictionairy = (CommentDictionary != null);

                    // return value
                    return hasCommentDictionairy;
                }
            }
            #endregion
            
            #region Indent
            /// <summary>
            /// This property gets or sets the value for Indent.
            /// This determines how many columns over data should be inserted at.
            /// </summary>
            public int Indent
            {
                get { return indent; }
                set { indent = value; }
            } 
            #endregion
            
        #endregion

    }
    #endregion

}
