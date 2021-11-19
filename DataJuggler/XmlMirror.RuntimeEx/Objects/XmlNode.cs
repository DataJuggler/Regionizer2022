

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmlMirror.Runtime.Util;
using XmlMirror.Runtime.Enumerations;
using DataJuggler.Core.UltimateHelper;

#endregion

namespace XmlMirror.Runtime.Objects
{

    #region class XmlNode
    /// <summary>
    /// This object represents an XmlNode.
    /// Each XmlNode maps directly to an XElement object.
    /// </summary>
    public class XmlNode
    {
        
        #region Private Variables
        private XmlNode parentNode;
        private List<XmlNode> childNodes;
        private string nodeText;
        private bool isRootNode;
        private string nodeValue;
        private List<XmlAttribute> attributes;
        private string formattedNodeValue;
        private string sourceFieldName;
        private string targetFieldName;
        private DataTypeEnum targetDataType;
        private FieldValuePair targetFieldValuePair;
        private XmlNode rootNode;
        private bool autoFormatNodeText;
        private bool autoFormatNodeValue;
        #endregion

        #region Constructors

            #region Default Constructor
            /// <summary>
            /// Create a new instance of an XmlNode
            /// </summary>
            public XmlNode()
            {
                // performs initializations for this object.
                Init();
            }
            #endregion

            #region Parameterized Constructor(string nodeText)
            /// <summary>
            /// Create a new instance of an XmlNode and set the parentNode
            /// </summary>
            /// <param name="nodeText">The text for this Node.</param>
            public XmlNode(string nodeText, DataTypeEnum dataType)
            {
                // set the NodeText
                this.NodeText = nodeText;
                
                // Set the TargetDataType
                this.TargetDataType = dataType;

                // performs initializations for this object.
                Init();
            }
            #endregion

            #region Parameterized Constructor(parentNode, string nodeText, DataManager.DataTypeEnum dataType)
            /// <summary>
            /// Create a new instance of an XmlNode and set the parentNode
            /// </summary>
            /// <param name="parentNode">The parent of this object.</param>
            /// <param name="nodeText">The text for this Node.</param>
            public XmlNode(XmlNode parentNode, string nodeText, DataTypeEnum dataType)
            {
                // set the parentNode
                this.ParentNode = parentNode;

                // set the NodeText
                this.NodeText = nodeText;

                // Set the TargetDataType
                this.TargetDataType = dataType;

                // performs initializations for this object.
                Init();
            }
            #endregion

        #endregion

        #region Methods

            #region Add(XmlNode childNode)
            /// <summary>
            /// This method adds a childNode to this collection
            /// </summary>
            public void Add(XmlNode childNode)
            {
                // if the ChildNodes collection exists
                if (this.HasChildNodes)
                {
                    // Add this item
                    this.ChildNodes.Add(childNode);
                }
            }
            #endregion
            
            #region FormatNodeText()
            /// <summary>
            /// This method returns the Node Text
            /// </summary>
            public string FormatNodeText()
            {
                // initial value
                string formatNodeText = "";

                // locals
                string openTag = "<";
                string openTag2 = "</";
                string closingTag = ">";
                string closingTag2 = "/>";
                string nil = " nil=\"true\" ";
                int nilIndex = -1;

                // if this node has a value
                if (this.HasNodeValue)
                {
                    // Several different types of items need to parsed out here
                    // 1. Open and closing tags
                    //     Example: <id>2</id>
                    // 2. Nill Tag
                    //     Example: <chat-client-sequence nil="true">
                    // 3. Self closing tag (I made these names up)
                    //     Example: <attachments/>

                    // set the open and closing tag names so that can be replaced out
                    string openTagName = TextHelper.CombineStrings(openTag, this.NodeText, closingTag);
                    string closingTagName = TextHelper.CombineStrings(openTag2, this.NodeText, closingTag);
                    string nilText = TextHelper.CombineStrings(openTag, this.NodeText, nil, closingTag2);
                    string selfClosing = TextHelper.CombineStrings(openTag, this.NodeText, " ", closingTag2);
                    int selfClosingIndex = this.NodeText.IndexOf(selfClosing);
                    bool isSelfClosing = (selfClosingIndex >= 0);
                    nilIndex = this.NodeValue.IndexOf("nil=");

                    // if nil 
                    if (nilIndex > 0)
                    {
                        // use nodeText & reset the nodeText
                        nodeText = this.NodeText + " = null";

                        // set the FormattedNodeValue
                        this.FormattedNodeValue = null;
                    }
                    else
                    {
                        // user nodeText & node value
                        nodeText = this.NodeText + " = " + this.NodeValue;
                    }

                    // replace out the open and closing tags
                    nodeText = nodeText.Replace(openTagName, "");
                    nodeText = nodeText.Replace(closingTagName, "");
                    nodeText = nodeText.Replace(nilText, "");
                    nodeText = nodeText.Replace(selfClosing, "");

                    // set the startIndex
                    int startIndex = nodeText.IndexOf("=");

                    // if the Length exists
                    if (nodeText.Length > startIndex)
                    {
                        // set the formattedNodeValue
                        this.FormattedNodeValue = nodeText.Substring(startIndex + 1).Trim();
                    }

                    // if this is a selfClosing node
                    if (isSelfClosing)
                    {
                        // replace out the = sign
                        nodeText = nodeText.Replace("=", "");
                    }
                }
                else
                {
                    // use text only
                    nodeText = this.NodeText;
                }

                // replace out the text
                nodeText = RVictorveTags(nodeText);
                
                // return value
                return formatNodeText;
            }
            #endregion
            
