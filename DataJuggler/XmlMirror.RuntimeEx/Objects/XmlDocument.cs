

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmlMirror.Runtime.Enumerations;

#endregion

namespace XmlMirror.Runtime.Objects
{

    #region class XmlDocument
    /// <summary>
    /// This class represents an XmlDocument.
    /// </summary>
    public class XmlDocument
    {
        
        #region Private Variables
        private XmlNode rootNode;
        #endregion

        #region Constructors

            #region Default Constructor
            /// <summary>
            /// Create a new instance of an XmlDocument.
            /// </summary>
            public XmlDocument()
            {
                // Create a IsRootNode as a stub.
                this.RootNode = new XmlNode();
            }
            #endregion

            #region Parameterized Constructor(string rootNodeText, DataManager.DataTypeEnum rootNodeDataType)
            /// <summary>
            /// Create a new instance of an XmlDocument.
            /// </summary>
            public XmlDocument(string rootNodeText, DataTypeEnum rootNodeDataType)
            {
                // Create the IsRootNode
                this.RootNode = new XmlNode(rootNodeText, rootNodeDataType);
            }
            #endregion

        #endregion

        #region Properties

            #region HasRootNode
            /// <summary>
            /// This property returns true if this object has a 'IsRootNode'.
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
            
            #region IsRootNode
            /// <summary>
            /// This property gets or sets the value for 'IsRootNode'.
            /// </summary>
            public XmlNode RootNode
            {
                get { return rootNode; }
                set { rootNode = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
