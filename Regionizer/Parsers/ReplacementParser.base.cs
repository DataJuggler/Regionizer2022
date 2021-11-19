

#region using statements

using DataJuggler.Core.UltimateHelper;
using DataJuggler.Regionizer.CodeModel.Objects;
using System;
using System.Collections.Generic;
using XmlMirror.Runtime.Objects;
using XmlMirror.Runtime.Util;

#endregion

namespace DataJuggler.Regionizer.Parsers
{

    #region class ReplacementParser : ParserBaseClass
    /// <summary>
    /// This class is used to parse 'Replacement' objects.
    /// </summary>
    public partial class ReplacementParser : ParserBaseClass
    {

        #region Private Variables
        private int commentID;
        #endregion

        #region Methods

            #region LoadReplacements(string sourceXmlText)
            /// <summary>
            /// This method is used to load a list of 'Replacement' objects.
            /// </summary>
            /// <param name="sourceXmlText">The source xml to be parsed.</param>
            /// <returns>A list of 'Replacement' objects.</returns>
            public List<Replacement> LoadReplacements(string sourceXmlText)
            {
                // initial value
                List<Replacement> replacements = null;

                // if the source text exists
                if (TextHelper.Exists(sourceXmlText))
                {
                    // create an instance of the parser
                    XmlParser parser = new XmlParser();

                    // Create the XmlDoc
                    this.XmlDoc = parser.ParseXmlDocument(sourceXmlText);

                    // If the XmlDoc exists and has a root node.
                    if ((this.HasXmlDoc) && (this.XmlDoc.HasRootNode))
                    {
                        // parse the replacements
                        replacements = ParseReplacements(this.XmlDoc.RootNode);
                    }
                }

                // return value
                return replacements;
            }
            #endregion

            #region ParseReplacement(ref Replacement replacement, XmlNode xmlNode)
            /// <summary>
            /// This method is used to parse Replacement objects.
            /// </summary>
            public Replacement ParseReplacement(ref Replacement replacement, XmlNode xmlNode)
            {
                // if the replacement object exists and the xmlNode exists
                if ((replacement != null) && (xmlNode != null))
                {
                    // get the full name of this node
                    string fullName = xmlNode.GetFullName();

                    // Check the name of this node to see if it is mapped to a property
                    switch(fullName)
                    {
                        case "CommentDictionary.CommentItem.Replacements.Replacement.Find":

                            // Set the value for replacement.Find
                            replacement.Find = xmlNode.FormattedNodeValue;

                            // required
                            break;

                        case "CommentDictionary.CommentItem.Replacements.Replacement.Replace":

                            // Set the value for replacement.Replace
                            replacement.Replace = xmlNode.FormattedNodeValue;

                            // required
                            break;

                        case "CommentDictionary.CommentItem.Replacements.Replacement.Target":

                            // Set the value for replacement.Target
                            // replacement.Target = // this field must be parsed manually.

                            // required
                            break;

                    }

                    // if there are ChildNodes
                    if (xmlNode.HasChildNodes)
                    {
                        // iterate the child nodes
                         foreach(XmlNode childNode in xmlNode.ChildNodes)
                        {
                            // append to this Replacement
                            replacement = ParseReplacement(ref replacement, childNode);
                        }
                    }
                }

                // return value
                return replacement;
            }
            #endregion

            #region ParseReplacements(XmlNode xmlNode, List<Replacement> replacements = null)
            /// <summary>
            /// This method is used to parse a list of 'Replacement' objects.
            /// </summary>
            /// <param name="XmlNode">The XmlNode to be parsed.</param>
            /// <returns>A list of 'Replacement' objects.</returns>
            public List<Replacement> ParseReplacements(XmlNode xmlNode, List<Replacement> replacements = null)
            {
                // locals
                Replacement replacement = null;
                bool cancel = false;

                // if the xmlNode exists
                if (xmlNode != null)
                {
                    // get the full name for this node
                    string fullName = xmlNode.GetFullName();

                    // if this is the new collection line
                    if (fullName == "CommentDictionary.CommentItem.Replacements")
                    {
                        // Raise event Parsing is starting.
                        cancel = Parsing(xmlNode);

                        // If not cancelled
                        if (!cancel)
                        {
                            // create the return collection
                            replacements = new List<Replacement>();
                        }
                    }
                    // if this is the new object line and the return collection exists
                    else if ((fullName == "CommentDictionary.CommentItem.Replacements.Replacement") && (replacements != null))
                    {
                        // Create a new object
                        replacement = new Replacement();

                        // Perform pre parse operations
                        cancel = Parsing(xmlNode, ref replacement);

                        // If not cancelled
                        if (!cancel)
                        {
                            // parse this object
                            replacement = ParseReplacement(ref replacement, xmlNode);
                        }

                        // Perform post parse operations
                        cancel = Parsed(xmlNode, ref replacement);

                        // If not cancelled
                        if (!cancel)
                        {
                            // Add this object to the return value
                            replacements.Add(replacement);
                        }
                    }

                    // if there are ChildNodes
                    if (xmlNode.HasChildNodes)
                    {
                        // iterate the child nodes
                        foreach(XmlNode childNode in xmlNode.ChildNodes)
                        {
                            // self call this method for each childNode
                            replacements = ParseReplacements(childNode, replacements);
                        }
                    }
                }

                // return value
                return replacements;
            }
            #endregion

        #endregion
    
        #region Properties
            
            #region CommentID
            /// <summary>
            /// This property gets or sets the value for 'CommentID'.
            /// </summary>
            public int CommentID
            {
                get { return commentID; }
                set { commentID = value; }
            }
            #endregion
            
            #region HasCommentID
            /// <summary>
            /// This property returns true if the 'CommentID' is set.
            /// </summary>
            public bool HasCommentID
            {
                get
                {
                    // initial value
                    bool hasCommentID = (this.CommentID > 0);
                    
                    // return value
                    return hasCommentID;
                }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