            #region FormatNodeValue()
            /// <summary>
            /// This method formats the Node Value and sets the value for FormattedNodeValue
            /// </summary>
            private string FormatNodeValue()
            {
                // initial value
                string formattedNodeValue = "";

                // if the node value exists
                if (this.HasNodeValue)
                {
                    // set the nodevalue
                    string nodeValue = RVictorveTags(this.NodeValue);

                    // set the NodeValue
                    this.FormattedNodeValue = nodeValue;
                }

                // return value
                return formattedNodeValue;
            }
            #endregion

            #region GetAncestors(bool alwaysCreate = false, bool addThisItem = true)
            /// <summary>
            /// This method returns a list of Ancestors for this object
            /// </summary>
            private List<XmlNode> GetAncestors(bool alwaysCreate = false, bool addThisItem = true)
            {
                // initial value
                List<XmlNode> ancestors = null;

                // locals
                XmlNode parentNode = null;

                // if always create is true
                if ((alwaysCreate) || (addThisItem))
                {
                    // create the ancestors collection 
                    ancestors = new List<XmlNode>();
                }
                else if (this.HasParentNode)
                {
                    // AutoCreate = false; here we only create it if the ParentNode exists

                    // create the ancestors collection 
                    ancestors = new List<XmlNode>();
                }
                
                // if add this item is true
                if (addThisItem)
                {
                    // add this item
                    ancestors.Add(this);
                }

                // Create a string builder
                StringBuilder sb = new StringBuilder();

                // if this object has a ParentNode
                if (this.HasParentNode)
                {
                    // set the parentNode
                    parentNode = this.ParentNode;
                }

                // if the parentNode exists
                if (parentNode != null)
                {
                    do
                    {
                        // add this node to the parentNodes collection
                        ancestors.Add(parentNode);

                        // if the Parentnode exists
                        if (parentNode.HasParentNode)
                        {
                            // set the new parentNode
                            parentNode = parentNode.ParentNode;
                        }
                        else
                        {
                            // break out of the loop
                            break;
                        }
                    } while (true);
                }

                // return value
                return ancestors;
            }
            #endregion
            
            #region GetFullName()
            /// <summary>
            /// This method returns the Full Name of a particular node.
            /// Example: Response.Messages.Message.ID
            /// </summary>
            public string GetFullName()
            {
                // initial value
                string fullName = "";

                // Get the full ancestors
                List<XmlNode> ancestors = GetAncestors();
                List<XmlNode> reversedAncestors = null;
                StringBuilder sb = new StringBuilder();
                int count = 0;

                // if there are one or more ancestors
                if (ListHelper.HasOneOrMoreItems(ancestors))
                {
                    // now we need to reverse the list
                    reversedAncestors = ReverseAncestors(ancestors);
                }

                // if there are one or more reversedAncestors
                if (ListHelper.HasOneOrMoreItems(reversedAncestors))
                {
                    // iterate the nodes
                    foreach(XmlNode node in reversedAncestors)
                    {
                        // increment count
                        count++;

                        // apped the nodeText
                        sb.Append(node.nodeText);

                        // if this is not the last item
                        if (count < reversedAncestors.Count)
                        {
                            // append a period in between names
                            sb.Append(".");
                        }
                    }

                    // set the return value
                    fullName = sb.ToString();
                }

                // return value
                return fullName;
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Create the ChildNodes collection
                this.ChildNodes = new List<XmlNode>();

                // Create a list of Attributes
                this.Attributes = new List<XmlAttribute>();

                // Default to AutoFormat unless turned off
                this.AutoFormatNodeText = true;
                this.AutoFormatNodeValue = true;
            }
            #endregion

