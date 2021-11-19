

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

    #region class CommentDictionaryParser : ParserBaseClass
    /// <summary>
    /// This class is used to parse 'CodeComment' objects.
    /// </summary>
    public partial class CommentDictionaryParser : ParserBaseClass
    {

        #region Methods

            #region LoadCodeComments(string sourceXmlText)
            /// <summary>
            /// This method is used to load a list of 'CodeComment' objects.
            /// </summary>
            /// <param name="sourceXmlText">The source xml to be parsed.</param>
            /// <returns>A list of 'CodeComment' objects.</returns>
            public List<CodeComment> LoadCodeComments(string sourceXmlText)
            {
                // initial value
                List<CodeComment> codeComments = null;

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
                        // parse the codeComments
                        codeComments = ParseCodeComments(this.XmlDoc.RootNode);
                    }
                }

                // return value
                return codeComments;
            }
            #endregion

            #region ParseCodeComment(ref CodeComment codeComment, XmlNode xmlNode)
            /// <summary>
            /// This method is used to parse CodeComment objects.
            /// </summary>
            public CodeComment ParseCodeComment(ref CodeComment codeComment, XmlNode xmlNode)
            {
                // if the codeComment object exists and the xmlNode exists
                if ((codeComment != null) && (xmlNode != null))
                {
                    // get the full name of this node
                    string fullName = xmlNode.GetFullName();

                    // Check the name of this node to see if it is mapped to a property
                    switch(fullName)
                    {
                        case "CommentDictionary.CommentItem.Comment":

                            // Set the value for codeComment.Comment
                            codeComment.Comment = xmlNode.FormattedNodeValue;

                            // required
                            break;

                        case "CommentDictionary.CommentItem.HasReplacements":

                            // Set the value for codeComment.Comment
                            codeComment.HasReplacements = BooleanHelper.ParseBoolean(xmlNode.FormattedNodeValue, false, false);

                            // required
                            break;

                        case "CommentDictionary.CommentItem.ID":

                            // Set the value for codeComment.Name
                            codeComment.ID = NumericHelper.ParseInteger(xmlNode.FormattedNodeValue, 0, -1);

                            // required
                            break;

                        case "CommentDictionary.CommentItem.Name":

                            // Set the value for codeComment.Name
                            codeComment.Name = xmlNode.FormattedNodeValue;

                            // required
                            break;

                        case "CommentDictionary.CommentItem.Pattern":

                            // Set the value for codeComment.Pattern
                            codeComment.Pattern = xmlNode.FormattedNodeValue;

                            // required
                            break;

                        case "CommentDictionary.CommentItem.TargetPattern":

                            // Set the value for codeComment.TargetPattern
                            codeComment.TargetPattern = xmlNode.FormattedNodeValue;

                            // required
                            break;

                        case "CommentDictionary.CommentItem.TargetPattern2":

                            // Set the value for codeComment.TargetPattern2
                            codeComment.TargetPattern2 = xmlNode.FormattedNodeValue;

                            // required
                            break;

                        case "CommentDictionary.CommentItem.Type":

                            // Set the value for codeComment.CommentType
                            // codeComment.CommentType = // this field must be parsed manually.

                            // required
                            break;

                    }

                    // if there are ChildNodes
                    if (xmlNode.HasChildNodes)
                    {
                        // iterate the child nodes
                         foreach(XmlNode childNode in xmlNode.ChildNodes)
                        {
                            // append to this CodeComment
                            codeComment = ParseCodeComment(ref codeComment, childNode);
                        }
                    }
                }

                // return value
                return codeComment;
            }
            #endregion

            #region ParseCodeComments(XmlNode xmlNode, List<CodeComment> codeComments = null)
            /// <summary>
            /// This method is used to parse a list of 'CodeComment' objects.
            /// </summary>
            /// <param name="XmlNode">The XmlNode to be parsed.</param>
            /// <returns>A list of 'CodeComment' objects.</returns>
            public List<CodeComment> ParseCodeComments(XmlNode xmlNode, List<CodeComment> codeComments = null)
            {
                // locals
                CodeComment codeComment = null;
                bool cancel = false;

                // if the xmlNode exists
                if (xmlNode != null)
                {
                    // get the full name for this node
                    string fullName = xmlNode.GetFullName();

                    // if this is the new collection line
                    if (fullName == "CommentDictionary")
                    {
                        // Raise event Parsing is starting.
                        cancel = Parsing(xmlNode);

                        // If not cancelled
                        if (!cancel)
                        {
                            // create the return collection
                            codeComments = new List<CodeComment>();
                        }
                    }
                    // if this is the new object line and the return collection exists
                    else if ((fullName == "CommentDictionary.CommentItem") && (codeComments != null))
                    {
                        // Create a new object
                        codeComment = new CodeComment();

                        // Perform pre parse operations
                        cancel = Parsing(xmlNode, ref codeComment);

                        // If not cancelled
                        if (!cancel)
                        {
                            // parse this object
                            codeComment = ParseCodeComment(ref codeComment, xmlNode);
                        }

                        // Perform post parse operations
                        cancel = Parsed(xmlNode, ref codeComment);

                        // If not cancelled
                        if (!cancel)
                        {
                            // Add this object to the return value
                            codeComments.Add(codeComment);
                        }
                    }

                    // if there are ChildNodes
                    if (xmlNode.HasChildNodes)
                    {
                        // iterate the child nodes
                        foreach(XmlNode childNode in xmlNode.ChildNodes)
                        {
                            // self call this method for each childNode
                            codeComments = ParseCodeComments(childNode, codeComments);
                        }
                    }
                }

                // return value
                return codeComments;
            }
            #endregion

        #endregion

    }
    #endregion

}
