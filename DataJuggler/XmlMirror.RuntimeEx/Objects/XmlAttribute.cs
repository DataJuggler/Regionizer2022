

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace XmlMirror.Runtime.Objects
{

    #region class XmlAttribute
    /// <summary>
    /// This class represents an XmlAttribute
    /// </summary>
    public class XmlAttribute
    {
        
        #region Private Variables
        private string attributeName;
        private string attributeValue;
        #endregion

        #region Constructors

            #region Default Constructor()
            /// <summary>
            /// Create a new instance of an XmlAttribute
            /// </summary>
            public XmlAttribute()
            {
                
            }
            #endregion

            #region Parameterized Constructor(string attributeName)
            /// <summary>
            /// Create a new instance of an XmlAttribute
            /// </summary>
            public XmlAttribute(string attributeName)
            {

            }
            #endregion

            #region Parameterized Constructor(string attributeName, string attributeValue)
            /// <summary>
            /// Create a new instance of an XmlAttribute
            /// </summary>
            public XmlAttribute(string attributeName, string attributeValue)
            {
                // store the values
                this.AttributeName = attributeName;
                this.AttributeValue = attributeValue;
            }
            #endregion

        #endregion

        #region Properties

            #region AttributeName
            /// <summary>
            /// This property gets or sets the value for 'AttributeName'.
            /// </summary>
            public string AttributeName
            {
                get { return attributeName; }
                set { attributeName = value; }
            }
            #endregion
            
            #region AttributeValue
            /// <summary>
            /// This property gets or sets the value for 'AttributeValue'.
            /// </summary>
            public string AttributeValue
            {
                get { return attributeValue; }
                set { attributeValue = value; }
            }
            #endregion
            
            #region HasAttributeValue
            /// <summary>
            /// This property returns true if the 'AttributeValue' exists.
            /// </summary>
            public bool HasAttributeValue
            {
                get
                {
                    // initial value
                    bool hasAttributeValue = (!String.IsNullOrEmpty(this.AttributeValue));
                    
                    // return value
                    return hasAttributeValue;
                }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
