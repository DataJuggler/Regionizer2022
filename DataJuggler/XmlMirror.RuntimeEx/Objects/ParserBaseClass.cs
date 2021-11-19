

#region using statements

using XmlMirror.Runtime.Util;
using DataJuggler.Core.UltimateHelper;

#endregion

namespace XmlMirror.Runtime.Objects
{

    #region class ParserBaseClass
    /// <summary>
    /// This is the BaseClass that all parsers wil use.
    /// </summary>
    public class ParserBaseClass
    {
        
        #region Private Variables
        private XmlDocument xmlDoc;
        private int networkId;
        #endregion

        #region Methods

            #region FindChildNodeByName(XmlNode parentNode, string childNodeName)
            /// <summary>
            /// This method finds a ChildNode of the 
            /// </summary>
            public XmlNode FindChildNodeByName(XmlNode parentNode, string childNodeName)
            {
                // initial value
                XmlNode node = null;

                // if the XmlDoc exists
                if ((parentNode != null) && (parentNode.HasChildNodes))
                {
                    // iterate the childNode
                    foreach (XmlNode childNode in parentNode.ChildNodes)
                    {
                        // get the fullName
                        string fullName = childNode.GetFullName();

                        // if this is the node being sought
                        if (TextHelper.IsEqual(fullName, childNodeName))
                        {
                            // set the return value
                            node = childNode;

                            // break out of the loop
                            break;
                        }
                        else if (childNode.HasChildNodes)
                        {
                            // find the node
                            node = FindChildNodeByName(childNode, childNodeName);

                            // if the node exists
                            if (node != null)
                            {
                                // break out of the loop
                                break;
                            }
                        }
                    }
                }

                // return value
                return node;
            }
            #endregion
            
            #region LoadXmlDocument(string xmlFilePath, string xmlSourceText)
            /// <summary>
            /// This method returns the Xml Document
            /// </summary>
            public void LoadXmlDocument(string xmlFilePath, string xmlSourceText)
            {
                // local
                XmlParser parser = null;

                // if the xmlFilePath exists
                if (TextHelper.Exists(xmlFilePath))
                {
                    // create an instance of the parser
                    parser = new XmlParser(xmlFilePath);

                    // parse the XmlDoc
                    this.XmlDoc = parser.ParseXmlDocument();
                }
                else
                {
                    // Create an xml parser
                    parser = new XmlParser();

                    // parse the XmlDoc
                    this.XmlDoc = parser.ParseXmlDocument(xmlSourceText);
                }
            }
            #endregion
            
        #endregion

        #region Properties

            #region HasNetworkId
            /// <summary>
            /// This property returns true if the 'NetworkId' is set.
            /// </summary>
            public bool HasNetworkId
            {
                get
                {
                    // initial value
                    bool hasNetworkId = (this.NetworkId > 0);
                    
                    // return value
                    return hasNetworkId;
                }
            }
            #endregion
            
            #region HasXmlDoc
            /// <summary>
            /// This property returns true if this object has a 'XmlDoc'.
            /// </summary>
            public bool HasXmlDoc
            {
                get
                {
                    // initial value
                    bool hasXmlDoc = (this.XmlDoc != null);
                    
                    // return value
                    return hasXmlDoc;
                }
            }
            #endregion
            
            #region NetworkId
            /// <summary>
            /// This property gets or sets the value for 'NetworkId'.
            /// </summary>
            public int NetworkId
            {
                get { return networkId; }
                set { networkId = value; }
            }
            #endregion
            
            #region XmlDoc
            /// <summary>
            /// This property gets or sets the value for 'XmlDoc'.
            /// </summary>
            public XmlDocument XmlDoc
            {
                get { return xmlDoc; }
                set { xmlDoc = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