            #region RVictorveTags(string sourceText)
            /// <summary>
            /// This method returns the Tag
            /// </summary>
            private string RVictorveTags(string sourceText)
            {
                // initial value
                string formattedText = "";

                // if the sourceText exists
                if (!String.IsNullOrEmpty(sourceText))
                {
                    // set the value for formattedText as is
                    formattedText = sourceText;

                    // locals
                    int openTagIndex = 0;
                    int closingTagIndex = 0;
                    bool needsReplacement = false;

                    do
                    {  
                        // Update: If the NodeText or node.FormattedValue contains a < and a > then they must be replaced out.
                        openTagIndex = formattedText.IndexOf("<");
                        closingTagIndex = formattedText.IndexOf(">");
                        needsReplacement = ((openTagIndex >= 0) && (closingTagIndex > openTagIndex));

                        // if a replacement needs to be made
                        if (needsReplacement)
                        {
                            // set the length for the 
                            int length = closingTagIndex - openTagIndex + 1;

                            // create a tagText string
                            string tagText = formattedText.Substring(openTagIndex, length);
                            string closingTagText = tagText.Replace("<", "</");

                            // replace out the text
                            formattedText = formattedText.Replace(tagText, "").Trim();
                            formattedText = formattedText.Replace(closingTagText, "");
                        }

                    } while (needsReplacement);
                }

                // return value
                return formattedText;
            }
            #endregion
            
            #region ReverseAncestors(List<XmlNode> ancestors)
            /// <summary>
            /// This method returns a list of XmlNodes in reverse order
            /// </summary>
            private List<XmlNode> ReverseAncestors(List<XmlNode> ancestors)
            {
                // initial value
                List<XmlNode> reversedAncestors = new List<XmlNode>();

                // local
                XmlNode node = null;
                int count = 0;

                // verify the list exists
                if (ListHelper.HasOneOrMoreItems(ancestors))
                {
                    // set the count
                    count = ancestors.Count;

                    // iterate the ancestors in reverse order
                    for (int x = ancestors.Count -1; x >= 0; x--)
                    {
                        // get the XmlNode at the specified index
                        node = ancestors[x];

                        // add this node
                        reversedAncestors.Add(node);
                    }
                }

                // return value
                return reversedAncestors;
            }
            #endregion
            
            #region SetRootNode()
            /// <summary>
            /// This method returns the Root Node for this object
            /// </summary>
            public void SetRootNode()
            {
                // If this object does not have a parent node
                if (!this.HasParentNode)
                {
                    // This must be the root node without a parent
                    this.IsRootNode = true;
                    
                    // set the rootNode
                    this.rootNode = this;
                }
                else
                {
                    // local
                    XmlNode parentNode = this.ParentNode;

                    do
                    {
                        // if the parentNode has a parentNode 
                        if (parentNode.HasParentNode)
                        {
                            // set the parentNode
                            parentNode = parentNode.ParentNode;
                        }
                        else
                        {
                            // set the rootNode
                            this.rootNode = parentNode;

                            // break out of the loop
                            break;
                        }

                    } while(parentNode != null);
                }
            }
            #endregion
            
            #region ToString()
            /// <summary>
            /// This method returns the String
            /// </summary>
            public override string ToString()
            {
                // return the NodeText
                return this.NodeText;
            }
            #endregion
            
        #endregion

        #region Properties

            #region Attributes
            /// <summary>
            /// This property gets or sets the value for 'Attributes'.
            /// </summary>
            public List<XmlAttribute> Attributes
            {
                get { return attributes; }
                set { attributes = value; }
            }
            #endregion
            
