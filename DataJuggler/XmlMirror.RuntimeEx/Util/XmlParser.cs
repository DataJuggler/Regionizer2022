

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmlMirror.Runtime.Objects;
using System.IO;
using System.Xml.Linq;
using XmlMirror.Runtime.Enumerations;
using DataJuggler.Core.UltimateHelper;

#endregion

namespace XmlMirror.Runtime.Util
{

    #region class XmlParser
    /// <summary>
    /// This class is used to parse an Xml file into an XmlDocument object.
    /// </summary>
    public class XmlParser
    {
        
        #region Private Variables
        private string sourceFileName;
        private string documentText;
        private Exception error;
        #endregion

        #region Constructors

            #region DefaultConstructor
            /// <summary>
            /// Create a new instance of an XmlParser object.
            /// </summary>
            public XmlParser()
            {
                
            }
            #endregion

            #region Parameterized Constructor(string sourceFileName)
            /// <summary>
            /// Create a new instance of an XmlParser.
            /// </summary>
            /// <param name="sourceFileName"></param>
            public XmlParser(string sourceFileName)
            {
                // store the value
                this.SourceFileName = sourceFileName;
            }
            #endregion

        #endregion

        #region Methods

            #region AddAttributes(XmlNode node, XElement element)
            /// <summary>
            /// This method adds the Attributes to the node supplied
            /// </summary>
            private void AddAttributes(XmlNode node, XElement element)
            {
                // local
                XmlAttribute attribute = null;

                // verify both objects exist
                if ((node != null) && (element != null) && (element.HasElements))
                {
                    // get the attributes
                    var attributes = element.Attributes();

                    // iterate the attributes
                    foreach (var tempAttribute in attributes)
                    {
                        // create the attribute
                        attribute = new XmlAttribute(tempAttribute.Name.ToString(), tempAttribute.Value);

                        // add this attribute
                        node.Attributes.Add(attribute);
                    }
                }
            }
            #endregion
            
            #region AddChildNodes(XmlNode parentNode, XElement element)
            /// <summary>
            /// This method returns the Child Nodes for the element given.
            /// <param param name="parentNode">The node that child nodes will be added to.</param>
            /// <param name="element" the current XElement that the parentNode was derived from.
            /// </summary>
            private void AddChildNodes(XmlNode parentNode, XElement element)
            {
                // locals
                XmlNode node = null;
                XmlNode childNode = null;
                
                try
                {
                    // if the parentNode and the element both exist
                    if ((parentNode != null) && (element != null))
                    {
                        // for debugging only
                        string parentNodeName = parentNode.NodeText;
                        string elementName = element.Name.ToString();

                        // Create the node from this element
                        node = new XmlNode(parentNode, elementName, DataTypeEnum.String);

                        // set the NodeValue
                        node.NodeValue = GetInnerXml(element.ToString());

                        // Add the node
                        AddNode(parentNode, node);

                        // if this childElement has 1 or more attributes
                        if (element.HasAttributes)
                        {
                            // Add the Attributes
                            AddAttributes(node, element);
                        }

                        // if the element has children
                        if (element.HasElements)
                        {  
                            // get the root elements
                            var elements = element.Elements();

                            // for debugging only
                            int elementCount = elements.Count();

                            // iterate the elements
                            foreach (var childElement in elements)
                            {
                                // for debugging only
                                string childNodeName = childElement.Name.ToString();
                                parentNodeName = parentNode.NodeText;
                                
                                // if there are children
                                if (childElement.HasElements)
                                {
                                    // add the child nodes recursively
                                    AddChildNodes(node, childElement);
                                }
                                else
                                {
                                    // create the node without and attachments
                                    childNode = new XmlNode(node, childNodeName, DataTypeEnum.String);

                                    // set the value
                                    childNode.NodeValue = childElement.ToString();

                                    // add the child nodes recursively until there are not any child elements to add.
                                    AddNode(node, childNode);
                                }
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // set the error
                    this.Error = error;

                    // raise the error
                    throw error;
                }
            }
            #endregion
            
            #region AddNode(XmlNode parentNode, XmlNode childNode)
            /// <summary>
            /// This method is here so all nodes are added here to have a central method.
            /// </summary>
            public void AddNode(XmlNode parentNode, XmlNode childNode)
            {
                // if the parentNode exists and the childNode exists
                if ((parentNode != null) && (childNode != null))
                {
                    // for debugging only
                    string parentNodeName = parentNode.NodeText;
                    string childNodeName = childNode.NodeText;
                   
                    // add this childNode
                    parentNode.ChildNodes.Add(childNode);
                }
            }
            #endregion
            
            #region GetInnerXml(string sourceString)
            /// <summary>
            /// This method returns the Inner Xml
            /// </summary>
            private string GetInnerXml(string sourceString)
            {
                // initial value
                string innerXml = "";

                // if the string exists
                if (TextHelper.Exists(sourceString))
                {
                    // set the startIndex
                    int startIndex = sourceString.IndexOf(">") + 1;
                    int endIndex = sourceString.LastIndexOf("<") - 1;
                    int length = endIndex - startIndex + 1;

                    // if everything is valid
                    if ((length > 0) && (startIndex >= 0) && (endIndex > 0))
                    { 
                        // set the return value
                        innerXml = sourceString.Substring(startIndex, length);
                    }
                }

                // if the TextHelper exists
                if (TextHelper.Exists(innerXml))
                {
                    // if there is an open tag
                    int openIndex = innerXml.IndexOf("<");
                    int closeIndex = innerXml.IndexOf(">");
                    bool hasOpenAndClosingTag = ((openIndex > 0) && (closeIndex > 0));

                    // if the openAndClosingTags exist
                    if (hasOpenAndClosingTag)
                    {
                        // set to emptyString
                        innerXml = "";
                    }
                }

                // return value
                return innerXml;
            }
            #endregion
            
            #region ParseXmlDocument(string documentText)
            /// <summary>
            /// This method returns a parsed XmlDocument for the documentText given.
            /// </summary>
            public XmlDocument ParseXmlDocument(string documentText)
            {
                // initial value
                XmlDocument xmlDocument = null;

                // locals
                string rootName = "";

                try
                {
                    // Store the document text
                    this.DocumentText = documentText;

                    // if the SourceFileName exists
                    if (this.HasDocumentText)
                    {
                        // Parse the xDocument
                        XDocument xDoc = XDocument.Parse(documentText);

                        // if the xDocument exists
                        if (xDoc != null)
                        {
                            // get the root element
                            XElement root = xDoc.Root;

                            // if the root element exists
                            if (root != null)
                            {
                                // set the root element name
                                rootName = root.Name.ToString();

                                // create the xmlDocument
                                xmlDocument = new XmlDocument(rootName, DataTypeEnum.String);

                                // Create the isRootNode
                                XmlNode rootNode = xmlDocument.RootNode;

                                // now iterate the Child elements for the root
                                if (root.HasElements)
                                {
                                    // get the root elements
                                    var elements = root.Elements();

                                    // iterate the elements
                                    foreach (var element in elements)
                                    {
                                        // Add this element
                                        AddChildNodes(rootNode, element);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // store the error
                    this.Error = error;
                }

                // return value
                return xmlDocument;
            }
            #endregion
            
            #region ParseXmlDocument()
            /// <summary>
            /// Parse the source xml file and return an XmlDocument.
            /// </summary>
            /// <returns></returns>
            public XmlDocument ParseXmlDocument()
            {
                // initial value
                XmlDocument xmlDocument = null;

                try
                {
                    // if the SourceFileName exists
                    if (TextHelper.Exists(this.SourceFileName))
                    {
                        // if the file exists
                        if (File.Exists(this.SourceFileName))
                        {
                            // set the DocumentText
                            this.DocumentText = File.ReadAllText(this.SourceFileName);

                            // call the override for this method
                            xmlDocument = ParseXmlDocument(this.DocumentText);
                        }
                    }
                }
                catch (Exception error)
                {
                    // store the error
                    this.Error = error;
                }

                // return value
                return xmlDocument;
            }
            #endregion

        #endregion

        #region Properties

            #region DocumentText
            /// <summary>
            /// This property gets or sets the value for 'DocumentText'.
            /// </summary>
            public string DocumentText
            {
                get { return documentText; }
                set { documentText = value; }
            }
            #endregion
            
            #region Error
            /// <summary>
            /// This property gets or sets the value for 'Error'.
            /// </summary>
            public Exception Error
            {
                get { return error; }
                set { error = value; }
            }
            #endregion
            
            #region HasDocumentText
            /// <summary>
            /// This property returns true if the 'DocumentText' exists.
            /// </summary>
            public bool HasDocumentText
            {
                get
                {
                    // initial value
                    bool hasDocumentText = (!String.IsNullOrEmpty(this.DocumentText));
                    
                    // return value
                    return hasDocumentText;
                }
            }
            #endregion
            
            #region HasError
            /// <summary>
            /// This property returns true if this object has an 'Error'.
            /// </summary>
            public bool HasError
            {
                get
                {
                    // initial value
                    bool hasError = (this.Error != null);
                    
                    // return value
                    return hasError;
                }
            }
            #endregion
            
            #region SourceFileName
            /// <summary>
            /// This property gets or sets the value for 'SourceFileName'.
            /// </summary>
            public string SourceFileName
            {
                get { return sourceFileName; }
                set { sourceFileName = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