            #region AutoFormatNodeText
            /// <summary>
            /// This property gets or sets the value for 'AutoFormatNodeText'.
            /// </summary>
            public bool AutoFormatNodeText
            {
                get { return autoFormatNodeText; }
                set { autoFormatNodeText = value; }
            }
            #endregion
            
            #region AutoFormatNodeValue
            /// <summary>
            /// This property gets or sets the value for 'AutoFormatNodeValue'.
            /// </summary>
            public bool AutoFormatNodeValue
            {
                get { return autoFormatNodeValue; }
                set { autoFormatNodeValue = value; }
            }
            #endregion
            
            #region ChildNodes
            /// <summary>
            /// This property gets or sets the value for 'ChildNodes'.
            /// </summary>
            public List<XmlNode> ChildNodes
            {
                get { return childNodes; }
                set { childNodes = value; }
            }
            #endregion
            
            #region FormattedNodeValue
            /// <summary>
            /// This property gets or sets the value for 'FormattedNodeValue'.
            /// </summary>
            public string FormattedNodeValue
            {
                get { return formattedNodeValue; }
                set { formattedNodeValue = value; }
            }
            #endregion
            
            #region HasAttributes
            /// <summary>
            /// This property returns true if this object has an 'Attributes'.
            /// </summary>
            public bool HasAttributes
            {
                get
                {
                    // initial value
                    bool hasAttributes = (this.Attributes != null);
                    
                    // return value
                    return hasAttributes;
                }
            }
            #endregion
            
            #region HasChildNodes
            /// <summary>
            /// This property returns true if this object has a 'ChildNodes'.
            /// </summary>
            public bool HasChildNodes
            {
                get
                {
                    // initial value
                    bool hasChildNodes = (this.ChildNodes != null);
                    
                    // return value
                    return hasChildNodes;
                }
            }
            #endregion
            
            #region HasNodeText
            /// <summary>
            /// This property returns true if the 'NodeText' exists.
            /// </summary>
            public bool HasNodeText
            {
                get
                {
                    // initial value
                    bool hasNodeText = (!String.IsNullOrEmpty(this.NodeText));
                    
                    // return value
                    return hasNodeText;
                }
            }
            #endregion
            
            #region HasNodeValue
            /// <summary>
            /// This property returns true if the 'NodeValue' exists.
            /// </summary>
            public bool HasNodeValue
            {
                get
                {
                    // initial value
                    bool hasNodeValue = (!String.IsNullOrEmpty(this.NodeValue));
                    
                    // return value
                    return hasNodeValue;
                }
            }
            #endregion
            
            #region HasOneOrMoreAttributes
            /// <summary>
            /// This property gets or sets the value for 'HasOneOrMoreAttributes'.
            /// </summary>
            public bool HasOneOrMoreAttributes
            {
                get
                {
                    // initial value
                    bool hasOneOrMoreAttributes = ((this.HasAttributes) && (this.Attributes.Count > 0));

                    // return value
                    return hasOneOrMoreAttributes;
                }
            }
            #endregion

            #region HasOneOrMoreChildNodes
            /// <summary>
            /// This property gets or sets the value for 'HasOneOrMoreChildNodes'.
            /// </summary>
            public bool HasOneOrMoreChildNodes
            {
                get
                {
                    // initial value
                    bool hasOneOrMoreChildNodes = ((this.HasChildNodes) && (this.ChildNodes.Count > 0));

                    // return value
                    return hasOneOrMoreChildNodes;
                }
            }
            #endregion
            
            #region HasParentNode
            /// <summary>
            /// This property returns true if this object has a 'ParentNode'.
            /// </summary>
            public bool HasParentNode
            {
                get
                {
                    // initial value
                    bool hasParentNode = (this.ParentNode != null);
                    
                    // return value
                    return hasParentNode;
                }
            }
            #endregion
            
            #region HasRootNode
            /// <summary>
            /// This property returns true if this object has a 'RootNode'.
            /// </summary>
            public bool HasRootNode
            {
                get
                {
                    // initial value
                    bool hasRootNode = (this.RootNode != null);
                    
                    // return value
                    return hasRootNode;
                }
            }
            #endregion
            
            #region HasSourceFieldName
            /// <summary>
            /// This property returns true if the 'SourceFieldName' exists.
            /// </summary>
            public bool HasSourceFieldName
            {
                get
                {
                    // initial value
                    bool hasSourceFieldName = (!String.IsNullOrEmpty(this.SourceFieldName));
                    
                    // return value
                    return hasSourceFieldName;
                }
            }
            #endregion
            
            #region HasTargetDataType
            /// <summary>
            /// This property returns true if this object has a 'TargetDataType'.
            /// </summary>
            public bool HasTargetDataType
            {
                get
                {
                    // initial value
                    bool hasTargetDataType = (this.TargetDataType != DataTypeEnum.NotSupported);
                    
                    // return value
                    return hasTargetDataType;
                }
            }
            #endregion
            
            #region HasTargetFieldName
            /// <summary>
            /// This property returns true if the 'TargetFieldName' exists.
            /// </summary>
            public bool HasTargetFieldName
            {
                get
                {
                    // initial value
                    bool hasTargetFieldName = (!String.IsNullOrEmpty(this.TargetFieldName));
                    
                    // return value
                    return hasTargetFieldName;
                }
            }
            #endregion
            
            #region HasTargetFieldValuePair
            /// <summary>
            /// This property returns true if this object has a 'TargetFieldValuePair'.
            /// </summary>
            public bool HasTargetFieldValuePair
            {
                get
                {
                    // initial value
                    bool hasTargetFieldValuePair = (this.TargetFieldValuePair != null);
                    
                    // return value
                    return hasTargetFieldValuePair;
                }
            }
            #endregion

            #region IsRootNode
            /// <summary>
            /// This property gets or sets the value for 'IsRootNode'.
            /// </summary>
            public bool IsRootNode
            {
                get { return isRootNode; }
                set { isRootNode = value; }
            }
            #endregion
            
            #region NodeText
            /// <summary>
            /// This property gets or sets the value for 'NodeText'.
            /// </summary>
            public string NodeText
            {
                get { return nodeText; }
                set 
                {
                    // set the nodeText
                    nodeText = value;

                    // make sure AutoFormatNodeText is true
                    if (this.AutoFormatNodeText)
                    {
                        // format the node text
                        nodeText = FormatNodeText();
                    }
                }
            }
            #endregion
            
            #region NodeValue
            /// <summary>
            /// This property gets or sets the value for 'NodeValue'.
            /// </summary>
            public string NodeValue
            {
                get { return nodeValue; }
                set 
                {
                    // set the value
                    nodeValue = value;

                    // if autoformat node value is true
                    if ((this.AutoFormatNodeValue) && (this.HasNodeValue))
                    {
                        // format the node value
                        this.nodeValue = FormatNodeValue();
                    }
                }
            }
            #endregion
            
            #region ParentNode
            /// <summary>
            /// This property gets or sets the value for 'ParentNode'.
            /// </summary>
            public XmlNode ParentNode
            {
                get { return parentNode; }
                set { parentNode = value; }
            }
            #endregion
            
            #region RootNode
            /// <summary>
            /// This property gets or sets the value for 'RootNode'.
            /// </summary>
            public XmlNode RootNode
            {
                get 
                {
                    // if this object does not have a root node yet
                    // node you cannot call HasRootNode here or an out of stack error occurs
                    if (this.rootNode == null)
                    {
                        // set the root node
                        this.SetRootNode();    
                    }
                    
                    // return value
                    return rootNode;
                }
            }
            #endregion
            
            #region SourceFieldName
            /// <summary>
            /// This property gets or sets the value for 'SourceFieldName'.
            /// </summary>
            public string SourceFieldName
            {
                get { return sourceFieldName; }
                set { sourceFieldName = value; }
            }
            #endregion
            
            #region TargetDataType
            /// <summary>
            /// This property gets or sets the value for 'TargetDataType'.
            /// </summary>
            public DataTypeEnum TargetDataType
            {
                get { return targetDataType; }
                set { targetDataType = value; }
            }
            #endregion
            
            #region TargetFieldName
            /// <summary>
            /// This property gets or sets the value for 'TargetFieldName'.
            /// </summary>
            public string TargetFieldName
            {
                get { return targetFieldName; }
                set { targetFieldName = value; }
            }
            #endregion
            
            #region TargetFieldValuePair
            /// <summary>
            /// This property gets or sets the value for 'TargetFieldValuePair'.
            /// </summary>
            public FieldValuePair TargetFieldValuePair
            {
                get { return targetFieldValuePair; }
                set { targetFieldValuePair = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
